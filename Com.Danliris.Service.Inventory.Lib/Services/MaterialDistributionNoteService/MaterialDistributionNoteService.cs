using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.Interfaces;
using Com.Danliris.Service.Inventory.Lib.Models.MaterialDistributionNoteModel;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.Lib.ViewModels.MaterialDistributionNoteViewModel;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Com.Moonlay.NetCore.Lib.Service;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Com.Danliris.Service.Inventory.Lib.Services.MaterialDistributionNoteService
{
    public class MaterialDistributionNoteService : BasicService<InventoryDbContext, MaterialDistributionNote>, IMap<MaterialDistributionNote, MaterialDistributionNoteViewModel>
    {
        public MaterialDistributionNoteService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<MaterialDistributionNote>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<MaterialDistributionNote> Query = this.DbContext.MaterialDistributionNotes;

            List<string> SearchAttributes = new List<string>()
            {
                "No"
            };

            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "No", "_CreatedUtc", "Type", "IsDisposition", "IsApproved"
            };

            Query = Query
                .Select(mdn => new MaterialDistributionNote
                {
                    Id = mdn.Id,
                    No = mdn.No,
                    _CreatedUtc = mdn._CreatedUtc,
                    Type = mdn.Type,
                    IsDisposition = mdn.IsDisposition,
                    IsApproved = mdn.IsApproved
                });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<MaterialDistributionNote> pageable = new Pageable<MaterialDistributionNote>(Query, Page - 1, Size);
            List<MaterialDistributionNote> Data = pageable.Data.ToList<MaterialDistributionNote>();
            int TotalData = pageable.TotalCount;


            //var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImRldjIiLCJwcm9maWxlIjp7ImZpcnN0bmFtZSI6IlRlc3QiLCJsYXN0bmFtZSI6IlVuaXQiLCJnZW5kZXIiOiJNIiwiZG9iIjoiMjAxNy0wMi0xN1QxMTozNToyMy4wMDBaIiwiZW1haWwiOiJkZXZAdW5pdC50ZXN0In0sInBlcm1pc3Npb24iOnsiVVQvVU5JVC8wMSI6NywiTEsiOjcsIlAxIjo0LCJQNCI6NCwiUDMiOjQsIlA3Ijo0LCJQNiI6NCwiQzkiOjd9LCJpYXQiOjE1MTk3MTUwNzN9.2kBc-iOxHI3wVIS8djU9Csfmdwr90Ob1P_xNcnYVru4";
            //HttpClient a = new HttpClient();
            //Dictionary<string, object> qdw = new Dictionary<string, object>
            //        {
            //            { "unit", "MTR" }
            //        };

            //a.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //var test = a.GetAsync("https://dl-core-api-dev.mybluemix.net/v1/master/uoms?filter=" + JsonConvert.SerializeObject(qdw)).Result.Content.ReadAsStringAsync();
            //Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(test.Result);
            //var UOM = result.Keys;
            


            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public MaterialDistributionNote MapToModel(MaterialDistributionNoteViewModel viewModel)
        {
            MaterialDistributionNote model = new MaterialDistributionNote();
            PropertyCopier<MaterialDistributionNoteViewModel, MaterialDistributionNote>.Copy(viewModel, model);

            model.UnitId = viewModel.Unit._id;
            model.UnitCode = viewModel.Unit.code;
            model.UnitName = viewModel.Unit.name;

            model.MaterialDistributionNoteItems = new List<MaterialDistributionNoteItem>();
            foreach(MaterialDistributionNoteItemViewModel mdni in viewModel.MaterialDistributionNoteItems)
            {
                MaterialDistributionNoteItem materialDistributionNoteItem = new MaterialDistributionNoteItem();
                PropertyCopier<MaterialDistributionNoteItemViewModel, MaterialDistributionNoteItem>.Copy(mdni, materialDistributionNoteItem);

                materialDistributionNoteItem.MaterialDistributionNoteDetails = new List<MaterialDistributionNoteDetail>();
                foreach (MaterialDistributionNoteDetailViewModel mdnd in mdni.MaterialDistributionNoteDetails)
                {
                    MaterialDistributionNoteDetail materialDistributionNoteDetail= new MaterialDistributionNoteDetail();
                    PropertyCopier<MaterialDistributionNoteDetailViewModel, MaterialDistributionNoteDetail>.Copy(mdnd, materialDistributionNoteDetail);

                    materialDistributionNoteDetail.ProductionOrderId = mdnd.ProductionOrder._id;
                    materialDistributionNoteDetail.ProductionOrderNo = mdnd.ProductionOrder.orderNo;

                    materialDistributionNoteDetail.ProductId = mdnd.Product._id;
                    materialDistributionNoteDetail.ProductCode = mdnd.Product.code;
                    materialDistributionNoteDetail.ProductName = mdnd.Product.name;

                    materialDistributionNoteDetail.SupplierId = mdnd.Supplier._id;
                    materialDistributionNoteDetail.SupplierCode = mdnd.Supplier.code;
                    materialDistributionNoteDetail.SupplierName = mdnd.Supplier.name;


                    materialDistributionNoteItem.MaterialDistributionNoteDetails.Add(materialDistributionNoteDetail);
                }

                model.MaterialDistributionNoteItems.Add(materialDistributionNoteItem);
            }

            return model;
        }

        public MaterialDistributionNoteViewModel MapToViewModel(MaterialDistributionNote model)
        {
            MaterialDistributionNoteViewModel viewModel = new MaterialDistributionNoteViewModel();
            PropertyCopier<MaterialDistributionNote, MaterialDistributionNoteViewModel>.Copy(model, viewModel);

            UnitViewModel Unit = new UnitViewModel()
            {
                _id = model.UnitId,
                code = model.UnitCode,
                name = model.UnitName
            };

            viewModel.Unit = Unit;

            viewModel.MaterialDistributionNoteItems = new List<MaterialDistributionNoteItemViewModel>();
            if (model.MaterialDistributionNoteItems != null)
            {
                foreach (MaterialDistributionNoteItem mdni in model.MaterialDistributionNoteItems)
                {
                    MaterialDistributionNoteItemViewModel materialDistributionNoteItemViewModel = new MaterialDistributionNoteItemViewModel();
                    PropertyCopier<MaterialDistributionNoteItem, MaterialDistributionNoteItemViewModel>.Copy(mdni, materialDistributionNoteItemViewModel);

                    materialDistributionNoteItemViewModel.MaterialDistributionNoteDetails = new List<MaterialDistributionNoteDetailViewModel>();
                    foreach (MaterialDistributionNoteDetail mdnd in mdni.MaterialDistributionNoteDetails)
                    {
                        MaterialDistributionNoteDetailViewModel materialDistributionNoteDetailViewModel = new MaterialDistributionNoteDetailViewModel();
                        PropertyCopier<MaterialDistributionNoteDetail, MaterialDistributionNoteDetailViewModel>.Copy(mdnd, materialDistributionNoteDetailViewModel);

                        ProductionOrderViewModel productionOrder = new ProductionOrderViewModel
                        {
                            _id = mdnd.ProductionOrderId,
                            orderNo = mdnd.ProductionOrderNo
                        };

                        ProductViewModel product = new ProductViewModel
                        {
                            _id = mdnd.ProductId,
                            code = mdnd.ProductCode,
                            name = mdnd.ProductName
                        };

                        SupplierViewModel supplier = new SupplierViewModel
                        {
                            _id = mdnd.SupplierId,
                            code = mdnd.SupplierCode,
                            name = mdnd.SupplierName
                        };

                        materialDistributionNoteDetailViewModel.ProductionOrder = productionOrder;
                        materialDistributionNoteDetailViewModel.Product = product;
                        materialDistributionNoteDetailViewModel.Supplier = supplier;

                        materialDistributionNoteItemViewModel.MaterialDistributionNoteDetails.Add(materialDistributionNoteDetailViewModel);
                    }

                    viewModel.MaterialDistributionNoteItems.Add(materialDistributionNoteItemViewModel);
                }
            }

            return viewModel;
        }

        public override async Task<MaterialDistributionNote> ReadModelById(int id)
        {
            return await this.DbSet
                .Where(d => d.Id.Equals(id) && d._IsDeleted.Equals(false))
                .Include(d => d.MaterialDistributionNoteItems)
                    .ThenInclude(d => d.MaterialDistributionNoteDetails)
                .FirstOrDefaultAsync();
        }

        public override async Task<int> CreateModel(MaterialDistributionNote Model)
        {
            int Created = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    Created = await this.CreateAsync(Model);

                    transaction.Commit();
                }
                catch (ServiceValidationExeption e)
                {
                    throw new ServiceValidationExeption(e.ValidationContext, e.ValidationResults);
                }
                finally
                {
                    transaction.Rollback();
                }
            }
            return Created;
        }


        public override void OnCreating(MaterialDistributionNote model)
        {
            do
            {
                model.No = CodeGenerator.GenerateCode();
            }
            while (this.DbSet.Any(d => d.No.Equals(model.No)));

            base.OnCreating(model);
            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;

            MaterialDistributionNoteItemService materialDistributionNoteItemService = ServiceProvider.GetService<MaterialDistributionNoteItemService>();
            materialDistributionNoteItemService.Username = this.Username;

            foreach (MaterialDistributionNoteItem mdni in model.MaterialDistributionNoteItems)
            {
                materialDistributionNoteItemService.OnCreating(mdni);
            }
        }

        public override void OnUpdating(int id, MaterialDistributionNote model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(MaterialDistributionNote model)
        {
            base.OnDeleting(model);
            model._DeletedAgent = "Service";
            model._DeletedBy = this.Username;
        }
    }
}
