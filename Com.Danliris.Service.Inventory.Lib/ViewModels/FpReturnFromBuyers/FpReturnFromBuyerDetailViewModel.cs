using Com.Danliris.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels.FpReturnFromBuyers
{
    public class FpReturnFromBuyerDetailViewModel : BasicViewModel, IValidatableObject
    {
        public IList<FpReturnFromBuyerItemViewModel> Items { get; set; }
        public ProductionOrderViewModel ProductionOrder { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
