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
    
    public partial class AuditActivityLog
    {
        public System.Guid AuditActID { get; set; }
        public System.Guid AccessID { get; set; }
        public string LogDesc { get; set; }
        public string OldVal { get; set; }
        public string NewVal { get; set; }
        public string LogCreateDate { get; set; }
        public string ModuleID { get; set; }
        public string UniqueKey { get; set; }
        public Nullable<System.Guid> AuditColumnID { get; set; }
    }
}
