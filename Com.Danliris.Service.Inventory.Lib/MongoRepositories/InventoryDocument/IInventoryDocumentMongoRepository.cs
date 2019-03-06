using Com.Danliris.Service.Inventory.Lib.MongoModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Inventory.Lib.MongoRepositories.InventoryDocument
{
    public interface IInventoryDocumentMongoRepository
    {
        Task<IEnumerable<InventoryDocumentMongo>> GetByDate(DateTimeOffset date);
    }
}