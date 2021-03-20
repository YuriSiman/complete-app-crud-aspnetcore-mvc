using CompleteApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompleteApp.Business.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);
        Task<IEnumerable<Produto>> ObterProdutosFornecedoresCategorias();
        Task<Produto> ObterProdutoFornecedorCategoria(Guid id);
    }
}
