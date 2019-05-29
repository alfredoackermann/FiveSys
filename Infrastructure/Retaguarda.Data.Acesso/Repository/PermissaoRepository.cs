using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;
using FiveSys.Retaguarda.Core.Domain.Acesso.Interface.Repository;
using FiveSys.Retaguarda.Infrastructure.Data.Acesso;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class PermissaoRepository : BaseRepository<Permissao, string>, IPermissaoRepository
    {
        private readonly RetaguardaAcessoContext dbContext;

        public PermissaoRepository(RetaguardaAcessoContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        void IPermissaoRepository.Remover(Permissao entity)
        {
            base.Remover(entity);
        }
    }
}
