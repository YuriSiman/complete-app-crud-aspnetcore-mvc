using CompleteApp.Business.Interfaces;
using CompleteApp.Business.Models;
using CompleteApp.Business.Models.Validations.Categorias;
using System;
using System.Threading.Tasks;

namespace CompleteApp.Business.Services
{
    public class CategoriaService : MainService, ICategoriaService
    {
        public async Task Adicionar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria)) return;
        }

        public async Task Atualizar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria)) return;
        }

        public async Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
