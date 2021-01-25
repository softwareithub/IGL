using IGL.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGL.Core.Entities.Master
{
    [Table("AuthUsers", Schema = "Master")]
    public class UserDetail:BaseClass<int>
    {
        [Required(ErrorMessage ="Please Enter User Name")]
        [Display(Prompt ="User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Please Enter Password.")]
        [Display(Prompt ="Password")]
        [StringLength(20,ErrorMessage ="Password is too long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [StringLength(20, ErrorMessage = "Confirm Password must be at least {2} characters long.", MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]
        [Display(Prompt ="Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Name.")]
        [Display(Prompt = "Name")]
        [StringLength(200, ErrorMessage = "Name is too long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Email Id.")]
        [Display(Prompt = "Email")]
        [StringLength(20, ErrorMessage = "Email is too long")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Character and space are not allowed.")]
        [MaxLength(15, ErrorMessage = "Invalid Phone Number.")]
        [MinLength(10, ErrorMessage = "Invalid Phone Number.")]
        [Display(Prompt ="Phone Number")]
        public string Phone { get; set; }
        [Display(Prompt ="Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Select Role.")]
        public int RoleId { get; set; }
    }
}
