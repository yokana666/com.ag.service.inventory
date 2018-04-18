using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels
{
    public class UnitReceiptNoteViewModel
    {
        public string _id { get; set; }
        public string no { get; set; }
        public UnitViewModel unit { get; set; }
        public SupplierViewModel supplier { get; set; }
        public List<Item> items { get; set; }
        public class Item
        {
            public ProductViewModel product { get; set; }
        }

    }
}
