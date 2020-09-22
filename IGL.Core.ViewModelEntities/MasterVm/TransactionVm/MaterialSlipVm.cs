using IGL.Core.Entities.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IGL.Core.ViewModelEntities.MasterVm.TransactionVm
{
    public class MaterialSlipVm
    {
        public int Id { get; set; }
        public string SlipName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime SlipDate { get; set; }
        public int MaterialTransctionId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Remarks { get; set; }
        public string UnitName { get; set; }
        public decimal TotalPrice { get; set; }
        public string SlipNumber { get; set; }
    }
}
