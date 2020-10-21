using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Core.Entities.Reports
{
    public class LowQuantityProductReport
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public decimal PeUnitCost { get; set; }
        public int ThresholdValue { get; set; }
        public int Quantity { get; set; }
    }
}
