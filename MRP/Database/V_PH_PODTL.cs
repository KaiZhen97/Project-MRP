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
    
    public partial class V_PH_PODTL
    {
        public int ID { get; set; }
        public int POID { get; set; }
        public int SEQ { get; set; }
        public string ITEMCODE { get; set; }
        public string PROJECT { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<int> QTY { get; set; }
        public string UOM { get; set; }
        public string TAX { get; set; }
        public string TARIFF { get; set; }
        public Nullable<int> TAXRATE { get; set; }
        public Nullable<decimal> TAXAMT { get; set; }
        public Nullable<decimal> LOCALTAXAMT { get; set; }
        public decimal AMOUNT { get; set; }
        public Nullable<decimal> LOCALAMOUNT { get; set; }
        public int TRANSFERABLE { get; set; }
        public string REMARK1 { get; set; }
        public string REMARK2 { get; set; }
        public string UDF_JOBNO { get; set; }
        public string UDF_TRACKING_NO { get; set; }
    }
}