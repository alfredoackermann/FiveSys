using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository
{
    public interface IEnderecoRepository : IBaseRepository<Endereco, string>
    {
        void Remover(Endereco endereco);
        void RemoverPorId(string id);
        void RemoverTodos(string metadataId);
        void Inserir(Endereco endereco);
        void Atualizar(Endereco endereco);
        IEnumerable<Endereco> RecuperarTodos(string metadataId);
    }
}
