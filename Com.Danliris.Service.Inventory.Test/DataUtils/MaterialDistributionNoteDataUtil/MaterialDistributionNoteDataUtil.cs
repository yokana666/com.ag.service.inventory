using Com.Danliris.Service.Inventory.Lib;
using Com.Danliris.Service.Inventory.Lib.Models.MaterialDistributionNoteModel;
using Com.Danliris.Service.Inventory.Lib.Services.MaterialDistributionNoteService;
using Com.Danliris.Service.Inventory.Lib.ViewModels.MaterialDistributionNoteViewModel;
using Com.Danliris.Service.Inventory.Test.Helpers;
using Com.Danliris.Service.Inventory.Test.Interfaces;
using System.Collections.Generic;
using Com.Danliris.Service.Inventory.Lib.Helpers;
using Newtonsoft.Json;
using Com.Danliris.Service.Inventory.Test.DataUtils.IntegrationDataUtil;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Inventory.Test.DataUtils.MaterialDistributionNoteDataUtil
{
    public class MaterialDistributionNoteDataUtil : BasicDataUtil<InventoryDbContext, MaterialDistributionNoteService, MaterialDistributionNote>, IEmptyData<MaterialDistributionNoteViewModel>
    {
        private readonly HttpClientService client;
        private readonly MaterialDistributionNoteItemDataUtil materialDistributionNoteItemDataUtil;

        public MaterialDistributionNoteDataUtil(InventoryDbContext dbContext, MaterialDistributionNoteService service, HttpClientService client, MaterialDistributionNoteItemDataUtil materialDistributionNoteItemDataUtil) : base(dbContext, service)
        {
            this.client = client;
            this.materialDistributionNoteItemDataUtil = materialDistributionNoteItemDataUtil;
        }

        public MaterialDistributionNoteViewModel GetEmptyData()
        {
            MaterialDistributionNoteViewModel Data = new MaterialDistributionNoteViewModel();

            Data.Type = string.Empty;
            Data.Unit = new UnitViewModel();
            Data.MaterialDistributionNoteItems = new List<MaterialDistributionNoteItemViewModel> {
                    new MaterialDistributionNoteItemViewModel {
                        MaterialDistributionNoteDetails = new List<MaterialDistributionNoteDetailViewModel>() { new MaterialDistributionNoteDetailViewModel()
                    }
                }
            };

            return Data;
        }

        public override MaterialDistributionNote GetNewData()
        {
            UnitViewModel fp = UnitDataUtil.GetFinishingUnit(client);

            MaterialDistributionNote TestData = new MaterialDistributionNote
            {
                UnitId = fp._id,
                UnitCode = fp.code,
                UnitName = fp.name,
                Type = "PRODUKSI",
                IsApproved = false,
                IsDisposition = false,
                MaterialDistributionNoteItems = new List<MaterialDistributionNoteItem> { materialDistributionNoteItemDataUtil.GetNewData() }
            };

            return TestData;
        }


        public override async Task<MaterialDistributionNote> GetTestData()
        {
            MaterialDistributionNote Data = GetNewData();
            this.Service.Token = HttpClientService.Token;
            await this.Service.CreateModel(Data);
            return Data;
        }
    }
}
