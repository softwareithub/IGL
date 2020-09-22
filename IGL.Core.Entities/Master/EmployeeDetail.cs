using IGL.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IGL.Core.Entities.Master
{
    [Table("EmployeeDetail",Schema = "Master")]
    public class EmployeeDetail:BaseClass<int>
    {
        [Required(ErrorMessage = "Employee name is required.")]
        [Display(Prompt = "Employee Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Employee Type is required.")]
        public int EmployeeTypeId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Display(Prompt = "Email")]
        [StringLength(20, ErrorMessage = "Email is too long")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Character and space are not allowed.")]
        [MaxLength(15, ErrorMessage = "Invalid Phone Number.")]
        [MinLength(10, ErrorMessage = "Invalid Phone Number.")]
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
