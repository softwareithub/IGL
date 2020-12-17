using IGL.Core.Entities.SIV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Service.IGLProductSIV
{
    public interface IIGLProductService
    {
        Task<(int responseStatus, string responseMessage)> InsertIGLProduct(IGLProduct modelEntity);
    }
}
