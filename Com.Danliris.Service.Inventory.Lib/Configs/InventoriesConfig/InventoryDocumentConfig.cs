using Com.Danliris.Service.Inventory.Lib.Models.InventoryModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.Configs.InventoriesConfig
{
    public class InventoryDocumentConfig : IEntityTypeConfiguration<InventoryDocument>
    {
        public void Configure(EntityTypeBuilder<InventoryDocument> builder)
        {
            builder.Property(p => p.No).HasMaxLength(255);
            builder.Property(p => p.ReferenceNo).HasMaxLength(255);
            builder.Property(p => p.ReferenceType).HasMaxLength(255);
            builder.Property(p => p.StorageId).HasMaxLength(255);
            builder.Property(p => p.StorageCode).HasMaxLength(255);
            builder.Property(p => p.StorageName).HasMaxLength(255);

            builder
                .HasMany(h => h.Items)
                .WithOne(w => w.InventoryDocument)
                .HasForeignKey(f => f.InventoryDocumentId)
                .IsRequired();
        }
    }
}

