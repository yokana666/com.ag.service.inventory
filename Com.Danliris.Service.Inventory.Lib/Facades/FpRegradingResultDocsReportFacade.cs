using Com.Danliris.Service.Inventory.Lib.Models;
using Com.Danliris.Service.Inventory.Lib.Services;
using Com.Danliris.Service.Inventory.Lib.ViewModels.FpRegradingResultDocs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using Com.Moonlay.NetCore.Lib;

namespace Com.Danliris.Service.Inventory.Lib.Facades
{
    public class FpRegradingResultDocsReportFacade
    {
        private readonly FpRegradingResultDocsService fpRegradingResultDocsService;

        public FpRegradingResultDocsReportFacade(FpRegradingResultDocsService fpRegradingResultDocsService)
        {
            this.fpRegradingResultDocsService = fpRegradingResultDocsService;
        }

        public Tuple<List<FpRegradingResultDocsReportViewModel>, int> GetReport(string unitName, string code, string productName, bool? isReturn, bool? isReturnedToPurchasing, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int page, int size, string Order)
        {
            IQueryable<FpRegradingResultDocs> Query = this.fpRegradingResultDocsService.DbSet;

            DateTimeOffset dateFromFilter = (dateFrom == null ? new DateTime(1970, 1, 1) : dateFrom.Value.Date);
            DateTimeOffset dateToFilter = (dateTo == null ? DateTimeOffset.UtcNow.Date : dateTo.Value.Date);

            Query = Query
                .Where(p => p._IsDeleted == false &&
                    p.UnitName == (string.IsNullOrWhiteSpace(unitName) ? p.UnitName : unitName) &&
                    p.Code == (string.IsNullOrWhiteSpace(code) ? p.Code : code) &&
                    p.ProductName == (string.IsNullOrWhiteSpace(productName) ? p.ProductName : productName) &&
                    p.IsReturn == (isReturn == null ? p.IsReturn : isReturn) &&
                    p.IsReturnedToPurchasing == (isReturnedToPurchasing == null ? p.IsReturnedToPurchasing : isReturnedToPurchasing) &&
                    p.Date.Date >= dateFromFilter &&
                    p.Date.Date <= dateToFilter
                );

            Query = Query
               .Select(s => new FpRegradingResultDocs
               {
                   Id = s.Id,
                   Code = s.Code,
                   UnitName = s.UnitName,
                   ProductName = s.ProductName,
                   SupplierName = s.SupplierName,
                   _CreatedUtc = s._CreatedUtc,
                   _LastModifiedUtc = s._LastModifiedUtc,
                   IsReturn = s.IsReturn,
                   IsReturnedToPurchasing = s.IsReturnedToPurchasing,
                   Details = s.Details.Select(p => new FpRegradingResultDocsDetails { FpReturProInvDocsId = p.FpReturProInvDocsId, Length = p.Length }).Where(i => i.FpReturProInvDocsId.Equals(s.Id)).ToList()
               });

            #region Order

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            if (OrderDictionary.Count.Equals(0))
            {
                Query = Query.OrderByDescending(b => b._LastModifiedUtc);
            }
            else
            {
                string Key = OrderDictionary.Keys.First();
                string OrderType = OrderDictionary[Key];

                Query = Query.OrderBy(string.Concat(Key, " ", OrderType));
            }

            #endregion Order

            #region Paging

            Pageable<FpRegradingResultDocs> pageable = new Pageable<FpRegradingResultDocs>(Query, page - 1, size);
            List<FpRegradingResultDocs> Data = pageable.Data.ToList<FpRegradingResultDocs>();
            int TotalData = pageable.TotalCount;

            #endregion Paging

            List<FpRegradingResultDocsReportViewModel> list = Data
                    .GroupBy(d => new { d.Id, d.Code, d.UnitName, d.ProductName, d.SupplierName, d._CreatedUtc, d.IsReturn, d.IsReturnedToPurchasing })
                    .Select(s => new FpRegradingResultDocsReportViewModel
                    {
                        Code = s.First().Code,
                        ProductName = s.First().ProductName,
                        UnitName = s.First().UnitName,
                        _CreatedUtc = s.First()._CreatedUtc,
                        TotalQuantity = s.Sum(d => d.Details.Count()),
                        TotalLength = s.Sum(d => d.Details.Sum(p => p.Length)),
                        IsReturn = s.First().IsReturn,
                        IsReturnedToPurchasing = s.First().IsReturnedToPurchasing,
                        SupplierName = s.First().SupplierName
                    }).ToList();

            return Tuple.Create(list, TotalData);
        }
    }
}
