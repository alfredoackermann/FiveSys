using FileSys.Shared.Core.Domain.Impl.Validator;
using FluentValidation.Results;

namespace FileSys.Shared.Core.Domain.Interface.Entity
{
    public interface IEntityCrud<TEntity> : IEntity<TEntity, string> where TEntity : class
    {
        ResultadoValidacao Validar();

        void PreencherDados(TEntity data);
    }
}
