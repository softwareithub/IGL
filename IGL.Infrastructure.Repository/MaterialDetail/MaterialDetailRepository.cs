using IGL.Core.Entities.CoreContext;
using IGL.Core.Repository.MaterialDetail;
using IGL.Core.ViewModelEntities.MasterVm;
using IGL.Infrastructure.Repository.SqlHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Infrastructure.Repository.MaterialDetail
{
    public class MaterialDetailRepository : IMaterialDetailRepo
    {
        private IGLContext baseContext = null;
        private readonly string _connectionString;

        public MaterialDetailRepository()
        {
            baseContext = new IGLContext();
            _connectionString = baseContext.Database.GetDbConnection().ConnectionString;
        }

        public async  Task<List<MaterialMasterVm>> GetMaterialDetail()
        {
            var models = new List<MaterialMasterVm>();
            var reader = await SqlHelperExtension.ExecuteReader(_connectionString, SqlConstant.ProcGetMaterialDetail, System.Data.CommandType.StoredProcedure, null);

            while (reader.Read())
            {
                MaterialMasterVm model = new MaterialMasterVm();
                model.Id = reader.DefaultIfNull<int>("Id");
                model.MaterialName = reader.DefaultIfNull<string>("Name");
                model.Code = reader.DefaultIfNull<string>("Code");
                model.Unit = reader.DefaultIfNull<string>("Unit");
                model.PerUnitCost = reader.DefaultIfNull<decimal>("PerUnitCost");
                model.Quantity = reader.DefaultIfNull<int>("OpeningQuantity");
                model.ThresholdValue = reader.DefaultIfNull<int>("ThresholdValue");
                model.IsUniqe =Convert.ToBoolean(reader.DefaultIfNull<int>("IsUnique"));
                models.Add(model);
            }

            return models;
        }
    }
}
