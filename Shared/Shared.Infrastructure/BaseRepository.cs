using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FileSys.Shared.Core.Domain.Interface.Entity;
using FileSys.Shared.Core.Domain.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace FileSys.Shared.Infrastructure.Data
{
    public abstract class BaseRepository<TEntity, TType> : IBaseRepository<TEntity, TType>
         where TEntity : class, IEntity<TEntity, TType>
    {
        protected readonly DbContext context;

        public BaseRepository(DbContext context)
        {
            this.context = context;
        }

        public virtual ICollection<TEntity> Pesquisar(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate).AsNoTracking().ToList();
        }

        public virtual TEntity RecuperarPorId(TType id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public virtual ICollection<TEntity> RecuperarTodos()
        {
            return context.Set<TEntity>().AsNoTracking().ToList();
        }

        protected void Inserir(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        protected void Atualizar(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        protected void Remover(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        protected void RemoverPorId(TType id)
        {
            var entity = RecuperarPorId(id);

            Remover(entity);
        }

        public int Commit()
        {
            return context.SaveChanges();
        }
    }
}
