using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Inventory.Lib.Helpers;
using Com.Danliris.Service.Inventory.Lib.Models.FpReturnFromBuyers;
using Com.Danliris.Service.Inventory.Lib.ViewModels.FpReturnFromBuyers;

namespace Com.Danliris.Service.Inventory.Lib.Services.FpReturnFromBuyers
{
    public class FpReturnFromBuyerService : IFpReturnFromBuyerService
    {
        public Task<int> CreateAsync(FpReturnFromBuyerModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public FpReturnFromBuyerModel MapToModel(FpReturnFromBuyerViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public FpReturnFromBuyerViewModel MapToViewModel(FpReturnFromBuyerModel model)
        {
            throw new NotImplementedException();
        }

        public ReadResponse<FpReturnFromBuyerModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            throw new NotImplementedException();
        }

        public Task<FpReturnFromBuyerModel> ReadByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, FpReturnFromBuyerModel model)
        {
            throw new NotImplementedException();
        }
    }
}
