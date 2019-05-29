using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class LojaConfiguration : IEntityTypeConfiguration<Loja>
    {
        void IEntityTypeConfiguration<Loja>.Configure(EntityTypeBuilder<Loja> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.RazaoSocial).HasMaxLength(100).IsRequired();
            builder.Property(x => x.NomeFantasia).HasMaxLength(100).IsRequired();
            builder.Property(x => x.RamoId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.Cnpj).IsRequired();
            builder.Property(x => x.Titular).IsRequired();
            builder.Property(x => x.InscricaoEstadual).IsRequired();
            builder.Property(x => x.InscricaoMunicipal).IsRequired();
            builder.Property(x => x.Crt).IsRequired();

            builder.Property(x => x.Cadastro);
            builder.Property(x => x.Responsavel).HasMaxLength(100);
            builder.Property(x => x.Cnae).HasMaxLength(30);
            builder.Property(x => x.SeriEnf);
            builder.Property(x => x.SequenciaLnf);
            builder.Property(x => x.Numero);
            builder.Property(x => x.SerieCte);
            builder.Property(x => x.SequencialCte);
            builder.Property(x => x.MicroempresaEstadual);
            builder.Property(x => x.ContribuinteIpi);
            builder.Property(x => x.OptanteSimples);
            builder.Property(x => x.SubstitutoTributario);
            builder.Property(x => x.OptanteSuperSimples);
            builder.Property(x => x.PermiteCreditoIcms);
            builder.Property(x => x.TaxaIpi);
            builder.Property(x => x.TaxaPis);
            builder.Property(x => x.SimplesNacional);
            builder.Property(x => x.Taxacofins);
            builder.Property(x => x.Fundacao);

            builder.Ignore(x => x.Telefones);

            builder.Ignore(x => x.Enderecos);

            builder.Ignore(x => x.Emails);
        }
    }
}
