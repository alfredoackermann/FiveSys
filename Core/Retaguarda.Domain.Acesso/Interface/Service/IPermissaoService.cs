using FileSys.Shared.Core.Domain.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Acesso.Interface.Service
{
    public interface IPermissaoService : IBaseService<Permissao, string>
    {
        void Remover(Permissao entity);
    }
}
