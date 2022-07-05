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
    
    public partial class V_ItemLibraryList
    {
        public int ID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string IPN { get; set; }
        public string CategoryName { get; set; }
        public string Manufacturer { get; set; }
        public string MPN { get; set; }
        public string ItemDescription { get; set; }
        public string Tariff { get; set; }
        public int RequiredSN { get; set; }
        public int RequiredCalibration { get; set; }
        public string MoreDetails { get; set; }
        public string SupplierName { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> UnitPriceDiscount { get; set; }
        public Nullable<int> Std_LeadTime_Days { get; set; }
        public Nullable<System.Guid> Purchaser1AccessID { get; set; }
        public string Purchaser1 { get; set; }
        public Nullable<System.Guid> Purchaser2AccessID { get; set; }
        public string Purchaser2 { get; set; }
        public string KeyTechSpec { get; set; }
        public int IsDraft { get; set; }
        public int IsAddApproved { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public Nullable<System.Guid> LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.Guid> DeletedBy { get; set; }
        public string CreatedByStaffName { get; set; }
        public string LastUpdatedByStaffName { get; set; }
        public string DeletedByStaffName { get; set; }
        public Nullable<System.Guid> Expr1 { get; set; }
        public Nullable<System.Guid> Expr2 { get; set; }
    }
}
