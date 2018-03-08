using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.Models
{
    public class FpReturProInvDocsDetails : StandardEntity, IValidatableObject
    {
        public int FpReturProInvDocsId { get; set; } // header Id as foreign key
        public int SupplierId { get; set; } // header Id supplier as foreign key
        public string Code { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double Length { get; set; }
        public string Remark { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
