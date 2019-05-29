using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FileSys.Retaguarda.Application.Admin.Interface
{
    public interface IUnidadeAppService : IBaseCrudAppService<UnidadeViewModel, Unidade>
    {
    }
}
