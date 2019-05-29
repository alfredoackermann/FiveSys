using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class PessoaJuridicaFornecedorConfiguration : IEntityTypeConfiguration<PessoaJuridicaFornecedor>
    {
        void IEntityTypeConfiguration<PessoaJuridicaFornecedor>.Configure(EntityTypeBuilder<PessoaJuridicaFornecedor> builder)
        {
            builder.Property(x => x.Cnpj).HasMaxLength(18);

            builder.Property(x => x.Titular).HasMaxLength(255);

            builder.Property(x => x.InscricaoEstadual).HasMaxLength(20);

            builder.Property(x => x.InscricaoMunicipal).HasMaxLength(20);

            builder.Property(x => x.Fundacao);
        }
    }
}
