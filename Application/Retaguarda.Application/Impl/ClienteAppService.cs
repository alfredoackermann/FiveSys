using System.Collections.Generic;
using AutoMapper;
using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Impl;
using FileSys.Shared.Application.Interface;
using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;

namespace FileSys.Retaguarda.Application.Admin.Impl
{
    public class ClienteAppService : BaseCrudAppService<ClienteViewModel, Cliente>, IClienteAppService
    {
        private readonly IClienteService service;
        private readonly IPessoaJuridicaService pessoaJuridicaService;
        private readonly IPessoaFisicaService pessoaFisicaService;
        private readonly IMapper mapper;

        public ClienteAppService(IClienteService service,
            IPessoaJuridicaService pessoaJuridicaService,
            IPessoaFisicaService pessoaFisicaService,
            IMapper mapper) : base(service, mapper)
        {
            this.service = service;
            this.pessoaJuridicaService = pessoaJuridicaService;
            this.pessoaFisicaService = pessoaFisicaService;
            this.mapper = mapper;
        }

        public override IResultadoApplication Inserir(ClienteViewModel viewModel)
        {
            var application = new ResultadoApplication();

            try
            {
                if (viewModel.TipoPessoaCodigo == PessoaTipoEnum.Fisica.Valor())
                    application.Resultado(pessoaFisicaService.Inserir(mapper.Map<PessoaFisica>(viewModel)));
                else
                    application.Resultado(pessoaJuridicaService.Inserir(mapper.Map<PessoaJuridica>(viewModel)));


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

        public override IResultadoApplication Atualizar(ClienteViewModel viewModel)
        {
            var application = new ResultadoApplication();

            try
            {
                if (viewModel.TipoPessoaCodigo == PessoaTipoEnum.Fisica.Valor())
                    application.Resultado(pessoaFisicaService.Atualizar(mapper.Map<PessoaFisica>(viewModel)));
                else
                    application.Resultado(pessoaJuridicaService.Atualizar(mapper.Map<PessoaJuridica>(viewModel)));


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

        IResultadoApplication<ICollection<ClienteListaViewModel>> IClienteAppService.Listar()
        {
            var result = new ResultadoApplication<ICollection<ClienteListaViewModel>>();

            try
            {
                result.DefinirData(mapper.Map<ICollection<ClienteListaViewModel>>(service.Listar()));
                result.ExecutadoComSuccesso();
            }
            catch (System.Exception ex)
            {
                result.ExecutadoComErro(ex);
            }

            return result;
        }
    }
}
