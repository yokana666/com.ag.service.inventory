using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.Interfaces;
using Com.Danliris.Service.Inventory.Lib.Models;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Danliris.Service.Inventory.Lib.ViewModels.InventoryDocumentViewModel;
using Com.Moonlay.NetCore.Lib;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;



namespace Com.Danliris.Service.Inventory.Lib.Services
{
    public class FpReturProInvDocsService : BasicService<InventoryDbContext, FpReturProInvDocs>, IMap<FpReturProInvDocs, FpReturProInvDocsViewModel>
    {
        public FpReturProInvDocsService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public FpReturProInvDocs MapToModel(FpReturProInvDocsViewModel viewModel)
        {
            FpReturProInvDocs model = new FpReturProInvDocs();
            PropertyCopier<FpReturProInvDocsViewModel, FpReturProInvDocs>.Copy(viewModel, model);

            model.NoBon = viewModel.Bon.no;
            model.NoBonId = viewModel.Bon._id;
            model.UnitName = viewModel.Bon.unitName;
            model.SupplierId = viewModel.Supplier._id;
            model.SupplierName = viewModel.Supplier.name;
            model.SupplierCode = viewModel.Supplier.code;
            model.ProductId = viewModel.Product.Id;
            model.ProductCode = viewModel.Product.Code;
            model.ProductName = viewModel.Product.Name;
            model.Shift = viewModel.Shift;
            model.MachineName = viewModel.Machine.name;
            model.MachineId = viewModel.Machine._id;
            model.MachineCode = viewModel.Machine.code;
            model.Operator = viewModel.Operator;
            model.Remark = viewModel.Remark;

            model.Details = new List<FpReturProInvDocsDetails>();

            foreach (FpReturProInvDocsDetailsViewModel data in viewModel.Details)
            {
                FpReturProInvDocsDetails detail = new FpReturProInvDocsDetails();
                //detail.SupplierId = viewModel.Supplier._id;
                detail.ProductId = viewModel.Product.Id;
                detail.ProductCode = viewModel.Product.Code;
                detail.ProductName = viewModel.Product.Name;
                //detail.Quantity = data.Quantity;
                detail.Length = data.Length;
                detail.Remark = data.Remark;
                detail.Grade = data.Grade;
                detail.Retur = data.Retur;
                detail.LengthBeforeReGrade = data.LengthBeforeReGrade;
                model.Details.Add(detail);

            }

            return model;
        }

        public FpReturProInvDocsViewModel MapToViewModel(FpReturProInvDocs model)
        {
            FpReturProInvDocsViewModel viewModel = new FpReturProInvDocsViewModel();
            PropertyCopier<FpReturProInvDocs, FpReturProInvDocsViewModel>.Copy(model, viewModel);

            viewModel.Details = new List<FpReturProInvDocsDetailsViewModel>();
            viewModel.Bon = new FpReturProInvDocsViewModel.noBon();
            viewModel.Bon._id = model.NoBonId;
            viewModel.Bon.no = model.NoBon;
            viewModel.Bon.unitName = model.UnitName;

            viewModel.Supplier = new FpReturProInvDocsViewModel.supplier();
            viewModel.Supplier.name = model.SupplierName;
            viewModel.Supplier._id = model.SupplierId;
            viewModel.Supplier.code = model.SupplierCode;
            viewModel._CreatedUtc = model._CreatedUtc;

            viewModel.Product = new FpReturProInvDocsViewModel.product();
            viewModel.Product.Name = model.ProductName;
            viewModel.Product.Id = model.ProductId;
            viewModel.Product.Code = model.SupplierCode;

            viewModel.Shift = model.Shift;
            viewModel.Remark = model.Remark;
            viewModel.Operator = model.Operator;

            viewModel.Machine = new FpReturProInvDocsViewModel.machine();
            viewModel.Machine.name = model.MachineName;
            viewModel.Machine.code = model.MachineCode;
            viewModel.Machine._id = model.MachineId;

            foreach (FpReturProInvDocsDetails data in model.Details)
            {
                FpReturProInvDocsDetailsViewModel detail = new FpReturProInvDocsDetailsViewModel();
                detail.Product = new FpReturProInvDocsDetailsViewModel.product();
                detail.Product.Id = data.ProductId;
                detail.Product.Name = data.ProductName;
                detail.Product.Length = data.Length;
                //detail.Product.Quantity = data.Quantity;
                //detail.Quantity = data.Quantity;
                detail.Remark = data.Remark;
                detail.Length = data.Length;
                detail.Grade = data.Grade;
                detail.Retur = data.Retur;
                detail.LengthBeforeReGrade = data.LengthBeforeReGrade;
                viewModel.Details.Add(detail);
            }
            return viewModel;
        }

