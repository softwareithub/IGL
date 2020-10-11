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
        [Required(ErrorMessage = "Please Enter Employee Name.")]
        [Display(Prompt = "Employee Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please Select Employee Type.")]
        public int EmployeeTypeId { get; set; }

        [Required(ErrorMessage = "Please Enter Email Id.")]
        [Display(Prompt = "Email")]
        [StringLength(20, ErrorMessage = "Email Id Is Too Long")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Character And Space Are Not Allowed.")]
        [MaxLength(15, ErrorMessage = "Invalid Phone Number.")]
        [MinLength(10, ErrorMessage = "Invalid Phone Number.")]
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
