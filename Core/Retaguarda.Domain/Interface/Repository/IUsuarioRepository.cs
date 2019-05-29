using FileSys.Shared.Core.Domain.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository
{
    public interface IUsuarioRepository : IBaseCrudRepository<Usuario>
    {
        bool VerificarDuplicado(string nome, string login, string id = null);

        Usuario RecuperarPorLogin(string login);
    }
}
