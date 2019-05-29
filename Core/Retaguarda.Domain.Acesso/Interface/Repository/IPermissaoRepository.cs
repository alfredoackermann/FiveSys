using FileSys.Shared.Core.Domain.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Acesso.Interface.Repository
{
    public interface IPermissaoRepository : IBaseRepository<Permissao, string>
    {
        void Remover(Permissao entity);
    }
}
