using System.Threading.Tasks;

namespace Com.Danliris.Service.Inventory.Lib.IntegrationServices
{
    public interface IInventoryDocumentIntegrationService
    {
        Task<int> IntegrateData();
        
    }
}