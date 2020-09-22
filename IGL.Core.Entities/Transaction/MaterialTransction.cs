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
        [Required(ErrorMessage ="Employee is required.")]
        public int EmployeeId { get; set; }
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string SlipNumber { get; set; }
    }
}
