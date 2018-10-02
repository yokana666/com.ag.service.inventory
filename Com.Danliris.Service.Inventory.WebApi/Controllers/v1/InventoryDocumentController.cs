using AutoMapper;
using Com.Danliris.Service.Inventory.Lib.Facades.InventoryFacades;
using Com.Danliris.Service.Inventory.Lib.Models.InventoryModel;
using Com.Danliris.Service.Inventory.Lib.Services;
using Com.Danliris.Service.Inventory.Lib.ViewModels.InventoryViewModel;
using Com.Danliris.Service.Inventory.WebApi.Helpers;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Inventory.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/inventory-documents")]
    [Authorize]
    public class InventoryDocumentController : Controller
    {
        private string ApiVersion = "1.0.0";
        private readonly IMapper _mapper;
        private readonly InventoryDocumentFacade _facade;
        private readonly IdentityService identityService;

        public InventoryDocumentController(IMapper mapper, InventoryDocumentFacade facade, IdentityService identityService)
        {
            _mapper = mapper;
            _facade = facade;
            this.identityService = identityService;
        }

        [HttpGet]
        public IActionResult Get(int page = 1, int size = 25, string order = "{}", string keyword = null, string filter = "{}")
        {
            identityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

            try
            {


                var Data = _facade.Read(page, size, order, keyword, filter);
                List<InventoryDocumentViewModel> newData = new List<InventoryDocumentViewModel>();
                foreach (var model in Data.Item1)
                {
                    List<InventoryDocumentItemViewModel> items = new List<InventoryDocumentItemViewModel>();
                    foreach (var item in model.Items)
                    {
                        items.Add(new InventoryDocumentItemViewModel
                        {
                            productCode = item.ProductCode,
                            productId = item.ProductId,
                            productName = item.ProductName,
                            remark = item.ProductRemark,
                            quantity = item.Quantity,
                            stockPlanning = item.StockPlanning,
                            uomId = item.UomId,
                            uom = item.UomUnit,

                        });
                    }
                    InventoryDocumentViewModel viewModel = new InventoryDocumentViewModel
                    {
                        Id=model.Id,
                        no= model.No,
                        referenceNo = model.ReferenceNo,
                        referenceType = model.ReferenceType,
                        remark = model.Remark,
                        storageCode = model.StorageCode,
                        storageId = model.StorageId,
                        storageName = model.StorageName,
                        date = model.Date,
                        type = model.Type,
                        items = items
                    };
                    newData.Add(viewModel);
                }

                List<object> listData = new List<object>();
                listData.AddRange(
                    newData.AsQueryable().Select(s => new
                    {
                        s.no,
                        s.Id,
                        s.code,
                        s.date,
                        s.items,
                        s.storageCode,
                        s.storageId,
                        s.storageName,
                        s.referenceNo,
                        s.referenceType,
                        s.type
                    }).ToList()
                );

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    statusCode = General.OK_STATUS_CODE,
                    message = General.OK_MESSAGE,
                    data = listData,
                    info = new Dictionary<string, object>
                    {
                        { "count", listData.Count },
                        { "total", Data.Item2 },
                        { "order", Data.Item3 },
                        { "page", page },
                        { "size", size }
                    },
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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");

                InventoryDocument model = _facade.ReadModelById(id);
                //InventoryDocumentViewModel viewModel = _mapper.Map<InventoryDocumentViewModel>(model);
                List<InventoryDocumentItemViewModel> items = new List<InventoryDocumentItemViewModel>();
                foreach (var item in model.Items)
                {
                    items.Add(new InventoryDocumentItemViewModel
                    {
                        productCode = item.ProductCode,
                        productId = item.ProductId,
                        productName = item.ProductName,
                        remark = item.ProductRemark,
                        quantity = item.Quantity,
                        stockPlanning = item.StockPlanning,
                        uomId = item.UomId,
                        uom = item.UomUnit,

                    });
                }
                InventoryDocumentViewModel viewModel = new InventoryDocumentViewModel
                {
                    no=model.No,
                    referenceNo = model.ReferenceNo,
                    referenceType = model.ReferenceType,
                    remark = model.Remark,
                    storageCode = model.StorageCode,
                    storageId = model.StorageId,
                    storageName = model.StorageName,
                    date = model.Date,
                    type = model.Type,
                    items = items
                };
                if (viewModel == null)
                {
                    throw new Exception("Invalid Id");
                }

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    statusCode = General.OK_STATUS_CODE,
                    message = General.OK_MESSAGE,
                    data = viewModel,
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

        

        //[HttpPost]
        //public async Task<ActionResult> Post([FromBody] InventoryDocumentViewModel viewModel)
        //{
        //    this.identityService.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");
        //    this.identityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

        //    ValidateService validateService = (ValidateService)_facade.serviceProvider.GetService(typeof(ValidateService));
        //    return await new BasePost<InventoryDocument, InventoryDocumentViewModel, InventoryDocumentFacade>(_facade, ApiVersion, validateService, Request.Path)
        //        .Post(viewModel);
        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]InventoryDocumentViewModel vm)
        {
            identityService.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");
            identityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

            //InventoryDocument m = _mapper.Map<InventoryDocument>(vm);
            List<InventoryDocumentItem> items = new List<InventoryDocumentItem>();
            foreach(var item in vm.items)
            {
                items.Add(new InventoryDocumentItem
                {
                    ProductCode=item.productCode,
                    ProductId=item.productId,
                    ProductName=item.productName,
                    ProductRemark=item.remark,
                    Quantity=item.quantity,
                    StockPlanning=item.stockPlanning,
                    UomId=item.uomId,
                    UomUnit=item.uom,
                    
                });
            }
            InventoryDocument m = new InventoryDocument
            {
                ReferenceNo=vm.referenceNo,
                ReferenceType=vm.referenceType,
                Remark=vm.remark,
                StorageCode=vm.storageCode,
                StorageId=Convert.ToInt32(vm.storageId),
                StorageName=vm.storageName,
                Date=vm.date,
                Type=vm.type,
                Items=items
            };

            ValidateService validateService = (ValidateService)_facade.serviceProvider.GetService(typeof(ValidateService));

            try
            {
                validateService.Validate(vm);

                //int clientTimeZoneOffset = int.Parse(Request.Headers["x-timezone-offset"].First());
                int result = await _facade.Create(m, identityService.Username);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.CREATED_STATUS_CODE, General.OK_MESSAGE)
                    .Ok();
                return Created(String.Concat(Request.Path, "/", 0), Result);
            }
            catch (ServiceValidationExeption e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.BAD_REQUEST_STATUS_CODE, General.BAD_REQUEST_MESSAGE)
                    .Fail(e);
                return BadRequest(Result);

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