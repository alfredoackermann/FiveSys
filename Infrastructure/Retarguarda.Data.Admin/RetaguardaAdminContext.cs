using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin
{
    [ExcludeFromCodeCoverage]
    public class RetaguardaAdminContext : DbContext
    {
        public RetaguardaAdminContext(DbContextOptions<RetaguardaAdminContext> options)
            : base(options)
        {
        }

        // Deixar em ordem alfabetica de entidade para ficar mais facil de procurar

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Identificacao> Identificacoes { get; set; }
        public DbSet<Industria> Industrias { get; set; }
        public DbSet<Loja> Lojas { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Ramo> Ramos { get; set; }
        public DbSet<Tara> Taras { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<TipoCliente> TiposCliente { get; set; }
        public DbSet<TipoIdentificacao> TiposIdentificacao { get; set; }
        public DbSet<TipoPreco> TiposPreco { get; set; }
        public DbSet<TipoProduto> TiposProduto { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Unidade> Unidades { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Deixar em ordem alfabetica de entidade para ficar mais facil de procurar

            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new EmailConfiguration());
            modelBuilder.ApplyConfiguration(new EnderecoConfiguration());
            modelBuilder.ApplyConfiguration(new FornecedorConfiguration());
            modelBuilder.ApplyConfiguration(new IdentificacaoConfiguration());
            modelBuilder.ApplyConfiguration(new IndustriaConfiguration());
            modelBuilder.ApplyConfiguration(new LojaConfiguration());
            modelBuilder.ApplyConfiguration(new MarcaConfiguration());
            modelBuilder.ApplyConfiguration(new ParametroConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaFisicaConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaFisicaFornecedorConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaJuridicaConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaJuridicaFornecedorConfiguration());
            modelBuilder.ApplyConfiguration(new RamoConfiguration());
            modelBuilder.ApplyConfiguration(new TaraConfiguration());
            modelBuilder.ApplyConfiguration(new TelefoneConfiguration());
            modelBuilder.ApplyConfiguration(new TipoClienteConfiguration());
            modelBuilder.ApplyConfiguration(new TipoIdentificacaoConfiguration());
            modelBuilder.ApplyConfiguration(new TipoPrecoConfiguration());
            modelBuilder.ApplyConfiguration(new TipoProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new UnidadeConfiguration());
        }
    }
}
