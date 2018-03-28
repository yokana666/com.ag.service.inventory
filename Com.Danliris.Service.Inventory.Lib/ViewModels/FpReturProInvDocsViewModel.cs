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
            public string code { get; set; }
        }

        public class noBon
        {
            public string _id { get; set; }
            public string no { get; set; }
            public string unitName { get; set; }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {


            if (this.Bon == null || (this.Bon._id) == "")
                yield return new ValidationResult("Bon harus di isi", new List<string> { "Bon" });

            //if (this.Supplier == null || string.IsNullOrWhiteSpace(this.Supplier.name))
            //    yield return new ValidationResult("Supplier harus di isi", new List<string> { "Supplier" });
            int Count = 0;
            string detailsError = "[";
            if (this.Details.Count.Equals(0) || this.Details == null)
            {
                yield return new ValidationResult("Details is required", new List<string> { "Details" });
            }
            else
            {
                List<string> temp = new List<string>();
                foreach (FpReturProInvDocsDetailsViewModel data in this.Details)
                {
                    detailsError += "{";
                    if (data.Product == null || string.IsNullOrWhiteSpace(data.Product.Name))
                    {
                        Count++;
                        detailsError += "Product: 'Barang harus di isi',";
                    }

                    //if (data.Quantity.Equals(0))
                    //{
                    //    Count++;
                    //    detailsError += "Quantity: '(Piece) harus di isi',";
                    //}
                    if (data.Product != null)
                    {

                        if ((data.Length > data.Product.Length) || data.Length.Equals(0))
                        {
                            Count++;
                            detailsError += "Length: 'Panjang harus lebih kecil / harus di isi',";
                        }

                        if (data.Product.Name != null || data.Product.Name != "")
                        {
                            if (!(temp.Contains(data.Product.Name)))
                            {
                                temp.Add(data.Product.Name);
                            }
                            else
                            {
                                Count++;
                                detailsError += "Product: 'Barang tidak boleh sama',";
                            }
                        }

                    }
                    else if (data.Length.Equals(0))
                    {
                        Count++;
                        detailsError += "Length: 'Panjang harus di isi',";
                    }
                    detailsError += "},";

                }

                detailsError += "]";
            }
            if (Count > 0)
            {
                yield return new ValidationResult(detailsError, new List<string> { "Details" });
            }
        }
    }
}
