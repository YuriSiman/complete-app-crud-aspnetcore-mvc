using CompleteApp.Business.Interfaces;
using CompleteApp.Business.Models;
using CompleteApp.Business.Models.Validations.Enderecos;
using CompleteApp.Business.Models.Validations.Fornecedores;
using System;
using System.Threading.Tasks;

namespace CompleteApp.Business.Services
{
    public class FornecedorService : MainService, IFornecedorService
    {
        public async Task Adicionar(Fornecedor fornecedor)
        {
            // Validar o estado da entidade
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor) && !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco))  return;
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;
        }

        public async Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
