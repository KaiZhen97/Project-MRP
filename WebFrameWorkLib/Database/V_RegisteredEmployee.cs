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
    
    public partial class V_RegisteredEmployee
    {
        public int ID { get; set; }
        public string StaffNumber { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeName { get; set; }
        public System.Guid ReportingManager { get; set; }
        public string ReportingManagerName { get; set; }
        public string LoginID { get; set; }
        public int AccessStatus { get; set; }
        public int Deleted { get; set; }
        public Nullable<System.DateTime> ResignDate { get; set; }
    }
}
