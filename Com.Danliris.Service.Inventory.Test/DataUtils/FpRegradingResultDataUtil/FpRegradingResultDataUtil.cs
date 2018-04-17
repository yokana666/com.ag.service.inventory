using Com.Danliris.Service.Inventory.Lib;
using Com.Danliris.Service.Inventory.Lib.Models;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.Test.Helpers;
using Com.Danliris.Service.Inventory.Test.Interfaces;
using Com.Danliris.Service.Inventory.Lib.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Inventory.Test.DataUtils.FpRegradingResultDataUtil
{
    public class FpRegradingResultDataUtil : BasicDataUtil<InventoryDbContext, FpRegradingResultDocsService, FpRegradingResultDocs>, IEmptyData<FpRegradingResultDocsViewModel>
    {
        private readonly Helpers.HttpClientService client;
        private readonly FpRegradingResultDetailsDataUtil fpRegradingResultDetailsDataUtil;
        public FpRegradingResultDataUtil(InventoryDbContext dbContext, FpRegradingResultDocsService service, Helpers.HttpClientService client, FpRegradingResultDetailsDataUtil fpRegradingResultDetailsDataUtil) : base(dbContext, service)
        {
            this.client = client;
            this.fpRegradingResultDetailsDataUtil = fpRegradingResultDetailsDataUtil;
        }
        public FpRegradingResultDocsViewModel GetEmptyData()
        {
            FpRegradingResultDocsViewModel Data = new FpRegradingResultDocsViewModel();

            Data.Bon = new FpRegradingResultDocsViewModel.noBon();
            Data.Machine = new FpRegradingResultDocsViewModel.machine();
            Data.Product = new FpRegradingResultDocsViewModel.product();
            Data.Supplier = new FpRegradingResultDocsViewModel.supplier();
            Data.Details = new List<FpRegradingResultDetailsDocsViewModel> { new FpRegradingResultDetailsDocsViewModel() };

            Data.Shift = string.Empty;
            Data.TotalLength = 0;
            Data.OriginalGrade = string.Empty;
            Data.Remark = string.Empty;

            return Data;
        }

        public override FpRegradingResultDocs GetNewData()
        {
            FpRegradingResultDocs TestData = new FpRegradingResultDocs
            {
          
            };
        
            return TestData;
    }

        public override async Task<FpRegradingResultDocs> GetTestData()
        {
            FpRegradingResultDocs Data = GetNewData();
            this.Service.Token = Helpers.HttpClientService.Token;
            await this.Service.CreateModel(Data);
            return Data;
        }
    }
}
