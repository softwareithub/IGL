using IGL.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IGL.Core.Entities.ProductTransaction
{
    [Table("ProductTransaction", Schema = "Master")]
    public class ProductTransactionDetail:BaseClass<int>
    {
        public int MaterialId { get; set; }
        public string ItemNumber { get; set; }
        public decimal Quantity { get; set; }
    }
}
