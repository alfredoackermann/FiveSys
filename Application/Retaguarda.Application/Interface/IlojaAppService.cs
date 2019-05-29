using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using System.Collections.Generic;

namespace FileSys.Retaguarda.Application.Admin.Interface
{
    public interface ILojaAppService : IBaseCrudAppService<LojaViewModel, Loja>
    {
        IResultadoApplication<ICollection<LojaListaViewModel>> Listar();
        string ProximaLoja();
    }
}
