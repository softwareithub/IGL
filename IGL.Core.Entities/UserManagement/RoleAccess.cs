using IGL.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGL.Core.Entities.UserManagement
{
    [Table("RoleRights")]
    public class RoleAccess:BaseClass<int>
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
    }
}
