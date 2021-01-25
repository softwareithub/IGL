using IGL.Core.Entities.CoreContext;
using IGL.Core.Entities.SIV;
using IGL.Core.Repository.IGLProductSIV;
using IGL.Infrastructure.Repository.SqlHelper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<List<ApprovedSIVDetail>> GetSIVApprovedDetail()
        {
            SqlParameter[] sqlParams = { };
            var models = new List<ApprovedSIVDetail>();

            try
            {
                var reader = await SqlHelperExtension.ExecuteReader(_connectionString, "usp_GetApprovedSIVDateWise", System.Data.CommandType.StoredProcedure, sqlParams);
                while (reader.Read())
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
            catch (Exception ex)
            {
                return models;
            }
        }

        public async Task<(int responseStatus, string responseMessage)> InsertIGLProduct(List<IGLProduct> modelEntities)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaterialId", typeof(int));
            dt.Columns.Add("ItemNumber", typeof(string));
            dt.Columns.Add("Quantity", typeof(decimal));
            dt.Columns.Add("CreatedBy", typeof(int));

            modelEntities.ForEach(item => {
                DataRow row = dt.NewRow();
                row["MaterialId"] = item.MaterialId;
                row["ItemNumber"] = item.ItemNumber;
                row["Quantity"] = item.Quantity;
                row["CreatedBy"] = item.CreatedBy;

                dt.Rows.Add(row);
            });

            SqlParameter[] sqlParams = {
                    new SqlParameter("@materialId",modelEntities.First().MaterialId),
                    new SqlParameter("@materialCount",modelEntities.Count()),
                    new SqlParameter("@iglProduct",dt){ SqlDbType= SqlDbType.Structured},
                    new SqlParameter("@poNumber", modelEntities.First().PoNumber)
            };
            try
            {
                var response = await SqlHelperExtension.ExecuteNonQuery(_connectionString, SqlConstant.ProcInsertIGLProduct, System.Data.CommandType.StoredProcedure, sqlParams);
                return response >= 1 ? (response, "Inserted") : (0, "Exception");
            }
            catch (Exception ex)
            {
                return (-1, ex.Message);
            }
        }

        public async Task<int> IsExistsItemNumber(string itemnumber)
        {
            SqlParameter[] sqlparams = {
            new SqlParameter("@in_itemNumber", itemnumber?? string.Empty)
            };
            try {
                var response = await SqlHelperExtension.ExecuteScalar(_connectionString, "usp_CheckProductItemExists", System.Data.CommandType.StoredProcedure, sqlparams);
                return Convert.ToInt32(response);
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public async Task<List<IGLProductPoWise>> PoWiseIGLProducts()
        {
            SqlParameter[] sqlParams = { };
            var models = new List<IGLProductPoWise>();
            using (var reader= await SqlHelperExtension.ExecuteReader(_connectionString, "Proc_GetIGLProductByPo", CommandType.StoredProcedure,sqlParams))
            {
                while(reader.Read())
                {
                    var model = new IGLProductPoWise();
                    model.PoNumber = reader.DefaultIfNull<string>("PONumber");
                    model.ProductName = reader.DefaultIfNull<string>("ProductName");
                    model.HSNCode = reader.DefaultIfNull<string>("HSNCode");
                    model.UnitName = reader.DefaultIfNull<string>("UnitName");
                    model.ThreshHoldValue = reader.DefaultIfNull<int>("ThresholdValue");
                    model.Quantity = reader.DefaultIfNull<int>("OpeningQuantity");
                    model.ItemNumber = reader.DefaultIfNull<string>("ItemNumber");

                    models.Add(model);
                }
            }
            return models;
        }
    }
}
