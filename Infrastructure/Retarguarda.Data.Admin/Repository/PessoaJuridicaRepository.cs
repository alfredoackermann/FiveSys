using System.Linq;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class PessoaJuridicaRepository : ClienteRepository, IPessoaJuridicaRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public PessoaJuridicaRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        bool IClienteRepository.VerificarDuplicado(string nome, string identificador, string id)
        {
            var query = context.Set<PessoaJuridica>() as IQueryable<PessoaJuridica>;

            if (!string.IsNullOrEmpty(id))
                query = query.Where(x => x.Id != id);

            return query.Count(x => x.Nome == nome || (x.Cnpj == identificador && x.Cnpj != null)) > 0;
        }
    }
}
