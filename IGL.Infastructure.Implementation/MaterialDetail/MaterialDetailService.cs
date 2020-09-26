using IGL.Core.Repository.MaterialDetail;
using IGL.Core.Service.MaterialDetail;
using IGL.Core.ViewModelEntities.MasterVm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Infastructure.Service.MaterialDetail
{
    public class MaterialDetailService : IMaterialDetailService
    {
        private readonly IMaterialDetailRepo _materialDetailRepo;
        public MaterialDetailService(IMaterialDetailRepo materialDetailRepo)
        {
            _materialDetailRepo = materialDetailRepo;
        }
        public async  Task<List<MaterialMasterVm>> GetMaterialDetail()
        {
            return await  _materialDetailRepo.GetMaterialDetail();
        }
    }
}
