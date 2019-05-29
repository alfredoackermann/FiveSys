using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        void IEntityTypeConfiguration<Cliente>.Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Nome).HasMaxLength(100).IsRequired();

            builder.Property(x => x.Cadastro);

            builder.Property(x => x.Regiao);

            builder.HasDiscriminator(x => x.TipoPessoa)
                .HasValue<PessoaFisica>((int)PessoaTipoEnum.Fisica)
                .HasValue<PessoaJuridica>((int)PessoaTipoEnum.Juridica);

            builder.Property(x => x.Referencias).HasColumnType("nvarchar(max)");

            builder.Property(x => x.CasaPropria);

            builder.Property(x => x.Renda);

            builder.Property(x => x.SituacaoCadastral);

            builder.Property(x => x.Limite);

            builder.Property(x => x.UltimaCompra);

            builder.Property(x => x.Pontos);

            builder.HasOne(x => x.TipoCliente)
               .WithMany(x => x.Clientes)
               .HasForeignKey(x => x.TipoClienteId);

            builder.Ignore(x => x.Telefones);

            builder.Ignore(x => x.Enderecos);

            builder.Ignore(x => x.Emails);

            builder.Ignore(x => x.Identificacoes);
        }
    }
}
