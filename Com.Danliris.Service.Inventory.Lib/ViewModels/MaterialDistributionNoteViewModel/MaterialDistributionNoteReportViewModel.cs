using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels.MaterialDistributionNoteViewModel
{
    public class MaterialDistributionNoteReportViewModel
    {
        public DateTime _LastModifiedUtc { get; set; }
        public string No { get; set; }
        public string Type { get; set; }
        public string MaterialRequestNoteNo { get; set; }
        public string ProductionOrderNo { get; set; }
        public string ProductName { get; set; }
        public string Grade { get; set; }
        public double Quantity { get; set; }
        public double Length { get; set; }
        public string SupplierName { get; set; }
        public bool IsDisposition { get; set; }
        public bool IsApproved { get; set; }
    }
}
