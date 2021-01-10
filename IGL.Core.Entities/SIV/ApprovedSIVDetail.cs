using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Core.Entities.SIV
{
    public class ApprovedSIVDetail
    {
        public DateTime PoDate { get; set; }
        public string PoNumber { get; set; }
        public string VendorName { get; set; }
        public string vendorEmail { get; set; }
        public string VendorPhone { get; set; }
        public string VendorType { get; set; }
    }
}
