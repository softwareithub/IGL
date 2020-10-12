using IGL.Core.Entities.ProductTransaction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Repository.MaterialDetail
{
    public interface IProductReturnRepository
    {
        Task<List<ProductIssueDetail>> GetProductIssueDetail(int Id);
    }
}
