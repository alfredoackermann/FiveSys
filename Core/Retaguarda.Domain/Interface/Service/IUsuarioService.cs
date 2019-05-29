using FileSys.Shared.Core.Domain.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service
{
    public interface IUsuarioService : IBaseCrudService<Usuario>
    {
        Usuario ValidarLogin(string email, string senha);

        Usuario TrocarSenha(string id, string novaSenha);
    }
}
