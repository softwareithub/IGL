using IGL.Core.Entities.ProductTransaction;
using IGL.Core.Repository.MaterialDetail;
using IGL.Core.Service.MaterialDetail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Infastructure.Service.MaterialDetail
{
    public class ProductReturnService : IProductReturnService
    {
        private readonly IProductReturnRepository _IProductReturnRepository;

        public ProductReturnService(IProductReturnRepository productReturnRepository)
        {
            _IProductReturnRepository = productReturnRepository;
        }
        public Task<List<ProductIssueDetail>> GetProductIssueDetail(int Id) => _IProductReturnRepository.GetProductIssueDetail(Id);
       
    }
}
