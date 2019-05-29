using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using System.Linq;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class PessoaFisicaFornecedorRepository : FornecedorRepository, IPessoaFisicaFornecedorRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public PessoaFisicaFornecedorRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        bool IFornecedorRepository.VerificarDuplicado(string nome, string identificador, string id)
        {
            var query = context.Set<PessoaFisicaFornecedor>() as IQueryable<PessoaFisicaFornecedor>;

            if (!string.IsNullOrEmpty(id))
                query = query.Where(x => x.Id != id);

            return query.Count(x => x.Nome == nome || (x.Cpf == identificador && x.Cpf != null)) > 0;
        }
    }
}
