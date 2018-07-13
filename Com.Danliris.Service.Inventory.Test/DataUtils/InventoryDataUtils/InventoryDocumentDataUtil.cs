using Com.Danliris.Service.Inventory.Lib.Facades.InventoryFacades;
using Com.Danliris.Service.Inventory.Lib.Models.InventoryModel;
using Com.Danliris.Service.Inventory.Lib.ViewModels.InventoryViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Inventory.Test.DataUtils.InventoryDataUtils
{
    public class InventoryDocumentDataUtil
    {
        private InventoryDocumentItemDataUtil inventoryDocumentItemDataUtil;
        private readonly InventoryDocumentFacade facade;

        public InventoryDocumentDataUtil(InventoryDocumentItemDataUtil inventoryDocumentItemDataUtil, InventoryDocumentFacade facade)
        {
            this.inventoryDocumentItemDataUtil = inventoryDocumentItemDataUtil;
            this.facade = facade;
        }

        public InventoryDocument GetNewData()
        {
            return new InventoryDocument
            {
                No = "No1",
                Date = DateTimeOffset.Now,
                StorageCode="test01",
                StorageId=2,
                StorageName="Test",
                ReferenceNo="Test001",
                ReferenceType="TestType",
                Type="IN",
                Remark = "Remark",
                Items = new List<InventoryDocumentItem> { inventoryDocumentItemDataUtil.GetNewData() }
            };
        }

        public InventoryDocumentViewModel GetNewDataViewModel()
        {
            return new InventoryDocumentViewModel
            {
                date = DateTimeOffset.Now,
                storageCode = "test01",
                storageId = "2",
                storageName = "Test",
                referenceNo = "Test001",
                referenceType = "TestType",
                type = "IN",
                remark = "Remark",
                items = new List<InventoryDocumentItemViewModel> { inventoryDocumentItemDataUtil.GetNewDataViewModel() }
            };
        }

        public async Task<InventoryDocument> GetTestData(string user)
        {
            InventoryDocument invDoc = GetNewData();

            await facade.Create(invDoc, user);

            return invDoc;
        }

        public async Task<InventoryDocument> GetTestDataOUT(string user)
        {
            InventoryDocument invDoc = GetNewData();
            invDoc.Type = "OUT";
            
            await facade.Create(invDoc, user);

            return invDoc;
        }
    }
}
