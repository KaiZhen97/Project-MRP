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
    
    public partial class ErrorLog
    {
        public int ErrLogID { get; set; }
        public System.DateTime ErrLogDate { get; set; }
        public string ErrLogThread { get; set; }
        public string ErrLogLevel { get; set; }
        public string ErrLogLogger { get; set; }
        public string ErrLogMessage { get; set; }
        public string ErrLogException { get; set; }
    }
}