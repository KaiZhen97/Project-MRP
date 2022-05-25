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
    
    public partial class ItemLibrary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ItemLibrary()
        {
            this.PRItems = new HashSet<PRItem>();
            this.UpdateTraces = new HashSet<UpdateTrace>();
        }
    
        public int ID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string IPN { get; set; }
        public string Manufacturer { get; set; }
        public string MPN { get; set; }
        public string ItemDescription { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public string Currency { get; set; }
        public string UOM { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> UnitPriceDiscount { get; set; }
        public Nullable<int> MinAmountPerOrder { get; set; }
        public int RequiredSN { get; set; }
        public string Tariff { get; set; }
        public int RequiredCalibration { get; set; }
        public string MoreDetails { get; set; }
        public string DeliveryTerm { get; set; }
        public Nullable<System.DateTime> QuotationDate { get; set; }
        public string QuotationValidity { get; set; }
        public Nullable<int> Std_LeadTime_Days { get; set; }
        public Nullable<System.Guid> Purchaser1 { get; set; }
        public Nullable<System.Guid> Purchaser2 { get; set; }
        public string KeyTechSpec { get; set; }
        public int IsDefault { get; set; }
        public int IsDraft { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public Nullable<System.Guid> LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.Guid> DeletedBy { get; set; }
        public Nullable<System.Guid> AppKey { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRItem> PRItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UpdateTrace> UpdateTraces { get; set; }
    }
}