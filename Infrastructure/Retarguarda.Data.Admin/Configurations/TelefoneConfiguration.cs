using System.Diagnostics.CodeAnalysis;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class TelefoneConfiguration : IEntityTypeConfiguration<Telefone>
    {
        void IEntityTypeConfiguration<Telefone>.Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Tipo).HasMaxLength(50);

            builder.Property(x => x.Numero).HasMaxLength(15);

            builder.Property(x => x.MetadataId).HasMaxLength(36);
        }
    }
}
