using IGL.Core.Entities.SIV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Repository.IGLProductSIV
{
    public interface ISIVIGLProductRepository
    {
        Task<(int responseStatus, string responseMessage)> InsertIGLProduct(List<IGLProduct> modelEntities);
        Task<int> ApprovedSIVCount();
        Task<List<ApprovedSIVDetail>> GetSIVApprovedDetail();
        Task<int> IsExistsItemNumber(string itemnumber);
        Task<List<IGLProductPoWise>> PoWiseIGLProducts();
    }
}
