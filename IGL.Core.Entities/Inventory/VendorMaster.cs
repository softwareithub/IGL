using IGL.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IGL.Core.Entities.Inventory
{
    [Table("VendorMaster", Schema = "Master")]
    public class VendorMaster:BaseClass<int>
    {
        [Required(ErrorMessage = "Vendor name is required.")]
        [Display(Prompt = "Vendor Name")]
        public string VendorName { get; set; }

        [Required(ErrorMessage = "Vendor type is required.")]
        [Display(Prompt = "Vendor Type")]

        public string VendorType { get; set; }

        [Required(ErrorMessage = "Vendor email is required.")]
        [Display(Prompt = "Vendor Email")]
        [DataType(DataType.EmailAddress)]
        public string VendorEmail { get; set; }

        [Required(ErrorMessage = "Vendor phone is required.")]
        [Display(Prompt = "Vendor Phone")]
        [DataType(DataType.PhoneNumber)]
        public string VendorPhone { get; set; }

        [Display(Prompt = "Vendor PAN Number")]
        public string PANNumber { get; set; }

        [Display(Prompt = "Vendor GST Number")]
        public string GSTNumber { get; set; }
        [Display(Prompt = "Vendor Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Prompt = "Vendor Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Prompt = "Contact Person Name")]
        public string ContactPerson { get; set; }

        [Display(Prompt = "Contact Person Email")]
        public string ContactEmail { get; set; }
        [Display(Prompt = "Contact Person Phone")]
        public string ContactPhone { get; set; }

    }
}
