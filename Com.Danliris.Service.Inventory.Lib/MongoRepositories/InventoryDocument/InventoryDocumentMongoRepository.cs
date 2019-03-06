using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Inventory.Lib.MongoModels;
using MongoDB.Driver;

namespace Com.Danliris.Service.Inventory.Lib.MongoRepositories.InventoryDocument
{
    public class InventoryDocumentMongoRepository : IInventoryDocumentMongoRepository
    {
        private readonly IMongoDbContext _context;

        public InventoryDocumentMongoRepository(IMongoDbContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<InventoryDocumentMongo>> GetByDate(DateTimeOffset date)
        {
            var filter = Builders<InventoryDocumentMongo>.Filter.Gte(_ => _._createdDate, date);

            return await _context
                           .InventoryDocuments
                           .Find(filter)
                           .ToListAsync();
        }
    }
}
