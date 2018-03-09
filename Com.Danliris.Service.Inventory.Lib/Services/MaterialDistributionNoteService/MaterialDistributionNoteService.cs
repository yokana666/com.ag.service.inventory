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
using Com.Danliris.Service.Inventory.Lib.ViewModels.InventoryDocumentViewModel;
using System.Text;

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
            foreach (MaterialDistributionNoteItemViewModel mdni in viewModel.MaterialDistributionNoteItems)
            {
                MaterialDistributionNoteItem materialDistributionNoteItem = new MaterialDistributionNoteItem();
                PropertyCopier<MaterialDistributionNoteItemViewModel, MaterialDistributionNoteItem>.Copy(mdni, materialDistributionNoteItem);

                materialDistributionNoteItem.MaterialDistributionNoteDetails = new List<MaterialDistributionNoteDetail>();
                foreach (MaterialDistributionNoteDetailViewModel mdnd in mdni.MaterialDistributionNoteDetails)
                {
                    MaterialDistributionNoteDetail materialDistributionNoteDetail = new MaterialDistributionNoteDetail();
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

        public void CreateInventoryDocument(MaterialDistributionNote Model, string Type)
        {
            string inventoryDocumentURI = "inventory/inventory-documents";
            string storageURI = "master/storages";
            string uomURI = "master/uoms";

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            /* Get UOM */
            Dictionary<string, object> filterUOM = new Dictionary<string, object> { { "unit", "MTR" } };
            var responseUOM = httpClient.GetAsync($@"{APIEndpoint.Core}{uomURI}?filter=" + JsonConvert.SerializeObject(filterUOM)).Result.Content.ReadAsStringAsync();
            Dictionary<string, object> resultUOM = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseUOM.Result);
            var jsonUOM = resultUOM.Single(p => p.Key.Equals("data")).Value;
            Dictionary<string, object> uom = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonUOM.ToString())[0];

            /* Get Storage */
            var storageName = Model.UnitName.Equals("PRINTING") ? "Gudang Greige Printing" : "Gudang Greige Finishing";
            Dictionary<string, object> filterStorage = new Dictionary<string, object> { { "name", storageName } };
            var responseStorage = httpClient.GetAsync($@"{APIEndpoint.Core}{storageURI}?filter=" + JsonConvert.SerializeObject(filterStorage)).Result.Content.ReadAsStringAsync();
            Dictionary<string, object> resultStorage = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseStorage.Result);
            var jsonStorage = resultStorage.Single(p => p.Key.Equals("data")).Value;
            Dictionary<string, object> storage = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonStorage.ToString())[0];

            /* Create Inventory Document */
            List<InventoryDocumentItemViewModel> inventoryDocumentItems = new List<InventoryDocumentItemViewModel>();

            foreach (MaterialDistributionNoteItem mdni in Model.MaterialDistributionNoteItems)
            {
                List<MaterialDistributionNoteDetail> list = mdni.MaterialDistributionNoteDetails
                    .GroupBy(m => new { m.ProductionOrderNo, m.ProductName, m.MaterialRequestNoteItemLength })
                    .Select(s => new MaterialDistributionNoteDetail
                    {
                        ProductId = s.First().ProductId,
                        ProductCode = s.First().ProductCode,
                        ProductName = s.First().ProductName,
                        ReceivedLength = s.Sum(d => d.ReceivedLength)
                    }).ToList();
                
                foreach (MaterialDistributionNoteDetail mdnd in list)
                {
                    InventoryDocumentItemViewModel inventoryDocumentItem = new InventoryDocumentItemViewModel
                    {
                        productId = mdnd.ProductId,
                        productCode = mdnd.ProductCode,
                        productName = mdnd.ProductName,
                        quantity = mdnd.ReceivedLength,
                        uomId = uom["_id"].ToString(),
                        uom = uom["unit"].ToString()
                    };

                    inventoryDocumentItems.Add(inventoryDocumentItem);
                }
            }

            InventoryDocumentViewModel inventoryDocument = new InventoryDocumentViewModel
            {
                date = DateTime.Now,
                referenceNo = Model.No,
                referenceType = "Bon Pengantar Greige",
                type = Type,
                storageId = storage["_id"].ToString(),
                storageCode = storage["code"].ToString(),
                storageName = storage["name"].ToString(),
                items = inventoryDocumentItems
            };

            var response = httpClient.PostAsync($"{APIEndpoint.Inventory}{inventoryDocumentURI}", new StringContent(JsonConvert.SerializeObject(inventoryDocument).ToString(), Encoding.UTF8, General.JsonMediaType)).Result;
            response.EnsureSuccessStatusCode();
        }

        public async Task<MaterialDistributionNote> CustomCodeGenerator(MaterialDistributionNote Model)
        {
            var Type = string.Equals(Model.UnitName.ToUpper(), "PRINTING") ? "PR" : "FS";
            var lastData = await this.DbSet.Where(w => string.Equals(w.UnitName, Model.UnitName)).OrderByDescending(o => o._CreatedUtc).FirstOrDefaultAsync();

            DateTime Now = DateTime.Now;
            string Year = Now.ToString("yy");

            if (lastData == null)
            {
                Model.AutoIncrementNumber = 1;
                string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                Model.No = $"P{Type}{Year}{Number}";
            }
            else
            {
                if (lastData._CreatedUtc.Year < Now.Year)
                {
                    Model.AutoIncrementNumber = 1;
                    string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                    Model.No = $"P{Type}{Year}{Number}";
                }
                else
                {
                    Model.AutoIncrementNumber = lastData.AutoIncrementNumber + 1;
                    string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                    Model.No = $"P{Type}{Year}{Number}";
                }
            }

            return Model;
        }

        public override async Task<int> CreateModel(MaterialDistributionNote Model)
        {
            int Created = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    Model = await this.CustomCodeGenerator(Model);
                    Created = await this.CreateAsync(Model);
                    CreateInventoryDocument(Model, "OUT");

                    transaction.Commit();
                }
                catch (ServiceValidationExeption e)
                {
                    transaction.Rollback();
                    throw new ServiceValidationExeption(e.ValidationContext, e.ValidationResults);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return Created;
        }

        public bool UpdateIsApprove(List<int> Ids)
        {
            bool IsSuccessful = false;

            using (var Transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var mdn = this.DbSet.Where(m => Ids.Contains(m.Id)).ToList();
                    mdn.ForEach(m => { m.IsApproved = true; m._LastModifiedUtc = DateTime.UtcNow; m._LastModifiedAgent = "Service"; m._LastModifiedBy = this.Username; });
                    this.DbContext.SaveChanges();

                    IsSuccessful = true;
                    Transaction.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    Transaction.Rollback();
                    throw;
                }
            }

            return IsSuccessful;
        }

        public override async Task<int> DeleteModel(int Id)
        {
            int Count = 0;

            using (var Transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    MaterialDistributionNote materialDistributionNote = await ReadModelById(Id);
                    Count = this.Delete(Id);

                    MaterialDistributionNoteItemService materialDistributionNoteItemService = ServiceProvider.GetService<MaterialDistributionNoteItemService>();
                    MaterialDistributionNoteDetailService materialDistributionNoteDetailService = ServiceProvider.GetService<MaterialDistributionNoteDetailService>();
                    materialDistributionNoteItemService.Username = this.Username;
                    materialDistributionNoteDetailService.Username = this.Username;


                    HashSet<int> MaterialDistributionNoteItems = new HashSet<int>(this.DbContext.MaterialDistributionNoteItems.Where(p => p.MaterialDistributionNoteId.Equals(Id)).Select(p => p.Id));

                    foreach (int item in MaterialDistributionNoteItems)
                    {
                        HashSet<int> MaterialDistributionNoteDetails = new HashSet<int>(this.DbContext.MaterialDistributionNoteDetails.Where(p => p.MaterialDistributionNoteItemId.Equals(item)).Select(p => p.Id));

                        foreach(int detail in MaterialDistributionNoteDetails)
                        {
                            await materialDistributionNoteDetailService.DeleteAsync(detail);
                        }

                        await materialDistributionNoteItemService.DeleteAsync(item);
                    }

                    CreateInventoryDocument(materialDistributionNote, "IN");

                    Transaction.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    Transaction.Rollback();
                    throw;
                }
            }

            return Count;
        }

        public override void OnCreating(MaterialDistributionNote model)
        {
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
