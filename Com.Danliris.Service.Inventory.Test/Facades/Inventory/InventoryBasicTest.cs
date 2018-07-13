using Com.Danliris.Service.Inventory.Lib.Facades.InventoryFacades;
using Com.Danliris.Service.Inventory.Lib.Models.InventoryModel;
using Com.Danliris.Service.Inventory.Lib.Services;
using Com.Danliris.Service.Inventory.Test.DataUtils.InventoryDataUtils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Inventory.Test.Facades.Inventory
{
    [Collection("ServiceProviderFixture Collection")]
    public class InventoryBasicTest
    {
        private IServiceProvider ServiceProvider { get; set; }

        public InventoryBasicTest(ServiceProviderFixture fixture)
        {
            ServiceProvider = fixture.ServiceProvider;

            IdentityService identityService = (IdentityService)ServiceProvider.GetService(typeof(IdentityService));
            identityService.Username = "Unit Test";
        }

        private InventoryDocumentDataUtil DataUtil
        {
            get { return (InventoryDocumentDataUtil)ServiceProvider.GetService(typeof(InventoryDocumentDataUtil)); }
        }

        private InventoryDocumentFacade Facade
        {
            get { return (InventoryDocumentFacade)ServiceProvider.GetService(typeof(InventoryDocumentFacade)); }
        }

        [Fact]
        public async void Should_Success_Get_Data()
        {
            await DataUtil.GetTestData("Unit test");
            Tuple<List<InventoryDocument>, int, Dictionary<string, string>> Response =  Facade.Read();
            Assert.NotEqual(Response.Item1.Count, 0);
        }


        [Fact]
        public async void Should_Success_Get_Data_By_Id()
        {
            InventoryDocument model = await DataUtil.GetTestData("Unit test");
            var Response = Facade.ReadModelById((int)model.Id);
            Assert.NotNull(Response);
        }

        [Fact]
        public async void Should_Success_Create_Data()
        {
            InventoryDocument model = DataUtil.GetNewData();
            var Response = await Facade.Create(model, "Unit Test");
            Assert.NotEqual(Response, 0);
        }
    }
}
