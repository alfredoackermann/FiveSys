using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class FornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
    {
        void IEntityTypeConfiguration<Fornecedor>.Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Numero).IsRequired();
            builder.Property(x => x.TipoPessoa).IsRequired();
            builder.HasDiscriminator(x => x.TipoPessoa)
                .HasValue<PessoaFisicaFornecedor>((int)PessoaTipoEnum.Fisica)
                .HasValue<PessoaJuridicaFornecedor>((int)PessoaTipoEnum.Juridica);
            builder.Property(x => x.Nome).HasMaxLength(100).IsRequired();
            builder.Property(x => x.NomeFantasia).HasMaxLength(100);
            builder.Property(x => x.Cadastro).IsRequired();
            builder.Property(x => x.Cnae);
            builder.Property(x => x.HomePage).HasMaxLength(255);
            builder.Property(x => x.AssistenciaTecnica);
            builder.Property(x => x.Banco);
            builder.Property(x => x.Agencia);
            builder.Property(x => x.Conta);


            builder.Ignore(x => x.Telefones);
            builder.Ignore(x => x.Enderecos);
            builder.Ignore(x => x.Emails);
        }
    }
}
