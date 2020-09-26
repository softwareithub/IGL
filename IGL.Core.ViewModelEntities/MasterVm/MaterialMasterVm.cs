using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Core.ViewModelEntities.MasterVm
{
    public class MaterialMasterVm
    {
        public int Id { get; set; }
        public string MaterialName { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public decimal PerUnitCost { get; set; }
        public string IsPayable { get; set; }
        public int ThresholdValue { get; set; }
        public int Quantity { get; set; }
        public string HSNCode { get; set; }
        public bool IsUniqe { get; set; }

    }
}
