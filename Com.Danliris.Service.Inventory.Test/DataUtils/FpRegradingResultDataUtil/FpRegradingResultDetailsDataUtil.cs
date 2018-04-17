using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.Models;
using Com.Danliris.Service.Inventory.Lib.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Test.DataUtils.FpRegradingResultDataUtil
{
    public class FpRegradingResultDetailsDataUtil
    {
        private readonly Helpers.HttpClientService client;

        public FpRegradingResultDetailsDataUtil(Helpers.HttpClientService client)
        {
            this.client = client;
        }

        public FpRegradingResultDocsDetails GetNewData()
        {
            return new FpRegradingResultDocsDetails
            {
                ProductName = "testProduct",
                ProductId = "testProductId",
                ProductCode = "testProductCode",
                Length = 100,
                Grade = "A",
                Remark = "test",
            };
        }
    }
}
