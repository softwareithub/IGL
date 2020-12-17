using IGL.Core.Entities.CoreContext;
using IGL.Core.Entities.ProductTransaction;
using IGL.Core.Entities.Transaction;
using IGL.Core.Repository.MaterialDetail;
using IGL.Core.ViewModelEntities.MasterVm.TransactionVm;
using IGL.Infrastructure.Repository.SqlHelper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Infrastructure.Repository.MaterialDetail
{
    public class ProductReturnRepository : IProductReturnRepository
    {
        private readonly IGLContext baseContext = null;
        private readonly string _connectionString;

        public ProductReturnRepository()
        {
            baseContext = new IGLContext();
            _connectionString = baseContext.Database.GetDbConnection().ConnectionString;
        }

        public async Task<(int responseStatus, string responseMessage)> CreateMaterialIssue(MaterialTransction model, List<TransactionItems> items)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaterialTransactionId", typeof(int));
            dt.Columns.Add("ItemId", typeof(int));
            dt.Columns.Add("Quantity", typeof(decimal));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("Remarks", typeof(string));
            dt.Columns.Add("ItemNumber", typeof(int));

            items.ForEach(item =>
            {
                DataRow dr = dt.NewRow();
                dr["MaterialTransactionId"] = 0;
                dr["ItemId"] = item.ItemId;
                dr["Quantity"] = item.Quantity;
                dr["UnitPrice"] = item.UnitPrice;
                dr["Remarks"] = item.Remarks;
                dr["ItemNumber"] = item.UniqueItemId;
                dt.Rows.Add(dr);
            });

            SqlParameter[] sqlParams = {
             new SqlParameter("@employeeId",model.EmployeeId),
             new SqlParameter("@transactionDate",model.TransactionDate),
             new SqlParameter("@transactionType",model.TransactionType),
             new SqlParameter("@slipNumber",model.SlipNumber?? string.Empty),
             new SqlParameter("@createdBy",model.CreatedBy),
             new SqlParameter("@transactionItem",dt),

            };
            try
            {
                var response = await SqlHelperExtension.ExecuteNonQuery(_connectionString, "Proc_MaterialIssue",
             CommandType.StoredProcedure,
             sqlParams);
                return (1, "Material issue inserted");
            }
            catch (Exception ex)
            {
                return (-1, ex.Message);
            }
        }

        public async Task<List<MaterialReturnDetail>> GetMaterialIssueDetail(int id)
        {
            var models = new List<MaterialReturnDetail>();

            SqlParameter[] sqlParams = {
            new SqlParameter("@id",id)
            };

            using (var reader = await SqlHelperExtension.ExecuteReader(_connectionString, "usp_GetIssueProduct", CommandType.StoredProcedure, sqlParams))
            {
                while (reader.Read())
                {
                    var model = new MaterialReturnDetail();
                    model.Id = reader.DefaultIfNull<int>("Id");
                    model.EmployeeName = reader.DefaultIfNull<string>("Name");
                    model.SlipDate = reader.DefaultIfNull<DateTime>("CreatedDate");
                    model.MaterialTransactionId = reader.DefaultIfNull<int>("MaterialTransctionId");
                    model.ProductId = reader.DefaultIfNull<int>("ProductId");
                    model.ProductName = reader.DefaultIfNull<string>("ProductName");
                    model.ProductCode = reader.DefaultIfNull<string>("ProductCode");
                    model.ItemNumber = reader.DefaultIfNull<string>("ItemNumber");
                    model.Quantity = reader.DefaultIfNull<decimal>("Quantity");
                    model.UnitPrice = reader.DefaultIfNull<decimal>("UnitPrice");
                    model.Remarks = reader.DefaultIfNull<string>("Remarks");
                    model.UnitName = reader.DefaultIfNull<string>("UnitName");
                    model.TotalPrice = reader.DefaultIfNull<decimal>("TotalPrice");
                    model.SlipNumber = reader.DefaultIfNull<string>("SlipNumber");
                    model.UniqueItemId = reader.DefaultIfNull<int>("UniqueItemId");

                    models.Add(model);
                }
            }
            return models;
        }

        public async Task<List<MaterialSlipVm>> GetMaterialSlipDetail(int id)
        {
            List<MaterialSlipVm> models = new List<MaterialSlipVm>();
            SqlParameter[] sqlParams = {
            new SqlParameter("@id",id)
            };
            using var reader = await SqlHelperExtension.ExecuteReader(_connectionString, "Proc_GetMaterialSlip", CommandType.StoredProcedure, sqlParams);
            while (reader.Read())
            {
                MaterialSlipVm model = new MaterialSlipVm();
                model.Id = reader.DefaultIfNull<int>("Id");
                model.EmployeeName = reader.DefaultIfNull<string>("Name");
                model.SlipDate = reader.DefaultIfNull<DateTime>("CreatedDate");
                model.MaterialTransctionId = reader.DefaultIfNull<int>("MaterialTransctionId");
                model.MaterialName = reader.DefaultIfNull<string>("ProductName");
                model.MaterialCode = reader.DefaultIfNull<string>("ProductCode");
                model.Quantity = reader.DefaultIfNull<decimal>("Quantity");
                model.UnitPrice = reader.DefaultIfNull<decimal>("UnitPrice");
                model.Remarks = reader.DefaultIfNull<string>("Remarks");
                model.UnitName = reader.DefaultIfNull<string>("UnitName");
                model.TotalPrice = reader.DefaultIfNull<decimal>("TotalPrice");
                model.SlipNumber = reader.DefaultIfNull<string>("SlipNumber");

                models.Add(model);
            }
            return models;
        }

        public async Task<List<ProductIssueDetail>> GetProductIssueDetail(int Id)
        {
            var models = new List<ProductIssueDetail>();

            SqlParameter[] sqlParams = {
                 new SqlParameter("@mtId",Id)
            };

            var reader = await SqlHelperExtension.ExecuteReader(_connectionString, SqlConstant.ProcGetIssueProduct, System.Data.CommandType.StoredProcedure, sqlParams);

            while (reader.Read())
            {
                ProductIssueDetail model = new ProductIssueDetail();
                model.Id = reader.DefaultIfNull<int>("Id");
                model.ProductName = reader.DefaultIfNull<string>("ProductName");
                model.EmployeeName = reader.DefaultIfNull<string>("EmployeeName");
                model.TransactionDate = reader.DefaultIfNull<DateTime>("TransactionDate");
                model.TransactionType = reader.DefaultIfNull<string>("TransactionType");
                model.SlipNumber = reader.DefaultIfNull<string>("SlipNumber");
                model.ItemId = reader.DefaultIfNull<int>("ItemId");
                model.ItemNumber = reader.DefaultIfNull<string>("ItemNumber");
                model.Quantity = reader.DefaultIfNull<decimal>("Quantity");
                models.Add(model);
            }

            return models;
        }

        public async Task<(int responseStatus, string responseMessage)> MaterialReturn(List<MaterialReturn> models)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaterialTransactionId", typeof(int));
            dt.Columns.Add("ItemId", typeof(int));
            dt.Columns.Add("Quantity", typeof(decimal));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("Remarks", typeof(string));
            dt.Columns.Add("ItemNumber", typeof(int));

            models.ForEach(item =>
            {
                DataRow dr = dt.NewRow();
                dr["MaterialTransactionId"] = 0;
                dr["ItemId"] = item.ProductId;
                dr["Quantity"] = item.Quantity;
                dr["UnitPrice"] = default(decimal);
                dr["Remarks"] = string.Empty;
                dr["ItemNumber"] = item.UniqueItemId;
                dt.Rows.Add(dr);
            });
            SqlParameter[] sqlParams = {
             new SqlParameter("@TransactionId",models.First().Id),
             new SqlParameter("@transactionDate",models.First().TransactionDate),
             new SqlParameter("@transactionType",models.First().TransactionType),
             new SqlParameter("@slipNumber",models.First().SlipNumber?? string.Empty),
             new SqlParameter("@createdBy",models.First().CreatedBy),
             new SqlParameter("@transactionItem",dt),

            };
            try
            {
                var response = await SqlHelperExtension.ExecuteNonQuery(_connectionString, "Proc_MaterialReturn",
                CommandType.StoredProcedure,
                sqlParams);

                return (1, "Material issue inserted");
            }
            catch (Exception ex)
            {
                return (-1, ex.Message);
            }
        }
    }
}
