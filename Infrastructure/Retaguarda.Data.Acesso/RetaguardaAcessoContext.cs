using System.Diagnostics.CodeAnalysis;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;
using FiveSys.Retaguarda.Infrastructure.Data.Acesso.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FiveSys.Retaguarda.Infrastructure.Data.Acesso
{
    [ExcludeFromCodeCoverage]
    public class RetaguardaAcessoContext : DbContext
    {
        public RetaguardaAcessoContext(DbContextOptions<RetaguardaAcessoContext> options)
            : base(options)
        {
        }

        public DbSet<Perfil> Perfis { get; set; }

        public DbSet<Permissao> Permissoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Deixar em ordem alfabetica de entidade para ficar mais facil de procurar

            modelBuilder.ApplyConfiguration(new PerfilConfiguration());
            modelBuilder.ApplyConfiguration(new PermissaoConfiguration());
        }
    }
}
