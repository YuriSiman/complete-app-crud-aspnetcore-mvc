using CompleteApp.Business.Interfaces;
using CompleteApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteApp.Business.Services
{
    public class FornecedorService : MainService, IFornecedorService
    {
        public Task Adicionar(Fornecedor fornecedor)
        {
            // Validar o estado da entidade

            // Validar se não existe fornecedor com o mesmo documento
            throw new NotImplementedException();
        }

        public Task Atualizar(Fornecedor fornecedor)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
