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
        public noBon Bon { get; set; }
        public supplier Supplier { get; set; }
        public List<FpReturProInvDocsDetailsViewModel> Details { get; set; }

        public class supplier
        {
            public string _id { get; set; }
            public string name { get; set; }
        }

        public class noBon
        {
            public int Id { get; set; }
            public string No { get; set; }
            public string UnitName { get; set; }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            int Count = 0;

            if (this.Bon == null || (this.Bon.Id)==0)
                yield return new ValidationResult("Bon is required", new List<string> { "Bon" });

            if (this.Supplier == null || string.IsNullOrWhiteSpace(this.Supplier.name))
                yield return new ValidationResult("Supplier is required", new List<string> { "Supplier" });

            if (this.Details.Count.Equals(0))
            {
                yield return new ValidationResult("Detail Distribution Note Item is required", new List<string> { "Details" });
            }
            else
            {
                string detailsError = "[";

                foreach (FpReturProInvDocsDetailsViewModel data in this.Details)
                {

                    if (data.Product==null || string.IsNullOrWhiteSpace(data.Product.Name))
                    {
                        Count++;
                        detailsError += "{ Supplier: 'Supplier is required' }, ";
                    }

                    if ((data.Length > data.Product.Length ))
                    {
                        Count++;
                        detailsError += "{ Length: 'Length is incorrect' }, ";
                    }
                }

                detailsError += "]";

                if (Count > 0)
                {
                    yield return new ValidationResult(detailsError, new List<string> { "Details" });
                }
            }
        }
    }
}
