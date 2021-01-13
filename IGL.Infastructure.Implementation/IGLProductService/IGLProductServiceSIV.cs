using IGL.Core.Entities.SIV;
using IGL.Core.Repository.IGLProductSIV;
using IGL.Core.Service.IGLProductSIV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Infastructure.Service.IGLProductService
{
    public class IGLProductServiceSIV : IIGLProductService
    {
        private readonly ISIVIGLProductRepository _sivIGLProductRepository;
        public IGLProductServiceSIV(ISIVIGLProductRepository sIVIGLProductRepository)
        {
            _sivIGLProductRepository = sIVIGLProductRepository;
        }

        public async Task<int> ApprovedSIVCount()
        {
            return await _sivIGLProductRepository.ApprovedSIVCount();
        }

        public async Task<List<ApprovedSIVDetail>> GetSIVApprovedDetail()
        {
            return await _sivIGLProductRepository.GetSIVApprovedDetail();
        }

        public async Task<(int responseStatus, string responseMessage)> InsertIGLProduct(IGLProduct modelEntity)
        {
           return await  _sivIGLProductRepository.InsertIGLProduct(modelEntity);
        }
    }
}
