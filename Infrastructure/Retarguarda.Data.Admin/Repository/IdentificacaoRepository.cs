using System.Collections.Generic;
using System.Linq;
using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class IdentificacaoRepository : BaseRepository<Identificacao, string>, IIdentificacaoRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public IdentificacaoRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        void IIdentificacaoRepository.Remover(Identificacao Identificacao)
        {
            base.Remover(Identificacao);
        }

        void IIdentificacaoRepository.RemoverPorId(string id)
        {
            base.Remover(base.RecuperarPorId(id));
        }

        void IIdentificacaoRepository.RemoverTodos(string metadataId)
        {
            dbContext.Set<Identificacao>()
                .Where(x => x.MetadataId == metadataId)
                .ToList()
                .ForEach(x => base.Remover(x));
        }

        void IIdentificacaoRepository.Inserir(Identificacao Identificacao)
        {
            base.Inserir(Identificacao);
        }

        void IIdentificacaoRepository.Atualizar(Identificacao Identificacao)
        {
            base.Atualizar(Identificacao);
        }

        IEnumerable<Identificacao> IIdentificacaoRepository.RecuperarTodos(string metadataId)
        {
            return dbContext.Set<Identificacao>()
                .Where(x => x.MetadataId == metadataId)
                .ToList();
        }
    }
}
