using System.Diagnostics.CodeAnalysis;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        void IEntityTypeConfiguration<Usuario>.Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Nome).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Login).HasMaxLength(20).IsRequired();

            builder.Property(x => x.Status);
            builder.Property(x => x.Admissao);
            builder.Property(x => x.Rg).HasMaxLength(15);
            builder.Property(x => x.Cpf).HasMaxLength(11);
            builder.Property(x => x.Nascimento);
            builder.Property(x => x.Comissao);
            builder.Property(x => x.Observacao);
            builder.Property(x => x.ExpirarSenha);
            builder.Ignore(x => x.Email);

            builder.Property(x => x.PerfilId);
            builder.Property(x => x.Senha).HasMaxLength(255);
            builder.Property(x => x.SenhaTemporaria);
        }
    }
}
