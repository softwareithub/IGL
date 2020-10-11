using IGL.Core.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGL.Core.Entities.SIV
{
	[Table("SIVDetail", Schema = "Master")]
    public class SIVDetail:BaseClass<int>
    {
		[Required(ErrorMessage ="Please Select Store.")]
		public int StoreId { get; set; }
		public int? PoId { get; set; }
		[Display(Prompt ="Invoice Number")]
		public string InvoiceNumber { get; set; }
		[DataType(DataType.Date)]
		public DateTime? InvoiceDate { get; set; }
		public string InvoicePath { get; set; }
	}
}
