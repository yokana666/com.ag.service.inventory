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
            builder.Property(p => p.Code).HasMaxLength(100);
            builder
                .HasMany(h => h.Details);
        }
    }
}
