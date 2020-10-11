using IGL.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IGL.Core.Entities.Master
{
    [Table("CustomerMaster", Schema ="Master")]
    public class CustomerMaster : BaseClass<int>
    {
		[Required(ErrorMessage ="Please Enter Customer Name.")]
		[Display(Prompt ="Customer Name")]
		public string CustomerName { get; set; }
		[Required(ErrorMessage = "Please Enter Customer Phone.")]
		[Display(Prompt = "Customer Phone")]
		[DataType(DataType.PhoneNumber)]
		public string CustomerPhone { get; set; }

		[Required(ErrorMessage = "Please Enter Customer Email Id.")]
		[Display(Prompt = "Customer Email")]
		[DataType(DataType.EmailAddress)]
		public string CustomerEmail { get; set; }
		public int CustomerState { get; set; }
		public int CustomerCity { get; set; }

		[Display(Prompt ="Address Line 1")]
		[StringLength(150,ErrorMessage ="Address Line Is Too Long")]
		public string AddressLine1 { get; set; }
		[Display(Prompt = "Address Line 2")]
		[StringLength(150, ErrorMessage = "Address Line Is Too Long")]
		public string AddressLine2 { get; set; }

		[Display(Prompt ="Buisness Partner Number")]
		[StringLength(100,ErrorMessage ="BP Number Is Too Long")]
		public string BPNumber { get; set; }

		[Required(ErrorMessage = "Please Enter Registration Date.")]
		[DataType(DataType.Date)]
		public DateTime RegistrationDate { get; set; }

	}
}
