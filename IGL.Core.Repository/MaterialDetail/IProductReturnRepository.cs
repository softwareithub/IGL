using IGL.Core.Entities.ProductTransaction;
using IGL.Core.Entities.Transaction;
using IGL.Core.ViewModelEntities.MasterVm.TransactionVm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Repository.MaterialDetail
{
    public interface IProductReturnRepository
    {
        Task<List<ProductIssueDetail>> GetProductIssueDetail(int Id);
        Task<(int responseStatus, string responseMessage)> CreateMaterialIssue(MaterialTransction model,
            List<TransactionItems> items);
        Task<List<MaterialSlipVm>> GetMaterialSlipDetail(int id);
        Task<List<MaterialReturnDetail>> GetMaterialIssueDetail(int id);
        Task<(int responseStatus, string responseMessage)> MaterialReturn(List<MaterialReturn> models);
    }
}
