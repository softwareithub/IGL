using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IGL.Core.ViewModelEntities.Authenticate
{
    public class AuthenticateModel
    {
        [Required(ErrorMessage ="User Name is required.")]
        [StringLength(20,ErrorMessage ="In Valida User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        public string Message { get; set; }
    }
}
