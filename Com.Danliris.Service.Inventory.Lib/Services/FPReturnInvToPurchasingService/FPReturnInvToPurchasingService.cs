using Com.Moonlay.NetCore.Lib.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Com.Danliris.Service.Inventory.Lib.Models.FPReturnInvToPurchasingModel;

namespace Com.Danliris.Service.Inventory.Lib.Services.FPReturnInvToPurchasingService
{
    public class FPReturnInvToPurchasingService : StandardEntityService<InventoryDbContext, FPReturnInvToPurchasing>
    {
        public FPReturnInvToPurchasingService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void OnCreating(FPReturnInvToPurchasing model)
        {
            IdentityService identityService = (IdentityService)ServiceProvider.GetService(typeof(IdentityService));

            base.OnCreating(model);
            model._CreatedAgent = "Service";
            model._CreatedBy = identityService.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = identityService.Username;
        }

        public override void OnUpdating(int id, FPReturnInvToPurchasing model)
        {
            IdentityService identityService = (IdentityService)ServiceProvider.GetService(typeof(IdentityService));

            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = identityService.Username;
        }

        public override void OnDeleting(FPReturnInvToPurchasing model)
        {
            IdentityService identityService = (IdentityService)ServiceProvider.GetService(typeof(IdentityService));

            base.OnDeleting(model);
            model._DeletedAgent = "Service";
            model._DeletedBy = identityService.Username;
        }
    }
}
