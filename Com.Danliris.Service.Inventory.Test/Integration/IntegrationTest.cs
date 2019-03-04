using Com.Danliris.Service.Inventory.Lib.Models.InventoryModel;
using Com.Danliris.Service.Inventory.Lib.MongoModels;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Xunit;

namespace Com.Danliris.Service.Inventory.Test.Integration
{
    public class IntegrationTest
    {
        private static readonly InventoryDocumentItemMongo InventoryDocumentItem = new InventoryDocumentItemMongo()
        {
            productCode = "productCode",
            productId = new ObjectId(),
            productName = "productName",
            quantity = 1,
            remark = "remark",
            stockPlanning = 1,
            uom = "uom",
            uomId = new ObjectId(),
            _active = true,
            _createAgent = "agent",
            _createdBy = "created",
            _createdDate = DateTime.Now,
            _deleted = false,
            _id = new ObjectId(),
            _stamp = "stamp",
            _type = "type",
            _updateAgent = "agent",
            _updatedBy = "updated",
            _updatedDate = DateTime.Now,
            _version = "1.0"
        };

        private static readonly InventoryDocumentMongo InventoryDocument = new InventoryDocumentMongo()
        {
            code = "code",
            date = DateTime.Now,
            items = new List<InventoryDocumentItemMongo>() { InventoryDocumentItem },
            referenceNo = "reference",
            referenceType = "reference",
            remark = "remark",
            storageCode = "storage code",
            storageId = new ObjectId(),
            storageName = "storageName",
            type = "type",
            _active = true,
            _createAgent = "agent",
            _createdBy = "created",
            _createdDate = DateTime.Now,
            _deleted = false,
            _id = new ObjectId(),
            _stamp = "stamp",
            _type = "type",
            _updateAgent = "agent",
            _updatedBy = "updated",
            _updatedDate = DateTime.Now,
            _version = "1.0"
        };

        [Fact]
        public void Should_Succes_Transform()
        {
            var sqlDocument = new InventoryDocument(InventoryDocument);
            Assert.NotNull(sqlDocument);
        }
    }
}
