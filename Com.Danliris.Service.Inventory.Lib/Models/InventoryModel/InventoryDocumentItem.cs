using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.Models.InventoryModel
{
    public class InventoryDocumentItem : StandardEntity
    {
        /* Product */
        public string ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public string UomUnit { get; set; }
        public string UomId { get; set; }

        public double Quantity { get; set; }
        public double StockPlanning { get; set; }
        public string ProductRemark { get; set; }

        public int InventoryDocumentId { get; set; }
        public virtual InventoryDocument InventoryDocument { get; set; }
    }
}

