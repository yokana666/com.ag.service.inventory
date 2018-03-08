using Microsoft.AspNetCore.Mvc;
using Com.Danliris.Service.Inventory.WebApi.Helpers;
using Com.Danliris.Service.Inventory.Lib;
using Microsoft.AspNetCore.Authorization;
using Com.Danliris.Service.Inventory.Lib.Services.MaterialDistributionNoteService;
using Com.Danliris.Service.Inventory.Lib.ViewModels.MaterialDistributionNoteViewModel;
using Com.Danliris.Service.Inventory.Lib.Models.MaterialDistributionNoteModel;

namespace Com.Danliris.Service.Inventory.WebApi.Controllers.v1.BasicControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/material-distribution-notes")]
    [Authorize]
    public class MaterialDistributionNoteController : BasicController<InventoryDbContext, MaterialDistributionNoteService, MaterialDistributionNoteViewModel, MaterialDistributionNote>
    {
        private static readonly string ApiVersion = "1.0";
        public MaterialDistributionNoteController(MaterialDistributionNoteService service) : base(service, ApiVersion)
        {
        }
    }
}