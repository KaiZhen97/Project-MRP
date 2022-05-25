using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRP.Models
{
    #region RFQ
    public class RFQList
    {
        public string CreatedByStaffName { get; set; }
        public string RFQNo { get; set; }
        public int RFQ_StatusID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Nullable<System.Guid> LastUpdatedBy { get; set; }
        public Nullable<System.Guid> Purchaser1 { get; set; }
        public int IsDraft { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime CancelDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ID { get; set; }
        public Nullable<System.Guid> Purchaser2 { get; set; }
        public string Purchaser2StaffName { get; set; }
        public string Purchaser1StaffName { get; set; }
        public string tempWatcher { get; set; }
    }
    #endregion

    #region PR
    public class PRDTO
    {
        public int PRID { get; set; }
        public Nullable<int> PRItemID { get; set; }
        public string PRNo { get; set; }
        public string JobNo { get; set; }
        public string Status { get; set; }
        public string IPN { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> ReqETA { get; set; }
        public Nullable<System.DateTime> UpdateETA { get; set; }
        public Nullable<int> AcceptUpdateETA { get; set; }
        public string UpdateETA_PurchaserRemark { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.Guid> Purchaser1 { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
    }
    #endregion
}