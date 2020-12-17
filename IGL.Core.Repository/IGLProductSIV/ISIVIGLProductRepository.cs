using IGL.Core.Entities.SIV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Repository.IGLProductSIV
{
    public interface ISIVIGLProductRepository
    {
        Task<(int responseStatus, string responseMessage)> InsertIGLProduct(IGLProduct modelEntity);
    }
}
