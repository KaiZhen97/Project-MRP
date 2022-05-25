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
    
    public partial class UAMUserGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UAMUserGroup()
        {
            this.EmployeeProfiles = new HashSet<EmployeeProfile>();
        }
    
        public System.Guid UserGroupID { get; set; }
        public string UserGroupName { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> Deleted { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeProfile> EmployeeProfiles { get; set; }
    }
}