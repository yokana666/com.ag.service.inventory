using Com.Danliris.Service.Inventory.Lib.IntegrationServices;
using Com.Danliris.Service.Inventory.WebApi.Controllers.v1.InventoryIntegrationController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Inventory.Test.Controllers.Integration
{
    public class IntegrationControllerTest
    {
        protected InventoryIntegrationController GetController(Mock<IInventoryDocumentIntegrationService> mock)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            InventoryIntegrationController controller = (InventoryIntegrationController)Activator.CreateInstance(typeof(InventoryIntegrationController), mock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user.Object
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = "7";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");
            return controller;
        }

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        [Fact]
        public async Task Integrate_WithoutException_ReturnOk()
        {
            Mock<IInventoryDocumentIntegrationService> mock = new Mock<IInventoryDocumentIntegrationService>();
            mock.Setup(s => s.IntegrateData()).ReturnsAsync(1);
            var response = await GetController(mock).Integrate();
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Integrate_WithException_InternalServer()
        {
            Mock<IInventoryDocumentIntegrationService> mock = new Mock<IInventoryDocumentIntegrationService>();
            mock.Setup(s => s.IntegrateData()).ThrowsAsync(new Exception());
            var response = await GetController(mock).Integrate();
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
