using Com.Moonlay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Inventory.Lib.Models;
using Com.Danliris.Service.Inventory.Lib.Configs;
using Com.Danliris.Service.Inventory.Lib.Configs.MaterialDistributionNoteConfig;
using Com.Danliris.Service.Inventory.Lib.Models.MaterialDistributionNoteModel;
using Com.Danliris.Service.Inventory.Lib.Models.StockTransferNoteModel;
using Com.Danliris.Service.Inventory.Lib.Configs.StockTransferNoteConfig;
using Com.Danliris.Service.Inventory.Lib.Configs.MaterialsRequestNoteConfig;
using Com.Danliris.Service.Inventory.Lib.Models.MaterialsRequestNoteModel;

namespace Com.Danliris.Service.Inventory.Lib
{
    public class InventoryDbContext : BaseDbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public DbSet<MaterialsRequestNote> MaterialsRequestNotes { get; set; }
        public DbSet<MaterialsRequestNote_Item> MaterialsRequestNote_Items { get; set; }
        public DbSet<FpReturProInvDocsDetails> fpRegradingResultDocsDetails { get; set; }
        public DbSet<FpReturProInvDocs> fpRegradingResultDocs { get; set; }
        public DbSet<MaterialDistributionNote> MaterialDistributionNotes { get; set; }
        public DbSet<MaterialDistributionNoteItem> MaterialDistributionNoteItems { get; set; }
        public DbSet<MaterialDistributionNoteDetail> MaterialDistributionNoteDetails { get; set; }
        public DbSet<StockTransferNote> StockTransferNotes { get; set; }
        public DbSet<StockTransferNote_Item> StockTransferNoteItems { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MaterialsRequestNoteConfig());
            modelBuilder.ApplyConfiguration(new MaterialsRequestNote_ItemConfig());
            modelBuilder.ApplyConfiguration(new FpReturProInvDocsDetailsConfig());
            modelBuilder.ApplyConfiguration(new FpReturProInvDocsConfig());
            modelBuilder.ApplyConfiguration(new MaterialDistributionNoteConfig());
            modelBuilder.ApplyConfiguration(new MaterialDistributionNoteItemConfig());
            modelBuilder.ApplyConfiguration(new MaterialDistributionNoteDetailConfig());
            modelBuilder.ApplyConfiguration(new StockTransferNoteConfig());
            modelBuilder.ApplyConfiguration(new StockTransferNoteItemConfig());
        }
    }
}