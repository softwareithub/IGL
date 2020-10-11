using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IGL.Core.ViewModelEntities.Authenticate
{
    public class AuthenticateModel
    {
        [Required(ErrorMessage ="Please Enter User Name.")]
        [StringLength(20,ErrorMessage ="Invalid User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Email Id.")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        public string Message { get; set; }
    }
}
