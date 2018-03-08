using Com.Danliris.Service.Inventory.Lib.Models.MaterialDistributionNoteModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.Configs.MaterialDistributionNoteConfig
{
    public class MaterialDistributionNoteItemConfig : IEntityTypeConfiguration<MaterialDistributionNoteItem>
    {
        public void Configure(EntityTypeBuilder<MaterialDistributionNoteItem> builder)
        {
            builder.Property(p => p.MaterialRequestNoteCode).HasMaxLength(100);

            builder
                .HasMany(h => h.MaterialDistributionNoteDetails)
                .WithOne(w => w.MaterialDistributionNoteItem)
                .HasForeignKey(f => f.MaterialDistributionNoteItemId)
                .IsRequired();
        }
    }
}
