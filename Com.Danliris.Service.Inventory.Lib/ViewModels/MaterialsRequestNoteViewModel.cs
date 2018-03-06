using Com.Danliris.Service.Inventory.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels
{
    public class MaterialsRequestNoteViewModel : BasicViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public UnitViewModel Unit { get; set; }
        public string RequestType { get; set; }
        public string Remark { get; set; }
        public List<MaterialsRequestNote_ItemViewModel> MaterialsRequestNote_Items { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Unit == null || string.IsNullOrWhiteSpace(this.Unit._id))
                yield return new ValidationResult("Unit harus diisi", new List<string> { "UnitId" });
            if (string.IsNullOrWhiteSpace(this.RequestType))
                yield return new ValidationResult("Jenis Request harus diisi", new List<string> { "RequestType" });

            int Count = 0;
            string materialsRequestNote_ItemsError = "[";

            if (this.MaterialsRequestNote_Items == null || this.MaterialsRequestNote_Items.Count.Equals(0))
                yield return new ValidationResult("Tabel dibawah harus diisi", new List<string> { "MaterialsRequestNote_Items" });
            else
            {
                foreach (MaterialsRequestNote_ItemViewModel materialsRequestNote_Item in this.MaterialsRequestNote_Items)
                {
                    materialsRequestNote_ItemsError += "{";
                    if (materialsRequestNote_Item.ProductionOrder == null || string.IsNullOrWhiteSpace(materialsRequestNote_Item.ProductionOrder._id))
                    {
                        Count++;
                        materialsRequestNote_ItemsError += "ProductionOrderId: 'Surat Perintah Produksi harus diisi', ";
                    }
                    if (materialsRequestNote_Item.Product == null || string.IsNullOrWhiteSpace(materialsRequestNote_Item.Product._id))
                    {
                        Count++;
                        materialsRequestNote_ItemsError += "ProductId: 'Barang harus diisi', ";
                    }
                    if (string.IsNullOrWhiteSpace(materialsRequestNote_Item.Grade))
                    {
                        Count++;
                        materialsRequestNote_ItemsError += "Grade: 'Grade harus diisi', ";
                    }
                    if (materialsRequestNote_Item.Length == null)
                    {
                        Count++;
                        materialsRequestNote_ItemsError += "Length: 'Panjang harus diisi', ";
                    }
                    else if (materialsRequestNote_Item.Length <= 0)
                    {
                        Count++;
                        materialsRequestNote_ItemsError += "Length: 'Panjang harus lebih besar dari 0', ";
                    }
                    else if (materialsRequestNote_Item.Length > (materialsRequestNote_Item.ProductionOrder.orderQuantity * 1.05))
                    {
                        Count++;
                        materialsRequestNote_ItemsError += "Length: 'Panjang total tidak boleh lebih dari 105% toleransi', ";
                    }
                    if (materialsRequestNote_Item.ProductionOrder != null)
                    {
                        int count = MaterialsRequestNote_Items
                            .Count(c => string.Equals(c.ProductionOrder._id, materialsRequestNote_Item.ProductionOrder._id));

                        if (count > 1)
                        {
                            Count++;
                            materialsRequestNote_ItemsError += "ProductionOrderId: 'Surat Perintah Produksi tidak boleh duplikat', ";
                        }
                    }
                    materialsRequestNote_ItemsError += "}, ";
                }
            }
            materialsRequestNote_ItemsError += "]";
            if (Count > 0)
            {
                yield return new ValidationResult(materialsRequestNote_ItemsError, new List<string> { "MaterialsRequestNote_Items" });
            }
        }
    }
}
