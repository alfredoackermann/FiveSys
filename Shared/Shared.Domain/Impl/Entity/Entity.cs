using FileSys.Shared.Core.Domain.Interface.Entity;

namespace FileSys.Shared.Core.Domain.Impl.Entity
{
    public abstract class Entity<TEntity, TTYpe> : IEntity<TEntity, TTYpe> where TEntity : class
    {
        public TTYpe Id { get; set; }
    }
}
