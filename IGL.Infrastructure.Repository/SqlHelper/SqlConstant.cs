using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Infrastructure.Repository.SqlHelper
{
    public static class SqlConstant
    {
        public static string ProcGetMaterialDetail = @"Proc_GetMaterialDetail";
        public static string ProcGetProductReport = @"Proc_ProductReport";
        public static string ProcGetVendorWisePoStatusReport = @"Proc_VendorWisePOStatusReport";
        public static string ProcGetVendorWisePoStatusDetailReport = @"Proc_VenderWisePoStatusDetailReport";
        public static string ProcGetProductTransactionStatusReport = @"Proc_GetProductTransactionStatusReport";
        public static string ProcGetIssueProduct = @"usp_GetIssueProduct";
        public static string ProcGetLowQuantityProductReport = @"Proc_LowQuantityProductReport";
    }
}
