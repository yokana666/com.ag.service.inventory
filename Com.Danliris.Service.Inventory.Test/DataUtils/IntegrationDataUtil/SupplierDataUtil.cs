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
    public class SupplierDataUtil
    {
        public static SupplierViewModel GetSupplier(HttpClientTestService client)
        {
            var response = client.GetAsync($"{APIEndpoint.Core}/master/suppliers?page=1&size=1").Result;
            response.EnsureSuccessStatusCode();

            var data = response.Content.ReadAsStringAsync();
            Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(data.Result.ToString());

            List<SupplierViewModel> list = JsonConvert.DeserializeObject<List<SupplierViewModel>>(result["data"].ToString());
            SupplierViewModel supplier = list.First();

            return supplier;
        }
    }
}
