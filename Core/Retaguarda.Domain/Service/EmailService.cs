using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FileSys.Shared.Core.Domain.Impl.Service;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;
using FluentValidation.Results;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Service
{
    public class EmailService : BaseService<Email, string>, IEmailService
    {
        private readonly IEmailRepository repository;

        public EmailService(IEmailRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public override ICollection<Email> Pesquisar(Expression<Func<Email, bool>> predicate)
        {
            throw new NotSupportedException("Para pesquisar utilizar a entidade agregadora e buscar por metadataId");
        }

        public override Email RecuperarPorId(string id)
        {
            throw new NotSupportedException("Para pesquisar utilizar a entidade agregadora e buscar por metadataId");
        }

        public override ICollection<Email> RecuperarTodos()
        {
            throw new NotSupportedException("Para pesquisar utilizar a entidade agregadora e buscar por metadataId");
        }

        IEnumerable<Email> IEmailService.RecuperarTodos(string metadataId)
        {
            return repository.RecuperarTodos(metadataId);
        }

        ResultadoValidacao IEmailService.Processar(Email emailModel, Email emailBancoDados, string metadataId)
        {
            var emailsModel = new Email[1] { emailModel };
            var emailsBancoDados = (emailBancoDados == null ? null : new Email[1] { emailBancoDados });

            return this.Processar(emailsModel, emailsBancoDados, metadataId);
        }

        ResultadoValidacao IEmailService.Processar(IEnumerable<Email> emailsModel, IEnumerable<Email> emailsBancoDados, string metadataId)
        {
            return this.Processar(emailsModel, emailsBancoDados, metadataId);
        }

        void IEmailService.RemoverPorId(string id)
        {
            repository.RemoverPorId(id);
        }

        void IEmailService.RemoverTodos(string metadataId)
        {
            repository.RemoverTodos(metadataId);
        }


        #region Metodos Privados

        private ResultadoValidacao Processar(IEnumerable<Email> emailsModel, IEnumerable<Email> emailsBancoDados, string metadataId)
        {
            if (string.IsNullOrEmpty(metadataId))
                throw new ArgumentNullException("O parâmetro metadataId não pode ser nulo ou vazio");

            var validacao = new ResultadoValidacao();

            if (emailsModel != null)
            {
                if (emailsBancoDados != null)
                {
                    // Verifica os registros removidos pelo usuario e remove da base de dados
                    var registrosParaRemover = emailsBancoDados.Where(bd => !emailsModel.Select(m => m.Id).Contains(bd.Id));
                    registrosParaRemover.ToList().ForEach(x => repository.Remover(x));
                }

                // Verifica a acao a ser tomada
                foreach (var emailModel in emailsModel)
                {
                    // Se o model nao tiver id e um novo registro
                    if (string.IsNullOrEmpty(emailModel.Id))
                    {
                        validacao.AdicionarMensagens(this.Inserir(emailModel, metadataId));
                    }
                    else
                    {
                        // Carrega o registro da base de dados com o mesmo id do enviado pelo model
                        var emailBancoDados = emailsBancoDados?.SingleOrDefault(x => x.Id == emailModel.Id);
                        // Se tem id e nao em correspondente na base de dados insere um novo registro pois pode ser um segundo usuario inserindo
                        if (emailsBancoDados == null)
                        {
                            validacao.AdicionarMensagens(this.Inserir(emailModel, metadataId));
                        }
                        else
                        {
                            // Se acho correspondente na base de dados atualiza as informacoes e grava
                            validacao.AdicionarMensagens(this.Atualizar(emailModel, emailBancoDados));
                        }
                    }
                }
            }
            else
            {
                emailsBancoDados?.ToList().ForEach(x => repository.Remover(x));
            }

            return validacao;
        }

        private ValidationResult Inserir(Email email, string metadataId)
        {
            email.MetadataId = metadataId;

            var validacao = email.Validar();

            if (validacao.IsValid)
            {
                repository.Inserir(email);
            }

            return validacao;
        }

        private ValidationResult Atualizar(Email emailModel, Email emailBancoDados)
        {
            emailBancoDados.PreencherDados(emailModel);

            var validacao = emailBancoDados.Validar();

            if (validacao.IsValid)
            {
                repository.Atualizar(emailBancoDados);
            }

            return validacao;
        }

        #endregion
    }
}
