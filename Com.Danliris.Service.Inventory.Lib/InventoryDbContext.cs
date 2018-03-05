using Com.Moonlay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Inventory.Lib.Models;
using Com.Danliris.Service.Inventory.Lib.Configs;

namespace Com.Danliris.Service.Inventory.Lib
{
    public class InventoryDbContext : BaseDbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public DbSet<MaterialsRequestNote> MaterialsRequestNotes { get; set; }
        public DbSet<MaterialsRequestNote_Item> MaterialsRequestNote_Items { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MaterialsRequestNoteConfig());
            modelBuilder.ApplyConfiguration(new MaterialsRequestNote_ItemConfig());

        }
    }
}