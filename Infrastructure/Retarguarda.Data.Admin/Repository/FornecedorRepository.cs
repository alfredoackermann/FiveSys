using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class FornecedorRepository : BaseCrudRepository<Fornecedor>, IFornecedorRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public FornecedorRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override IDictionary<string, string> RecuperarDropDown()
        {
            return dbContext.Set<Fornecedor>()
                 .AsNoTracking()
                 .OrderBy(x => x.NomeFantasia)
                 .ToDictionary(x => x.Id, x => x.NomeFantasia)
                 ;
        }

        string IFornecedorRepository.ProximoFornecedor()
        {
            return dbContext.Set<Fornecedor>()
                .Max(x => x.Numero + 1).ToString();
        }

        bool IFornecedorRepository.VerificarDuplicado(string Nome, string id)
        {
            var query = context.Set<Fornecedor>() as IQueryable<Fornecedor>;

            if (!string.IsNullOrEmpty(id))
                query = query.Where(x => x.Id != id);

            return query.Count(x => x.Nome == Nome) > 0;
        }

        bool IFornecedorRepository.VerificarDuplicado(string Nome, string identificador, string id)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<FornecedorDTO> IFornecedorRepository.Listar()
        {
            return (from Fornecedor in context.Set<Fornecedor>()
                    select new FornecedorDTO()
                    {
                        Id = Fornecedor.Id,
                        Nome = Fornecedor.Nome,
                        NomeFantasia = Fornecedor.NomeFantasia,
                        Cadastro = Fornecedor.Cadastro,
                        Numero = Fornecedor.Numero


                    }).ToList();
        }
    }
}