        public override Tuple<List<FpReturProInvDocs>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {

            IQueryable<FpReturProInvDocs> Query = this.DbContext.fpRegradingResultDocs;

            List<string> SearchAttributes = new List<string>()
                {
                    "Code", "NoBon", "SupplierName","ProductName"
                };
            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
                {
                    "Id", "Code", "Bon", "Supplier", "Product", "Details", "Machine", "Remark", "Operator"
                };
            Query = Query
                .Select(o => new FpReturProInvDocs
                {
                    Id = o.Id,
                    Code = o.Code,
                    NoBon = o.NoBon,
                    NoBonId = o.NoBonId,
                    UnitName = o.UnitName,
                    SupplierId = o.SupplierId,
                    SupplierName = o.SupplierName,
                    SupplierCode = o.SupplierCode,
                    ProductId = o.ProductId,
                    ProductName = o.ProductName,
                    ProductCode = o.ProductCode,
                    Remark = o.Remark,
                    Operator = o.Operator,
                    MachineCode = o.MachineCode,
                    MachineId = o.MachineId,
                    MachineName = o.MachineName,
                    Shift = o.Shift,
                    _CreatedUtc = o._CreatedUtc,
                    Details = o.Details.Select(p => new FpReturProInvDocsDetails { FpReturProInvDocsId = p.FpReturProInvDocsId, ProductName = p.ProductName, ProductCode = p.ProductCode, ProductId = p.ProductId, Id = o.Id, Length = p.Length, Remark = p.Remark, Code = p.Code, Grade = p.Grade, Retur = p.Retur }).Where(i => i.FpReturProInvDocsId.Equals(o.Id)).ToList()
                });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<FpReturProInvDocs> pageable = new Pageable<FpReturProInvDocs>(Query, Page - 1, Size);
            List<FpReturProInvDocs> Data = pageable.Data.ToList<FpReturProInvDocs>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }
        public override async Task<FpReturProInvDocs> ReadModelById(int id)
        {
            return await this.DbSet
                .Where(d => d.Id.Equals(id) && d._IsDeleted.Equals(false))
                .Include(d => d.Details)
                .FirstOrDefaultAsync();
        }

        public void CreateInventoryDocument(FpReturProInvDocs Model, string Type)
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
            InventoryDocumentItemViewModel inventoryDocumentItem = new InventoryDocumentItemViewModel();
            double TotalLength = 0;
            foreach (FpReturProInvDocsDetails o in Model.Details)
            {
                TotalLength += o.Length;
                //inventoryDocumentItem = new InventoryDocumentItemViewModel
                //{
                //productId = o.ProductId,
                //productCode = o.ProductCode,
                //productName = o.ProductName,
                //quantity = o.Length,
                //uomId = uom["_id"].ToString(),
                //uom = uom["unit"].ToString()
                //};
                //inventoryDocumentItems.Add(inventoryDocumentItem);
            }

            inventoryDocumentItem.productId = Model.ProductId;
            inventoryDocumentItem.productCode = Model.ProductCode;
            inventoryDocumentItem.productName = Model.ProductName;
            inventoryDocumentItem.quantity = TotalLength;
            inventoryDocumentItem.uomId = uom["_id"].ToString();
            inventoryDocumentItem.uom = uom["unit"].ToString();

            inventoryDocumentItems.Add(inventoryDocumentItem);

