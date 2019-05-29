using System.Diagnostics.CodeAnalysis;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        void IEntityTypeConfiguration<Endereco>.Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Localizacao).HasMaxLength(100).IsRequired();

            builder.Property(x => x.Numero);

            builder.Property(x => x.Bairro).HasMaxLength(100).IsRequired();

            builder.Property(x => x.Cidade).HasMaxLength(100).IsRequired();

            builder.Property(x => x.Uf);

            builder.Property(x => x.Cep).HasMaxLength(9);

            builder.Property(x => x.Complemento).HasMaxLength(100);

            builder.Property(x => x.CodigoMunicipio);

            builder.Property(x => x.Ibge).HasMaxLength(100);

            builder.Property(x => x.MetadataId).HasMaxLength(36);
        }
    }
}
