using IGL.Core.Entities.Base;
using System;

namespace IGL.Core.Entities.Transaction
{
    public class MaterialReturn:BaseClass<int>
    {
        public int ProductId { get; set; }
        public string ItemNumber { get; set; }
        public decimal Quantity { get; set; }
        public string SlipNumber { get; set; }
        public int UniqueItemId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
