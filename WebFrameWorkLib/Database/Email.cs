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
    
    public partial class Email
    {
        public int ID { get; set; }
        public string EmailName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string Recipient { get; set; }
        public System.DateTime DateCreate { get; set; }
        public int Status { get; set; }
    }
}