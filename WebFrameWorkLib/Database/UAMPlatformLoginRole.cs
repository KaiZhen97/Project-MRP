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
    
    public partial class UAMPlatformLoginRole
    {
        public System.Guid PlatformLoginRoleID { get; set; }
        public System.Guid PlatformID { get; set; }
        public System.Guid RoleID { get; set; }
    
        public virtual UAMPlatform UAMPlatform { get; set; }
        public virtual UAMUserRole UAMUserRole { get; set; }
    }
}
