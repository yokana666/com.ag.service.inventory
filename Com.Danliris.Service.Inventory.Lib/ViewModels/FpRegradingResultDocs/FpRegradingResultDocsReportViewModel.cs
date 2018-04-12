using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels.FpRegradingResultDocs
{
    public class FpRegradingResultDocsReportViewModel
    {
        public DateTime _LastModifiedUtc { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string UnitName { get; set; }
        public DateTime _CreatedUtc { get; set; }
        public double TotalQuantity { get; set; }
        public double TotalLength { get; set; }
        public bool IsReturn { get; set; }
        public bool IsReturnedToPurchasing { get; set; }
        public string SupplierName { get; set; }
    }
}
