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
    public class IdentificacaoService : BaseService<Identificacao, string>, IIdentificacaoService
    {
        private readonly IIdentificacaoRepository repository;

        public IdentificacaoService(IIdentificacaoRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public override ICollection<Identificacao> Pesquisar(Expression<Func<Identificacao, bool>> predicate)
        {
            throw new NotSupportedException("Para pesquisar utilizar a entidade agregadora e buscar por metadataId");
        }

        public override Identificacao RecuperarPorId(string id)
        {
            throw new NotSupportedException("Para pesquisar utilizar a entidade agregadora e buscar por metadataId");
        }

        public override ICollection<Identificacao> RecuperarTodos()
        {
            throw new NotSupportedException("Para pesquisar utilizar a entidade agregadora e buscar por metadataId");
        }

        IEnumerable<Identificacao> IIdentificacaoService.RecuperarTodos(string metadataId)
        {
            return repository.RecuperarTodos(metadataId);
        }

        ResultadoValidacao IIdentificacaoService.Processar(Identificacao identificacaoModel, Identificacao identificacaoBancoDados, string metadataId)
        {
            var IdentificacaosModel = new Identificacao[1] { identificacaoModel };
            var IdentificacaosBancoDados = (identificacaoBancoDados == null ? null : new Identificacao[1] { identificacaoBancoDados });

            return this.Processar(IdentificacaosModel, IdentificacaosBancoDados, metadataId);
        }

        ResultadoValidacao IIdentificacaoService.Processar(IEnumerable<Identificacao> identificacaosModel, IEnumerable<Identificacao> identificacaosBancoDados, string metadataId)
        {
            return this.Processar(identificacaosModel, identificacaosBancoDados, metadataId);
        }

        void IIdentificacaoService.RemoverPorId(string id)
        {
            repository.RemoverPorId(id);
        }

        void IIdentificacaoService.RemoverTodos(string metadataId)
        {
            repository.RemoverTodos(metadataId);
        }

        #region Metodos Privados

        private ResultadoValidacao Processar(IEnumerable<Identificacao> identificacaosModel, IEnumerable<Identificacao> identificacaosBancoDados, string metadataId)
        {
            if (string.IsNullOrEmpty(metadataId))
                throw new ArgumentNullException("O parâmetro metadataId não pode ser nulo ou vazio");

            var validacao = new ResultadoValidacao();

            if (identificacaosModel != null)
            {
                if (identificacaosBancoDados != null)
                {
                    // Verifica os registros removidos pelo usuario e remove da base de dados
                    var registrosParaRemover = identificacaosBancoDados.Where(bd => !identificacaosModel.Select(m => m.Id).Contains(bd.Id));
                    registrosParaRemover.ToList().ForEach(x => repository.Remover(x));
                }

                // Verifica a acao a ser tomada
                foreach (var IdentificacaoModel in identificacaosModel)
                {
                    // Se o model nao tiver id e um novo registro
                    if (string.IsNullOrEmpty(IdentificacaoModel.Id))
                    {
                        validacao.AdicionarMensagens(this.Inserir(IdentificacaoModel, metadataId));
                    }
                    else
                    {
                        // Carrega o registro da base de dados com o mesmo id do enviado pelo model
                        var IdentificacaoBancoDados = identificacaosBancoDados?.SingleOrDefault(x => x.Id == IdentificacaoModel.Id);
                        // Se tem id e nao em correspondente na base de dados insere um novo registro pois pode ser um segundo usuario inserindo
                        if (identificacaosBancoDados == null)
                        {
                            validacao.AdicionarMensagens(this.Inserir(IdentificacaoModel, metadataId));
                        }
                        else
                        {
                            // Se acho correspondente na base de dados atualiza as informacoes e grava
                            validacao.AdicionarMensagens(this.Atualizar(IdentificacaoModel, IdentificacaoBancoDados));
                        }
                    }
                }
            }
            else
            {
                identificacaosBancoDados?.ToList().ForEach(x => repository.Remover(x));
            }

            return validacao;
        }

        private ValidationResult Inserir(Identificacao identificacao, string metadataId)
        {
            identificacao.MetadataId = metadataId;

            var validacao = identificacao.Validar();

            if (validacao.IsValid)
            {
                repository.Inserir(identificacao);
            }

            return validacao;
        }

        private ValidationResult Atualizar(Identificacao identificacaoModel, Identificacao identificacaoBancoDados)
        {
            identificacaoBancoDados.PreencherDados(identificacaoModel);

            var validacao = identificacaoBancoDados.Validar();

            if (validacao.IsValid)
            {
                repository.Atualizar(identificacaoBancoDados);
            }

            return validacao;
        }

        #endregion
    }
}
