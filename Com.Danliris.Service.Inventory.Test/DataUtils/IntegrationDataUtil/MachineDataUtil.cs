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
    public static class MachineDataUtil
    {
        public static MachineViewModel GetMachine(HttpClientTestService client)
        {
            var response = client.GetAsync($"{APIEndpoint.Core}/master/machines?page=1&size=1").Result;
            response.EnsureSuccessStatusCode();

            var dataProduct = response.Content.ReadAsStringAsync();
            Dictionary<string, object> resultProduct = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataProduct.Result.ToString());

            List<MachineViewModel> listMachine = JsonConvert.DeserializeObject<List<MachineViewModel>>(resultProduct["data"].ToString());
            MachineViewModel machine = listMachine.First();

            return machine;
        }
    }
}
