using Microsoft.AspNetCore.Mvc;
using Com.Danliris.Service.Inventory.WebApi.Helpers;
using Com.Danliris.Service.Inventory.Lib.Services;
using Com.Danliris.Service.Inventory.Lib.Models;
using Com.Danliris.Service.Inventory.Lib;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Com.Danliris.Service.Inventory.WebApi.Controllers.v1.BasicControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/materials-request-notes")]
    [Authorize]
    public class MaterialsRequestNoteController : BasicController<InventoryDbContext, MaterialsRequestNoteService, MaterialsRequestNoteViewModel, MaterialsRequestNote>
    {
        private static readonly string ApiVersion = "1.0";
        public MaterialsRequestNoteController(MaterialsRequestNoteService service) : base(service, ApiVersion)
        {
        }
    }
}