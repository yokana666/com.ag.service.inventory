using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.Models.MaterialsRequestNoteModel;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.Test.DataUtils.IntegrationDataUtil;
using Com.Danliris.Service.Inventory.Test.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Danliris.Service.Inventory.Test.DataUtils.MaterialRequestNoteDataUtil
{
    public class MaterialRequestNoteItemDataUtil
    {
        private readonly HttpClientTestService client;

        public MaterialRequestNoteItemDataUtil(HttpClientTestService client)
        {
            this.client = client;
        }

        public MaterialsRequestNote_Item GetNewData()
        {
            ProductionOrderViewModel productionOrder = ProductionOrderDataUtil.GetProductionOrder(client);
            ProductViewModel product = ProductDataUtil.GetProduct(client);
            
            return new MaterialsRequestNote_Item
            {
                ProductionOrderId = productionOrder._id,
                ProductionOrderNo = productionOrder.orderNo,
                ProductionOrderIsCompleted = false,
                OrderQuantity = (double)productionOrder.orderQuantity,
                OrderTypeId = productionOrder.orderType._id,
                OrderTypeCode = productionOrder.orderType.code,
                OrderTypeName = productionOrder.orderType.name,
                ProductId = product._id,
                ProductCode = product.code,
                ProductName = product.name,
                Grade = "A",
                Length = 5
            };
        }
    }
}
