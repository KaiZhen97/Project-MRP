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
    
    public partial class Dependant
    {
        public int ID { get; set; }
        public string DependantName { get; set; }
        public Nullable<int> DependantGender { get; set; }
        public string DependantDOB { get; set; }
        public string DependantOccupation { get; set; }
        public Nullable<int> EmployeeProfileID { get; set; }
    
        public virtual EmployeeProfile EmployeeProfile { get; set; }
    }
}
