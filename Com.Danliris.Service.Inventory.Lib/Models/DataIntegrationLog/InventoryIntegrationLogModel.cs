using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Inventory.Lib.Models.DataIntegrationLog
{
    public class InventoryIntegrationLogModel : StandardEntity, IValidatableObject
    {
        public DateTimeOffset LastIntegrationDate { get; set; }
        [MaxLength(100)]
        public string Remark { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
