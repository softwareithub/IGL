using IGL.Core.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Repository.PurchaseOrder
{
    public interface IPurchaseOrderRepository
    {
        Task<List<PoStatusCount>> GetPoStatusCount();
    }
}
