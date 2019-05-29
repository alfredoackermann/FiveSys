using FileSys.Shared.Core.Domain.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using System.Collections.Generic;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository
{
    public interface ILojaRepository : IBaseCrudRepository<Loja>
    {
        bool VerificarDuplicado(string RazaoSocial, string id = null);

        IEnumerable<LojaDTO> Listar();
        string ProximaLoja();
    }
}
