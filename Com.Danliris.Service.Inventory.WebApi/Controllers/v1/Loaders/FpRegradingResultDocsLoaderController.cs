using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Com.Danliris.Service.Inventory.Lib.Models;
using Com.Danliris.Service.Inventory.Lib.Services;
using Com.Danliris.Service.Inventory.WebApi.Helpers;
using Com.Danliris.Service.Inventory.Lib.ViewModels;

namespace Com.Danliris.Service.Inventory.WebApi.Controllers.v1.Loaders
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/fp-regrading-result-docs-loader")]
    [Authorize]
    public class FpRegradingResultDocsLoaderController : Controller
    {
        private string ApiVersion = "1.0.0";
        private readonly FpRegradingResultDocsService service;

        public FpRegradingResultDocsLoaderController(FpRegradingResultDocsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult GetNo(string Keyword, string Filter = "{}")
        {
            try
            {
                Tuple<List<FpRegradingResultDocs>, int, Dictionary<string, string>, List<string>> Data = service.ReadNo(Keyword, Filter);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                    .Ok<FpRegradingResultDocs, FpRegradingResultDocsViewModel>(Data.Item1, service.MapToViewModel, 1, 25, Data.Item2, Data.Item1.Count, Data.Item3, Data.Item4);

                return Ok(Result);
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