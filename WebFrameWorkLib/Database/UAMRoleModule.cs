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
    
    public partial class UAMRoleModule
    {
        public System.Guid RoleModuleID { get; set; }
        public Nullable<System.Guid> RoleID { get; set; }
        public Nullable<System.Guid> ModuleID { get; set; }
        public Nullable<System.Guid> MAction { get; set; }
        public Nullable<System.Guid> SystemID { get; set; }
    
        public virtual UAMModule UAMModule { get; set; }
        public virtual UAMUserRole UAMUserRole { get; set; }
    }
}