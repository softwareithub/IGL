using IGL.Core.Entities.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IGL.Core.Service.PurchaseOrder
{
    public interface IPurchaseOrderService
    {
        Task<List<PoStatusCount>> GetPoStatusCount();
    }
}
