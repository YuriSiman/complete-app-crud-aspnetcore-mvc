using CompleteApp.Business.Interfaces;
using CompleteApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteApp.Business.Services
{
    public class CategoriaService : MainService, ICategoriaService
    {
        public Task Adicionar(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
