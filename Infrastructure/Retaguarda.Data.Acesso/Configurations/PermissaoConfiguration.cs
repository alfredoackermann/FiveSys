using System.Diagnostics.CodeAnalysis;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiveSys.Retaguarda.Infrastructure.Data.Acesso.Configurations
{
    [ExcludeFromCodeCoverage]
    public class PermissaoConfiguration : IEntityTypeConfiguration<Permissao>
    {
        void IEntityTypeConfiguration<Permissao>.Configure(EntityTypeBuilder<Permissao> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Funcionalidade).IsRequired();

            builder.Property(x => x.Acoes).IsRequired();

            builder.HasOne(x => x.Perfil)
               .WithMany(x => x.Permissoes)
               .HasForeignKey(x => x.PerfilId);
        }
    }
}
