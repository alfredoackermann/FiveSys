using System.Collections.Generic;
using System.Linq;
using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class TipoIdentificacaoRepository : BaseCrudRepository<TipoIdentificacao>, ITipoIdentificacaoRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public TipoIdentificacaoRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override IDictionary<string, string> RecuperarDropDown()
        {
            return context.Set<TipoIdentificacao>()
                .AsNoTracking()
                .OrderBy(x => x.Descricao)
                .ToDictionary(x => x.Id, x => x.Descricao);
        }

        bool ITipoIdentificacaoRepository.VerificarDuplicado(string descricao)
        {
            return context.Set<TipoIdentificacao>()
                .Count(x => x.Descricao == descricao) > 0;
        }
    }
}
