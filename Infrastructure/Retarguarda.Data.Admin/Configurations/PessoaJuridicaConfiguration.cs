using System.Diagnostics.CodeAnalysis;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class PessoaJuridicaConfiguration : IEntityTypeConfiguration<PessoaJuridica>
    {
        void IEntityTypeConfiguration<PessoaJuridica>.Configure(EntityTypeBuilder<PessoaJuridica> builder)
        {
            builder.Property(x => x.Cnpj).HasMaxLength(18);

            builder.Property(x => x.Titular).HasMaxLength(255);

            builder.Property(x => x.InscricaoEstadual).HasMaxLength(20);

            builder.Property(x => x.InscricaoMunicipal).HasMaxLength(20);

            builder.Property(x => x.Fundacao);

        }
    }
}
