using Com.Danliris.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels
{
    public class ProductViewModel
    {
        public string _id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
}
