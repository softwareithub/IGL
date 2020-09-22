using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Core.ViewModelEntities.MasterVm.UserManagementVm
{
    public class UserDetailVm
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string RoleName { get; set; }
    }
}
