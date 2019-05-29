using FileSys.Retaguarda.Application.Acesso.ViewModel;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;

namespace FileSys.Retaguarda.Application.Acesso.Interface
{
    public interface IPerfilAppService : IBaseCrudAppService<PerfilViewModel, Perfil>
    {
    }
}
