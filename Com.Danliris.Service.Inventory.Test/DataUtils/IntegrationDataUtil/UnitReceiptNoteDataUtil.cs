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
    public static class UnitReceiptNoteDataUtil
    {
        public static UnitReceiptNoteViewModel GetUnitReceiptNote(HttpClientTestService client)
        {
            var response = client.GetAsync($"{APIEndpoint.Purchasing}/unit-receipt-notes-basic?page=1&size=1").Result;
            response.EnsureSuccessStatusCode();

            var data = response.Content.ReadAsStringAsync();
            Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(data.Result.ToString());

            List<UnitReceiptNoteViewModel> list = JsonConvert.DeserializeObject<List<UnitReceiptNoteViewModel>>(result["data"].ToString());
            //List<UnitReceiptNoteViewModel.Item> item =  
            UnitReceiptNoteViewModel UnitReceiptNote = list.First();

            return UnitReceiptNote;
        }
    }


}
