using FileSys.Shared.Infrastructure.Data;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Repository
{
    public class ClienteRepository : BaseCrudRepository<Cliente>, IClienteRepository
    {
        private readonly RetaguardaAdminContext dbContext;

        public ClienteRepository(RetaguardaAdminContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override IDictionary<string, string> RecuperarDropDown()
        {
            return context.Set<Cliente>()
                .AsNoTracking()
                .OrderBy(x => x.Nome)
                .ToDictionary(x => x.Id, x => x.Nome);
        }

        IEnumerable<ClienteDTO> IClienteRepository.Listar()
        {
            return (from cliente in context.Set<Cliente>()
                    join tipo in context.Set<TipoCliente>() on cliente.TipoClienteId equals tipo.Id into tipoCliente
                    from tipo in tipoCliente.DefaultIfEmpty()
                    select new ClienteDTO()
                    {
                        Id = cliente.Id,
                        Nome = cliente.Nome,
                        Cadastro = cliente.Cadastro,
                        Regiao = cliente.Regiao,
                        TipoCliente = tipo.Descricao,
                        TipoPessoa = cliente.TipoPessoa,
                        Limite = cliente.Limite,
                        UltimaCompra = cliente.UltimaCompra,
                        Pontos = cliente.Pontos,
                    }).ToList();
        }

        public virtual bool VerificarDuplicado(string nome, string identificador, string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
