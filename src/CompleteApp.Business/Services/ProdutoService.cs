using CompleteApp.Business.Interfaces;
using CompleteApp.Business.Models;
using CompleteApp.Business.Models.Validations.Produtos;
using System;
using System.Threading.Tasks;

namespace CompleteApp.Business.Services
{
    public class ProdutoService : MainService, IProdutoService
    {
        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;
        }

        public async Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
