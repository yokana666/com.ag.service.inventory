using Com.Danliris.Service.Inventory.Lib.Models.FPReturnInvToPurchasingModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.Configs.FPReturnInvToPurchasingConfig
{
    public class FPReturnInvToPurchasingConfig : IEntityTypeConfiguration<FPReturnInvToPurchasing>
    {
        public void Configure(EntityTypeBuilder<FPReturnInvToPurchasing> builder)
        {
            builder.Property(p => p.No).HasMaxLength(255);
            builder.Property(p => p.UnitName).HasMaxLength(255);
            builder.Property(p => p.SupplierId).HasMaxLength(255);
            builder.Property(p => p.SupplierCode).HasMaxLength(255);
            builder.Property(p => p.SupplierName).HasMaxLength(255);

            builder
                .HasMany(h => h.FPReturnInvToPurchasingDetails)
                .WithOne(w => w.FPReturnInvToPurchasing)
                .HasForeignKey(f => f.FPReturnInvToPurchasingId)
                .IsRequired();
        }
    }
}
