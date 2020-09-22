using IGL.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGL.Core.Entities.Master
{
    [Table("RoleMaster", Schema ="Master")]
    public class RoleMaster: BaseClass<int>
    {
        [Required(ErrorMessage ="Role name is required.")]
        [Display(Prompt ="Enter Role Name")]
        [StringLength(50,ErrorMessage ="Too Long Role name")]
        public string RoleName { get; set; }

        [Display(Prompt ="Enter Description Name")]
        public string Description { get; set; }
    }
}
