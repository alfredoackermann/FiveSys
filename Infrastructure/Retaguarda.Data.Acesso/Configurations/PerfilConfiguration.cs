using System.Diagnostics.CodeAnalysis;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiveSys.Retaguarda.Infrastructure.Data.Acesso.Configurations
{
    [ExcludeFromCodeCoverage]
    public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
    {
        void IEntityTypeConfiguration<Perfil>.Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Descricao).HasMaxLength(100).IsRequired();
        }
    }
}
