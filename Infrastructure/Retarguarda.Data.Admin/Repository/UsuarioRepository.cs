using System.Collections.Generic;
using System.Linq;
using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class UsuarioRepository : BaseCrudRepository<Usuario>, IUsuarioRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public UsuarioRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override IDictionary<string, string> RecuperarDropDown()
        {
            return context.Set<Usuario>()
                    .AsNoTracking()
                    .OrderBy(x => x.Nome)
                    .ToDictionary(x => x.Id, x => x.Nome);
        }

        bool IUsuarioRepository.VerificarDuplicado(string nome, string login, string id)
        {

            var query = context.Set<Usuario>() as IQueryable<Usuario>;

            if (!string.IsNullOrEmpty(id))
                query = query.Where(x => x.Id != id);

            return query.Count(x => x.Nome == nome || x.Login == login) > 0;
        }

        Usuario IUsuarioRepository.RecuperarPorLogin(string login)
        {
            return (from usuario in context.Set<Usuario>()
                    where usuario.Login == login
                    select usuario).SingleOrDefault();
        }
    }
}
