using Com.Danliris.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels.MaterialDistributionNoteViewModel
{
    public class MaterialDistributionNoteDetailViewModel : BasicViewModel
    {
        public int MaterialDistributionNoteItemId { get; set; }
        public int MaterialsRequestNoteItemId { get; set; }
        public ProductionOrderViewModel ProductionOrder { get; set; }
        public ProductViewModel Product { get; set; }
        public string Grade { get; set; }
        public double? Quantity { get; set; }
        public double? MaterialRequestNoteItemLength { get; set; }
        public double? DistributedLength { get; set; }
        public double? ReceivedLength { get; set; }
        public bool IsDisposition { get; set; }
        public bool IsCompleted { get; set; }
        public SupplierViewModel Supplier { get; set; }
    }
}
