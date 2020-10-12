using IGL.Core.Entities.ProductTransaction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Service.MaterialDetail
{
    public interface IProductReturnService
    {
        Task<List<ProductIssueDetail>> GetProductIssueDetail(int Id);
    }
}
