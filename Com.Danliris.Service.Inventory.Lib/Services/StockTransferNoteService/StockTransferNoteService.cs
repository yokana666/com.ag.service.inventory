using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.Interfaces;
using Com.Danliris.Service.Inventory.Lib.Models.StockTransferNoteModel;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.Lib.ViewModels.StockTransferNoteViewModel;
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

namespace Com.Danliris.Service.Inventory.Lib.Services.StockTransferNoteService
{
    public class StockTransferNoteService : BasicService<InventoryDbContext, StockTransferNote>, IMap<StockTransferNote, StockTransferNoteViewModel>
    {
        public StockTransferNoteService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<StockTransferNote>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<StockTransferNote> Query = this.DbContext.StockTransferNotes;

            List<string> SearchAttributes = new List<string>()
            {
                "Code"
            };

            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "_CreatedUtc", "ReferenceNo", "ReferenceType", "SourceStorage", "TargetStorage", "IsApproved", "_LastModifiedUtc"
            };

            Query = Query
                .Select(stn => new StockTransferNote
                {
                    Id = stn.Id,
                    Code = stn.Code,
                    _CreatedUtc = stn._CreatedUtc,
                    ReferenceNo = stn.ReferenceNo,
                    ReferenceType = stn.ReferenceType,
                    SourceStorageName = stn.SourceStorageName,
                    TargetStorageName = stn.TargetStorageName,
                    IsApproved = stn.IsApproved,
                    _LastModifiedUtc = stn._LastModifiedUtc
                });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<StockTransferNote> pageable = new Pageable<StockTransferNote>(Query, Page - 1, Size);
            List<StockTransferNote> Data = pageable.Data.ToList();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public Tuple<List<StockTransferNote>, int, Dictionary<string, string>, List<string>> ReadModelByNotUser(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<StockTransferNote> Query = this.DbContext.StockTransferNotes;

            List<string> SearchAttributes = new List<string>()
            {
                "Code"
            };

            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "_CreatedUtc", "ReferenceNo", "ReferenceType", "SourceStorage", "TargetStorage", "IsApproved", "_LastModifiedUtc", "_CreatedBy"
            };

            Query = Query
                .Select(stn => new StockTransferNote
                {
                    Id = stn.Id,
                    Code = stn.Code,
                    _CreatedUtc = stn._CreatedUtc,
                    ReferenceNo = stn.ReferenceNo,
                    ReferenceType = stn.ReferenceType,
                    SourceStorageName = stn.SourceStorageName,
                    TargetStorageName = stn.TargetStorageName,
                    IsApproved = stn.IsApproved,
                    _LastModifiedUtc = stn._LastModifiedUtc,
                    _CreatedBy = stn._CreatedBy
                }).Where(w => !string.Equals(w._CreatedBy, Username));

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<StockTransferNote> pageable = new Pageable<StockTransferNote>(Query, Page - 1, Size);
            List<StockTransferNote> Data = pageable.Data.ToList();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public StockTransferNote MapToModel(StockTransferNoteViewModel viewModel)
        {
            StockTransferNote model = new StockTransferNote();
            PropertyCopier<StockTransferNoteViewModel, StockTransferNote>.Copy(viewModel, model);

            model.SourceStorageId = viewModel.SourceStorage._id;
            model.SourceStorageCode = viewModel.SourceStorage.code;
            model.SourceStorageName = viewModel.SourceStorage.name;
            model.TargetStorageId = viewModel.TargetStorage._id;
            model.TargetStorageCode = viewModel.TargetStorage.code;
            model.TargetStorageName = viewModel.TargetStorage.name;

            model.StockTransferNoteItems = new List<StockTransferNote_Item>();
            foreach (StockTransferNote_ItemViewModel stn in viewModel.StockTransferNoteItems)
            {
                StockTransferNote_Item stockTransferNoteItem = new StockTransferNote_Item();
                PropertyCopier<StockTransferNote_ItemViewModel, StockTransferNote_Item>.Copy(stn, stockTransferNoteItem);

                stockTransferNoteItem.ProductId = stn.Summary.productId;
                stockTransferNoteItem.ProductCode = stn.Summary.productCode;
                stockTransferNoteItem.ProductName = stn.Summary.productName;
                stockTransferNoteItem.UomId = stn.Summary.uomId;
                stockTransferNoteItem.UomUnit = stn.Summary.uom;
                stockTransferNoteItem.StockQuantity = stn.Summary.quantity;
                stockTransferNoteItem.TransferedQuantity = stn.TransferedQuantity!= null ? (double)stn.TransferedQuantity: 0;

                model.StockTransferNoteItems.Add(stockTransferNoteItem);
            }

            return model;
        }

        public StockTransferNoteViewModel MapToViewModel(StockTransferNote model)
        {
            StockTransferNoteViewModel viewModel = new StockTransferNoteViewModel();
            PropertyCopier<StockTransferNote, StockTransferNoteViewModel>.Copy(model, viewModel);

            StorageViewModel SourceStorage = new StorageViewModel()
            {
                _id = model.SourceStorageId,
                code = model.SourceStorageCode,
                name = model.SourceStorageName
            };

            StorageViewModel TargetStorage = new StorageViewModel()
            {
                _id = model.TargetStorageId,
                code = model.TargetStorageCode,
                name = model.TargetStorageName
            };

            viewModel.SourceStorage = SourceStorage;
            viewModel.TargetStorage = TargetStorage;

            viewModel.StockTransferNoteItems = new List<StockTransferNote_ItemViewModel>();
            if (model.StockTransferNoteItems != null)
            {
                foreach (StockTransferNote_Item stn in model.StockTransferNoteItems)
                {
                    StockTransferNote_ItemViewModel stockTransferNoteItemViewModel = new StockTransferNote_ItemViewModel();
                    PropertyCopier<StockTransferNote_Item, StockTransferNote_ItemViewModel>.Copy(stn, stockTransferNoteItemViewModel);

                    InventorySummaryViewModel Summary = new InventorySummaryViewModel()
                    {
                        productId = stn.ProductId,
                        productCode = stn.ProductCode,
                        productName = stn.ProductName,
                        quantity = stn.StockQuantity,
                        uomId = stn.UomId,
                        uom = stn.UomUnit
                    };

                    stockTransferNoteItemViewModel.Summary = Summary;

                    viewModel.StockTransferNoteItems.Add(stockTransferNoteItemViewModel);
                }
            }

            return viewModel;
        }

