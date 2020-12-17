using IGL.Core.Entities.ProductTransaction;
using IGL.Core.Entities.Transaction;
using IGL.Core.Repository.MaterialDetail;
using IGL.Core.Service.MaterialDetail;
using IGL.Core.ViewModelEntities.MasterVm.TransactionVm;
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

        public async Task<(int responseStatus, string responseMessage)> CreateMaterialIssue(MaterialTransction model, List<TransactionItems> items)
        {
            return await _IProductReturnRepository.CreateMaterialIssue(model, items);
        }

        public  async Task<List<MaterialSlipVm>> GetMaterialSlipDetail(int id)
        {
            return await _IProductReturnRepository.GetMaterialSlipDetail(id);
        }

        public async Task<List<MaterialReturnDetail>> GetMaterialIssueDetail(int id)
        {
            return await _IProductReturnRepository.GetMaterialIssueDetail(id);
        }

        public async Task<(int responseStatus, string responseMessage)> MaterialReturn(List<MaterialReturn> models)
        {
            return await _IProductReturnRepository.MaterialReturn(models);
        }
    }
}
