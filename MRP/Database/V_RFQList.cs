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
    
    public partial class V_RFQList
    {
        public string CreatedByStaffName { get; set; }
        public string RFQNo { get; set; }
        public int RFQ_StatusID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public Nullable<System.Guid> LastUpdatedBy { get; set; }
        public Nullable<System.Guid> Purchaser1 { get; set; }
        public int IsDraft { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ID { get; set; }
        public Nullable<System.Guid> Purchaser2 { get; set; }
        public string Purchaser2StaffName { get; set; }
        public string Purchaser1StaffName { get; set; }
        public Nullable<System.Guid> Watcher_AccessID { get; set; }
        public string Status { get; set; }
        public Nullable<System.Guid> AttachmentKey { get; set; }
    }
}
