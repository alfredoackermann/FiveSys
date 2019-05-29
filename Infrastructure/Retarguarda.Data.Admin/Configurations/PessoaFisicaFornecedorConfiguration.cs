using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class PessoaFisicaFornecedorConfiguration : IEntityTypeConfiguration<PessoaFisicaFornecedor>
    {
        void IEntityTypeConfiguration<PessoaFisicaFornecedor>.Configure(EntityTypeBuilder<PessoaFisicaFornecedor> builder)
        {
            builder.Property(x => x.Cpf).HasMaxLength(14);

            builder.Property(x => x.Rg).HasMaxLength(15);

            builder.Property(x => x.OrgaoEmissor).HasMaxLength(3);

            builder.Property(x => x.Emissao);

            builder.Property(x => x.UfOrgaoEmissor);

            builder.Property(x => x.Profissao).HasMaxLength(100);

            builder.Property(x => x.Empresa).HasMaxLength(100);

            builder.Property(x => x.Nascimento);

            builder.Property(x => x.Filiacao).HasMaxLength(255);

            builder.Property(x => x.EstadoCivil);

            builder.Property(x => x.Conjuge).HasMaxLength(255);

            builder.Property(x => x.Sexo);
        }
    }
}
