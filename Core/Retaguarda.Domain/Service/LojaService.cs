using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Core.Domain.Impl.Service;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;
using System;
using System.Collections.Generic;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Service
{
    public class LojaService : BaseCrudService<Loja>, ILojaService
    {
        private readonly ILojaRepository repository;
        private readonly IEmailService emailService;
        private readonly IEnderecoService enderecoService;
        private readonly ITelefoneService telefoneService;

        public LojaService(ILojaRepository repository,
            IEmailService emailService,
            IEnderecoService enderecoService,
            ITelefoneService telefoneService) : base(repository)
        {
            this.repository = repository;
            this.emailService = emailService;
            this.enderecoService = enderecoService;
            this.telefoneService = telefoneService;
        }

        private bool VerificarDuplicado(string razaosocial, string id = null)
        {
            return repository.VerificarDuplicado(razaosocial, id);
        }

        IEnumerable<LojaDTO> ILojaService.Listar()
        {
            return repository.Listar();
        }

        public override ResultadoValidacao Inserir(Loja model)
        {
            if (VerificarDuplicado(model.RazaoSocial))
                throw new InvalidOperationException(Textos.Geral_Mensagem_Erro_NomeDuplicado);

            var resultado = base.Inserir(model);

            if (resultado.IsValid)
            {
                resultado.AdicionarMensagens(enderecoService.Processar(model.Enderecos, null, model.Id));

                resultado.AdicionarMensagens(telefoneService.Processar(model.Telefones, null, model.Id));

                resultado.AdicionarMensagens(emailService.Processar(model.Emails, null, model.Id));
            }
            return resultado;
        }

        public override ResultadoValidacao Atualizar(Loja model)
        {
            if (VerificarDuplicado(model.RazaoSocial, model.Id))
                throw new InvalidOperationException(Textos.Geral_Mensagem_Erro_NomeDuplicado);

            var loja = this.RecuperarPorId(model.Id);
            loja.PreencherDados(model);

            var resultado = base.Atualizar(model);

            if (resultado.IsValid)
            {
                resultado.AdicionarMensagens(enderecoService.Processar(model.Enderecos, loja.Enderecos, model.Id));

                resultado.AdicionarMensagens(telefoneService.Processar(model.Telefones, loja.Telefones, model.Id));

                resultado.AdicionarMensagens(emailService.Processar(model.Emails, loja.Emails, model.Id));
            }
            return resultado;
        }

        public override Loja RecuperarPorId(string id)
        {
            var loja = base.RecuperarPorId(id);

            loja.Enderecos = enderecoService.RecuperarTodos(id);

            loja.Telefones = telefoneService.RecuperarTodos(id);

            loja.Emails = emailService.RecuperarTodos(id);

            return loja;
        }

        public override void RemoverPorId(string id)
        {
            emailService.RemoverTodos(id);

            telefoneService.RemoverTodos(id);

            enderecoService.RemoverTodos(id);

            base.RemoverPorId(id);
        }

        public string ProximaLoja()
        {
            var resultado = repository.RecuperarTodos().Count;

            if (resultado > 0)
            {
                return repository.ProximaLoja();
            }
            else
            {
                return "1";
            }

        }
    }
}
