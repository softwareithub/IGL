using IGL.Core.ViewModelEntities.MasterVm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Service.MaterialDetail
{
    public interface IMaterialDetailService
    {
        Task<List<MaterialMasterVm>> GetMaterialDetail();
    }
}
