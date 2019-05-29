using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using System.Collections.Generic;

namespace FileSys.Retaguarda.Application.Admin.Interface
{
    public interface IFornecedorAppService : IBaseCrudAppService<FornecedorViewModel, Fornecedor>
    {
        IResultadoApplication<ICollection<FornecedorListaViewModel>> Listar();
        string ProximoFornecedor();
    }
}
