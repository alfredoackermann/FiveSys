using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Interface.Entity;

namespace FileSys.Shared.Application.Interface
{
    public interface IBaseCrudAppService<TViewModel, TEntity> : IBaseAppService<TViewModel, TEntity, string>
         where TEntity : class, IEntity<TEntity, string>
    {
        IResultadoApplication Inserir(TViewModel viewModel);
        IResultadoApplication Atualizar(TViewModel viewModel);
        IResultadoApplication RemoverPorId(string id);
        IResultadoApplication<IDictionary<string, string>> RecuperarDropdown();
    }
}
