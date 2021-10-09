using IGL.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGL.Core.Entities.Master
{
    [Table("MenuSubMenuDetails")]
    public class MenuSubMenuModel: BaseClass<int>
    {
        public string Menu { get; set; }
        public string SubMenu { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        [NotMapped]
        public int IsMapped { get; set; }
    }
}
