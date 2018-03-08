using Com.Danliris.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels
{
    public class FpReturProInvDocsDetailsViewModel : BasicViewModel, IValidatableObject
    {
        public supplier Supplier { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double Length { get; set; }
        public string Remark { get; set; }

        public class supplier
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Length { get; set; }
            public double Quantity { get; set; }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
