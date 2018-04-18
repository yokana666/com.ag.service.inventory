using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.Models;
using Com.Danliris.Service.Inventory.Lib.Services;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.Test.DataUtils.IntegrationDataUtil;
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
            ProductViewModel product = ProductDataUtil.GetProduct(client);

            return new FpRegradingResultDocsDetails
            {
                ProductName = product.name,
                ProductId = product._id,
                ProductCode = product.code,
                Length = 100,
                Grade = "A",
                Remark = "test",
                Retur ="Ya",
                
            };
        }
    }
}
