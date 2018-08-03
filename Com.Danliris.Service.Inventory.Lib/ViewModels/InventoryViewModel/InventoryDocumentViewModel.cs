using Com.Danliris.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels.InventoryViewModel
{
    public class InventoryDocumentViewModel : BasicViewModel
    {
        public string code { get; set; }
        public DateTimeOffset date { get; set; }
        public string referenceNo { get; set; }
        public string referenceType { get; set; }
        public string type { get; set; }
        public string storageId { get; set; }
        public string storageCode { get; set; }
        public string storageName { get; set; }
        public List<InventoryDocumentItemViewModel> items { get; set; }
        public string remark { get; set; }
    }
}
