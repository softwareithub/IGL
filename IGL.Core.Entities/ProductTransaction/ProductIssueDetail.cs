using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Core.Entities.ProductTransaction
{
    public class ProductIssueDetail
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string SlipNumber { get; set; }
        public int ItemId { get; set; }
        public string ItemNumber  { get; set; }
        public decimal Quantity { get; set; }
    }
}
