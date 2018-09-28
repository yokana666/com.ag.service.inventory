using Com.Danliris.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels.InventoryViewModel
{
    public class InventoryMovementViewModel : BasicViewModel
    {
        public string no { get; set; }
        public DateTimeOffset date { get; set; }
        public string referenceNo { get; set; }
        public string referenceType { get; set; }
        public int productId { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }

        public string uomUnit { get; set; }
        public int uomId { get; set; }

        public int storageId { get; set; }
        public string storageCode { get; set; }
        public string storageName { get; set; }

        public double stockPlanning { get; set; }

        public double before { get; set; }
        public double quantity { get; set; }
        public double after { get; set; }
        public string remark { get; set; }
        public string type { get; set; }
    }
}
