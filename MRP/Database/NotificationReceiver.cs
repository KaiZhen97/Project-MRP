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
    
    public partial class NotificationReceiver
    {
        public int ID { get; set; }
        public int NotificationID { get; set; }
        public System.Guid Receiver_AccessID { get; set; }
        public int Readed { get; set; }
    }
}