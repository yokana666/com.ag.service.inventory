using Com.Danliris.Service.Inventory.Lib.MongoModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Com.Danliris.Service.Inventory.Lib
{
    public class MongoDbMigrationContext : IMongoDbContext
    {
        private readonly IMongoDatabase _db;

        public MongoDbMigrationContext(IOptions<MongoDbSettings> options, IMongoClient client)
        {
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<InventoryDocumentMongo> InventoryDocuments => _db.GetCollection<InventoryDocumentMongo>("inventory-documents");
    }
}
