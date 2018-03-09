using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.Interfaces;
using Com.Danliris.Service.Inventory.Lib.Models;
using Com.Danliris.Service.Inventory.Lib.ViewModels;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            model.SupplierName = viewModel.Supplier.Name;

            model.NoBon = viewModel.Bon.Code;
            model.NoBonId = viewModel.Bon.Id;

            model.Details = new List<FpReturProInvDocsDetails>();

            foreach (FpReturProInvDocsDetailsViewModel data in viewModel.Details)
            {
                FpReturProInvDocsDetails detail = new FpReturProInvDocsDetails();

                detail.SupplierId = data.Supplier.Id;
                detail.ProductName = data.ProductName;
                detail.Quantity = data.Quantity;
                detail.Length = data.Length;
                detail.Remark = data.Remark;
        
                model.Details.Add(detail);

            }

            return model;
        }

        public FpReturProInvDocsViewModel MapToViewModel(FpReturProInvDocs model)
        {
            FpReturProInvDocsViewModel viewModel = new FpReturProInvDocsViewModel();
            PropertyCopier<FpReturProInvDocs, FpReturProInvDocsViewModel>.Copy(model, viewModel);

            viewModel.Details = new List<FpReturProInvDocsDetailsViewModel>();

            viewModel.Bon.Id = model.NoBonId;
            viewModel.Bon.Code = model.NoBon;

            viewModel.Supplier.Name = model.SupplierName;

            foreach (FpReturProInvDocsDetails data in model.Details)
            {
                FpReturProInvDocsDetailsViewModel detail = new FpReturProInvDocsDetailsViewModel();
                detail.Supplier.Id = data.Id;
                detail.Supplier.Name = data.ProductName;
                detail.Supplier.Length = data.Length;
                detail.Supplier.Quantity = data.Quantity;
                detail.ProductName = data.ProductName;
                detail.Quantity = data.Quantity;
                detail.Remark = data.Remark;
                detail.Length = data.Length;
            }
            return viewModel;
        }

        public override Tuple<List<FpReturProInvDocs>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {

            IQueryable<FpReturProInvDocs> Query = this.DbContext.FpReturProInvDocs;

            List<string> SearchAttributes = new List<string>()
                {
                    "Code", "NoBon", "SupplierName"
                };
            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
                {
                    "Id", "Code", "Bon", "Supplier","Details"
                };
            Query = Query
                .Select(o => new FpReturProInvDocs
                {
                    Id = o.Id,
                    Code = o.Code,
                    Details = o.Details.Select(p => new FpReturProInvDocsDetails { FpReturProInvDocsId = p.FpReturProInvDocsId,ProductName = p.ProductName }).Where(i => i.FpReturProInvDocsId.Equals(o.Id)).ToList()
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

    }
}
