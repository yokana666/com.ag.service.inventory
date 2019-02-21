using Com.Danliris.Service.Inventory.Lib.Models.InventoryModel;
using Com.Danliris.Service.Inventory.Lib.ViewModels.InventoryViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Test.DataUtils.InventoryDataUtils
{
    public class InventoryDocumentItemDataUtil
    {
        public InventoryDocumentItem GetNewData() => new InventoryDocumentItem
        {
            ProductId = 1,
            ProductCode = "ProductCode",
            ProductName = "ProductName",
            Quantity = 10,
            UomId = 1,
            UomUnit = "Uom",
            StockPlanning=0,
            
        };
        public InventoryDocumentItemViewModel GetNewDataViewModel() => new InventoryDocumentItemViewModel
        {
            productId =  1,
            productCode = "ProductCode",
            productName = "ProductName",
            uomId = 1,
            uom = "Uom",
            quantity=10,
            stockPlanning=0,
            
        };
    }
}
