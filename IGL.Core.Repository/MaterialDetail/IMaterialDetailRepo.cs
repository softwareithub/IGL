using IGL.Core.ViewModelEntities.MasterVm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Core.Repository.MaterialDetail
{
    public interface IMaterialDetailRepo
    {
        Task<List<MaterialMasterVm>> GetMaterialDetail();
    }
}
