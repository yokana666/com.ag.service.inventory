
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.Models
{
   public class FpReturProInvDocs : StandardEntity, IValidatableObject
    {
        public string Code { get; set; }
        public string NoBon { get; set; } // MaterialDistributionNote No
        public int NoBonId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public ICollection<FpReturProInvDocsDetails> Details { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
