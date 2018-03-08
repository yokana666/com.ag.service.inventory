using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.Models.MaterialDistributionNoteModel;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Inventory.Lib.Services.MaterialDistributionNoteService
{
    public class MaterialDistributionNoteItemService : BasicService<InventoryDbContext, MaterialDistributionNoteItem>
    {
        public MaterialDistributionNoteItemService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<MaterialDistributionNoteItem>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            throw new NotImplementedException();
        }

        public override void OnCreating(MaterialDistributionNoteItem model)
        {
            base.OnCreating(model);
            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;

            MaterialDistributionNoteDetailService materialDistributionNoteDetailService = ServiceProvider.GetService<MaterialDistributionNoteDetailService>();
            materialDistributionNoteDetailService.Username = this.Username;

            foreach (MaterialDistributionNoteDetail mdnd in model.MaterialDistributionNoteDetails)
            {
                materialDistributionNoteDetailService.OnCreating(mdnd);
            }
        }

        public override void OnUpdating(int id, MaterialDistributionNoteItem model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(MaterialDistributionNoteItem model)
        {
            base.OnDeleting(model);
            model._DeletedAgent = "Service";
            model._DeletedBy = this.Username;
        }
    }
}
