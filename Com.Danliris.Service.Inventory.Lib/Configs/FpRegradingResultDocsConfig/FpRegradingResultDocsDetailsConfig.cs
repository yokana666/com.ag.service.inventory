using Com.Danliris.Service.Inventory.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.Configs
{
    class FpRegradingResultDocsDetailsConfig : IEntityTypeConfiguration<FpRegradingResultDocsDetails>
    {
        public void Configure(EntityTypeBuilder<FpRegradingResultDocsDetails> builder)
        {
            builder.Property(p => p.Code).HasMaxLength(255);
        }
    }
}
