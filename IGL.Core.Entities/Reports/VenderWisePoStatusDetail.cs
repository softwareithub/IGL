using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Core.Entities.Reports
{
    public class VenderWisePoStatusDetail
    {
        public DateTime PoDate { get; set; }
        public string PoNumber { get; set; }
        public string VendorName { get; set; }
        public string POStatus { get; set; }
    }
}
