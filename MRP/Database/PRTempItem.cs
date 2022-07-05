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
    
    public partial class PRTempItem
    {
        public int ID { get; set; }
        public int PRID { get; set; }
        public Nullable<int> ItemLibraryID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string IPN { get; set; }
        public string Manufacturer { get; set; }
        public string MPN { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> Recv_Quantity { get; set; }
        public Nullable<int> Withdrawn_Quantity { get; set; }
        public Nullable<System.DateTime> ReqETA { get; set; }
        public Nullable<System.DateTime> UpdateETA { get; set; }
        public Nullable<int> AcceptUpdateETA { get; set; }
        public string UpdateETA_PurchaserRemark { get; set; }
        public string UpdateETA_RequesterRemark { get; set; }
        public int IsUrgent { get; set; }
        public string Remark { get; set; }
    }
}