            InventoryDocumentViewModel inventoryDocument = new InventoryDocumentViewModel
            {
                date = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                referenceNo = Model.Code,
                referenceType = "Bon Hasil Re-grading",
                type = Type,
                storageId = storage["_id"].ToString(),
                storageCode = storage["code"].ToString(),
                storageName = storage["name"].ToString(),
                items = inventoryDocumentItems
            };

            var response = httpClient.PostAsync($"{APIEndpoint.Inventory}{inventoryDocumentURI}", new StringContent(JsonConvert.SerializeObject(inventoryDocument).ToString(), Encoding.UTF8, General.JsonMediaType)).Result;
            response.EnsureSuccessStatusCode();
        }

        public async Task<FpReturProInvDocs> CustomCodeGenerator(FpReturProInvDocs Model)
        {
            var unit = string.Equals(Model.UnitName.ToUpper(), "PRINTING") ? "PR" : "FS";
            var lastData = await this.DbSet.Where(w => string.Equals(w.UnitName, Model.UnitName)).OrderByDescending(o => o._CreatedUtc).FirstOrDefaultAsync();

            DateTime Now = DateTime.Now;
            string Year = Now.ToString("yy");
            //string Month = Now.ToString("MM");

            if (lastData == null)
            {
                Model.AutoIncrementNumber = 1;
                string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                Model.Code = $"KB{unit}{Year}{Number}";
            }
            else
            {
                if (lastData._CreatedUtc.Year < Now.Year)
                {
                    Model.AutoIncrementNumber = 1;
                    string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                    Model.Code = $"KB{unit}{Year}{Number}";
                }
                else
                {
                    Model.AutoIncrementNumber = lastData.AutoIncrementNumber + 1;
                    string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                    Model.Code = $"KB{unit}{Year}{Number}";
                }
            }

            return Model;
        }

        public override async Task<int> CreateModel(FpReturProInvDocs Model)
        {
            int Created = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    Model = await this.CustomCodeGenerator(Model);
                    Created = await this.CreateAsync(Model);
                    CreateInventoryDocument(Model, "IN");

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

        public override void OnCreating(FpReturProInvDocs model)
        {
            //do
            //{
            //    model.Code = CodeGenerator.GenerateCode();
            //}
            //while (this.DbSet.Any(d => d.Code.Equals(model.Code)));
            try
            {
                base.OnCreating(model);
                model._CreatedAgent = "Service";
                model._CreatedBy = this.Username;
                model._LastModifiedAgent = "Service";
                model._LastModifiedBy = this.Username;

                FpReturProInvDocsDetailsService fpReturProInvDocsDetailsService = ServiceProvider.GetService<FpReturProInvDocsDetailsService>();
                fpReturProInvDocsDetailsService.Username = this.Username;
                foreach (FpReturProInvDocsDetails data in model.Details)
                {

                    fpReturProInvDocsDetailsService.OnCreating(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

        }

        public override async Task<int> DeleteModel(int Id)
        {
            int Count = 0;

            using (var Transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    FpReturProInvDocs fpReturProInvDocs = await ReadModelById(Id);
                    Count = this.Delete(Id);


                    FpReturProInvDocsDetailsService fpReturProInvDocsDetailsService = ServiceProvider.GetService<FpReturProInvDocsDetailsService>();
                    fpReturProInvDocsDetailsService.Username = this.Username;


                    HashSet<int> fpReturProInvDocsDetails = new HashSet<int>(this.DbContext.fpRegradingResultDocsDetails.Where(p => p.FpReturProInvDocsId.Equals(Id)).Select(p => p.Id));

                    foreach (int detail in fpReturProInvDocsDetails)
                    {
                        await fpReturProInvDocsDetailsService.DeleteAsync(detail);
                    }

                    CreateInventoryDocument(fpReturProInvDocs, "OUT");

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

        public override void OnUpdating(int id, FpReturProInvDocs model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(FpReturProInvDocs model)
        {
            base.OnDeleting(model);
            model._DeletedAgent = "Service";
            model._DeletedBy = this.Username;
        }
    }
}
