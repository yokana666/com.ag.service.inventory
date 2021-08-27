﻿using Com.Danliris.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels.InventoryWeavingViewModel
{
    public class InventoryWeavingDocumentViewModel : BasicViewModel, IValidatableObject
    {
        public DateTimeOffset date { get; set; }
        public string bonNo { get; set; }
        public string bonType { get; set; }


        public int storageId { get; set; }
        public string storageCode { get; set; }
        public string storageName { get; set; }
        public string remark { get; set; }
        public string type { get; set; }
        public List<InventoryWeavingDocumentItemViewModel> items { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.date.Equals(DateTimeOffset.MinValue) || this.date == null)
            {
                yield return new ValidationResult("Date is required", new List<string> { "date" });
            }
            if (this.bonType == null)
            {
                yield return new ValidationResult("Source is required", new List<string> { "destination" });
            }
            
        }
    }
}
