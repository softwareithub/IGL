using IGL.Core.Entities.CoreContext;
using IGL.Core.Entities.Reports;
using IGL.Core.Repository.ReportsRepository;
using IGL.Infrastructure.Repository.SqlHelper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Infrastructure.Repository.Reports
{
    public class VendorWisePoStatusReportRepository : IVendorWisePoStatusReportRepository
    {
        private IGLContext baseContext = null;
        private readonly string _connectionString;

        public VendorWisePoStatusReportRepository()
        {
            baseContext = new IGLContext();
            _connectionString = baseContext.Database.GetDbConnection().ConnectionString;
        }
        public async Task<List<VendorWisePOStatus>> GetVendorWisePOStatusReport()
        {
            var models = new List<VendorWisePOStatus>();
            var reader = await SqlHelperExtension.ExecuteReader(_connectionString, SqlConstant.ProcGetVendorWisePoStatusReport, System.Data.CommandType.StoredProcedure, null);

            while (reader.Read())
            {
                VendorWisePOStatus model = new VendorWisePOStatus();
                model.POCountByStatus = reader.DefaultIfNull<int>("POCountByStatus");
                model.VendorId = reader.DefaultIfNull<int>("VendorId");
                model.VendorName = reader.DefaultIfNull<string>("VendorName");
                model.POStatus = reader.DefaultIfNull<string>("POStatus");
                models.Add(model);
            }

            return models;
        }

        public async Task<List<VenderWisePoStatusDetail>> GetVendorWisePoStatusDetailReport(int vendorId, string status)
        {

            var models = new List<VenderWisePoStatusDetail>();

            try
            {
                SqlParameter[] sqlParams ={
                    new SqlParameter("@VendorId",vendorId),
                    new SqlParameter("@PoStatus",status)
            };
                var reader = await SqlHelperExtension.ExecuteReader(_connectionString, SqlConstant.ProcGetVendorWisePoStatusDetailReport, System.Data.CommandType.StoredProcedure, sqlParams);

                while (reader.Read())
                {
                    VenderWisePoStatusDetail model = new VenderWisePoStatusDetail();
                    model.PoDate = reader.DefaultIfNull<DateTime>("PoDate");
                    model.PoNumber = reader.DefaultIfNull<string>("PoNumber");
                    model.VendorName = reader.DefaultIfNull<string>("VendorName");
                    model.POStatus = reader.DefaultIfNull<string>("PoStatus");
                    models.Add(model);
                }

                return models;
            }
            catch(Exception ex)
            {
                return models;
            }

        }
    }
}
