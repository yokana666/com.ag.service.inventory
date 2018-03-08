using Com.Danliris.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels.MaterialDistributionNoteViewModel
{
    public class MaterialDistributionNoteViewModel : BasicViewModel, IValidatableObject
    {
        public string No { get; set; }
        public UnitViewModel Unit { get; set; }
        public string Type { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDisposition { get; set; }
        public List<MaterialDistributionNoteItemViewModel> MaterialDistributionNoteItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            int Count = 0;

            if (this.Unit == null || string.IsNullOrWhiteSpace(this.Unit._id))
                yield return new ValidationResult("Unit is required", new List<string> { "Unit" });

            if (string.IsNullOrWhiteSpace(this.Type))
                yield return new ValidationResult("Type is required", new List<string> { "Type" });

            if (MaterialDistributionNoteItems.Count.Equals(0))
            {
                yield return new ValidationResult("Material Distribution Note Item is required", new List<string> { "MaterialDistributionNoteItemCollection" });
            }
            else
            {
                string materialDistributionNoteItemError = "[";

                foreach (MaterialDistributionNoteItemViewModel mdni in this.MaterialDistributionNoteItems)
                {
                    if (string.IsNullOrWhiteSpace(mdni.MaterialRequestNoteCode))
                    {
                        Count++;
                        materialDistributionNoteItemError += "{ MaterialRequestNote: 'SPB is required' }, ";
                    }
                    else
                    {
                        int CountDetail = 0;

                        string materialDistributionNoteDetailError = "[";

                        foreach (MaterialDistributionNoteDetailViewModel mdnd in mdni.MaterialDistributionNoteDetails)
                        {
                            materialDistributionNoteDetailError += "{";

                            if(mdnd.Quantity == null)
                            {
                                CountDetail++;
                                materialDistributionNoteDetailError += "Quantity: 'Quantity is required', ";
                            }
                            else if(mdnd.Quantity <= 0)
                            {
                                CountDetail++;
                                materialDistributionNoteDetailError += "Quantity: 'Quantity must be greater than zero', ";
                            }

                            if (mdnd.ReceivedLength == null)
                            {
                                CountDetail++;
                                materialDistributionNoteDetailError += "ReceivedLength: 'Received Length is required', ";
                            }
                            else if (mdnd.ReceivedLength <= 0)
                            {
                                CountDetail++;
                                materialDistributionNoteDetailError += "ReceivedLength: 'Received Length must be greater than zero', ";
                            }

                            if (mdnd.Supplier == null || string.IsNullOrWhiteSpace(mdnd.Supplier._id))
                            {
                                CountDetail++;
                                materialDistributionNoteDetailError += "Supplier: 'Supplier is required', ";
                            }

                            materialDistributionNoteDetailError += "}, ";
                        }

                        materialDistributionNoteDetailError += "]";

                        if (CountDetail > 0)
                        {
                            Count++;
                            materialDistributionNoteItemError += string.Concat("{ MaterialDistributionNoteDetails: ", materialDistributionNoteDetailError, " }, ");
                        }
                        else
                        {
                            materialDistributionNoteItemError += "{}, ";
                        }
                    }
                }

                materialDistributionNoteItemError += "]";

                if (Count > 0)
                {
                    yield return new ValidationResult(materialDistributionNoteItemError, new List<string> { "MaterialDistributionNoteItems" });
                }
            }
        }
    }
}
