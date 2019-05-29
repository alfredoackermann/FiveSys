using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class UnidadeRepository : BaseCrudRepository<Unidade>, IUnidadeRepository
    {
        private readonly RetaguardaAdminContext dbContext;
        public UnidadeRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override IDictionary<string, string> RecuperarDropDown()
        {
            return context.Set<Unidade>()
                .AsNoTracking()
                .OrderBy(x => x.Descricao)
                .ToDictionary(x => x.Id, x => x.Descricao);
        }

        bool IUnidadeRepository.VerificaDuplicado(string descricao)
        {
            return context.Set<Unidade>()
                .Count(x => x.Descricao == descricao) > 0;
        }
    }
}
