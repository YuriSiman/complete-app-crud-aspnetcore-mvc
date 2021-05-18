using AutoMapper;
using CompleteApp.App.ViewModels;
using CompleteApp.Business.Interfaces;
using CompleteApp.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompleteApp.App.Controllers
{
    public class CategoriasController : MainController
    {
        protected readonly ICategoriaRepository _categoriaRepository;
        protected readonly ICategoriaService _categoriaService;
        protected readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository categoriaRepository, ICategoriaService categoriaService ,IMapper mapper, INotificador notificador) : base(notificador)
        {
            _categoriaRepository = categoriaRepository;
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        [Route("lista-de-categorias")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos()));
        }

        [Route("dados-da-categoria/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaProdutos(id);

            if (categoriaViewModel == null) return NotFound();

            return View(categoriaViewModel);
        }

        [Route("nova-categoria")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("nova-categoria")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return View(categoriaViewModel);

            var categoria = _mapper.Map<Categoria>(categoriaViewModel);
            await _categoriaService.Adicionar(categoria);

            if (!OperacaoValida()) return View(categoriaViewModel);

            return RedirectToAction(nameof(Index));
        }

        [Route("editar-categoria/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaProdutos(id);

            if (categoriaViewModel == null) return NotFound();

            return View(categoriaViewModel);
        }

        [Route("editar-categoria/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CategoriaViewModel categoriaViewModel)
        {
            if (categoriaViewModel.Id != id) return NotFound();

            if (!ModelState.IsValid) return View(categoriaViewModel);

            var categoria = _mapper.Map<Categoria>(categoriaViewModel);
            await _categoriaService.Atualizar(categoria);

            if (!OperacaoValida()) return View(await ObterCategoriaProdutos(id));

            return RedirectToAction(nameof(Index));
        }

        [Route("excluir-categoria/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaProdutos(id);

            if (categoriaViewModel == null) return NotFound();

            return View(categoriaViewModel);
        }

        [Route("excluir-categoria/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaProdutos(id);

            if (categoriaViewModel == null) return NotFound();

            await _categoriaService.Remover(id);

            if (!OperacaoValida()) return View(categoriaViewModel);

            TempData["Sucesso"] = "Categoria excluída com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        private async Task<CategoriaViewModel> ObterCategoriaProdutos(Guid id)
        {
            return _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterCategoriaProdutos(id));
        }
    }
}
