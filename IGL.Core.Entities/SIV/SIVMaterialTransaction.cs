using IGL.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGL.Core.Entities.SIV
{
	[Table("SIVMaterialTransaction",Schema = "Master")]
    public class SIVMaterialTransaction:BaseClass<int>
    {
		public int SIVId { get; set; }
		public int ItemId { get; set; }
		public string ItemNumber { get; set; }
		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }
	}
}
