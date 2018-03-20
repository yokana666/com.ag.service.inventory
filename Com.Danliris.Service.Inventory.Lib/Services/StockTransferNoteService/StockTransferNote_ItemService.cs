using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.Models.StockTransferNoteModel;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Inventory.Lib.Services.StockTransferNoteService
{
    public class StockTransferNote_ItemService : BasicService<InventoryDbContext, StockTransferNote_Item>
    {
        public StockTransferNote_ItemService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<StockTransferNote_Item>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            throw new NotImplementedException();
        }

        public override void OnCreating(StockTransferNote_Item model)
        {
            base.OnCreating(model);
            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnUpdating(int id, StockTransferNote_Item model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(StockTransferNote_Item model)
        {
            base.OnDeleting(model);
            model._DeletedAgent = "Service";
            model._DeletedBy = this.Username;
        }
    }
}
