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
    
    public partial class PO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PO()
        {
            this.Attachments = new HashSet<Attachment>();
            this.POItems = new HashSet<POItem>();
            this.UpdateTraces = new HashSet<UpdateTrace>();
        }
    
        public int ID { get; set; }
        public string POBatchNo { get; set; }
        public string PONo { get; set; }
        public string Description { get; set; }
        public int PO_StatusID { get; set; }
        public Nullable<int> PrevPO_StatusID { get; set; }
        public string Supplier { get; set; }
        public string SupplierCode { get; set; }
        public string Suppl_BranchName { get; set; }
        public string Suppl_Address1 { get; set; }
        public string Suppl_Address2 { get; set; }
        public string Suppl_Address3 { get; set; }
        public string Suppl_Address4 { get; set; }
        public string Suppl_Area { get; set; }
        public string Suppl_EmailTo { get; set; }
        public string Suppl_AttentionTo { get; set; }
        public string Suppl_Phone { get; set; }
        public string Suppl_Mobile { get; set; }
        public string Suppl_Fax { get; set; }
        public string Suppl_Currency { get; set; }
        public Nullable<decimal> Suppl_CurrencyBuyingRate { get; set; }
        public string CompProfile_Address1 { get; set; }
        public string CompProfile_Address2 { get; set; }
        public string CompProfile_Address3 { get; set; }
        public string CompProfile_Address4 { get; set; }
        public string CompProfile_AttentionTo { get; set; }
        public string CompProfile_Phone { get; set; }
        public string CompProfile_Mobile { get; set; }
        public string CompProfile_Fax { get; set; }
        public string CreditTerm { get; set; }
        public string DeliveryTerm { get; set; }
        public string QuotationNo { get; set; }
        public string Validity { get; set; }
        public string Remark { get; set; }
        public Nullable<int> ReqApprv_T1 { get; set; }
        public Nullable<int> Approv_T1 { get; set; }
        public Nullable<int> ReqApprv_T2 { get; set; }
        public Nullable<int> Apprv_T2 { get; set; }
        public Nullable<int> ReqApprv_T3 { get; set; }
        public Nullable<int> Apprv_T3 { get; set; }
        public Nullable<int> ReqApprv_T4 { get; set; }
        public Nullable<int> Apprv_T4 { get; set; }
        public decimal NetTotal { get; set; }
        public decimal LocalNetTotal { get; set; }
        public int IsDraft { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public Nullable<System.Guid> LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public Nullable<System.Guid> CancelBy { get; set; }
        public string Transferable { get; set; }
        public Nullable<int> UpdateCount { get; set; }
        public Nullable<System.Guid> AppBatchKey { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attachment> Attachments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POItem> POItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UpdateTrace> UpdateTraces { get; set; }
    }
}
