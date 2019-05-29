using System.Collections.Generic;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FileSys.Retaguarda.Application.Admin.Interface
{
    public interface IClienteAppService : IBaseCrudAppService<ClienteViewModel, Cliente>
    {
        IResultadoApplication<ICollection<ClienteListaViewModel>> Listar();
    }
}
