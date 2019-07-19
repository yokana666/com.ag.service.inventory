using Com.Danliris.Service.Inventory.Lib.IntegrationServices;
using Com.Danliris.Service.Inventory.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Inventory.WebApi.Controllers.v1.InventoryIntegrationController
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/inventory/integrations")]
    [Authorize]
    public class InventoryIntegrationController : ControllerBase
    {
        private readonly IInventoryDocumentIntegrationService _integrationService;

        public InventoryIntegrationController(IInventoryDocumentIntegrationService integrationService)
        {
            _integrationService = integrationService;
        }

        [HttpGet]
        public async Task<IActionResult> Integrate()
        {
            try
            {
                await _integrationService.IntegrateData();

                return Ok();
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter("1.0", General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        
    }
}
