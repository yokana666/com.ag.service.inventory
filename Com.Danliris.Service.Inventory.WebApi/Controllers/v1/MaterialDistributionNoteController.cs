using Com.Danliris.Service.Inventory.Lib;
using Com.Danliris.Service.Inventory.Lib.Models.MaterialDistributionNoteModel;
using Com.Danliris.Service.Inventory.Lib.PDFTemplates;
using Com.Danliris.Service.Inventory.Lib.Services.MaterialDistributionNoteService;
using Com.Danliris.Service.Inventory.Lib.ViewModels.MaterialDistributionNoteViewModel;
using Com.Danliris.Service.Inventory.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        [HttpPut]
        public IActionResult Put([FromBody]List<int> Ids)
        {
            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                if (this.Service.UpdateIsApprove(Ids))
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
                }
            }
            catch (Exception e)
            {
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
            }
        }

        [HttpGet("pdf/{id}")]
        public IActionResult GetPDF([FromRoute]int Id)
        {
            try
            {
                var model = Service.ReadModelById(Id).Result;
                var viewModel = Service.MapToViewModel(model);

                MaterialDistributionNotePdfTemplate PdfTemplate = new MaterialDistributionNotePdfTemplate();
                MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel);

                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = "Bon Pengantar Greige - " + model.No + ".pdf"
                };
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }
}