        public override async Task<StockTransferNote> ReadModelById(int id)
        {
            return await this.DbSet
                .Where(d => d.Id.Equals(id) && d._IsDeleted.Equals(false))
                .Include(d => d.StockTransferNoteItems)
                .FirstOrDefaultAsync();
        }

        public override async Task<int> CreateModel(StockTransferNote Model)
        {
            int Created = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    Model.Code = CodeGenerator.GenerateCode();
                    Created = await this.CreateAsync(Model);
                    CreateInventoryDocument(Model, "OUT", "CREATE");

                    transaction.Commit();
                }
                catch (ServiceValidationExeption e)
                {
                    transaction.Rollback();
                    throw new ServiceValidationExeption(e.ValidationContext, e.ValidationResults);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new ServiceValidationExeption(null, null);
                }
            }
            return Created;
        }

        public void CreateInventoryDocument(StockTransferNote Model, string Type, string Context)
        {
            StockTransferNoteViewModel ViewModel = MapToViewModel(Model);

            string inventoryDocumentURI = "inventory/inventory-documents";

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            /* Create Inventory Document */
            List<InventoryDocumentItemViewModel> inventoryDocumentItems = new List<InventoryDocumentItemViewModel>();

            foreach (StockTransferNote_ItemViewModel stni in ViewModel.StockTransferNoteItems)
            {
                InventoryDocumentItemViewModel inventoryDocumentItem = new InventoryDocumentItemViewModel
                {
                    productId = stni.Summary.productId,
                    productCode = stni.Summary.productCode,
                    productName = stni.Summary.productName,
                    quantity = stni.TransferedQuantity != null ? (double)stni.TransferedQuantity : 0,
                    uomId = stni.Summary.uomId,
                    uom = stni.Summary.uom
                };

                inventoryDocumentItems.Add(inventoryDocumentItem);
            }

            InventoryDocumentViewModel inventoryDocument = new InventoryDocumentViewModel
            {
                date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                referenceNo = Model.ReferenceNo,
                referenceType = Model.ReferenceType,
                type = Type,
                storageId = string.Equals(Context.ToUpper(), "CREATE") || string.Equals(Context.ToUpper(), "DELETE-SOURCE") ? Model.SourceStorageId : Model.TargetStorageId,
                storageCode = string.Equals(Context.ToUpper(), "CREATE") || string.Equals(Context.ToUpper(), "DELETE-SOURCE") ? Model.SourceStorageCode : Model.TargetStorageCode,
                storageName = string.Equals(Context.ToUpper(), "CREATE") || string.Equals(Context.ToUpper(), "DELETE-SOURCE") ? Model.SourceStorageName : Model.TargetStorageName,
                items = inventoryDocumentItems
            };

            var response = httpClient.PostAsync($"{APIEndpoint.Inventory}{inventoryDocumentURI}", new StringContent(JsonConvert.SerializeObject(inventoryDocument).ToString(), Encoding.UTF8, General.JsonMediaType)).Result;
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> UpdateIsApprove(int Id)
        {
            bool IsSuccessful = false;

            using (var Transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var stockTransferNote = await this.ReadModelById(Id);
                    stockTransferNote.IsApproved = true;
                    stockTransferNote._LastModifiedUtc = DateTime.UtcNow;
                    stockTransferNote._LastModifiedAgent = "Service";
                    stockTransferNote._LastModifiedBy = this.Username;
                    this.DbContext.SaveChanges();
                    CreateInventoryDocument(stockTransferNote, "IN", "APPROVE");

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
                    StockTransferNote stockTransferNote = await ReadModelById(Id);
                    Count = this.Delete(Id);

                    StockTransferNoteService stockTransferNoteService = ServiceProvider.GetService<StockTransferNoteService>();
                    StockTransferNote_ItemService stockTransferNoteItemService = ServiceProvider.GetService<StockTransferNote_ItemService>();
                    stockTransferNoteItemService.Username = this.Username;

                    HashSet<int> StockTransferNoteItems = new HashSet<int>(this.DbContext.StockTransferNoteItems.Where(p => p.StockTransferNote.Equals(Id)).Select(p => p.Id));

                    foreach (int item in StockTransferNoteItems)
                    {
                        await stockTransferNoteItemService.DeleteAsync(item);
                    }

                    CreateInventoryDocument(stockTransferNote, "IN", "DELETE-SOURCE");

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

        public override void OnCreating(StockTransferNote model)
        {
            base.OnCreating(model);

            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;

            StockTransferNote_ItemService stockTransferNoteItemService = ServiceProvider.GetService<StockTransferNote_ItemService>();
            stockTransferNoteItemService.Username = this.Username;

            foreach (StockTransferNote_Item stni in model.StockTransferNoteItems)
            {
                stockTransferNoteItemService.OnCreating(stni);
            }
        }

        public override void OnUpdating(int id, StockTransferNote model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(StockTransferNote model)
        {
            base.OnDeleting(model);
            model._DeletedAgent = "Service";
            model._DeletedBy = this.Username;
        }
    }
}
