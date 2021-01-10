using IGL.Core.Entities.Inventory;
using IGL.Core.Repository.PurchaseOrder;
using IGL.Core.Service.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Infastructure.Service.PurchaseOrder
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _IpurchaseOrderRepository;

        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _IpurchaseOrderRepository = purchaseOrderRepository;
        }
        public async Task<List<PoStatusCount>> GetPoStatusCount()
        {
            return await _IpurchaseOrderRepository.GetPoStatusCount();
        }
    }
}
