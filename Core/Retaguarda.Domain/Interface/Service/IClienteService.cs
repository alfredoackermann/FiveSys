using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service
{
    public interface IClienteService : IBaseCrudService<Cliente>
    {
        IEnumerable<ClienteDTO> Listar();
    }
}