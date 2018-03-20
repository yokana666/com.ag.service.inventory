using Com.Danliris.Service.Inventory.Lib.Models.MaterialsRequestNoteModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Inventory.Lib.Configs.MaterialsRequestNoteConfig
{
    public class MaterialsRequestNote_ItemConfig : IEntityTypeConfiguration<MaterialsRequestNote_Item>
    {
        public void Configure(EntityTypeBuilder<MaterialsRequestNote_Item> builder)
        {
            builder.Property(p => p.Code).HasMaxLength(100);
            builder.Property(p => p.ProductionOrderId).HasMaxLength(100);
            builder.Property(p => p.ProductionOrderNo).HasMaxLength(100);
            builder.Property(p => p.OrderTypeId).HasMaxLength(100);
            builder.Property(p => p.OrderTypeCode).HasMaxLength(100);
            builder.Property(p => p.OrderTypeName).HasMaxLength(100);
            builder.Property(p => p.ProductId).HasMaxLength(100);
            builder.Property(p => p.ProductCode).HasMaxLength(100);
            builder.Property(p => p.ProductName).HasMaxLength(100);
            builder.Property(p => p.Grade).HasMaxLength(500);
        }
    }
}
