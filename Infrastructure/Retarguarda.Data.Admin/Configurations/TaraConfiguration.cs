using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class TaraConfiguration : IEntityTypeConfiguration<Tara>
    {
        public void Configure(EntityTypeBuilder<Tara> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Descricao).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Peso).IsRequired();
        }
    }
}
