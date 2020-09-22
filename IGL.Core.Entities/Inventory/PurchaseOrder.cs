using IGL.Core.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGL.Core.Entities.Inventory
{
	[Table("PurchaseOrder", Schema= "Master")]
	public class PurchaseOrder: BaseClass<int>
    {
		[Required(ErrorMessage ="PO Date is required.")]
		[DataType(DataType.Date)]
		public DateTime PODate { get; set; }
		public string PONumber { get; set; }

		[Required(ErrorMessage = "Vendor is required.")]
		public int VendorId { get; set; }

		[Display(Prompt ="Delivery Term")]
		[StringLength(450,ErrorMessage ="Delivery is too long")]
		public string DeliveryTerm { get; set; }
		[Display(Prompt = "Payment Terms")]
		[StringLength(450, ErrorMessage = "Term of Payment is too long")]
		public string TermOfPayment { get; set; }
		[Display(Prompt = "Special Comment")]
		[StringLength(450, ErrorMessage = "Special Comment is too long")]
		public string SpecialComment { get; set; }
		public string POStatus { get; set; }
		public string Remarks { get; set; }
	}
}
