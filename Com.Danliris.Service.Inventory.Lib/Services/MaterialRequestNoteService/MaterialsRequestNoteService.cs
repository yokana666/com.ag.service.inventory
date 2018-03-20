using Com.Danliris.Service.Inventory.Lib.Models.MaterialsRequestNoteModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Com.Danliris.Service.Inventory.Lib.Helpers;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Com.Moonlay.NetCore.Lib;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Inventory.Lib.Interfaces;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.Lib.ViewModels.MaterialsRequestNoteViewModel;
using Com.Moonlay.NetCore.Lib.Service;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.Services.MaterialsRequestNoteService
{
    public class MaterialsRequestNoteService : BasicService<InventoryDbContext, MaterialsRequestNote>, IMap<MaterialsRequestNote, MaterialsRequestNoteViewModel>
    {
        public MaterialsRequestNoteService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<MaterialsRequestNote>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<MaterialsRequestNote> Query = this.DbContext.MaterialsRequestNotes;

            List<string> SearchAttributes = new List<string>()
                {
                    "UnitName", "RequestType", "Code"
                };
            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
                {
                    "Id", "Code", "Unit", "RequestType", "Remark", "MaterialsRequestNote_Items", "_LastModifiedUtc"
                };
            Query = Query
                .Select(mrn => new MaterialsRequestNote
                {
                    Id = mrn.Id,
                    Code = mrn.Code,
                    UnitId = mrn.UnitId,
                    UnitCode = mrn.UnitCode,
                    UnitName = mrn.UnitName,
                    IsDistributed = mrn.IsDistributed,
                    RequestType = mrn.RequestType,
                    _LastModifiedUtc = mrn._LastModifiedUtc,
                    MaterialsRequestNote_Items = mrn.MaterialsRequestNote_Items.Select(p => new MaterialsRequestNote_Item { MaterialsRequestNoteId = p.MaterialsRequestNoteId, ProductionOrderNo = p.ProductionOrderNo}).Where(i => i.MaterialsRequestNoteId.Equals(mrn.Id)).ToList()
                });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<MaterialsRequestNote> pageable = new Pageable<MaterialsRequestNote>(Query, Page - 1, Size);
            List<MaterialsRequestNote> Data = pageable.Data.ToList<MaterialsRequestNote>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public async Task<MaterialsRequestNote> CustomCodeGenerator(MaterialsRequestNote Model)
        {
            Model.Type = string.Equals(Model.UnitName.ToUpper(), "PRINTING") ? "P" : "F";
            var lastData = await this.DbSet.Where(w => string.Equals(w.Type, Model.Type)).OrderByDescending(o => o._CreatedUtc).FirstOrDefaultAsync();

            DateTime Now = DateTime.Now;
            string Year = Now.ToString("yy");
            string Month = Now.ToString("MM");

            if (lastData == null)
            {
                Model.AutoIncrementNumber = 1;
                string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                Model.Code = $"SPB{Model.Type}{Month}{Year}{Number}";
            }
            else
            {
                if (lastData._CreatedUtc.Year < Now.Year)
                {
                    Model.AutoIncrementNumber = 1;
                    string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                    Model.Code = $"SPB{Model.Type}{Month}{Year}{Number}";
                }
                else
                {
                    Model.AutoIncrementNumber = lastData.AutoIncrementNumber + 1;
                    string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                    Model.Code = $"SPB{Model.Type}{Month}{Year}{Number}";
                }
            }

            return Model;
        }

        public void UpdateProductionOrder(MaterialsRequestNote Model, string Context)
        {
            foreach (MaterialsRequestNote_Item item in Model.MaterialsRequestNote_Items)
            {
                string productionOrderUri = "sales/production-orders";

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                /* Get ProductionOrder */
                var responseProductionOrder = httpClient.GetAsync($@"{APIEndpoint.Production}{productionOrderUri}/{item.ProductionOrderId}").Result.Content.ReadAsStringAsync();
                Dictionary<string, object> resultProductionOrder = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseProductionOrder.Result);
                var jsonProductionOrder = resultProductionOrder.Single(p => p.Key.Equals("data")).Value;
                Dictionary<string, object> productionOrder = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonProductionOrder.ToString());

                productionOrder["isRequested"] = Context.Equals("CREATE") ? true : false;

                var response = httpClient.PutAsync($"{APIEndpoint.Production}{productionOrderUri}/{item.ProductionOrderId}", new StringContent(JsonConvert.SerializeObject(productionOrder).ToString(), Encoding.UTF8, General.JsonMediaType)).Result;
                response.EnsureSuccessStatusCode();
            }
        }

        public override async Task<int> CreateModel(MaterialsRequestNote Model)
        {
            int Created = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    Model = await this.CustomCodeGenerator(Model);
                    Created = await this.CreateAsync(Model);
                    UpdateProductionOrder(Model, "CREATE");
                    transaction.Commit();
                }
                catch (ServiceValidationExeption e)
                {
                    throw new ServiceValidationExeption(e.ValidationContext, e.ValidationResults);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }
            return Created;
        }

        public override async Task<MaterialsRequestNote> ReadModelById(int id)
        {
            return await this.DbSet
                .Where(d => d.Id.Equals(id) && d._IsDeleted.Equals(false))
                .Include(d => d.MaterialsRequestNote_Items)
                .FirstOrDefaultAsync();
        }

        public override async Task<int> UpdateModel(int Id, MaterialsRequestNote Model)
        {
            MaterialsRequestNote_ItemService materialsRequestNote_ItemService = this.ServiceProvider.GetService<MaterialsRequestNote_ItemService>();

            int Updated = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    HashSet<int> materialsRequestNote_Items = new HashSet<int>(materialsRequestNote_ItemService.DbSet
                        .Where(w => w.MaterialsRequestNoteId.Equals(Id))
                        .Select(s => s.Id));
                    Updated = await this.UpdateAsync(Id, Model);

                    materialsRequestNote_ItemService.Username = this.Username;

                    foreach (int materialsRequestNote_Item in materialsRequestNote_Items)
                    {
                        MaterialsRequestNote_Item model = Model.MaterialsRequestNote_Items.FirstOrDefault(prop => prop.Id.Equals(materialsRequestNote_Item));
                        if (model == null)
                        {
                            await materialsRequestNote_ItemService.DeleteModel(materialsRequestNote_Item);
                        }
                        else
                        {
                            await materialsRequestNote_ItemService.UpdateModel(materialsRequestNote_Item, model);
                        }
                    }

                    foreach (MaterialsRequestNote_Item materialsRequestNote_Item in Model.MaterialsRequestNote_Items)
                    {
                        if (materialsRequestNote_Item.Id.Equals(0))
                        {
                            await materialsRequestNote_ItemService.CreateModel(materialsRequestNote_Item);
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return Updated;
        }

        public override async Task<int> DeleteModel(int Id)
        {
            MaterialsRequestNote_ItemService materialsRequestNote_ItemService = this.ServiceProvider.GetService<MaterialsRequestNote_ItemService>();

            int Deleted = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    MaterialsRequestNote Model = await this.ReadModelById(Id);
                    Deleted = await this.DeleteAsync(Id);

                    HashSet<int> materialsRequestNote_Items = new HashSet<int>(materialsRequestNote_ItemService.DbSet
                        .Where(p => p.MaterialsRequestNoteId.Equals(Id))
                        .Select(p => p.Id));

                    materialsRequestNote_ItemService.Username = this.Username;

                    foreach (int materialsRequestNote_Item in materialsRequestNote_Items)
                    {
                        await materialsRequestNote_ItemService.DeleteModel(materialsRequestNote_Item);
                    }

                    UpdateProductionOrder(Model, "DELETE");
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return Deleted;
        }

        public override void OnCreating(MaterialsRequestNote model)
        {
            if (model.MaterialsRequestNote_Items.Count > 0)
            {
                MaterialsRequestNote_ItemService materialsRequestNote_ItemService = this.ServiceProvider.GetService<MaterialsRequestNote_ItemService>();

                materialsRequestNote_ItemService.Username = this.Username;
                foreach (MaterialsRequestNote_Item materialsRequestNoteItem in model.MaterialsRequestNote_Items)
                {
                    materialsRequestNote_ItemService.OnCreating(materialsRequestNoteItem);
                }
            }

            base.OnCreating(model);
            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnUpdating(int id, MaterialsRequestNote model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(MaterialsRequestNote model)
        {
            base.OnDeleting(model);
            model._DeletedAgent = "Service";
            model._DeletedBy = this.Username;
        }

        public MaterialsRequestNoteViewModel MapToViewModel(MaterialsRequestNote model)
        {
            MaterialsRequestNoteViewModel viewModel = new MaterialsRequestNoteViewModel();

            PropertyCopier<MaterialsRequestNote, MaterialsRequestNoteViewModel>.Copy(model, viewModel);

            UnitViewModel Unit = new UnitViewModel()
            {
                _id = model.UnitId,
                code = model.UnitCode,
                name = model.UnitName
            };

            viewModel.Code = model.Code;
            viewModel.Unit = Unit;
            viewModel.RequestType = model.RequestType;
            viewModel.Remark = model.Remark;

            viewModel.MaterialsRequestNote_Items = new List<MaterialsRequestNote_ItemViewModel>();
            if (model.MaterialsRequestNote_Items != null)
            {
                foreach (MaterialsRequestNote_Item materialsRequestNote_Item in model.MaterialsRequestNote_Items)
                {
                    MaterialsRequestNote_ItemViewModel materialsRequestNote_ItemViewModel = new MaterialsRequestNote_ItemViewModel();
                    PropertyCopier<MaterialsRequestNote_Item, MaterialsRequestNote_ItemViewModel>.Copy(materialsRequestNote_Item, materialsRequestNote_ItemViewModel);

                    OrderTypeViewModel OrderType = new OrderTypeViewModel()
                    {
                        _id = materialsRequestNote_Item.OrderTypeId,
                        code = materialsRequestNote_Item.OrderTypeCode,
                        name = materialsRequestNote_Item.OrderTypeName
                    };

                    ProductionOrderViewModel ProductionOrder = new ProductionOrderViewModel()
                    {
                        _id = materialsRequestNote_Item.ProductionOrderId,
                        orderNo = materialsRequestNote_Item.ProductionOrderNo,
                        orderQuantity = materialsRequestNote_Item.OrderQuantity,
                        orderType = OrderType
                    };
                    materialsRequestNote_ItemViewModel.ProductionOrder = ProductionOrder;

                    ProductViewModel Product = new ProductViewModel()
                    {
                        _id = materialsRequestNote_Item.ProductId,
                        code = materialsRequestNote_Item.ProductCode,
                        name = materialsRequestNote_Item.ProductName
                    };
                    materialsRequestNote_ItemViewModel.Product = Product;

                    materialsRequestNote_ItemViewModel.Length = materialsRequestNote_Item.Length;

                    viewModel.MaterialsRequestNote_Items.Add(materialsRequestNote_ItemViewModel);
                }
            }

            return viewModel;
        }

        public MaterialsRequestNote MapToModel(MaterialsRequestNoteViewModel viewModel)
        {
            MaterialsRequestNote model = new MaterialsRequestNote();

            PropertyCopier<MaterialsRequestNoteViewModel, MaterialsRequestNote>.Copy(viewModel, model);

            model.UnitId = viewModel.Unit._id;
            model.UnitCode = viewModel.Unit.code;
            model.UnitName = viewModel.Unit.name;
            model.RequestType = viewModel.RequestType;
            model.Remark = viewModel.Remark;

            model.MaterialsRequestNote_Items = new List<MaterialsRequestNote_Item>();

            foreach (MaterialsRequestNote_ItemViewModel materialsRequestNote_ItemViewModel in viewModel.MaterialsRequestNote_Items)
            {
                MaterialsRequestNote_Item materialsRequestNote_Item = new MaterialsRequestNote_Item();

                PropertyCopier<MaterialsRequestNote_ItemViewModel, MaterialsRequestNote_Item>.Copy(materialsRequestNote_ItemViewModel, materialsRequestNote_Item);

                materialsRequestNote_Item.ProductionOrderId = materialsRequestNote_ItemViewModel.ProductionOrder != null ? materialsRequestNote_ItemViewModel.ProductionOrder._id : "";
                materialsRequestNote_Item.ProductionOrderNo = materialsRequestNote_ItemViewModel.ProductionOrder != null ? materialsRequestNote_ItemViewModel.ProductionOrder.orderNo : "";
                materialsRequestNote_Item.OrderQuantity = materialsRequestNote_ItemViewModel.ProductionOrder != null && materialsRequestNote_ItemViewModel.ProductionOrder.orderQuantity != null ? (double)materialsRequestNote_ItemViewModel.ProductionOrder.orderQuantity : 0;
                materialsRequestNote_Item.OrderTypeId = materialsRequestNote_ItemViewModel.ProductionOrder != null ? materialsRequestNote_ItemViewModel.ProductionOrder.orderType._id : "";
                materialsRequestNote_Item.OrderTypeCode = materialsRequestNote_ItemViewModel.ProductionOrder != null ? materialsRequestNote_ItemViewModel.ProductionOrder.orderType.code : "";
                materialsRequestNote_Item.OrderTypeName = materialsRequestNote_ItemViewModel.ProductionOrder != null ? materialsRequestNote_ItemViewModel.ProductionOrder.orderType.name : "";
                materialsRequestNote_Item.ProductId = materialsRequestNote_ItemViewModel.Product._id;
                materialsRequestNote_Item.ProductCode = materialsRequestNote_ItemViewModel.Product.code;
                materialsRequestNote_Item.ProductName = materialsRequestNote_ItemViewModel.Product.name;
                materialsRequestNote_Item.Length = materialsRequestNote_ItemViewModel.Length != null ? (double)materialsRequestNote_ItemViewModel.Length : 0;

                model.MaterialsRequestNote_Items.Add(materialsRequestNote_Item);
            }

            return model;
        }
    }
}
