//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebFrameWorkLib.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class PolicyAndMemo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int StatusID { get; set; }
        public System.DateTime Date { get; set; }
        public string PolicyMemoContent { get; set; }
        public string Attachment { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.Guid> LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public int CategoryID { get; set; }
        public int Deleted { get; set; }
    
        public virtual Category Category { get; set; }
    }
}
