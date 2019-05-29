using FileSys.Shared.Core.Domain.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using System.Collections.Generic;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service
{
    public interface ILojaService : IBaseCrudService<Loja>
    {
        IEnumerable<LojaDTO> Listar();
        string ProximaLoja();
    }
}
