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
    
    public partial class ItemLibraryAttachment
    {
        public System.Guid AttachmentID { get; set; }
        public Nullable<int> ItemLibraryID { get; set; }
        public string AttachmentName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int IsConfidential { get; set; }
        public int IsSubmitted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.Guid> DeletedBy { get; set; }
    
        public virtual ItemLibrary ItemLibrary { get; set; }
    }
}
