using System.Diagnostics.CodeAnalysis;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class TipoClienteConfiguration : IEntityTypeConfiguration<TipoCliente>
    {
        void IEntityTypeConfiguration<TipoCliente>.Configure(EntityTypeBuilder<TipoCliente> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Descricao).HasMaxLength(100).IsRequired();
        }
    }
}
