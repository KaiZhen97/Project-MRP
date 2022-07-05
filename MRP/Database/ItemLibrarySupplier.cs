//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MRP.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class ItemLibrarySupplier
    {
        public int ID { get; set; }
        public int ItemLibraryID { get; set; }
        public int IsDefault { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string DeliveryTerm { get; set; }
        public Nullable<System.DateTime> QuotationDate { get; set; }
        public string QuotationValidity { get; set; }
        public string UOM { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> UnitPriceDiscount { get; set; }
        public Nullable<int> MinAmountPerOrder { get; set; }
        public Nullable<int> Std_LeadTime_Days { get; set; }
    
        public virtual ItemLibrary ItemLibrary { get; set; }
    }
}