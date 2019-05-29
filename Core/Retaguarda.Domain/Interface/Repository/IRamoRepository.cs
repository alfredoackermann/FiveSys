using FileSys.Shared.Core.Domain.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository
{
    public interface IRamoRepository : IBaseCrudRepository<Ramo>
    {
        bool VerificaDuplicado(string descricao);
    }
}
