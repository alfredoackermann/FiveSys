using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository
{
    public interface IClienteRepository : IBaseCrudRepository<Cliente>
    {
        IEnumerable<ClienteDTO> Listar();

        bool VerificarDuplicado(string nome, string identificador, string id = null);
    }
}
