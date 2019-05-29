using System.Linq;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class PessoaFisicaRepository : ClienteRepository, IPessoaFisicaRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public PessoaFisicaRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        bool IClienteRepository.VerificarDuplicado(string nome, string identificador, string id)
        {
            var query = context.Set<PessoaFisica>() as IQueryable<PessoaFisica>;

            if (!string.IsNullOrEmpty(id))
                query = query.Where(x => x.Id != id);

            return query.Count(x => x.Nome == nome || (x.Cpf == identificador && x.Cpf != null)) > 0;
        }
    }
}
