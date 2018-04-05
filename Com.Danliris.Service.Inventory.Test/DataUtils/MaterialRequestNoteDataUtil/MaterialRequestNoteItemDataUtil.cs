using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.Models.MaterialsRequestNoteModel;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.Test.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Com.Danliris.Service.Inventory.Test.DataUtils.MaterialRequestNoteDataUtil
{
    public class MaterialRequestNoteItemDataUtil
    {
        private readonly HttpClientService client;

        public MaterialRequestNoteItemDataUtil(HttpClientService client)
        {
            this.client = client;
        }

        public MaterialsRequestNote_Item GetNewData()
        {
            #region SPP
            var responseSPP = this.client.GetAsync($"{APIEndpoint.Production}sales/production-orders?page=1&size=1").Result;
            responseSPP.EnsureSuccessStatusCode();

            var dataSPP = responseSPP.Content.ReadAsStringAsync();
            Dictionary<string, object> resultSPP = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataSPP.Result.ToString());

            List<ProductionOrderViewModel> listSPP = JsonConvert.DeserializeObject<List<ProductionOrderViewModel>>(resultSPP["data"].ToString());
            ProductionOrderViewModel productionOrder = listSPP.First();
            #endregion SPP

            #region Product
            var responseProduct = this.client.GetAsync($"{APIEndpoint.Core}/master/products?page=1&size=1").Result;
            responseProduct.EnsureSuccessStatusCode();

            var dataProduct = responseProduct.Content.ReadAsStringAsync();
            Dictionary<string, object> resultProduct = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataProduct.Result.ToString());

            List<ProductViewModel> listProduct = JsonConvert.DeserializeObject<List<ProductViewModel>>(resultProduct["data"].ToString());
            ProductViewModel product = listProduct.First();
            #endregion Product

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
