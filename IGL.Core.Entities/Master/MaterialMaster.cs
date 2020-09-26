using IGL.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IGL.Core.Entities.Master
{
	[Table("Material",Schema ="Master")]
    public class MaterialMaster: BaseClass<int>
    {
		[Required(ErrorMessage ="Material name is required.")]
		[StringLength(100,ErrorMessage ="Too Long name")]
		[Display(Prompt ="Material name")]
		public string Name { get; set; }
		[StringLength(50, ErrorMessage = "Too Long code")]
		public string Code { get; set; }

		[Required(ErrorMessage ="Unit type is required.")]
		public int UnitId { get; set; }
		[DataType(DataType.Currency)]
		[Display(Prompt ="Per Unit Cost")]
		public decimal PerUnitCost { get; set; }
		public int IsPayable { get; set; }

		[Display(Prompt ="Threshold Value")]
		public int ThresholdValue { get; set; }

		[Display(Prompt ="Quantity")]
		public int OpeningQuantity { get; set; }

		[Display(Prompt ="HSN Code")]
		public string HSNCode { get; set; }
		[NotMapped]
		public bool IsEachProductUniqueNumber { get; set; }
		public int IsUnique { get; set; }


	}
}
