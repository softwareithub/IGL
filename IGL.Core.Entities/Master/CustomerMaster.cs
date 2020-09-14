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
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string HouseNumber { get; set; }
        public string PlaceAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }

    }
}
