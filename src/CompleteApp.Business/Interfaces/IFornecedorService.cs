﻿using CompleteApp.Business.Models;
using System;
using System.Threading.Tasks;

namespace CompleteApp.Business.Interfaces
{
    public interface IFornecedorService
    {
        Task Adicionar(Fornecedor fornecedor);
        Task Atualizar(Fornecedor fornecedor);
        Task Remover(Guid id);
        Task AtualizarEndereco(Endereco endereco);
    }
}
