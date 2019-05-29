using System.Collections.Generic;
using System.Linq;
using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class TipoClienteRepository : BaseCrudRepository<TipoCliente>, ITipoClienteRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public TipoClienteRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override IDictionary<string, string> RecuperarDropDown()
        {
            return context.Set<TipoCliente>()
                .AsNoTracking()
                .OrderBy(x => x.Descricao)
                .ToDictionary(x => x.Id, x => x.Descricao);
        }

        public bool VerificaDuplicado(string descricao)
        {
            return context.Set<TipoCliente>()
                .Count(x => x.Descricao == descricao) > 0;
        }
    }
}