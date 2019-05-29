using AutoMapper;
using FileSys.Retaguarda.Application.Acesso.ViewModel;
using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Impl;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;

namespace FileSys.Retaguarda.Application.Admin.Impl
{
    public class UsuarioAppService : BaseCrudAppService<UsuarioViewModel, Usuario>, IUsuarioAppService
    {
        private readonly IUsuarioService service;
        private readonly IMapper mapper;

        public UsuarioAppService(IUsuarioService service,
            IMapper mapper) : base(service, mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        IResultadoApplication<UsuarioViewModel> IUsuarioAppService.ValidarLogin(LoginViewModel viewModel)
        {
            var resultado = new ResultadoApplication<UsuarioViewModel>();

            try
            {
                var usuario = mapper.Map<UsuarioViewModel>(service.ValidarLogin(viewModel.Login, viewModel.Senha));
                resultado.DefinirData(usuario);
                resultado.ExecutadoComSuccesso();

                if (usuario.SenhaTemporaria)
                    resultado.ExibirMensagem("É necessário que você troque a sua senha !!");
                else
                    resultado.ExibirMensagem("Seja bem vindo !!");
            }
            catch (System.Exception ex)
            {
                resultado.ExecutadoComErro(ex);
            }

            return resultado;
        }

        IResultadoApplication<UsuarioViewModel> IUsuarioAppService.TrocarSenha(TrocarSenhaViewModel viewModel)
        {
            var resultado = new ResultadoApplication<UsuarioViewModel>();

            try
            {
                var usuario = mapper.Map<UsuarioViewModel>(service.ValidarLogin(viewModel.Login, viewModel.SenhaAtual));
                if (usuario != null)
                {
                    usuario = mapper.Map<UsuarioViewModel>(service.TrocarSenha(usuario.Id, viewModel.NovaSenha));
                    service.Commit();

                    resultado.DefinirData(usuario);
                    resultado.ExecutadoComSuccesso();
                    resultado.ExibirMensagem("Seja bem vindo !!");
                }
            }
            catch (System.Exception ex)
            {
                resultado.ExecutadoComErro(ex);
            }

            return resultado;
        }
    }
}
