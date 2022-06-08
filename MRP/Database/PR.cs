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
    
    public partial class PR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PR()
        {
            this.UpdateTraces = new HashSet<UpdateTrace>();
        }
    
        public int ID { get; set; }
        public string PRBatchNo { get; set; }
        public string PRNo { get; set; }
        public string JobNo { get; set; }
        public string PrevJobNo { get; set; }
        public string Description { get; set; }
        public int PR_StatusID { get; set; }
        public Nullable<int> PrevPR_StatusID { get; set; }
        public Nullable<System.Guid> Purchaser1 { get; set; }
        public Nullable<System.Guid> Purchaser2 { get; set; }
        public Nullable<System.Guid> HOD { get; set; }
        public Nullable<int> ReqHOD_RaiseApprv { get; set; }
        public Nullable<int> HOD_RaiseApprv { get; set; }
        public Nullable<int> ReqHOD_EditApprv { get; set; }
        public Nullable<int> HOD_EditApprv { get; set; }
        public Nullable<int> Req_EditApprv { get; set; }
        public Nullable<int> EditApprv { get; set; }
        public Nullable<int> Req_CancelApprv { get; set; }
        public Nullable<int> CancelApprv { get; set; }
        public Nullable<int> Req_OnHoldApprv { get; set; }
        public Nullable<int> OnHoldApprv { get; set; }
        public int IsDraft { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public Nullable<System.Guid> LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public Nullable<System.Guid> CancelBy { get; set; }
        public Nullable<System.Guid> AppKey { get; set; }
        public Nullable<System.Guid> AppBatchKey { get; set; }
        public string CancelRemark { get; set; }
    
        public virtual PRStatu PRStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UpdateTrace> UpdateTraces { get; set; }
    }
}
