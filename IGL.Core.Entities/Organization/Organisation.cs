using IGL.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGL.Core.Entities.Organization
{
	[Table("OrganisationDetail", Schema ="Master")]
    public class Organisation:BaseClass<int>
    {
		[Required(ErrorMessage ="Please Enter Name.")]
		[Display(Prompt ="Organisation Name")]
		[StringLength(200,ErrorMessage ="Organisation name is too large.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please Enter Address.")]
		[Display(Prompt = "Address Line 1")]
		[StringLength(450, ErrorMessage = "Address Line 1 is too large.")]
		public string AddressLine1 { get; set; }

		[Display(Prompt = "Address Line 2")]
		[StringLength(450, ErrorMessage = "Address Line 2 is too large.")]
		public string AddressLine2 { get; set; }

		[Display(Prompt = "CIN Number")]
		[StringLength(90, ErrorMessage = "CIN Number is too large.")]
		public string CINNumber { get; set; }

		[Display(Prompt = "GST Number")]
		[StringLength(90, ErrorMessage = "GST Number is too large.")]
		public string GSTNumber { get; set; }

		[DataType(DataType.PhoneNumber)]
		[Display(Prompt ="Landline number ")]
		public string LandlineNumber { get; set; }

		[DataType(DataType.EmailAddress)]
		[Display(Prompt = "Organisation Email Id. ")]
		public string EmailId { get; set; }

		[StringLength(250,ErrorMessage ="Name is too long.")]
		[Display(Prompt = "Contact Person Name. ")]
		public string ContactPerson { get; set; }

		[DataType(DataType.EmailAddress)]
		[Display(Prompt = "Contact Email Address")]
		public string ContactEmailId { get; set; }

		[DataType(DataType.PhoneNumber)]
		[Display(Prompt = "Contact Phone Detail")]
		public string ContactPhone { get; set; }
	}
}
