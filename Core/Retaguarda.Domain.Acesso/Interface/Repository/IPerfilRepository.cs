using FileSys.Shared.Core.Domain.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Acesso.Interface.Repository
{
    public interface IPerfilRepository : IBaseCrudRepository<Perfil>
    {
        bool VerificaDuplicado(string descricao, string id);
    }
}
