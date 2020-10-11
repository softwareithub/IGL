using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Core.Entities.Reports
{
    public class ProductTransactionStatusReport
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Phone { get; set; }
        public string EmailId { get; set; }
        public string TransactionType { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
