using IGL.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGL.Core.Entities.StoreMaster
{
	[Table("StoreDetail", Schema = "Master")]
    public class StoreDetail:BaseClass<int>
    {
		[Required(ErrorMessage ="Store Name is required.")]
		[Display(Prompt ="Store Name")]
		[MaxLength(200,ErrorMessage ="Store Name is too long..")]
		public string StoreName { get; set; }

		[Required(ErrorMessage = "Store Phone is required.")]
		[Display(Prompt = "Store Phone")]
		[DataType(DataType.PhoneNumber)]
		public string StorePhone { get; set; }

		[Required(ErrorMessage = "Store Address is required.")]
		[Display(Prompt = "Store Address")]
		[MaxLength(200, ErrorMessage = "Store Address is too long..")]
		public string StoreAddress { get; set; }

		[Required(ErrorMessage = "Store Manager is required.")]
		[Display(Prompt = "Store Manager")]
		[MaxLength(200, ErrorMessage = "Store Manager is too long..")]
		public string StoreManager { get; set; }

		[DataType(DataType.EmailAddress)]
		[Display(Prompt = "Store Email")]
		public string ManagerEmail { get; set; }

		[DataType(DataType.PhoneNumber)]
		[Display(Prompt = "Store Manager Phone ")]
		[Required(ErrorMessage = "Store Manager Phone is required.")]
		public string ManagerPhone { get; set; }
	}
}
