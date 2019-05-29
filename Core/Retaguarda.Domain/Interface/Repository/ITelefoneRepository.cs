using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository
{
    public interface ITelefoneRepository : IBaseRepository<Telefone, string>
    {
        void Remover(Telefone telefone);
        void RemoverPorId(string id);
        void RemoverTodos(string metadataId);
        void Inserir(Telefone telefone);
        void Atualizar(Telefone telefone);
        IEnumerable<Telefone> RecuperarTodos(string metadataId);
    }
}
