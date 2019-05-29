using System;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Service
{
    public class PessoaJuridicaService : ClienteService, IPessoaJuridicaService
    {
        private readonly IPessoaJuridicaRepository repository;

        public PessoaJuridicaService(IPessoaJuridicaRepository repository,
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
            var pessoaJuridica = model as PessoaJuridica;

            if (VerificarDuplicado(pessoaJuridica.Nome, pessoaJuridica.Cnpj))
                throw new InvalidOperationException("Já existe um registro com o nome ou cnpj informado.");

            return base.Inserir(pessoaJuridica);
        }

        public override ResultadoValidacao Atualizar(Cliente model)
        {
            var pessoaJuridica = model as PessoaJuridica;

            if (VerificarDuplicado(pessoaJuridica.Nome, pessoaJuridica.Cnpj, pessoaJuridica.Id))
                throw new InvalidOperationException("Já existe um registro com o nome ou cnpj informado.");

            return base.Atualizar(pessoaJuridica);
        }
    }
}
