﻿using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Configurations
{
    [ExcludeFromCodeCoverage]
    public class UnidadeConfiguration : IEntityTypeConfiguration<Unidade>
    {
        void IEntityTypeConfiguration<Unidade>.Configure(EntityTypeBuilder<Unidade> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);

            builder.Property(x => x.Descricao).HasMaxLength(2).IsRequired();
        }
    }
}
