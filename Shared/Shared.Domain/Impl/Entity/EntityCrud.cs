using FileSys.Shared.Core.Domain.Impl.Validator;
using FileSys.Shared.Core.Domain.Interface.Entity;
using FluentValidation;
using FluentValidation.Results;

namespace FileSys.Shared.Core.Domain.Impl.Entity
{
    public abstract class EntityCrud<TEntity> : Entity<TEntity, string>, IEntityCrud<TEntity> where TEntity : class
    {
        public abstract AbstractValidator<TEntity> Validador { get; }

        public abstract void PreencherDados(TEntity data);

        public abstract ResultadoValidacao Validar();

        protected virtual ResultadoValidacao ExecutarValidacaoPadrao(TEntity entity)
        {
            return Validador.Validate(entity).Transformar();
        }
    }
}
