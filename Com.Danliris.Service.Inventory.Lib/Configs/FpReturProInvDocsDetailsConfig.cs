using Com.Danliris.Service.Inventory.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.Configs
{
    class FpReturProInvDocsDetailsConfig : IEntityTypeConfiguration<FpReturProInvDocsDetails>
    {
        public void Configure(EntityTypeBuilder<FpReturProInvDocsDetails> builder)
        {
            builder.Property(p => p.Code).HasMaxLength(255);
        }
    }
}
