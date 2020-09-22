using IGL.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Serialization;

namespace IGL.Core.Entities.Inventory
{
	[Table("PoItems", Schema = "Master")]
    public class POItem: BaseClass<int>
    {
		public int PoId { get; set; }
		public int ItemId { get; set; }
		public int Quantity { get; set; }
		public decimal Amount { get; set; }
		public string Remarks { get; set; }

	}
}
