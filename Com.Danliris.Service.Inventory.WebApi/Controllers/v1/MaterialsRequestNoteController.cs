using Microsoft.AspNetCore.Mvc;
using Com.Danliris.Service.Inventory.WebApi.Helpers;
using Com.Danliris.Service.Inventory.Lib;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Com.Danliris.Service.Inventory.Lib.PDFTemplates;

//using System.Reflection.Metadata;
using System.IO;
using Com.Danliris.Service.Inventory.Lib.Services.MaterialsRequestNoteServices;
using Com.Danliris.Service.Inventory.Lib.ViewModels.MaterialsRequestNoteViewModel;
using Com.Danliris.Service.Inventory.Lib.Models.MaterialsRequestNoteModel;

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

        [HttpGet("pdf/{Id}")]
        public async Task<IActionResult> GetPdfById([FromRoute] int Id)
        {
            try
            {
                var model = await Service.ReadModelById(Id);
                var viewModel = Service.MapToViewModel(model);

                MaterialsRequestNotePdfTemplate PdfTemplate = new MaterialsRequestNotePdfTemplate();
                MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel);

                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = $"Bon Surat Permintaan Barang {viewModel.Code}.pdf"
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
