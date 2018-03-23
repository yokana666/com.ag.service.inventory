using Com.Danliris.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels
{
    public class ProductionOrderViewModel
    {
        public string _id { get; set; }
        public string orderNo { get; set; }
        public double? orderQuantity { get; set; }
        public bool isCompleted { get; set; }
        public double? distributedQuantity { get; set; }
        public OrderTypeViewModel orderType { get; set; }
    }
}
