using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels
{
    public class FpReturProInvDocsViewModel : BasicViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string No { get; set; }
        public supplier Supplier { get; set; }
        public List<FpReturProInvDocsDetailsViewModel> Details { get; set; }

        public class supplier
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
