using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FileSys.Shared.Core.Domain.Interface.Entity;
using FluentValidation.Results;

namespace FileSys.Shared.Core.Domain.Interface.Service
{
    public interface IBaseCrudService<TEntity> : IBaseService<TEntity, string> where TEntity : class, IEntity<TEntity, string>
    {
        ResultadoValidacao Inserir(TEntity model);

        ResultadoValidacao Atualizar(TEntity model);

        void RemoverPorId(string id);

        IDictionary<string, string> RecuperarDropDown();
    }
}
