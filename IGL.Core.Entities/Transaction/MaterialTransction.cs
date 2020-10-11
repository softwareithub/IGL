using IGL.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IGL.Core.Entities.Transaction
{
    [Table("MaterialTransction", Schema = "Master")]
    public class MaterialTransction:BaseClass<int>
    {
        [Required(ErrorMessage ="Please Select Employee.")]
        public int EmployeeId { get; set; }
        [DataType(DataType.Date)]

        [Required(ErrorMessage = "Please Enter Transaction Date.")]
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string SlipNumber { get; set; }
    }
}
