using Com.Danliris.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels.FpReturnFromBuyers
{
    public class FpReturnFromBuyerViewModel : BasicViewModel, IValidatableObject
    {
        public BuyerViewModel Buyer { get; set; }
        public string CodeProduct { get; set; }
        public string CoverLetter { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Destination { get; set; }
        public IList<FpReturnFromBuyerDetailViewModel> Details { get; set; }
        public string SpkNo { get; set; }
        public StorageViewModel Storage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }

    public class BuyerViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
