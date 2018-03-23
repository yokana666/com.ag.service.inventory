using Com.Danliris.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Inventory.Lib.ViewModels.MaterialDistributionNoteViewModel
{
    public class MaterialDistributionNoteItemViewModel : BasicViewModel
    {
        public int MaterialDistributionNoteId { get; set; }
        public int MaterialRequestNoteId { get; set; }
        public string MaterialRequestNoteCode { get; set; }
        public DateTime MaterialRequestNoteCreatedDateUtc { get; set; }
        public List<MaterialDistributionNoteDetailViewModel> MaterialDistributionNoteDetails { get; set; }
    }
}