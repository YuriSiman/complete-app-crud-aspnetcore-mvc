using AutoMapper;
using CompleteApp.App.Extensions;
using CompleteApp.App.ViewModels;
using CompleteApp.Business.Interfaces;
using CompleteApp.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompleteApp.App.Controllers
{
    public class ProdutosController : MainController
    {
        protected readonly IProdutoRepository _produtoRepository;
        protected readonly IFornecedorRepository _fornecedorRepository;
        protected readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoService _produtoService;
        protected readonly IMapper _mapper;
        protected readonly UploadFiles _uploadFiles;

        public ProdutosController(IProdutoRepository produtoRepository, IFornecedorRepository fornecedorRepository, ICategoriaRepository categoriaRepository, IProdutoService produtoService, IMapper mapper, UploadFiles uploadFiles, INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
            _categoriaRepository = categoriaRepository;
            _produtoService = produtoService;
            _mapper = mapper;
            _uploadFiles = uploadFiles;
        }

        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedoresCategorias()));
        }

        [Route("dados-do-produto/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [Route("novo-produto")]
        public async Task<IActionResult> Create()
        {
            var produtoViewModel = await PopularFornecedoresCategorias(new ProdutoViewModel());

            return View(produtoViewModel);
        }

        [Route("novo-produto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularFornecedoresCategorias(produtoViewModel);

            if (!ModelState.IsValid) return View(produtoViewModel);

            // Upload da Imagem
            if (!await UploadImagemProduto(produtoViewModel)) return View(produtoViewModel);

            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction(nameof(Index));
        }

        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            var produtoAtualizado = await ObterProduto(id);

            //Update da Imagem
            if (!await UpdateImagemProduto(produtoViewModel, produtoAtualizado)) return View(produtoAtualizado);

            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizado));

            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction(nameof(Index));
        }

        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            return View(produtoViewModel);
        }

        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            await _produtoService.Remover(id);

            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction(nameof(Index));
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produtoViewModel = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedorCategoria(id));
            produtoViewModel.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            produtoViewModel.Categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());
            return produtoViewModel;
        }

        private async Task<ProdutoViewModel> PopularFornecedoresCategorias(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            produtoViewModel.Categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());
            return produtoViewModel;
        }

        private async Task<bool> UploadImagemProduto(ProdutoViewModel produtoViewModel)
        {
            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await _uploadFiles.UploadImage(produtoViewModel.ImagemUpload, imgPrefixo))
            {
                ModelState.AddModelError(string.Empty, "Arquivo inválido!");
                return false;
            }
            produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            return true;
        }

        private async Task<bool> UpdateImagemProduto(ProdutoViewModel produtoViewModel, ProdutoViewModel produtoAtualizado)
        {
            produtoViewModel.Imagem = produtoAtualizado.Imagem;

            if (!ModelState.IsValid) return false;

            if (produtoViewModel.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await _uploadFiles.UploadImage(produtoViewModel.ImagemUpload, imgPrefixo))
                {
                    ModelState.AddModelError(string.Empty, "Arquivo inválido!");
                    return false;
                }
                produtoAtualizado.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            }

            produtoAtualizado.Nome = produtoViewModel.Nome;
            produtoAtualizado.Descricao = produtoViewModel.Descricao;
            produtoAtualizado.Valor = produtoViewModel.Valor;
            produtoAtualizado.Ativo = produtoViewModel.Ativo;
            produtoAtualizado.Categoria = produtoViewModel.Categoria;
            produtoAtualizado.Fornecedor = produtoViewModel.Fornecedor;

            return true;
        }
    }
}
