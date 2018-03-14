using Com.Moonlay.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Inventory.Lib.Models.StockTransferNoteModel
{
    public class StockTransferNote : StandardEntity, IValidatableObject
    {
        public string Code { get; set; }
        public string ReferenceNo { get; set; }
        public string ReferenceType { get; set; }
        public string SourceStorageId { get; set; }
        public string SourceStorageCode { get; set; }
        public string SourceStorageName { get; set; }
        public string TargetStorageId { get; set; }
        public string TargetStorageCode { get; set; }
        public string TargetStorageName { get; set; }
        public bool IsApproved { get; set; }
        public virtual ICollection<StockTransferNote_Item> StockTransferNoteItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
