using System;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Service
{
    public class PessoaFisicaService : ClienteService, IPessoaFisicaService
    {
        private readonly IPessoaFisicaRepository repository;

        public PessoaFisicaService(IPessoaFisicaRepository repository,
            IClienteService clienteService,
            IEmailService emailService,
            IEnderecoService enderecoService,
            IIdentificacaoService identificacaoService,
            ITelefoneService telefoneService) : base(repository, emailService, enderecoService, identificacaoService, telefoneService)
        {
            this.repository = repository;
        }

        public override ResultadoValidacao Inserir(Cliente model)
        {
            var pessoaFisica = model as PessoaFisica;

            if (VerificarDuplicado(pessoaFisica.Nome, pessoaFisica.Cpf))
                throw new InvalidOperationException("Já existe um registro com o nome ou cpf informado.");

            return base.Inserir(pessoaFisica);
        }

        public override ResultadoValidacao Atualizar(Cliente model)
        {
            var pessoaFisica = model as PessoaFisica;

            if (VerificarDuplicado(pessoaFisica.Nome, pessoaFisica.Cpf, pessoaFisica.Id))
                throw new InvalidOperationException("Já existe um registro com o nome ou cpf informado.");

            return base.Atualizar(model);
        }
    }
}
