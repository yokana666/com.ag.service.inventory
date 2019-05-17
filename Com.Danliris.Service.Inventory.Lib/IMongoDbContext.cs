using Com.Danliris.Service.Inventory.Lib.MongoModels;
using MongoDB.Driver;

namespace Com.Danliris.Service.Inventory.Lib
{
    public interface IMongoDbContext
    {
        IMongoCollection<InventoryDocumentMongo> InventoryDocuments { get; }
    }
}
