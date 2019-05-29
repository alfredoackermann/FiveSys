using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class MarcaRepository : BaseCrudRepository<Marca>, IMarcaRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public MarcaRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override IDictionary<string, string> RecuperarDropDown()
        {
            return context.Set<Marca>()
                .AsNoTracking()
                .OrderBy(x => x.Descricao)
                .ToDictionary(x => x.Id, x => x.Descricao);
        }

        bool IMarcaRepository.VerificaDuplicado(string descricao)
        {
            return context.Set<Marca>()
                .Count(x => x.Descricao == descricao) > 0;
        }
    }
}
