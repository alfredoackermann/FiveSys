using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;
using System;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Service
{
    public class PessoaFisicaFornecedorService : FornecedorService, IPessoaFisicaFornecedorService
    {
        private readonly IPessoaFisicaFornecedorRepository repository;

        public PessoaFisicaFornecedorService(
            IPessoaFisicaFornecedorRepository repository,
            IFornecedorService clienteService,
            IEmailService emailService,
            IEnderecoService enderecoService,
            ITelefoneService telefoneService) : base(repository, emailService, enderecoService, telefoneService)
        {
            this.repository = repository;
        }

        public override ResultadoValidacao Inserir(Fornecedor model)
        {
            var pessoaFisica = model as PessoaFisicaFornecedor;

            if (VerificarDuplicado(pessoaFisica.Nome, pessoaFisica.Cpf))
                throw new InvalidOperationException(Textos.Geral_Mensagem_Erro_NomeDuplicado);

            return base.Inserir(pessoaFisica);
        }

        public override ResultadoValidacao Atualizar(Fornecedor model)
        {
            var pessoaFisica = model as PessoaFisicaFornecedor;

            if (VerificarDuplicado(pessoaFisica.Nome, pessoaFisica.Cpf, pessoaFisica.Id))
                throw new InvalidOperationException(Textos.Geral_Mensagem_Erro_NomeDuplicado);

            return base.Atualizar(model);
        }
    }
}
