using Com.Danliris.Service.Inventory.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.Configs
{
    public class FpReturProInvDocsConfig : IEntityTypeConfiguration<FpReturProInvDocs>
    {
        public void Configure(EntityTypeBuilder<FpReturProInvDocs> builder)
        {
            builder.Property(p => p.Code).HasMaxLength(255);
            builder.Property(p => p.NoBonId).HasMaxLength(128);
            builder.Property(p => p.NoBon).HasMaxLength(255);
            builder.Property(p => p.SupplierId).HasMaxLength(128);
            builder.Property(p => p.SupplierName).HasMaxLength(255);
            builder
                .HasMany(h => h.Details)
                .WithOne(w => w.FpReturProInvDocs)
                .HasForeignKey(f => f.FpReturProInvDocsId)
                .IsRequired();
        }
    }
}
