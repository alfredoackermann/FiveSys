using FileSys.Retaguarda.Application.Acesso.ViewModel;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FileSys.Retaguarda.Application.Admin.Interface
{
    public interface IUsuarioAppService : IBaseCrudAppService<UsuarioViewModel, Usuario>
    {
        IResultadoApplication<UsuarioViewModel> ValidarLogin(LoginViewModel viewModel);

        IResultadoApplication<UsuarioViewModel> TrocarSenha(TrocarSenhaViewModel viewModel);
    }
}
