﻿using IGL.Core.Entities.SIV;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IGL.Core.Service.IGLProductSIV
{
    public interface IIGLProductService
    {
        Task<(int responseStatus, string responseMessage)> InsertIGLProduct(List<IGLProduct> modelEntities);
        Task<int> ApprovedSIVCount();
        Task<List<ApprovedSIVDetail>> GetSIVApprovedDetail();
        Task<int> IsExistsItemNumber(string itemnumber);
        Task<List<IGLProductPoWise>> PoWiseIGLProducts();
    }
}
