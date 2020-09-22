using IGL.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGL.Core.Entities.Master
{
    [Table("EmployeeType", Schema = "Master")]
    public class EmployeeType:BaseClass<int>
    {
        [Required(ErrorMessage ="Employee type is required.")]
        [StringLength(20,ErrorMessage = "InValida employee type.")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
