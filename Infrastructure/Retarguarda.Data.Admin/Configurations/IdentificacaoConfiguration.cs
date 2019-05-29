using System.Diagnostics.CodeAnalysis;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class IdentificacaoConfiguration : IEntityTypeConfiguration<Identificacao>
    {
        void IEntityTypeConfiguration<Identificacao>.Configure(EntityTypeBuilder<Identificacao> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.MetadataId).HasMaxLength(36);

            builder.Property(x => x.TipoIdentificacaoId).HasMaxLength(36);

            builder.Property(x => x.Descricao).HasMaxLength(100);

            builder.HasOne(x => x.TipoIdentificacao)
                .WithMany(x => x.Identificacoes)
                .HasForeignKey(x => x.TipoIdentificacaoId);
        }
    }
}
