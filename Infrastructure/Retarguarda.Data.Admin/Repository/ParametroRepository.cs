using System.Collections.Generic;
using System.Linq;
using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class ParametroRepository: BaseCrudRepository<Parametro>, IParametroRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public ParametroRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override IDictionary<string, string> RecuperarDropDown()
        {
            return context.Set<Parametro>()
                    .AsNoTracking()
                    .OrderBy(x => x.Nome)
                    .ToDictionary(x => x.Id, x => x.Descricao);
        }

        bool IParametroRepository.VerificarDuplicado(string nome)
        {
            return context.Set<Parametro>()
                .Count(x => x.Nome == nome) > 0;
        }

        Parametro IParametroRepository.RecuperarPorNome(string nome)
        {
            return context.Set<Parametro>()
                .SingleOrDefault(x => x.Nome == nome);
        }
    }
}
