using Com.Danliris.Service.Inventory.Lib.ViewModels.InventoryViewModel;
using Com.Danliris.Service.Inventory.Test.DataUtils.InventoryDataUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Inventory.Test.Controllers.InventoryDocument
{
    [Collection("TestServerFixture Collection")]
    public class InventoryDocumentControllerTest
    {
        private const string MediaType = "application/json";
        private readonly string URI = "v1/inventory-documents";

        private TestServerFixture TestFixture { get; set; }

        private HttpClient Client
        {
            get { return this.TestFixture.Client; }
        }

        protected InventoryDocumentDataUtil DataUtil
        {
            get { return (InventoryDocumentDataUtil)this.TestFixture.Service.GetService(typeof(InventoryDocumentDataUtil)); }
        }

        public InventoryDocumentControllerTest(TestServerFixture fixture)
        {
            TestFixture = fixture;
        }

        [Fact]
        public async Task Should_Success_Get_All_Data()
        {
            var response = await this.Client.GetAsync(URI);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Should_Success_Get_Data_By_Id()
        {
            Lib.Models.InventoryModel.InventoryDocument model = await DataUtil.GetTestData("dev2");
            var response = await this.Client.GetAsync($"{URI}/{model.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Should_Success_Create_Data()
        {
            InventoryDocumentViewModel viewModel = DataUtil.GetNewDataViewModel();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(viewModel).ToString(), Encoding.UTF8, MediaType);
            httpContent.Headers.Add("x-timezone-offset", "0");
            var response = await this.Client.PostAsync(URI, httpContent);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        public async Task Should_Error_Create_Invalid_Data()
        {
            InventoryDocumentViewModel viewModel = DataUtil.GetNewDataViewModel();
            
            viewModel.date = DateTimeOffset.MinValue;
            viewModel.type = null;
            viewModel.referenceNo = null;
            viewModel.referenceType = null;
            viewModel.storageId = 0;
            var response = await this.Client.PostAsync(URI, new StringContent(JsonConvert.SerializeObject(viewModel).ToString(), Encoding.UTF8, MediaType));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        //[Fact]
        //public async Task Should_Error_Create_Null_Item_Data()
        //{
        //    InventoryDocumentViewModel viewModel = DataUtil.GetNewDataViewModel();
            
        //    viewModel.items = new List<InventoryDocumentItemViewModel> { null };
        //    var response = await this.Client.PostAsync(URI, new StringContent(JsonConvert.SerializeObject(viewModel).ToString(), Encoding.UTF8, MediaType));
        //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //}

        [Fact]
        public async Task Should_Error_Create_IN_Minus_Data()
        {
            InventoryDocumentViewModel viewModel = DataUtil.GetNewDataViewModel();
            viewModel.type = "IN";
            viewModel.items[0].quantity=-1;
            var response = await this.Client.PostAsync(URI, new StringContent(JsonConvert.SerializeObject(viewModel).ToString(), Encoding.UTF8, MediaType));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Should_Error_Create_ADJ_zero_Data()
        {
            InventoryDocumentViewModel viewModel = DataUtil.GetNewDataViewModel();
            viewModel.type = "ADJ";
            viewModel.items[0].quantity = 0;
            var response = await this.Client.PostAsync(URI, new StringContent(JsonConvert.SerializeObject(viewModel).ToString(), Encoding.UTF8, MediaType));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
