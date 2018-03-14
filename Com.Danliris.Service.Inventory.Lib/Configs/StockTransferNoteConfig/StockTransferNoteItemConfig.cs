using Com.Danliris.Service.Inventory.Lib.Models.StockTransferNoteModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Inventory.Lib.Configs.StockTransferNoteConfig
{
    public class StockTransferNoteItemConfig : IEntityTypeConfiguration<StockTransferNote_Item>
    {
        public void Configure(EntityTypeBuilder<StockTransferNote_Item> builder)
        {
            builder.Property(p => p.ProductId).HasMaxLength(255);
            builder.Property(p => p.ProductCode).HasMaxLength(255);
            builder.Property(p => p.ProductName).HasMaxLength(255);
            builder.Property(p => p.UomUnit).HasMaxLength(255);
            builder.Property(p => p.UomId).HasMaxLength(255);
        }
    }
}
