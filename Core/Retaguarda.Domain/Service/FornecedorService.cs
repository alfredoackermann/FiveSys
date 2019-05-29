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
    public class FornecedorService : BaseCrudService<Fornecedor>, IFornecedorService
    {
        private readonly IFornecedorRepository repository;
        private readonly IEmailService emailService;
        private readonly IEnderecoService enderecoService;
        private readonly ITelefoneService telefoneService;

        public FornecedorService(IFornecedorRepository repository,
                                        IEmailService emailService,
                                        IEnderecoService enderecoService,
                                        ITelefoneService telefoneService) : base(repository)
        {
            this.repository = repository;
            this.emailService = emailService;
            this.enderecoService = enderecoService;
            this.telefoneService = telefoneService;
        }

        string IFornecedorService.ProximoFornecedor()
        {
            var resultado = repository.RecuperarTodos().Count;

            if (resultado > 0)
            {
                return repository.ProximoFornecedor();
            }
            else
            {
                return "1";
            }
        }

        public override ResultadoValidacao Inserir(Fornecedor model)
        {
            if (VerificarDuplicado(model.Nome))
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

        public override ResultadoValidacao Atualizar(Fornecedor model)
        {
            if (VerificarDuplicado(model.Nome, model.Id))
                throw new InvalidOperationException(Textos.Geral_Mensagem_Erro_NomeDuplicado);

            var fornecedor = this.RecuperarPorId(model.Id);
            fornecedor.PreencherDados(model);

            var resultado = base.Atualizar(model);

            if (resultado.IsValid)
            {
                resultado.AdicionarMensagens(enderecoService.Processar(model.Enderecos, fornecedor.Enderecos, model.Id));

                resultado.AdicionarMensagens(telefoneService.Processar(model.Telefones, fornecedor.Telefones, model.Id));

                resultado.AdicionarMensagens(emailService.Processar(model.Emails, fornecedor.Emails, model.Id));
            }
            return resultado;
        }

        public override Fornecedor RecuperarPorId(string id)
        {
            var fornecedor = base.RecuperarPorId(id);

            fornecedor.Enderecos = enderecoService.RecuperarTodos(id);

            fornecedor.Telefones = telefoneService.RecuperarTodos(id);

            fornecedor.Emails = emailService.RecuperarTodos(id);

            return fornecedor;
        }

        public override void RemoverPorId(string id)
        {
            emailService.RemoverTodos(id);

            telefoneService.RemoverTodos(id);

            enderecoService.RemoverTodos(id);

            base.RemoverPorId(id);
        }

        public bool VerificarDuplicado(string Nome, string id = null)
        {
            return repository.VerificarDuplicado(Nome, id);
        }

        protected bool VerificarDuplicado(string nome, string identificador, string id = null)
        {
            return repository.VerificarDuplicado(nome, identificador, id);
        }

        IEnumerable<FornecedorDTO> IFornecedorService.Listar()
        {
            return repository.Listar();
        }
    }

}
