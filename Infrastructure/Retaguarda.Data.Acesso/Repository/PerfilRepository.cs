using System.Collections.Generic;
using System.Linq;
using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;
using FiveSys.Retaguarda.Core.Domain.Acesso.Interface.Repository;
using FiveSys.Retaguarda.Infrastructure.Data.Acesso;
using Microsoft.EntityFrameworkCore;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class PerfilRepository : BaseCrudRepository<Perfil>, IPerfilRepository
    {
        private readonly RetaguardaAcessoContext dbContext;

        public PerfilRepository(RetaguardaAcessoContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }


        public override IDictionary<string, string> RecuperarDropDown()
        {
            return context.Set<Perfil>()
                .AsNoTracking()
                .OrderBy(x => x.Descricao)
                .ToDictionary(x => x.Id, x => x.Descricao);
        }

        public override Perfil RecuperarPorId(string id)
        {
            return context.Set<Perfil>()
                .Include(x => x.Permissoes)
                .SingleOrDefault(x => x.Id == id);
        }

        bool IPerfilRepository.VerificaDuplicado(string descricao, string id)
        {
            var query = context.Set<Perfil>() as IQueryable<Perfil>;

            if (!string.IsNullOrEmpty(id))
                query = query.Where(x => x.Id != id);

            return query.Count(x => x.Descricao == descricao) > 0;
        }
    }
}
