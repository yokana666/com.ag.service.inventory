using Com.Danliris.Service.Inventory.Lib;
using Com.Danliris.Service.Inventory.Lib.Models;
using Com.Danliris.Service.Inventory.Lib.Services;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Com.Danliris.Service.Inventory.WebApi.Controllers.v1.BasicControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/fp-retur-pro-inv-docs")]
    [Authorize]
    public class FpReturProInvDocsController : BasicController<InventoryDbContext, FpReturProInvDocsService, FpReturProInvDocsViewModel, FpReturProInvDocs>
    {
        private static readonly string ApiVersion = "1.0";
        public FpReturProInvDocsController(FpReturProInvDocsService service) : base(service, ApiVersion)
        {
        }
    }
}
