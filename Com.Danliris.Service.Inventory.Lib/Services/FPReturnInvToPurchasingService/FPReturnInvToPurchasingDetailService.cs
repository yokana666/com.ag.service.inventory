using Com.Danliris.Service.Inventory.Lib.Models.FPReturnInvToPurchasingModel;
using Com.Moonlay.NetCore.Lib.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Inventory.Lib.Services.FPReturnInvToPurchasingService
{
    public class FPReturnInvToPurchasingDetailService : StandardEntityService<InventoryDbContext, FPReturnInvToPurchasingDetail>
    {
        public FPReturnInvToPurchasingDetailService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void OnCreating(FPReturnInvToPurchasingDetail model)
        {
            IdentityService identityService = (IdentityService)ServiceProvider.GetService(typeof(IdentityService));

            base.OnCreating(model);
            model._CreatedAgent = "Service";
            model._CreatedBy = identityService.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = identityService.Username;
        }

        public override void OnUpdating(int id, FPReturnInvToPurchasingDetail model)
        {
            IdentityService identityService = (IdentityService)ServiceProvider.GetService(typeof(IdentityService));

            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = identityService.Username;
        }

        public override void OnDeleting(FPReturnInvToPurchasingDetail model)
        {
            IdentityService identityService = (IdentityService)ServiceProvider.GetService(typeof(IdentityService));

            base.OnDeleting(model);
            model._DeletedAgent = "Service";
            model._DeletedBy = identityService.Username;
        }
    }
}
