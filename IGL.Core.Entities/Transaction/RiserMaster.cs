using IGL.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IGL.Core.Entities.Transaction
{
    [Table("RiserMaster", Schema = "Master")]
    public class RiserMaster : BaseClass<int>
    {
        [Display(Prompt = "Riser Code")]
        public string RiseCode { get; set; }

        [Required(ErrorMessage = "Raise Number is required.")]
        [Display(Prompt = "Riser Number")]
        public string RiserNumber { get; set; }
        [Display(Prompt = "Tower Name/Number")]
        public string Tower { get; set; }
        [Display(Prompt = "Enter Name ")]
        public string FirstName { get; set; }

        [Display(Prompt = "Floar Number ")]
        public string Floar { get; set; }

        [Display(Prompt = "Apartment Name ")]
        public string Apartment { get; set; }

        [Display(Prompt = "House Number")]
        public string HouseNumber { get; set; }
        [Display(Prompt = "Block Colony")]
        public string BlockColony { get; set; }
        [Display(Prompt = "Sub Area Phase")]
        public string SubAreaPhase { get; set; }
        [Display(Prompt = "Sub Area Phase")]
        public string Sector { get; set; }
        [Display(Prompt = "Sector Name")]
        public string Location { get; set; }
        [Display(Prompt = "Potential Customer Count")]
        public int PotentialCustomer { get; set; }
        [Display(Prompt = "Connected Customer Count")]
        public int ConectedCustomer { get; set; }
    }
}
