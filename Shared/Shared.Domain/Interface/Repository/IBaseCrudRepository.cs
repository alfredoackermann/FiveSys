using FileSys.Shared.Core.Domain.Interface.Entity;
using System.Collections.Generic;

namespace FileSys.Shared.Core.Domain.Interface.Repository
{
    public interface IBaseCrudRepository<TEntity> : IBaseRepository<TEntity, string> 
        where TEntity : class, IEntityCrud<TEntity>
    {
        void Inserir(TEntity entity);

        void Atualizar(TEntity entity);

        void Remover(TEntity entity);

        void RemoverPorId(string id);

        IDictionary<string, string> RecuperarDropDown();
    }
}
