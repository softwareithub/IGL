using IGL.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IGL.Core.Entities.Master
{
    [Table("UnitMaster", Schema = "Master")]
    public class UnitMaster : BaseClass<int>
    {
        [Required(ErrorMessage ="Unit name is required.")]
        [StringLength(50,ErrorMessage ="Too Long Name")]
        public string Name { get; set; }
        public string Code { get; set; }
        public int IsDefault { get; set; }
    }
}
