using System.Collections.Generic;
using System.Linq;
using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class TelefoneRepository : BaseRepository<Telefone, string>, ITelefoneRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public TelefoneRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        void ITelefoneRepository.Remover(Telefone telefone)
        {
            base.Remover(telefone);
        }

        void ITelefoneRepository.RemoverPorId(string id)
        {
            base.Remover(base.RecuperarPorId(id));
        }

        void ITelefoneRepository.RemoverTodos(string metadataId)
        {
            dbContext.Set<Telefone>()
                .Where(x => x.MetadataId == metadataId)
                .ToList()
                .ForEach(x => base.Remover(x));
        }

        void ITelefoneRepository.Inserir(Telefone telefone)
        {
            base.Inserir(telefone);
        }

        void ITelefoneRepository.Atualizar(Telefone telefone)
        {
            base.Atualizar(telefone);
        }

        IEnumerable<Telefone> ITelefoneRepository.RecuperarTodos(string metadataId)
        {
            return dbContext.Set<Telefone>()
                .Where(x => x.MetadataId == metadataId)
                .ToList();
        }
    }
}
