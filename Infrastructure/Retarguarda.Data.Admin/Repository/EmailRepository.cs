using System.Collections.Generic;
using System.Linq;
using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class EmailRepository : BaseRepository<Email, string>, IEmailRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public EmailRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        void IEmailRepository.Remover(Email email)
        {
            base.Remover(email);
        }

        void IEmailRepository.RemoverPorId(string id)
        {
            base.Remover(base.RecuperarPorId(id));
        }

        void IEmailRepository.RemoverTodos(string metadataId)
        {
            dbContext.Set<Email>()
                .Where(x => x.MetadataId == metadataId)
                .ToList()
                .ForEach(x => base.Remover(x));
        }

        void IEmailRepository.Inserir(Email email)
        {
            base.Inserir(email);
        }

        void IEmailRepository.Atualizar(Email email)
        {
            base.Atualizar(email);
        }

        IEnumerable<Email> IEmailRepository.RecuperarTodos(string metadataId)
        {
            return dbContext.Set<Email>()
                .Where(x => x.MetadataId == metadataId)
                .ToList();
        }
    }
}
