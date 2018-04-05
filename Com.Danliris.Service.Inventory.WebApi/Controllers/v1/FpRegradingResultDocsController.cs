using Com.Danliris.Service.Inventory.Lib;
using Com.Danliris.Service.Inventory.Lib.Models;
using Com.Danliris.Service.Inventory.Lib.Services;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Com.Danliris.Service.Inventory.Lib.PDFTemplates;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Inventory.WebApi.Controllers.v1.BasicControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/FpRegradingResultDocs")]
    [Authorize]
    public class FpRegradingResultDocsController : BasicController<InventoryDbContext, FpRegradingResultDocsService, FpRegradingResultDocsViewModel, FpReturProInvDocs>
    {
        private static readonly string ApiVersion = "1.0";
        public FpRegradingResultDocsController(FpRegradingResultDocsService service) : base(service, ApiVersion)
        {
        }

        [HttpGet("pdf/{Id}")]
        public async Task<IActionResult> GetPdfById([FromRoute] int Id)
        {
            try
            {
                var model = await Service.ReadModelById(Id);
                var viewModel = Service.MapToViewModel(model);

                FpRegradingResultDocsPdfTemplate PdfTemplate = new FpRegradingResultDocsPdfTemplate();
                MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel);

                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = $"Bon Retur Barang {viewModel.Code}.pdf"
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
