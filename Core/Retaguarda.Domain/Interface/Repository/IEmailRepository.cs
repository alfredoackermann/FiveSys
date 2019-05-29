using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository
{
    public interface IEmailRepository : IBaseRepository<Email, string>
    {
        void Remover(Email email);
        void RemoverPorId(string id);
        void RemoverTodos(string metadataId);
        void Inserir(Email email);
        void Atualizar(Email email);
        IEnumerable<Email> RecuperarTodos(string metadataId);
    }
}
