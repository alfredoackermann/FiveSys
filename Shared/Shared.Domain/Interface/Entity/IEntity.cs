namespace FileSys.Shared.Core.Domain.Interface.Entity
{
    public interface IEntity<TEntity, TType> where TEntity : class
    {
        TType Id { get; set; }
    }
}
