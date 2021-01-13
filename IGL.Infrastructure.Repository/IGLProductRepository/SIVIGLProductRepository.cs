using IGL.Core.Entities.CoreContext;
using IGL.Core.Entities.SIV;
using IGL.Core.Repository.IGLProductSIV;
using IGL.Infrastructure.Repository.SqlHelper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        public async Task<int> ApprovedSIVCount()
        {
            SqlParameter[] sqlParams = { };
            try
            {
                var response = await SqlHelperExtension.ExecuteScalar(_connectionString, SqlConstant.ProcGetApprovedSIVCount, System.Data.CommandType.StoredProcedure, sqlParams);
                return Convert.ToInt32(response);
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public async Task<List<ApprovedSIVDetail>> GetSIVApprovedDetail()
        {
            SqlParameter[] sqlParams = { };
            var models = new List<ApprovedSIVDetail>();

            try {
                var reader = await SqlHelperExtension.ExecuteReader(_connectionString, "usp_GetApprovedSIVDateWise", System.Data.CommandType.StoredProcedure, sqlParams);
                while(reader.Read())
                {
                    var model = new ApprovedSIVDetail();
                    model.PoDate = reader.DefaultIfNull<DateTime>("PODate");
                    model.PoNumber = reader.DefaultIfNull<string>("PONumber");
                    model.VendorName = reader.DefaultIfNull<string>("VendorName");
                    model.vendorEmail = reader.DefaultIfNull<string>("VendorEmail");
                    model.VendorPhone = reader.DefaultIfNull<string>("VendorPhone");
                    model.VendorType = reader.DefaultIfNull<string>("VendorType");
                    models.Add(model);
                }
                return models;
            }
            catch (Exception ex) {
                return models;
            }
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
                return response >=1 ? (response, "Inserted") : (0, "Exception");
            }
            catch(Exception ex)
            {
                return (-1, ex.Message);
            }
          
        }
    }
}
