using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Com.Danliris.Service.Inventory.Lib.Facades;
using Com.Danliris.Service.Inventory.WebApi.Helpers;

namespace Com.Danliris.Service.Inventory.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/fp-regrading-result-docs-report")]
    public class FpRegradingResultDocsReportController : Controller
    {
        private static readonly string ApiVersion = "1.0";
        private readonly FpRegradingResultDocsReportFacade fpRegradingResultDocsReportFacade;

        public FpRegradingResultDocsReportController(FpRegradingResultDocsReportFacade fpRegradingResultDocsReportFacade)
        {
            this.fpRegradingResultDocsReportFacade = fpRegradingResultDocsReportFacade;
        }

        [HttpGet]
        public IActionResult Get(string unitName, string code, string productName, bool? isReturn, bool? isReturnedToPurchasing, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int page, int size, string Order = "{}")
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];

            try
            {
                var data = fpRegradingResultDocsReportFacade.GetReport(unitName, code, productName, isReturn, isReturnedToPurchasing, dateFrom, dateTo, page, size, Order);

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data = data.Item1,
                    info = new { total = data.Item2, page = page, size = size }
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