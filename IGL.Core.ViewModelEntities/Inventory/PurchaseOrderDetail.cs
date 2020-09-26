using IGL.Core.Entities.Inventory;
using IGL.Core.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Core.ViewModelEntities.Inventory
{
    public class PurchaseOrderDetail
    {
        public int PoId { get; set; }
        public string PoNUmber { get; set; }
        public DateTime PoDate { get; set; }
        public string VendorName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public string POStatus { get; set; }
        public string VendorEmail { get; set; }
        public string VendorPhone { get; set; }
        public string VendorGST { get; set; }
        public string VendorCIN { get; set; }
        public string VendorAddress { get; set; }
        public Organisation Organisation { get; set; }
        public string ItemCode { get; set; }
        public string HSNCode { get; set; }
        public string UnitName { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoicePath { get; set; }

    }
}
