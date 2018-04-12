using Com.Danliris.Service.Inventory.Lib;
using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.Models.MaterialsRequestNoteModel;
using Com.Danliris.Service.Inventory.Lib.Services.MaterialsRequestNoteServices;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.Lib.ViewModels.MaterialsRequestNoteViewModel;
using Com.Danliris.Service.Inventory.Test.Helpers;
using Com.Danliris.Service.Inventory.Test.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Inventory.Test.DataUtils.MaterialRequestNoteDataUtil
{
    public class MaterialRequestNoteDataUtil : BasicDataUtil<InventoryDbContext, MaterialsRequestNoteService, MaterialsRequestNote>, IEmptyData<MaterialsRequestNoteViewModel>
    {
        private readonly HttpClientService client;
        private readonly MaterialRequestNoteItemDataUtil materialRequestNoteItemDataUtil;

        public MaterialRequestNoteDataUtil(InventoryDbContext dbContext, MaterialsRequestNoteService service, HttpClientService client, MaterialRequestNoteItemDataUtil materialRequestNoteItemDataUtil) : base(dbContext, service)
        {
            this.client = client;
            this.materialRequestNoteItemDataUtil = materialRequestNoteItemDataUtil;
        }

        public MaterialsRequestNoteViewModel GetEmptyData()
        {
            MaterialsRequestNoteViewModel Data = new MaterialsRequestNoteViewModel();

            Data.RequestType = string.Empty;
            Data.Unit = new UnitViewModel();
            Data.Remark = string.Empty;
            Data.MaterialsRequestNote_Items = new List<MaterialsRequestNote_ItemViewModel> { new MaterialsRequestNote_ItemViewModel() };

            return Data;
        }

        public override MaterialsRequestNote GetNewData()
        {
            try
            {
                #region Unit
                var response = this.client.GetAsync($"{APIEndpoint.Core}master/units").Result;

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("TEST JACKY");
                    var ex = ErrorHelper.CreateExceptionFromResponseErrors(response);
                    Console.WriteLine(ex.Data.Values);
                }

                response.EnsureSuccessStatusCode();

                var data = response.Content.ReadAsStringAsync();
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(data.Result.ToString());

                List<UnitViewModel> list = JsonConvert.DeserializeObject<List<UnitViewModel>>(result["data"].ToString());
                UnitViewModel fp = list.First(p => p.name.Equals("FINISHING"));
                #endregion Unit

                MaterialsRequestNote TestData = new MaterialsRequestNote
                {
                    UnitId = fp._id,
                    UnitCode = fp.code,
                    UnitName = fp.name,
                    Remark = "",
                    RequestType = "AWAL",
                    IsDistributed = false,
                    IsCompleted = false,
                    MaterialsRequestNote_Items = new List<MaterialsRequestNote_Item> { materialRequestNoteItemDataUtil.GetNewData() }
                };

                return TestData;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public override async Task<MaterialsRequestNote> GetTestData()
        {
            MaterialsRequestNote Data = GetNewData();
            this.Service.Token = HttpClientService.Token;
            await this.Service.CreateModel(Data);
            return Data;
        }
    }
}
