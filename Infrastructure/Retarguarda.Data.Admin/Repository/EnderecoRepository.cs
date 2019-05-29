using System.Collections.Generic;
using System.Linq;
using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class EnderecoRepository : BaseRepository<Endereco, string>, IEnderecoRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public EnderecoRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        void IEnderecoRepository.Remover(Endereco endereco)
        {
            base.Remover(endereco);
        }

        void IEnderecoRepository.RemoverPorId(string id)
        {
            base.Remover(base.RecuperarPorId(id));
        }

        void IEnderecoRepository.RemoverTodos(string metadataId)
        {
            dbContext.Set<Endereco>()
                .Where(x => x.MetadataId == metadataId)
                .ToList()
                .ForEach(x => base.Remover(x));
        }

        void IEnderecoRepository.Inserir(Endereco endereco)
        {
            base.Inserir(endereco);
        }

        void IEnderecoRepository.Atualizar(Endereco endereco)
        {
            base.Atualizar(endereco);
        }

        IEnumerable<Endereco> IEnderecoRepository.RecuperarTodos(string metadataId)
        {
            return dbContext.Set<Endereco>()
                .Where(x => x.MetadataId == metadataId)
                .ToList();
        }
    }
}
