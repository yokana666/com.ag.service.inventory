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
            ProductId = "ProductId",
            ProductCode = "ProductCode",
            ProductName = "ProductName",
            Quantity = 10,
            UomId = "UomId",
            UomUnit = "Uom",
            StockPlanning=0,
            
        };
        public InventoryDocumentItemViewModel GetNewDataViewModel() => new InventoryDocumentItemViewModel
        {
            productId =  "ProductId",
            productCode = "ProductCode",
            productName = "ProductName",
            uomId = "UomId",
            uom = "Uom",
            quantity=10,
            stockPlanning=0,
            
        };
    }
}
