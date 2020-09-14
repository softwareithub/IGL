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
        public int IsPayable { get; set; }
    }
}
