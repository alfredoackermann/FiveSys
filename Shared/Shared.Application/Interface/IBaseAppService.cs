using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Interface.Entity;

namespace FileSys.Shared.Application.Interface
{
    public interface IBaseAppService<TViewModel, TEntity, TType> where TEntity : class, IEntity<TEntity, TType>
    {
        IResultadoApplication<ICollection<TViewModel>> RecuperarTodos();
        IResultadoApplication<TViewModel> RecuperarPorId(TType id);
    }
}
