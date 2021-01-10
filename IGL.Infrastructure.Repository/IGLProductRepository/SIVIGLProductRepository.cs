using IGL.Core.Entities.CoreContext;
using IGL.Core.Entities.SIV;
using IGL.Core.Repository.IGLProductSIV;
using IGL.Infrastructure.Repository.SqlHelper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace IGL.Infrastructure.Repository.IGLProductRepository
{
    public class SIVIGLProductRepository : ISIVIGLProductRepository
    {
        private readonly string _connectionString;
        private IGLContext baseContext = null;
        public SIVIGLProductRepository()
        {
            baseContext = new IGLContext();
            _connectionString = baseContext.Database.GetDbConnection().ConnectionString;
        }
        public async Task<(int responseStatus, string responseMessage)> InsertIGLProduct(IGLProduct modelEntity)
        {
            SqlParameter[] sqlParams = {
                    new SqlParameter("@materialId",modelEntity.MaterialId),
                    new SqlParameter("@itemNumber",modelEntity.ItemNumber?? string.Empty),
                    new SqlParameter("@quntity",modelEntity.Quantity),
                    new SqlParameter("@createdBy",modelEntity.CreatedBy),
            };
            try {
                var response = await SqlHelperExtension.ExecuteNonQuery(_connectionString, SqlConstant.ProcInsertIGLProduct, System.Data.CommandType.StoredProcedure, sqlParams);
                return response == 1 ? (response, "Inserted") : (0, "Exception");
            }
            catch(Exception ex)
            {
                return (-1, ex.Message);
            }
          
        }
    }
}
