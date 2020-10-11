using IGL.Core.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGL.Core.Entities.Master
{
    [Table("RateMaster",Schema ="Master")]
    public class RateMaster:BaseClass<int>
    {
        public int ProductId { get; set; }
        public decimal Rate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
