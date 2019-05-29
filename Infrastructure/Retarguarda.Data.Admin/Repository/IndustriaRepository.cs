using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class IndustriaRepository : BaseCrudRepository<Industria>, IIndustriaRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public IndustriaRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override IDictionary<string, string> RecuperarDropDown()
        {
            return context.Set<Industria>()
                .AsNoTracking()
                .OrderBy(x => x.Descricao)
                .ToDictionary(x => x.Id, x => x.Descricao);
        }

        public bool VerificaDuplicado(string descricao)
        {
            return context.Set<Industria>()
                .Count(x => x.Descricao == descricao) > 0;
        }
    }
}
