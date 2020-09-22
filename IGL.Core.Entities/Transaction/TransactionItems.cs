using IGL.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGL.Core.Entities.Transaction
{
    [Table("TransactionItems", Schema = "Master")]
    public class TransactionItems : BaseClass<int>
    {
        public int MaterialTransctionId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Remarks { get; set; }

    }
}
