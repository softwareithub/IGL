using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Core.ViewModelEntities.MasterVm
{
    public class RateMasterVm
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Rate { get; set; }
        public DateTime FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
