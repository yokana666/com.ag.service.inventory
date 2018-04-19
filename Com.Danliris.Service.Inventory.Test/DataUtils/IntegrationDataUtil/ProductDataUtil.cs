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
    public static class ProductDataUtil
    {
        public static ProductViewModel GetProduct(HttpClientTestService client)
        {
            var responseProduct = client.GetAsync($"{APIEndpoint.Core}/master/products?page=1&size=1").Result;
            responseProduct.EnsureSuccessStatusCode();

            var dataProduct = responseProduct.Content.ReadAsStringAsync();
            Dictionary<string, object> resultProduct = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataProduct.Result.ToString());

            List<ProductViewModel> listProduct = JsonConvert.DeserializeObject<List<ProductViewModel>>(resultProduct["data"].ToString());
            ProductViewModel product = listProduct.First();

            return product;
        }
    }
}
