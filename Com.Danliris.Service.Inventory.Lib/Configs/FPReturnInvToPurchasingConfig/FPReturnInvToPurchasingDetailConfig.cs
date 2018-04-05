using Com.Danliris.Service.Inventory.Lib.Models.FPReturnInvToPurchasingModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.Configs.FPReturnInvToPurchasingConfig
{
    public class FPReturnInvToPurchasingDetailConfig : IEntityTypeConfiguration<FPReturnInvToPurchasingDetail>
    {
        public void Configure(EntityTypeBuilder<FPReturnInvToPurchasingDetail> builder)
        {
            builder.Property(p => p.FPRegradingResultDocsCode).HasMaxLength(255);
            builder.Property(p => p.ProductId).HasMaxLength(255);
            builder.Property(p => p.ProductCode).HasMaxLength(255);
            builder.Property(p => p.ProductName).HasMaxLength(255);
            builder.Property(p => p.Description).HasMaxLength(2000);
        }
    }
}
