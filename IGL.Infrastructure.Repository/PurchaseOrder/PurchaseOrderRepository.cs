using IGL.Core.Entities.CoreContext;
using IGL.Core.Entities.Inventory;
using IGL.Core.Repository.PurchaseOrder;
using IGL.Infrastructure.Repository.SqlHelper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IGL.Infrastructure.Repository.PurchaseOrder
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private IGLContext baseContext = null;
        private readonly string _connectionString;

        public PurchaseOrderRepository()
        {
            baseContext = new IGLContext();
            _connectionString = baseContext.Database.GetDbConnection().ConnectionString;
        }
        public async Task<List<PoStatusCount>> GetPoStatusCount()
        {
            var models = new List<PoStatusCount>();
            SqlParameter[] sqlParams = { };
            using (var reader =await SqlHelperExtension.ExecuteReader(_connectionString, "Proc_PoStatusCount", System.Data.CommandType.StoredProcedure, sqlParams))
            {
                while(reader.Read())
                {
                    var model = new PoStatusCount();
                    model.StatusName = reader.DefaultIfNull<string>("POStatus");
                    model.StatusCount = reader.DefaultIfNull<int>("StatusCount");

                    models.Add(model);
                }
            }
            return models;
        }
    }
}
