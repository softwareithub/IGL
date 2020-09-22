using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IGL.Core.ViewModelEntities.MasterVm.TransactionVm
{
    public class MaterialTransactionModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string SlipNumber { get; set; }
    }
}
