using Com.Moonlay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Inventory.Lib.Models;
using Com.Danliris.Service.Inventory.Lib.Configs;
using Com.Danliris.Service.Inventory.Lib.Configs.MaterialDistributionNoteConfig;
using Com.Danliris.Service.Inventory.Lib.Models.MaterialDistributionNoteModel;

namespace Com.Danliris.Service.Inventory.Lib
{
    public class InventoryDbContext : BaseDbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public DbSet<MaterialsRequestNote> MaterialsRequestNotes { get; set; }
        public DbSet<MaterialsRequestNote_Item> MaterialsRequestNote_Items { get; set; }
        public DbSet<MaterialDistributionNote> MaterialDistributionNotes { get; set; }
        public DbSet<MaterialDistributionNoteItem> MaterialDistributionNoteItems { get; set; }
        public DbSet<MaterialDistributionNoteDetail> MaterialDistributionNoteDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MaterialsRequestNoteConfig());
            modelBuilder.ApplyConfiguration(new MaterialsRequestNote_ItemConfig());
            modelBuilder.ApplyConfiguration(new MaterialDistributionNoteConfig());
            modelBuilder.ApplyConfiguration(new MaterialDistributionNoteItemConfig());
            modelBuilder.ApplyConfiguration(new MaterialDistributionNoteDetailConfig());
        }
    }
}