using IGL.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Core.Entities.Transaction
{
    public class MaterialReturnDetail:BaseClass<int>
    {
        public string EmployeeName { get; set; }
        public DateTime SlipDate { get; set; }
        public int MaterialTransactionId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ItemNumber { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Remarks { get; set; }
        public string UnitName { get; set; }
        public decimal TotalPrice { get; set; }
        public string SlipNumber { get; set; }
        public int UniqueItemId { get; set; }
    }
}
