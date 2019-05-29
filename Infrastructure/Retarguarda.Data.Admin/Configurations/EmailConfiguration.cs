using System.Diagnostics.CodeAnalysis;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class EmailConfiguration : IEntityTypeConfiguration<Email>
    {
        void IEntityTypeConfiguration<Email>.Configure(EntityTypeBuilder<Email> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Endereco).HasMaxLength(255).IsRequired();

            builder.Property(x => x.MetadataId).HasMaxLength(36);
        }
    }
}
