using AutoMapper;
using CompleteApp.App.ViewModels;
using CompleteApp.Business.Interfaces;
using CompleteApp.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteApp.App.Controllers
{
    public class CategoriasController : MainController
    {
        protected readonly ICategoriaRepository _categoriaRepository;
        protected readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos()));
        } 

        public async Task<IActionResult> Details(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaProdutos(id);

            if (categoriaViewModel == null) return NotFound();

            return View(categoriaViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return View(categoriaViewModel);

            var categoria = _mapper.Map<Categoria>(categoriaViewModel);
            await _categoriaRepository.Adicionar(categoria);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaProdutos(id);

            if (categoriaViewModel == null) return NotFound();

            return View(categoriaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CategoriaViewModel categoriaViewModel)
        {
            if (categoriaViewModel.Id != id) return NotFound();

            if (!ModelState.IsValid) return View(categoriaViewModel);

            var categoria = _mapper.Map<Categoria>(categoriaViewModel);
            await _categoriaRepository.Atualizar(categoria);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaProdutos(id);

            if (categoriaViewModel == null) return NotFound();

            return View(categoriaViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var categoriaViewModel = await ObterCategoriaProdutos(id);

            if (categoriaViewModel == null) return NotFound();

            await _categoriaRepository.Remover(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<CategoriaViewModel> ObterCategoriaProdutos(Guid id)
        {
            return _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterCategoriaProdutos(id));
        }
    }
}
