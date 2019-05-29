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
    public class TelefoneService : BaseService<Telefone, string>, ITelefoneService
    {
        private readonly ITelefoneRepository repository;

        public TelefoneService(ITelefoneRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public override ICollection<Telefone> Pesquisar(Expression<Func<Telefone, bool>> predicate)
        {
            throw new NotSupportedException("Para pesquisar utilizar a entidade agregadora e buscar por metadataId");
        }

        public override Telefone RecuperarPorId(string id)
        {
            throw new NotSupportedException("Para pesquisar utilizar a entidade agregadora e buscar por metadataId");
        }

        public override ICollection<Telefone> RecuperarTodos()
        {
            throw new NotSupportedException("Para pesquisar utilizar a entidade agregadora e buscar por metadataId");
        }

        IEnumerable<Telefone> ITelefoneService.RecuperarTodos(string metadataId)
        {
            return repository.RecuperarTodos(metadataId);
        }

        ResultadoValidacao ITelefoneService.Processar(Telefone telefoneModel, Telefone telefoneBancoDados, string metadataId)
        {
            var telefonesModel = new Telefone[1] { telefoneModel };
            var telefonesBancoDados = (telefoneBancoDados == null ? null : new Telefone[1] { telefoneBancoDados });

            return this.Processar(telefonesModel, telefonesBancoDados, metadataId);
        }

        ResultadoValidacao ITelefoneService.Processar(IEnumerable<Telefone> telefonesModel, IEnumerable<Telefone> telefonesBancoDados, string metadataId)
        {
            return this.Processar(telefonesModel, telefonesBancoDados, metadataId);
        }

        void ITelefoneService.RemoverPorId(string id)
        {
            repository.RemoverPorId(id);
        }

        void ITelefoneService.RemoverTodos(string metadataId)
        {
            repository.RemoverTodos(metadataId);
        }


        #region Metodos Privados

        private ResultadoValidacao Processar(IEnumerable<Telefone> telefonesModel, IEnumerable<Telefone> telefonesBancoDados, string metadataId)
        {
            if (string.IsNullOrEmpty(metadataId))
                throw new ArgumentNullException("O parâmetro metadataId não pode ser nulo ou vazio");

            var validacao = new ResultadoValidacao();

            if (telefonesModel != null)
            {
                if (telefonesBancoDados != null)
                {
                    // Verifica os registros removidos pelo usuario e remove da base de dados
                    var registrosParaRemover = telefonesBancoDados.Where(bd => !telefonesModel.Select(m => m.Id).Contains(bd.Id));
                    registrosParaRemover.ToList().ForEach(x => repository.Remover(x));
                }

                // Verifica a acao a ser tomada
                foreach (var telefoneModel in telefonesModel)
                {
                    // Se o model nao tiver id e um novo registro
                    if (string.IsNullOrEmpty(telefoneModel.Id))
                    {
                        validacao.AdicionarMensagens(this.Inserir(telefoneModel, metadataId));
                    }
                    else
                    {
                        // Carrega o registro da base de dados com o mesmo id do enviado pelo model
                        var telefoneBancoDados = telefonesBancoDados?.SingleOrDefault(x => x.Id == telefoneModel.Id);
                        // Se tem id e nao em correspondente na base de dados insere um novo registro pois pode ser um segundo usuario inserindo
                        if (telefonesBancoDados == null)
                        {
                            validacao.AdicionarMensagens(this.Inserir(telefoneModel, metadataId));
                        }
                        else
                        {
                            // Se acho correspondente na base de dados atualiza as informacoes e grava
                            validacao.AdicionarMensagens(this.Atualizar(telefoneModel, telefoneBancoDados));
                        }
                    }
                }
            }
            else
            {
                telefonesBancoDados?.ToList().ForEach(x => repository.Remover(x));
            }

            return validacao;
        }

        private ValidationResult Inserir(Telefone telefone, string metadataId)
        {
            telefone.MetadataId = metadataId;

            var validacao = telefone.Validar();

            if (validacao.IsValid)
            {
                repository.Inserir(telefone);
            }

            return validacao;
        }

        private ValidationResult Atualizar(Telefone telefoneModel, Telefone telefoneBancoDados)
        {
            telefoneBancoDados.PreencherDados(telefoneModel);

            var validacao = telefoneBancoDados.Validar();

            if (validacao.IsValid)
            {
                repository.Atualizar(telefoneBancoDados);
            }

            return validacao;
        }

        #endregion
    }
}
