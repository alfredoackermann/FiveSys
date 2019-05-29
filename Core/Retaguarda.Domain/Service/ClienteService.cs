using FileSys.Shared.Core.Domain.Impl.Service;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;
using System.Collections.Generic;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Service
{
    public class ClienteService : BaseCrudService<Cliente>, IClienteService
    {
        private readonly IClienteRepository repository;
        private readonly IEmailService emailService;
        private readonly IEnderecoService enderecoService;
        private readonly IIdentificacaoService identificacaoService;
        private readonly ITelefoneService telefoneService;

        public ClienteService(IClienteRepository repository,
            IEmailService emailService,
            IEnderecoService enderecoService,
            IIdentificacaoService identificacaoService,
            ITelefoneService telefoneService) : base(repository)
        {
            this.repository = repository;
            this.emailService = emailService;
            this.enderecoService = enderecoService;
            this.identificacaoService = identificacaoService;
            this.telefoneService = telefoneService;
        }

        public override Cliente RecuperarPorId(string id)
        {
            var cliente = base.RecuperarPorId(id);

            cliente.Enderecos = enderecoService.RecuperarTodos(id);

            cliente.Telefones = telefoneService.RecuperarTodos(id);

            cliente.Emails = emailService.RecuperarTodos(id);

            cliente.Identificacoes = identificacaoService.RecuperarTodos(id);

            return cliente;
        }

        public override ResultadoValidacao Inserir(Cliente model)
        {
            var resultado = base.Inserir(model);

            if (resultado.IsValid)
            {
                // Grava enderecos
                resultado.AdicionarMensagens(enderecoService.Processar(model.Enderecos, null, model.Id));

                // Grava telefones
                resultado.AdicionarMensagens(telefoneService.Processar(model.Telefones, null, model.Id));

                //Grava email
                resultado.AdicionarMensagens(emailService.Processar(model.Emails, null, model.Id));


                // Grava identificacoes
                resultado.AdicionarMensagens(identificacaoService.Processar(model.Identificacoes, null, model.Id));
            }

            return resultado;
        }

        public override ResultadoValidacao Atualizar(Cliente model)
        {
            var cliente = this.RecuperarPorId(model.Id);
            cliente.PreencherDados(model);

            var resultado = base.Atualizar(model);

            if (resultado.IsValid)
            {
                // Grava enderecos
                var resultadoEndereco = enderecoService.Processar(model.Enderecos, cliente.Enderecos, model.Id);

                // Grava telefones
                var resultadoTelefone = telefoneService.Processar(model.Telefones, cliente.Telefones, model.Id);

                //Grava email               
                var resultadoEmail = emailService.Processar(model.Emails, cliente.Emails, model.Id);

                // Grava identificacoes
                resultado.AdicionarMensagens(identificacaoService.Processar(model.Identificacoes, cliente.Identificacoes, model.Id));
            }

            return resultado;
        }

        public override void RemoverPorId(string id)
        {
            emailService.RemoverTodos(id);

            telefoneService.RemoverTodos(id);

            enderecoService.RemoverTodos(id);

            identificacaoService.RemoverTodos(id);

            base.RemoverPorId(id);
        }

        IEnumerable<ClienteDTO> IClienteService.Listar()
        {
            return repository.Listar();
        }

        protected bool VerificarDuplicado(string nome, string identificador, string id = null)
        {
            return repository.VerificarDuplicado(nome, identificador, id);
        }
    }
}


