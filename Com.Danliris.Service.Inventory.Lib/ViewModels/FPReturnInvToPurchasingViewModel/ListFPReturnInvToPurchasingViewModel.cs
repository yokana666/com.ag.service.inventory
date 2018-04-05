using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels.FPReturnInvToPurchasingViewModel
{
    public class ListFPReturnInvToPurchasingViewModel
    {
        public int Id { get; set; }
        public string No { get; set; }
        public string UnitName { get; set; }
        public DateTime _CreatedUtc { get; set; }
        public double TotalQuantity { get; set; }
        public double TotalLength { get; set; }
        public string SupplierName { get; set; }
    }
}
