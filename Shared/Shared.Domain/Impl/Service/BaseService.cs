using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FileSys.Shared.Core.Domain.Interface.Entity;
using FileSys.Shared.Core.Domain.Interface.Repository;
using FileSys.Shared.Core.Domain.Interface.Service;

namespace FileSys.Shared.Core.Domain.Impl.Service
{
    public abstract class BaseService<TEntity, TTYpe> : IBaseService<TEntity, TTYpe>
        where TEntity : class, IEntity<TEntity, TTYpe>
    {
        private readonly IBaseRepository<TEntity, TTYpe> repository;

        public BaseService(IBaseRepository<TEntity, TTYpe> repository)
        {
            this.repository = repository;
        }

        public virtual ICollection<TEntity> Pesquisar(Expression<Func<TEntity, bool>> predicate)
        {
            return repository.Pesquisar(predicate);
        }

        public virtual TEntity RecuperarPorId(TTYpe id)
        {
            return repository.RecuperarPorId(id);
        }

        public virtual ICollection<TEntity> RecuperarTodos()
        {
            return repository.RecuperarTodos();
        }

        public int Commit()
        {
            return repository.Commit();
        }
    }
}
