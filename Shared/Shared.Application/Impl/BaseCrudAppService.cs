using System.Collections.Generic;
using AutoMapper;
using FileSys.Shared.Application.Impl;
using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Core.Domain.Interface.Entity;
using FileSys.Shared.Core.Domain.Interface.Service;

namespace FileSys.Shared.Application.Interface
{
    public abstract class BaseCrudAppService<TViewModel, TEntity> : BaseAppService<TViewModel, TEntity, string>, 
        IBaseCrudAppService<TViewModel, TEntity> where TEntity : class, IEntity<TEntity, string>
    {
        private readonly IBaseCrudService<TEntity> service;
        private readonly IMapper mapper;

        public BaseCrudAppService(
            IBaseCrudService<TEntity> service,
            IMapper mapper) : base (service, mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public virtual IResultadoApplication Inserir(TViewModel viewModel)
        {
            var application = new ResultadoApplication();

            try
            {
                application.Resultado(service.Inserir(mapper.Map<TEntity>(viewModel)));

                if (application.Successo)
                {
                    service.Commit();
                    application.ExibirMensagem(Textos.Geral_Mensagem_Sucesso_Inclusao);
                }
            }
            catch (System.Exception ex)
            {
                application.ExecutadoComErro(ex);
            }

            return application;
        }

        public virtual IResultadoApplication Atualizar(TViewModel viewModel)
        {
            var application = new ResultadoApplication();

            try
            {
                application.Resultado(service.Atualizar(mapper.Map<TEntity>(viewModel)));

                if (application.Successo)
                {
                    service.Commit();
                    application.ExibirMensagem(Textos.Geral_Mensagem_Sucesso_Alteracao);
                }
            }
            catch (System.Exception ex)
            {
                application.ExecutadoComErro(ex);
            }

            return application;
        }

        public virtual IResultadoApplication RemoverPorId(string id)
        {
            var application = new ResultadoApplication();

            try
            {
                service.RemoverPorId(id);
                service.Commit();
                application.ExecutadoComSuccesso().ExibirMensagem(Textos.Geral_Mensagem_Sucesso_Exclusao);
            }
            catch (System.Exception ex)
            {
                application.ExecutadoComErro(ex);
            }

            return application;
        }

        public virtual IResultadoApplication<IDictionary<string, string>> RecuperarDropdown()
        {
            var application = new ResultadoApplication<IDictionary<string, string>>();

            try
            {
                application.DefinirData(service.RecuperarDropDown());
                application.ExecutadoComSuccesso();
            }
            catch (System.Exception ex)
            {
                application.ExecutadoComErro(ex);
            }

            return application;
        }
    }
}
