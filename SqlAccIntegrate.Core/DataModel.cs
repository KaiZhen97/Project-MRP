using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace SqlAccIntegrate.Core
{
    public class Tariff
    {
        [XmlAttribute("AUTOKEY")]
        public Int64 AUTOKEY { get; set; }
        [XmlAttribute("CODE")]
        public string CODE { get; set; }
        [XmlAttribute("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [XmlAttribute("ISDEFAULT")]
        public Int16 ISDEFAULT { get; set; }
        [XmlAttribute("ISACTIVE")]
        public Int16 ISACTIVE { get; set; }
        [XmlAttribute("ROWVER")]
        public Int32 ROWVER { get; set; }
    }

    #region PO
    public class InputParams_Main_PO
    {
        public Nullable<Int32> DOCKEY { get; set; }
        public string DOCNO { get; set; }
        public string DOCNOEX { get; set; }
        public Nullable<DateTime> DOCDATE { get; set; }
        public Nullable<DateTime> POSTDATE { get; set; }
        public Nullable<DateTime> TAXDATE { get; set; }
        public string CODE { get; set; }
        public string COMPANYNAME { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string ADDRESS4 { get; set; }
        public string PHONE1 { get; set; }
        public string MOBILE { get; set; }
        public string FAX1 { get; set; }
        public string ATTENTION { get; set; }
        public string AREA { get; set; }
        public string AGENT { get; set; }
        public string PROJECT { get; set; }
        public string TERMS { get; set; }
        public string CURRENCYCODE { get; set; }
        public Nullable<decimal> CURRENCYRATE { get; set; }
        public string SHIPPER { get; set; }
        public string DESCRIPTION { get; set; }
        public string CANCELLED { get; set; }
        public Nullable<decimal> DOCAMT { get; set; }
        public Nullable<decimal> LOCALDOCAMT { get; set; }
        public string D_DOCNO { get; set; }
        public string D_PAYMENTMETHOD { get; set; }
        public string D_CHEQUENUMBER { get; set; }
        public string D_PAYMENTPROJECT { get; set; }
        public Nullable<decimal> D_BANKCHARGE { get; set; }
        public Nullable<decimal> D_AMOUNT { get; set; }
        public string VALIDITY { get; set; }
        public string DELIVERYTERM { get; set; }
        public string CC { get; set; }
        public string DOCREF1 { get; set; }
        public string DOCREF2 { get; set; }
        public string DOCREF3 { get; set; }
        public string DOCREF4 { get; set; }
        public string BRANCHNAME { get; set; }
        public string DADDRESS1 { get; set; }
        public string DADDRESS2 { get; set; }
        public string DADDRESS3 { get; set; }
        public string DADDRESS4 { get; set; }
        public string DATTENTION { get; set; }
        public string DPHONE1 { get; set; }
        public string DMOBILE { get; set; }
        public string DFAX1 { get; set; }
        public string TAXEXEMPTNO { get; set; }
        public object ATTACHMENTS { get; set; }
        public object NOTE { get; set; }
        public string TRANSFERABLE { get; set; }
        public Nullable<Int32> UPDATECOUNT { get; set; }
        public Nullable<Int32> PRINTCOUNT { get; set; }
        public Nullable<Int64> DOCNOSETKEY { get; set; }
        public string NEXTDOCNO { get; set; }
        public string CHANGED { get; set; }

        public string UDF_APPROVED { get; set; }
        public string UDF_APPROVER { get; set; }
        public string UDF_PO_ACK { get; set; }
        public string UDF_EPR_NO { get; set; }
        public Nullable<DateTime> UDF_PROJECT_REQ_DATE { get; set; }
        public string UDF_PROJECT_CODE { get; set; }
        public Nullable<DateTime> UDF_SHIP_DATE { get; set; }
        public string UDF_TRACK_NO { get; set; }
        public Nullable<Int32> UDF_MRP_POID { get; set; }
    }

    public class InputParams_sdsDocDetail_PO
    {
        public Nullable<Int32> DTLKEY { get; set; }
        public Nullable<Int32> DOCKEY { get; set; }
        public Nullable<Int32> SEQ { get; set; }
        public string STYLEID { get; set; }
        public string NUMBER { get; set; }
        public string ITEMCODE { get; set; }
        public string LOCATION { get; set; }
        public string BATCH { get; set; }
        public string PROJECT { get; set; }
        public string DESCRIPTION { get; set; }
        public string DESCRIPTION2 { get; set; }
        public object DESCRIPTION3 { get; set; }
        public string PERMITNO { get; set; }
        public Nullable<decimal> QTY { get; set; }
        public string UOM { get; set; }
        public Nullable<decimal> RATE { get; set; }
        public Nullable<decimal> SQTY { get; set; }
        public Nullable<decimal> SUOMQTY { get; set; }
        public Nullable<decimal> OFFSETQTY { get; set; }
        public Nullable<decimal> UNITPRICE { get; set; }
        public Nullable<DateTime> DELIVERYDATE { get; set; }
        public string DISC { get; set; }
        public string TAX { get; set; }
        public string TARIFF { get; set; }
        public string TAXRATE { get; set; }
        public Nullable<decimal> TAXAMT { get; set; }
        public Nullable<decimal> LOCALTAXAMT { get; set; }
        public Nullable<Int16> TAXINCLUSIVE { get; set; }
        public Nullable<decimal> AMOUNT { get; set; }
        public Nullable<decimal> LOCALAMOUNT { get; set; }
        public string PRINTABLE { get; set; }
        public string FROMDOCTYPE { get; set; }
        public Nullable<Int32> FROMDOCKEY { get; set; }
        public Nullable<Int32> FROMDTLKEY { get; set; }
        public string TRANSFERABLE { get; set; }
        public string REMARK1 { get; set; }
        public string REMARK2 { get; set; }
        public string CHANGED { get; set; }
        public string CompanyItemCode { get; set; }
        public string UDF_JOBNO { get; set; }
        public Nullable<DateTime> UDF_SHIP_DATE { get; set; }
        public string UDF_TRACK_NO { get; set; }
        public string UDF_MIDA_SST { get; set; }
    }

    public class InputParams_GetPO
    {
        public Nullable<Int32> DOCKEY { get; set; }
        public string DOCNO { get; set; }
    }

    public class InputParams_DelPO
    {
        public Nullable<Int32> DOCKEY { get; set; }
    }

    public class ReturnObj_AddPO : ReturnObj
    {
        public Nullable<Int32> DOCKEY { get; set; } = -1;
        public string DOCNO { get; set; }
    }

    public class ReturnObj_EditPO : ReturnObj
    {
        public Nullable<Int32> UPDATECOUNT { get; set; }
    }

    public class PO_Master
    {
        [XmlAttribute("DOCKEY")]
        public Int32 DOCKEY { get; set; }
        [XmlAttribute("DOCNO")]
        public string DOCNO { get; set; }
        [XmlAttribute("DOCNOEX")]
        public string DOCNOEX { get; set; }
        [XmlAttribute("DOCDATE")]
        public string DOCDATE { get; set; }
        [XmlAttribute("POSTDATE")]
        public string POSTDATE { get; set; }
        [XmlAttribute("TAXDATE")]
        public string TAXDATE { get; set; }
        [XmlAttribute("CODE")]
        public string CODE { get; set; }
        [XmlAttribute("COMPANYNAME")]
        public string COMPANYNAME { get; set; }
        [XmlAttribute("ADDRESS1")]
        public string ADDRESS1 { get; set; }
        [XmlAttribute("ADDRESS2")]
        public string ADDRESS2 { get; set; }
        [XmlAttribute("ADDRESS3")]
        public string ADDRESS3 { get; set; }
        [XmlAttribute("ADDRESS4")]
        public string ADDRESS4 { get; set; }
        [XmlAttribute("PHONE1")]
        public string PHONE1 { get; set; }
        [XmlAttribute("MOBILE")]
        public string MOBILE { get; set; }
        [XmlAttribute("FAX1")]
        public string FAX1 { get; set; }
        [XmlAttribute("ATTENTION")]
        public string ATTENTION { get; set; }
        [XmlAttribute("AREA")]
        public string AREA { get; set; }
        [XmlAttribute("AGENT")]
        public string AGENT { get; set; }
        [XmlAttribute("PROJECT")]
        public string PROJECT { get; set; }
        [XmlAttribute("TERMS")]
        public string TERMS { get; set; }
        [XmlAttribute("CURRENCYCODE")]
        public string CURRENCYCODE { get; set; }
        [XmlAttribute("CURRENCYRATE")]
        public decimal CURRENCYRATE { get; set; }
        [XmlAttribute("SHIPPER")]
        public string SHIPPER { get; set; }
        [XmlAttribute("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [XmlAttribute("CANCELLED")]
        public string CANCELLED { get; set; }
        [XmlAttribute("DOCAMT")]
        public decimal DOCAMT { get; set; }
        [XmlAttribute("LOCALDOCAMT")]
        public decimal LOCALDOCAMT { get; set; }
        [XmlAttribute("D_DOCNO")]
        public string D_DOCNO { get; set; }
        [XmlAttribute("D_PAYMENTMETHOD")]
        public string D_PAYMENTMETHOD { get; set; }
        [XmlAttribute("D_CHEQUENUMBER")]
        public string D_CHEQUENUMBER { get; set; }
        [XmlAttribute("D_PAYMENTPROJECT")]
        public string D_PAYMENTPROJECT { get; set; }
        [XmlAttribute("D_BANKCHARGE")]
        public decimal D_BANKCHARGE { get; set; }
        [XmlAttribute("D_AMOUNT")]
        public decimal D_AMOUNT { get; set; }
        [XmlAttribute("VALIDITY")]
        public string VALIDITY { get; set; }
        [XmlAttribute("DELIVERYTERM")]
        public string DELIVERYTERM { get; set; }
        [XmlAttribute("CC")]
        public string CC { get; set; }
        [XmlAttribute("DOCREF1")]
        public string DOCREF1 { get; set; }
        [XmlAttribute("DOCREF2")]
        public string DOCREF2 { get; set; }
        [XmlAttribute("DOCREF3")]
        public string DOCREF3 { get; set; }
        [XmlAttribute("DOCREF4")]
        public string DOCREF4 { get; set; }
        [XmlAttribute("BRANCHNAME")]
        public string BRANCHNAME { get; set; }
        [XmlAttribute("DADDRESS1")]
        public string DADDRESS1 { get; set; }
        [XmlAttribute("DADDRESS2")]
        public string DADDRESS2 { get; set; }
        [XmlAttribute("DADDRESS3")]
        public string DADDRESS3 { get; set; }
        [XmlAttribute("DADDRESS4")]
        public string DADDRESS4 { get; set; }
        [XmlAttribute("DATTENTION")]
        public string DATTENTION { get; set; }
        [XmlAttribute("DPHONE1")]
        public string DPHONE1 { get; set; }
        [XmlAttribute("DMOBILE")]
        public string DMOBILE { get; set; }
        [XmlAttribute("DFAX1")]
        public string DFAX1 { get; set; }
        [XmlAttribute("TAXEXEMPTNO")]
        public string TAXEXEMPTNO { get; set; }
        //[XmlAttribute("ATTACHMENTS")]
        //public object ATTACHMENTS { get; set; }
        //[XmlAttribute("NOTE")]
        //public object NOTE { get; set; }
        [XmlAttribute("TRANSFERABLE")]
        public string TRANSFERABLE { get; set; }
        [XmlAttribute("UPDATECOUNT")]
        public Int32 UPDATECOUNT { get; set; }
        [XmlAttribute("PRINTCOUNT")]
        public Int32 PRINTCOUNT { get; set; }
        [XmlAttribute("UDF_APPROVED")]
        public string UDF_APPROVED { get; set; }
        [XmlAttribute("UDF_APPROVER")]
        public string UDF_APPROVER { get; set; }
        [XmlAttribute("UDF_PO_ACK")]
        public string UDF_PO_ACK { get; set; }
        [XmlAttribute("UDF_EPR_NO")]
        public string UDF_EPR_NO { get; set; }
        [XmlAttribute("UDF_PROJECT_REQ_DATE")]
        public string UDF_PROJECT_REQ_DATE { get; set; }
        [XmlAttribute("UDF_PROJECT_CODE")]
        public string UDF_PROJECT_CODE { get; set; }
        [XmlAttribute("UDF_SHIP_DATE")]
        public string UDF_SHIP_DATE { get; set; }
        [XmlAttribute("UDF_TRACK_NO")]
        public string UDF_TRACK_NO { get; set; }
        [XmlAttribute("UDF_MRP_POID")]
        public Int32 UDF_MRP_POID { get; set; }
    }

    public class PO_Detail
    {
        [XmlAttribute("DTLKEY")]
        public Int32 DTLKEY { get; set; }
        [XmlAttribute("DOCKEY")]
        public Int32 DOCKEY { get; set; }
        [XmlAttribute("SEQ")]
        public Int32 SEQ { get; set; }
        [XmlAttribute("STYLEID")]
        public string STYLEID { get; set; }
        [XmlAttribute("NUMBER")]
        public string NUMBER { get; set; }
        [XmlAttribute("ITEMCODE")]
        public string ITEMCODE { get; set; }
        [XmlAttribute("LOCATION")]
        public string LOCATION { get; set; }
        [XmlAttribute("BATCH")]
        public string BATCH { get; set; }
        [XmlAttribute("PROJECT")]
        public string PROJECT { get; set; }
        [XmlAttribute("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [XmlAttribute("DESCRIPTION2")]
        public string DESCRIPTION2 { get; set; }
        //[XmlAttribute("DESCRIPTION3")]
        //public object DESCRIPTION3 { get; set; }
        [XmlAttribute("PERMITNO")]
        public string PERMITNO { get; set; }
        [XmlAttribute("QTY")]
        public decimal QTY { get; set; }
        [XmlAttribute("UOM")]
        public string UOM { get; set; }
        [XmlAttribute("RATE")]
        public decimal RATE { get; set; }
        [XmlAttribute("SQTY")]
        public decimal SQTY { get; set; }
        [XmlAttribute("SUOMQTY")]
        public decimal SUOMQTY { get; set; }
        [XmlAttribute("OFFSETQTY")]
        public decimal OFFSETQTY { get; set; }
        [XmlAttribute("UNITPRICE")]
        public decimal UNITPRICE { get; set; }
        [XmlAttribute("DELIVERYDATE")]
        public string DELIVERYDATE { get; set; }
        [XmlAttribute("DISC")]
        public string DISC { get; set; }
        [XmlAttribute("TAX")]
        public string TAX { get; set; }
        [XmlAttribute("TARIFF")]
        public string TARIFF { get; set; }
        [XmlAttribute("TAXRATE")]
        public string TAXRATE { get; set; }
        [XmlAttribute("TAXAMT")]
        public decimal TAXAMT { get; set; }
        [XmlAttribute("LOCALTAXAMT")]
        public decimal LOCALTAXAMT { get; set; }
        [XmlAttribute("TAXINCLUSIVE")]
        public Int16 TAXINCLUSIVE { get; set; }
        [XmlAttribute("AMOUNT")]
        public decimal AMOUNT { get; set; }
        [XmlAttribute("LOCALAMOUNT")]
        public decimal LOCALAMOUNT { get; set; }
        [XmlAttribute("PRINTABLE")]
        public string PRINTABLE { get; set; }
        [XmlAttribute("FROMDOCTYPE")]
        public string FROMDOCTYPE { get; set; }
        [XmlAttribute("FROMDOCKEY")]
        public Int32 FROMDOCKEY { get; set; }
        [XmlAttribute("FROMDTLKEY")]
        public Int32 FROMDTLKEY { get; set; }
        [XmlAttribute("TRANSFERABLE")]
        public string TRANSFERABLE { get; set; }
        [XmlAttribute("REMARK1")]
        public string REMARK1 { get; set; }
        [XmlAttribute("REMARK2")]
        public string REMARK2 { get; set; }
        [XmlAttribute("UDF_JOBNO")]
        public string UDF_JOBNO { get; set; }
        [XmlAttribute("UDF_SHIP_DATE")]
        public string UDF_SHIP_DATE { get; set; }
        [XmlAttribute("UDF_TRACK_NO")]
        public string UDF_TRACK_NO { get; set; }
        [XmlAttribute("UDF_MIDA_SST")]
        public string UDF_MIDA_SST { get; set; }
    }

    public class PO_Supplier
    {
        [XmlAttribute("CODE")]
        public string CODE { get; set; }
        [XmlAttribute("COMPANYNAME")]
        public string COMPANYNAME { get; set; }
        [XmlAttribute("AREA")]
        public string AREA { get; set; }
        [XmlAttribute("AGENT")]
        public string AGENT { get; set; }
        [XmlAttribute("CREDITTERM")]
        public string CREDITTERM { get; set; }
        [XmlAttribute("CURRENCYCODE")]
        public string CURRENCYCODE { get; set; }
        [XmlAttribute("CURRENCYRATE")]
        public decimal CURRENCYRATE { get; set; }
        [XmlAttribute("TAXEXEMPTNO")]
        public string TAXEXEMPTNO { get; set; }
        [XmlAttribute("BRANCHNAME")]
        public string BRANCHNAME { get; set; }
        [XmlAttribute("ADDRESS1")]
        public string ADDRESS1 { get; set; }
        [XmlAttribute("ADDRESS2")]
        public string ADDRESS2 { get; set; }
        [XmlAttribute("ADDRESS3")]
        public string ADDRESS3 { get; set; }
        [XmlAttribute("ADDRESS4")]
        public string ADDRESS4 { get; set; }
        [XmlAttribute("ATTENTION")]
        public string ATTENTION { get; set; }
        [XmlAttribute("PHONE1")]
        public string PHONE1 { get; set; }
        [XmlAttribute("MOBILE")]
        public string MOBILE { get; set; }
        [XmlAttribute("FAX1")]
        public string FAX1 { get; set; }
        [XmlAttribute("UDF_DTERM")]
        public string UDF_DTERM { get; set; }
    }

    public class PO_ItemCode
    {
        [XmlAttribute("DOCKEY")]
        public int DOCKEY { get; set; }
        [XmlAttribute("CODE")]
        public string CODE { get; set; }
        [XmlAttribute("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
    }

    public class PO_Tax
    {
        [XmlAttribute("AUTOKEY")]
        public int AUTOKEY { get; set; }
        [XmlAttribute("CODE")]
        public string CODE { get; set; }
        [XmlAttribute("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [XmlAttribute("TAXRATE")]
        public string TAXRATE { get; set; }
        public List<string> SplitedTaxRate { get; set; }
    }

    public class PO_Agent
    {
        [XmlAttribute("CODE")]
        public string CODE { get; set; }
        [XmlAttribute("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
    }

    public class PO_Terms
    {
        [XmlAttribute("CODE")]
        public string CODE { get; set; }
        [XmlAttribute("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [XmlAttribute("TERMDAY")]
        public int TERMDAY { get; set; }
    }

    public class PO_Project
    {
        [XmlAttribute("CODE")]
        public string CODE { get; set; }
        [XmlAttribute("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [XmlAttribute("DESCRIPTION2")]
        public string DESCRIPTION2 { get; set; }
    }
    #endregion

    #region Supplier
    public class SP_Master
    {
        public string CODE { get; set; }
        public string CONTROLACCOUNT { get; set; }
        public string COMPANYNAME { get; set; }
        public string COMPANYNAME2 { get; set; }
        public string COMPANYCATEGORY { get; set; }
        public string AREA { get; set; }
        public string AGENT { get; set; }
        public string BIZNATURE { get; set; }
        public string CREDITTERM { get; set; }
        public decimal CREDITLIMIT { get; set; }
        public decimal OVERDUELIMIT { get; set; }
        public string STATEMENTTYPE { get; set; }
        public string CURRENCYCODE { get; set; }
        public decimal OUTSTANDING { get; set; }
        public string ALLOWEXCEEDCREDITLIMIT { get; set; }
        public string ADDPDCTOCRLIMIT { get; set; }
        public string AGINGON { get; set; }
        public string STATUS { get; set; }
        public string PRICETAG { get; set; }
        public DateTime CREATIONDATE { get; set; }
        public string TAX { get; set; }
        public string TAXEXEMPTNO { get; set; }
        public DateTime TAXEXPDATE { get; set; }
        public string BRN { get; set; }
        public string BRN2 { get; set; }
        public string GSTNO { get; set; }
        public string SALESTAXNO { get; set; }
        public string SERVICETAXNO { get; set; }
        public string TAXAREA { get; set; }
        public object ATTACHMENTS { get; set; }
        public string REMARK { get; set; }
        public object NOTE { get; set; }
        public Int64 LASTMODIFIED { get; set; }
        public string DIRTY { get; set; }
    }

    public class SP_Detail
    {

    }
    #endregion

    #region Shared
    public class ReturnObj_GetNextRunningNum : ReturnObj
    {
        public string Description { get; set; }
        public double NextRunningNum { get; set; }
        public string Format { get; set; }
    }

    public class ReturnObj
    {
        public bool Status { get; set; }
        public string ErrorMsg { get; set; }
    }

    public class ReturnObj_Data<T> : ReturnObj
    {
        public T Data { get; set; }
    }

    public class ReturnObj_Master<T> : ReturnObj
    {
        public T Master { get; set; }
    }

    public class ReturnObj_Full<T, U> : ReturnObj_Master<T>
    {
        public U Detail { get; set; }
    }

    public static class CommonMsg
    {
        public static string LoginFailed = "Login failed";
        public static string DataNotFound = "Data not found";
    }

    [Serializable(), XmlRoot("DATAPACKET")]
    public class XmlObj<T>
    {
        [XmlArray("ROWDATA")]
        [XmlArrayItem("ROW")]
        public List<T> DataList { get; set; }
    }
    #endregion
}
