using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class LojaRepository : BaseCrudRepository<Loja>, ILojaRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public LojaRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public string ProximaLoja()
        {   
            return dbContext.Set<Loja>()
                .Max(x => x.Numero + 1).ToString();
        }

        public override IDictionary<string, string> RecuperarDropDown()
        {
            return dbContext.Set<Loja>()
                .AsNoTracking()
                .OrderBy(x => x.NomeFantasia)
                .ToDictionary(x => x.Id, x => x.NomeFantasia)
                ;
        }

        IEnumerable<LojaDTO> ILojaRepository.Listar()
        {
            return (from loja in context.Set<Loja>()
                    join ramos in context.Set<Ramo>() on loja.RamoId equals ramos.Id into ramoDescricao
                    from ramos in ramoDescricao.DefaultIfEmpty()
                    select new LojaDTO()
                    {
                        Id = loja.Id,
                        NomeFantasia = loja.NomeFantasia,
                        RazaoSocial = loja.RazaoSocial,
                        Cnpj = loja.Cnpj,
                        Ramo = ramos.Descricao,
                        Responsavel = loja.Responsavel
                    }).ToList();
        }

        bool ILojaRepository.VerificarDuplicado(string RazaoSocial, string id)
        {
            var query = context.Set<Loja>() as IQueryable<Loja>;

            if (!string.IsNullOrEmpty(id))
                query = query.Where(x => x.Id != id);

            return query.Count(x => x.RazaoSocial == RazaoSocial) > 0;
        }
    }
}
