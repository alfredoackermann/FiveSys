using FileSys.Shared.Core.Domain.Interface.Entity;
using FileSys.Shared.Core.Domain.Interface.Repository;
using FileSys.Shared.Core.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentValidation.Results;
using FileSys.Shared.Core.Domain.Impl.Validator;

namespace FileSys.Shared.Core.Domain.Impl.Service
{
    public abstract class BaseCrudService<TEntity> : BaseService<TEntity, string>, IBaseCrudService<TEntity>
        where TEntity : class, IEntityCrud<TEntity>
    {
        private readonly IBaseCrudRepository<TEntity> repository;

        public BaseCrudService(IBaseCrudRepository<TEntity> repository) : base(repository)
        {
            this.repository = repository;
        }

        public IDictionary<string, string> RecuperarDropDown()
        {
            return repository.RecuperarDropDown();
        }

        public virtual ResultadoValidacao Atualizar(TEntity model)
        {
            var entidade = base.RecuperarPorId(model.Id);

            entidade.PreencherDados(model);

            var validate = entidade.Validar();

            if (validate.IsValid)
                repository.Atualizar(entidade);

            return validate;
        }

        public virtual ResultadoValidacao Inserir(TEntity model)
        {
            var validate = model.Validar();

            if (validate.IsValid)
                repository.Inserir(model);

            return validate;
        }

        public virtual void RemoverPorId(string id)
        {
            repository.RemoverPorId(id);
        }
    }
}
