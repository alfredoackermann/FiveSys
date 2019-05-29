using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using System.Linq;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class PessoaJuridicaFornecedorRepository : FornecedorRepository, IPessoaJuridicaFornecedorRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public PessoaJuridicaFornecedorRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        bool IFornecedorRepository.VerificarDuplicado(string nome, string identificador, string id)
        {
            var query = context.Set<PessoaJuridicaFornecedor>() as IQueryable<PessoaJuridicaFornecedor>;

            if (!string.IsNullOrEmpty(id))
                query = query.Where(x => x.Id != id);

            return query.Count(x => x.Nome == nome || (x.Cnpj == identificador && x.Cnpj != null)) > 0;
        }
    }
}
