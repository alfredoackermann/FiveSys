using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;
using System;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Service
{
    public class PessoaJuridicaFornecedorService : FornecedorService, IPessoaJuridicaFornecedorService
    {
        private readonly IPessoaJuridicaFornecedorRepository repository;

        public PessoaJuridicaFornecedorService(IPessoaJuridicaFornecedorRepository repository,
            IFornecedorService clienteService,
            IEmailService emailService,
            IEnderecoService enderecoService,
            ITelefoneService telefoneService) : base(repository, emailService, enderecoService, telefoneService)
        {
            this.repository = repository;
        }

        public override ResultadoValidacao Inserir(Fornecedor model)
        {
            var pessoaJuridica = model as PessoaJuridicaFornecedor;

            if (VerificarDuplicado(pessoaJuridica.Nome, pessoaJuridica.Cnpj))
                throw new InvalidOperationException(Textos.Geral_Mensagem_Erro_NomeDuplicado);

            return base.Inserir(pessoaJuridica);
        }

        public override ResultadoValidacao Atualizar(Fornecedor model)
        {
            var pessoaJuridica = model as PessoaJuridicaFornecedor;

            if (VerificarDuplicado(pessoaJuridica.Nome, pessoaJuridica.Cnpj, pessoaJuridica.Id))
                throw new InvalidOperationException(Textos.Geral_Mensagem_Erro_NomeDuplicado);

            return base.Atualizar(pessoaJuridica);
        }
    }
}
