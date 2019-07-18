using Com.Danliris.Service.Inventory.Lib.Models.DataIntegrationLog;
using Com.Danliris.Service.Inventory.Lib.Models.InventoryModel;
using Com.Danliris.Service.Inventory.Lib.MongoModels;
using Com.Danliris.Service.Inventory.Lib.MongoRepositories.InventoryDocument;
using Com.Danliris.Service.Inventory.Lib.Services.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Inventory.Lib.IntegrationServices
{
    public class InventoryDocumentIntegrationService : IInventoryDocumentIntegrationService
    {
        private const string _remark = "InventoryDocument";
        private readonly InventoryDbContext _dbContext;
        private readonly DbSet<InventoryIntegrationLogModel> _integrationLogSet;
        private readonly DbSet<InventorySummary> _inventorySummarySet;
        private readonly IInventoryDocumentMongoRepository _mongoRepository;
        private readonly IServiceProvider _serviceProvider;

        public InventoryDocumentIntegrationService(InventoryDbContext dbContext, IInventoryDocumentMongoRepository mongoRepository, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _integrationLogSet = dbContext.Set<InventoryIntegrationLogModel>();
            _inventorySummarySet = dbContext.Set<InventorySummary>();
            _mongoRepository = mongoRepository;
            _serviceProvider = serviceProvider;
        }

        public async Task<int> IntegrateData()
        {
            var lastIntegrationLog = _integrationLogSet.FirstOrDefault(f => f.Remark.Equals(_remark));

            if (lastIntegrationLog != null)
            {
                var mongoInventoryDocuments = await _mongoRepository.GetByDate(lastIntegrationLog.LastIntegrationDate);

                var extractedDocuments = Transform(mongoInventoryDocuments);

                foreach (var inventoryDocument in extractedDocuments)
                {
                    var storageFromSummary = _inventorySummarySet.Select(s => new { s.StorageId, s.StorageCode }).FirstOrDefault(f => f.StorageCode.Equals(inventoryDocument.StorageCode));
                    inventoryDocument.StorageId = storageFromSummary == null ? 0 : storageFromSummary.StorageId;

                    foreach (var item in inventoryDocument.Items)
                    {
                        var productFromSummary = _inventorySummarySet.Select(s => new { s.ProductId, s.ProductCode }).FirstOrDefault(f => f.ProductCode.Equals(item.ProductCode));
                        var uomFromSummary = _inventorySummarySet.Select(s => new { s.UomUnit, s.UomId }).FirstOrDefault(f => f.UomUnit.Equals(item.UomUnit));

                        item.ProductId = productFromSummary == null ? 0 : productFromSummary.ProductId;
                        item.UomId = uomFromSummary == null ? 0 : uomFromSummary.UomId;
                    }

                    var inventoryDocumentFacade = _serviceProvider.GetService<IInventoryDocumentService>();

                    await inventoryDocumentFacade.Create(inventoryDocument);
                }

                lastIntegrationLog.LastIntegrationDate = DateTimeOffset.Now;
                _integrationLogSet.Update(lastIntegrationLog);

                await _dbContext.SaveChangesAsync();
                return extractedDocuments.Count;

            }


            return 0;
        }

        private List<InventoryDocument> Transform(IEnumerable<InventoryDocumentMongo> extractedData)
        {
            return extractedData.Select(mongoInventoryMovement => new InventoryDocument(mongoInventoryMovement)).ToList();
        }

        
    }
}
