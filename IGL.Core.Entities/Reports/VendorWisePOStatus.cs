using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Core.Entities.Reports
{
    public class VendorWisePOStatus
    {
        public int POCountByStatus { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string POStatus { get; set; }
    }
}
