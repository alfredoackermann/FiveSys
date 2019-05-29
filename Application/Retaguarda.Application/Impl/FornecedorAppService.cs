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
using System.Collections.Generic;

namespace FileSys.Retaguarda.Application.Admin.Impl
{
    public class FornecedorAppService : BaseCrudAppService<FornecedorViewModel, Fornecedor>, IFornecedorAppService
    {
        private readonly IFornecedorService service;
        private readonly IPessoaJuridicaFornecedorService pessoaJuridicaService;
        private readonly IPessoaFisicaFornecedorService pessoaFisicaService;
        private readonly IMapper mapper;
        public FornecedorAppService(IFornecedorService service,
                                IPessoaJuridicaFornecedorService pessoaJuridicaService,
                                IPessoaFisicaFornecedorService pessoaFisicaService,
                                IMapper mapper) : base(service, mapper)
        {
            this.service = service;
            this.pessoaFisicaService = pessoaFisicaService;
            this.pessoaJuridicaService = pessoaJuridicaService;
            this.mapper = mapper;
        }

        string IFornecedorAppService.ProximoFornecedor()
        {
            return service.ProximoFornecedor();
        }


        public override IResultadoApplication Inserir(FornecedorViewModel viewModel)
        {
            var application = new ResultadoApplication();

            try
            {
                if (viewModel.TipoPessoaCodigo == PessoaTipoEnum.Fisica.Valor())
                    application.Resultado(pessoaFisicaService.Inserir(mapper.Map<PessoaFisicaFornecedor>(viewModel)));
                else
                    application.Resultado(pessoaJuridicaService.Inserir(mapper.Map<PessoaJuridicaFornecedor>(viewModel)));


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

        public override IResultadoApplication Atualizar(FornecedorViewModel viewModel)
        {
            var application = new ResultadoApplication();

            try
            {
                if (viewModel.TipoPessoaCodigo == PessoaTipoEnum.Fisica.Valor())
                    application.Resultado(pessoaFisicaService.Atualizar(mapper.Map<PessoaFisicaFornecedor>(viewModel)));
                else
                    application.Resultado(pessoaJuridicaService.Atualizar(mapper.Map<PessoaJuridicaFornecedor>(viewModel)));


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
        IResultadoApplication<ICollection<FornecedorListaViewModel>> IFornecedorAppService.Listar()
        {
            var result = new ResultadoApplication<ICollection<FornecedorListaViewModel>>();

            try
            {
                result.DefinirData(mapper.Map<ICollection<FornecedorListaViewModel>>(service.Listar()));
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
