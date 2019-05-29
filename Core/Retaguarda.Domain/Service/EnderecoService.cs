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
    public class EnderecoService : BaseService<Endereco, string>, IEnderecoService
    {
        private readonly IEnderecoRepository repository;

        public EnderecoService(IEnderecoRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public override ICollection<Endereco> Pesquisar(Expression<Func<Endereco, bool>> predicate)
        {
            throw new NotSupportedException("Para pesquisar utilizar a entidade agregadora e buscar por metadataId");
        }

        public override Endereco RecuperarPorId(string id)
        {
            throw new NotSupportedException("Para pesquisar utilizar a entidade agregadora e buscar por metadataId");
        }

        public override ICollection<Endereco> RecuperarTodos()
        {
            throw new NotSupportedException("Para pesquisar utilizar a entidade agregadora e buscar por metadataId");
        }
        IEnumerable<Endereco> IEnderecoService.RecuperarTodos(string metadataId)
        {
            return repository.RecuperarTodos(metadataId);
        }

        ResultadoValidacao IEnderecoService.Processar(Endereco enderecoModel, Endereco enderecoBancoDados, string metadataId)
        {
            var enderecosModel = new Endereco[1] { enderecoModel };
            var enderecosBancoDados = (enderecoBancoDados == null ? null : new Endereco[1] { enderecoBancoDados });

            return this.Processar(enderecosModel, enderecosBancoDados, metadataId);
        }

        ResultadoValidacao IEnderecoService.Processar(IEnumerable<Endereco> enderecosModel, IEnumerable<Endereco> enderecosBancoDados, string metadataId)
        {
            return this.Processar(enderecosModel, enderecosBancoDados, metadataId);
        }

        void IEnderecoService.RemoverPorId(string id)
        {
            repository.RemoverPorId(id);
        }

        void IEnderecoService.RemoverTodos(string metadataId)
        {
            repository.RemoverTodos(metadataId);
        }

        #region Metodos Privados

        private ResultadoValidacao Processar(IEnumerable<Endereco> enderecosModel, IEnumerable<Endereco> enderecosBancoDados, string metadataId)
        {
            if (string.IsNullOrEmpty(metadataId))
                throw new ArgumentNullException("O parâmetro metadataId não pode ser nulo ou vazio");

            var validacao = new ResultadoValidacao();

            if (enderecosModel != null)
            {
                if (enderecosBancoDados != null)
                {
                    // Verifica os registros removidos pelo usuario e remove da base de dados
                    var registrosParaRemover = enderecosBancoDados.Where(bd => !enderecosModel.Select(m => m.Id).Contains(bd.Id));
                    registrosParaRemover.ToList().ForEach(x => repository.Remover(x));
                }

                // Verifica a acao a ser tomada
                foreach (var enderecoModel in enderecosModel)
                {
                    // Se o model nao tiver id e um novo registro
                    if (string.IsNullOrEmpty(enderecoModel.Id))
                    {
                        validacao.AdicionarMensagens(this.Inserir(enderecoModel, metadataId));
                    }
                    else
                    {
                        // Carrega o registro da base de dados com o mesmo id do enviado pelo model
                        var enderecoBancoDados = enderecosBancoDados?.SingleOrDefault(x => x.Id == enderecoModel.Id);
                        // Se tem id e nao em correspondente na base de dados insere um novo registro pois pode ser um segundo usuario inserindo
                        if (enderecosBancoDados == null)
                        {
                            validacao.AdicionarMensagens(this.Inserir(enderecoModel, metadataId));
                        }
                        else
                        {
                            // Se acho correspondente na base de dados atualiza as informacoes e grava
                            validacao.AdicionarMensagens(this.Atualizar(enderecoModel, enderecoBancoDados));
                        }
                    }
                }
            }
            else
            {
                enderecosBancoDados?.ToList().ForEach(x => repository.Remover(x));
            }

            return validacao;
        }

        private ValidationResult Inserir(Endereco endereco, string metadataId)
        {
            endereco.MetadataId = metadataId;

            var validacao = endereco.Validar();

            if (validacao.IsValid)
            {
                repository.Inserir(endereco);
            }

            return validacao;
        }

        private ValidationResult Atualizar(Endereco enderecoModel, Endereco enderecoBancoDados)
        {
            enderecoBancoDados.PreencherDados(enderecoModel);

            var validacao = enderecoBancoDados.Validar();

            if (validacao.IsValid)
            {
                repository.Atualizar(enderecoBancoDados);
            }

            return validacao;
        }

        #endregion
    }
}
