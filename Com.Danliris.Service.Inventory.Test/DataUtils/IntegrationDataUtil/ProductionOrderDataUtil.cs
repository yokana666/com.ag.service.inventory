using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.Test.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Inventory.Test.DataUtils.IntegrationDataUtil
{
    public static class ProductionOrderDataUtil
    {
        public static ProductionOrderViewModel GetProductionOrder(HttpClientTestService client)
        {
            var responseSPP = client.GetAsync($"{APIEndpoint.Production}sales/production-orders?page=1&size=1").Result;
            responseSPP.EnsureSuccessStatusCode();

            var dataSPP = responseSPP.Content.ReadAsStringAsync();
            Dictionary<string, object> resultSPP = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataSPP.Result.ToString());

            List<ProductionOrderViewModel> listSPP = JsonConvert.DeserializeObject<List<ProductionOrderViewModel>>(resultSPP["data"].ToString());
            ProductionOrderViewModel productionOrder = listSPP.First();

            return productionOrder;
        }
    }
}
