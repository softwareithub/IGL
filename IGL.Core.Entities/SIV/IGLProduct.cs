using IGL.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IGL.Core.Entities.SIV
{
    public class IGLProduct:BaseClass<int>
    {
        [Required(ErrorMessage ="Please select Product")]
        public int MaterialId { get; set; }
        [Required(ErrorMessage ="Item Number is required.")]
        public string ItemNumber { get; set; }
        [Required(ErrorMessage ="Product quantity is required.")]
        public decimal Quantity { get; set; }
        public int StoreId { get; set; }
        public string PoNumber { get; set; }
    }
}
