using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Com.Danliris.Service.Inventory.Lib.Services.MaterialsRequestNoteServices;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Inventory.WebApi.Helpers;
using System.IO;

namespace Com.Danliris.Service.Inventory.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/material-request-note-report")]
    [Authorize]
    public class MaterialsRequestNoteReportController : Controller
    {
        private static readonly string ApiVersion = "1.0";
        private MaterialsRequestNoteService materialsRequestNoteService { get; }

        public MaterialsRequestNoteReportController(MaterialsRequestNoteService materialsRequestNoteService)
        {
            this.materialsRequestNoteService = materialsRequestNoteService;
        }

        [HttpGet]
        public IActionResult Get(string materialsRequestNoteCode, string productionOrderId, string unitId, string productId, string status, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order = "{}")
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];

            try
            {

                var data = materialsRequestNoteService.GetReport(materialsRequestNoteCode, productionOrderId, unitId, productId, status, dateFrom, dateTo, page, size, Order, offset);

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data = data.Item1,
                    info = new { total = data.Item2 }
                });
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