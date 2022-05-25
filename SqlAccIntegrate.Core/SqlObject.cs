using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace SqlAccIntegrate.Core
{
    public class SqlObject
    {
        private string DCF_FILE_PATH, FDB_NAME, SQL_ACC_USER_NAME, SQL_ACC_PASSWORD;
        static dynamic ComServer;
        static Int32 lBuildNo;
        static Type lBizType;

        public SqlObject(string dcfFilePath, string fdbName, string userName, string password)
        {
            ComServerInit();

            DCF_FILE_PATH = dcfFilePath;
            FDB_NAME = fdbName;
            SQL_ACC_USER_NAME = userName;
            SQL_ACC_PASSWORD = password;
        }

        #region General
        public ReturnObj_Master<List<Tariff>> GetTariffList()
        {
            ReturnObj_Master<List<Tariff>> retVal = new ReturnObj_Master<List<Tariff>>();
            string query = "SELECT * FROM TARIFF WHERE ISACTIVE=1";

            retVal = GenericGetMasterDataList<Tariff>(query);

            return retVal;
        }

        public ReturnObj_Data<bool> IsTariffExist(string code)
        {
            ReturnObj_Data<bool> retVal = new ReturnObj_Data<bool>();
            string query = $"SELECT FIRST 1 AUTOKEY FROM TARIFF WHERE CODE='{code}'";

            retVal = GenericCheckDataExist(query);

            return retVal;
        }
        #endregion

        #region PO
        public ReturnObj_Master<List<PO_Agent>> GetPO_AgentList()
        {
            ReturnObj_Master<List<PO_Agent>> retVal = new ReturnObj_Master<List<PO_Agent>>();
            string query = "SELECT CODE, DESCRIPTION FROM AGENT WHERE ISACTIVE='T'";

            retVal = GenericGetMasterDataList<PO_Agent>(query);

            return retVal;
        }

        public ReturnObj_Master<List<PO_ItemCode>> GetPO_ItemCodeList()
        {
            ReturnObj_Master<List<PO_ItemCode>> retVal = new ReturnObj_Master<List<PO_ItemCode>>();
            string query = "SELECT DOCKEY, CODE, DESCRIPTION FROM ST_ITEM WHERE ISACTIVE='T'";

            retVal = GenericGetMasterDataList<PO_ItemCode>(query);

            return retVal;
        }

        public ReturnObj_Master<List<PO_Tax>> GetPO_TaxList()
        {
            ReturnObj_Master<List<PO_Tax>> retVal = new ReturnObj_Master<List<PO_Tax>>();
            string query = "SELECT AUTOKEY, CODE, DESCRIPTION, TAXRATE FROM TAX WHERE ISACTIVE=1";

            retVal = GenericGetMasterDataList<PO_Tax>(query);

            if (retVal.Status && retVal.Master.Count > 0)
            {
                try
                {
                    foreach (PO_Tax tax in retVal.Master)
                    {
                        if (tax.TAXRATE.Contains(';'))
                            tax.SplitedTaxRate = tax.TAXRATE.Split(';').ToList();
                    }
                }
                catch (Exception ex)
                {
                    retVal.ErrorMsg = ex.ToString();
                }
            }

            return retVal;
        }

        public ReturnObj_Master<List<PO_Agent>> GetPO_Agent()
        {
            ReturnObj_Master<List<PO_Agent>> retVal = new ReturnObj_Master<List<PO_Agent>>();
            string query = "SELECT CODE, DESCRIPTION FROM AGENT WHERE ISACTIVE='T'";

            retVal = GenericGetMasterDataList<PO_Agent>(query);

            return retVal;
        }

        public ReturnObj_Master<List<PO_Terms>> GetPO_Terms()
        {
            ReturnObj_Master<List<PO_Terms>> retVal = new ReturnObj_Master<List<PO_Terms>>();
            string query = "SELECT CODE, DESCRIPTION FROM TERMS WHERE ISACTIVE='T'";

            retVal = GenericGetMasterDataList<PO_Terms>(query);

            return retVal;
        }

        public ReturnObj_Master<List<PO_Project>> GetPO_Project()
        {
            ReturnObj_Master<List<PO_Project>> retVal = new ReturnObj_Master<List<PO_Project>>();
            string query = "SELECT CODE, DESCRIPTION, DESCRIPTION2 FROM PROJECT WHERE ISACTIVE='T'";

            retVal = GenericGetMasterDataList<PO_Project>(query);

            return retVal;
        }

        public ReturnObj_Master<List<PO_Supplier>> GetPO_SupplierList()
        {
            ReturnObj_Master<List<PO_Supplier>> retVal = new ReturnObj_Master<List<PO_Supplier>>();

            string query = "SELECT S.CODE, S.COMPANYNAME, S.AREA, S.AGENT, S.CREDITTERM, S.CURRENCYCODE, C.BUYINGRATE AS CURRENCYRATE, S.TAXEXEMPTNO, ";
            query += "SB.BRANCHNAME, SB.ADDRESS1, SB.ADDRESS2, SB.ADDRESS3, SB.ADDRESS4, SB.ATTENTION, SB.PHONE1, SB.MOBILE, SB.FAX1 ";
            query += "FROM AP_SUPPLIER AS S INNER JOIN ";
            query += "(SELECT * FROM AP_SUPPLIERBRANCH WHERE BRANCHNAME='BILLING') AS SB ";
            query += "ON S.CODE = SB.CODE ";
            query += "LEFT JOIN (SELECT CODE, BUYINGRATE FROM CURRENCY) AS C ";
            query += "ON S.CURRENCYCODE = C.CODE ";

            retVal = GenericGetMasterDataList<PO_Supplier>(query);

            return retVal;
        }

        public ReturnObj_Full<PO_Master, List<PO_Detail>> GetPO(InputParams_GetPO input)
        {
            ReturnObj_Full<PO_Master, List<PO_Detail>> retVal = new ReturnObj_Full<PO_Master, List<PO_Detail>>();

            string mainQuery = "";
            string detailQuery = "";

            if ((input.DOCKEY == null || input.DOCKEY == 0) && string.IsNullOrEmpty(input.DOCNO))
            {
                retVal.ErrorMsg = "Missing input params: DOCKEY or DOCNO";
                return retVal;
            }

            mainQuery = $"SELECT * FROM PH_PO WHERE DOCKEY={input.DOCKEY} OR DOCNO='{input.DOCNO}'";
            detailQuery = @"SELECT * FROM PH_PODTL WHERE DOCKEY='{_pk}'";

            retVal = GenericGetFullData<PO_Master, PO_Detail>(mainQuery, detailQuery, "DOCKEY");

            return retVal;
        }

        public ReturnObj_AddPO AddPO(InputParams_Main_PO mainPO, List<InputParams_sdsDocDetail_PO> docDetails)
        {
            ReturnObj_AddPO retVal = new ReturnObj_AddPO();

            string _addedQuery = "";
            dynamic _addedObj;

            try
            {
                // 1. Initialize
                mainPO.DOCKEY = -1;
                mainPO.DOCNO = @"<<New>>";
                _addedQuery = $"SELECT FIRST 1 * FROM PH_PO WHERE UDF_MRP_POID='{mainPO.UDF_MRP_POID}' ORDER BY DOCKEY DESC";

                // 2. Process Add
                var innerRetVal = GenericAdd<InputParams_Main_PO, InputParams_sdsDocDetail_PO>(
                    "PH_Po", mainPO, inputDetailData: docDetails, detailDataSetStr: "cdsDocDetail");

                // 3. Get Added item if add success
                if (innerRetVal.Status)
                {
                    _addedObj = GetDynamicDataByQuery(_addedQuery);
                    if (_addedObj != null && _addedObj.RecordCount > 0)
                    {
                        retVal.DOCKEY = _addedObj.FindField("DOCKEY").Value;
                        retVal.DOCNO = _addedObj.FindField("DOCNO").AsString;
                    }
                }

                retVal.ErrorMsg = innerRetVal.ErrorMsg;
                retVal.Status = innerRetVal.Status;
            }
            catch (Exception ex)
            {
                retVal.ErrorMsg = ex.ToString();
            }

            return retVal;
        }

        public ReturnObj_EditPO EditPO(InputParams_Main_PO mainPO, List<InputParams_sdsDocDetail_PO> docDetails)
        {
            ReturnObj_EditPO retVal = new ReturnObj_EditPO();

            object searchKey = null;

            string _updatedQuery;
            dynamic _updatedObj;

            try
            {
                //  1. Initialize serach key
                if (mainPO.DOCKEY > 0)
                    searchKey = new { DOCKEY = mainPO.DOCKEY };
                else if (!string.IsNullOrEmpty(mainPO.DOCNO) && mainPO.DOCNO != @"<<New>>")
                    searchKey = new { DOCNO = mainPO.DOCNO };

                // 2. Process Edit
                var innerRetVal = GenericEdit<InputParams_Main_PO, InputParams_sdsDocDetail_PO>(
                    "PH_Po", searchKey, mainPO, inputDetailData: docDetails, detailDataSetStr: "cdsDocDetail");

                // 3. Get UpdateCount
                if (innerRetVal.Status)
                {
                    _updatedQuery = mainPO.DOCKEY > 0 ?
                        $"SELECT FIRST 1 UPDATECOUNT FROM PH_PO WHERE DOCKEY='{mainPO.DOCKEY}'"
                        :
                        $"SELECT FIRST 1 UPDATECOUNT FROM PH_PO WHERE DOCNO='{mainPO.DOCNO}'";

                    _updatedObj = GetDynamicDataByQuery(_updatedQuery);
                    if (_updatedObj != null && _updatedObj.RecordCount > 0)
                    {
                        retVal.UPDATECOUNT = _updatedObj.FindField("UPDATECOUNT").Value;
                    }
                }

                retVal.ErrorMsg = innerRetVal.ErrorMsg;
                retVal.Status = innerRetVal.Status;
            }
            catch (Exception ex)
            {
                retVal.ErrorMsg = ex.ToString();
            }

            return retVal;
        }

        public ReturnObj DelPO(InputParams_DelPO input)
        {
            ReturnObj retVal = new ReturnObj();

            try
            {
                retVal = GenericDelete("PH_Po", input);
            }
            catch (Exception ex)
            {
                retVal.ErrorMsg = ex.ToString();
            }

            return retVal;
        }
        #endregion

        #region Shared
        public void ComServerInit()
        {
            lBizType = Type.GetTypeFromProgID("SQLAcc.BizApp");
            ComServer = Activator.CreateInstance(lBizType);
        }

        public ReturnObj CheckLogin(string dcfFilePath = null, string fdbName = null, string userName = null, string password = null)
        {
            ReturnObj retVal = new ReturnObj();

            try
            {
                if (ComServer == null)
                {
                    ComServerInit();
                }

                if (!ComServer.IsLogin)
                {
                    ComServer.Login(
                        userName == null ? SQL_ACC_USER_NAME : userName,
                        password == null ? SQL_ACC_PASSWORD : password,
                        dcfFilePath == null ? DCF_FILE_PATH : dcfFilePath,
                        fdbName == null ? FDB_NAME : fdbName
                    );
                    ComServer.Minimize();
                }

                if (!ComServer.IsLogin)
                    retVal.ErrorMsg = "Login failed";
                else
                    retVal.Status = true;
            }
            catch (Exception ex)
            {
                retVal.ErrorMsg = ex.ToString();
            }

            return retVal;
        }

        public ReturnObj TryLogut()
        {
            ReturnObj retVal = new ReturnObj();

            try
            {
                if (ComServer != null && ComServer.IsLogin)
                {
                    ComServer.Logout();
                    FreeBiz(ComServer);
                }

                retVal.Status = true;
            }
            catch (Exception ex)
            {
                retVal.ErrorMsg = ex.ToString();
            }

            return retVal;
        }

        public ReturnObj_Master<List<T>> GenericGetMasterDataList<T>(string mainQuery)
        {
            ReturnObj_Master<List<T>> retVal = new ReturnObj_Master<List<T>>();

            try
            {
                if (CheckLogin().Status)
                {
                    retVal.Master = GetListByQuery_XmlStr<T>(mainQuery);
                    retVal.Status = true;
                }
                else
                {
                    retVal.ErrorMsg = CommonMsg.LoginFailed;
                }

            }
            catch (Exception ex)
            {
                retVal.ErrorMsg = ex.ToString();
            }

            return retVal;
        }

        public ReturnObj_Full<T, List<U>> GenericGetFullData<T, U>(string mainQuery, string detailQuery, string pkString, string docKeyPlaceHolder = "{_pk}")
        {
            ReturnObj_Full<T, List<U>> retVal = new ReturnObj_Full<T, List<U>>();
            dynamic _main = null, _detail = null, _pk = null;

            try
            {
                // 1. Check Login
                if (CheckLogin().Status)
                {
                    // 2. Get Master data
                    _main = GetDynamicDataByQuery(mainQuery);

                    // 3. Return if Master data not found
                    if (_main.RecordCount <= 0)
                    {
                        retVal.ErrorMsg = CommonMsg.DataNotFound;
                        return retVal;
                    }

                    // 4. Get Detail data
                    if (!string.IsNullOrEmpty(detailQuery) || !string.IsNullOrEmpty(pkString))
                    {
                        _pk = _main.FindField(pkString).Value;

                        if (_pk != null)
                        {
                            detailQuery = detailQuery.Replace(docKeyPlaceHolder, $"{_pk.ToString()}");

                            _detail = GetDynamicDataByQuery(detailQuery);
                        }
                    }

                    // 5. Convert
                    retVal.Master = Util.XMLStrToObject<T>(_main.XMLData);

                    if (_detail != null && _detail.RecordCount != null && _detail.RecordCount > 0)
                        retVal.Detail = Util.XMLStrToList<U>(_detail.XMLData);

                    retVal.Status = true;
                }
                else
                {
                    retVal.ErrorMsg = CommonMsg.LoginFailed;
                }
            }
            catch (Exception ex)
            {
                retVal.ErrorMsg = ex.ToString();
            }

            return retVal;
        }

        public ReturnObj GenericAdd<T, U>(string bizObjectName, T inputMainData, string mainDataSetStr = "MainDataSet", List<U> inputDetailData = null, string detailDataSetStr = null)
        {
            ReturnObj retVal = new ReturnObj();

            dynamic BizObject, _main, _detail;

            try
            {
                if (CheckLogin().Status)
                {
                    // 1. Initialize BizObject
                    BizObject = ComServer.BizObjects.Find(bizObjectName);

                    if (BizObject == null)
                    {
                        retVal.ErrorMsg = "BizObject not found";
                        return retVal;
                    }

                    // 2. Initialize DataSets
                    _main = BizObject.DataSets.Find(mainDataSetStr);
                    _detail = string.IsNullOrEmpty(detailDataSetStr) ? null : BizObject.DataSets.Find(detailDataSetStr);

                    if (_main == null)
                    {
                        retVal.ErrorMsg = "MainDataSet not found";
                        return retVal;
                    }

                    if (_detail == null && !string.IsNullOrEmpty(detailDataSetStr))
                    {
                        retVal.ErrorMsg = "DetailDataSet not found";
                        return retVal;
                    }

                    // 3. Insert New
                    BizObject.New();

                    // 3.1 Set MainDataSet
                    SetFieldValueByObjProps(_main, inputMainData);

                    // 3.2 Set DetailDataSet
                    if (inputDetailData != null && inputDetailData.Count > 0)
                    {
                        for (int i = 0; i < inputDetailData.Count; i++)
                        {
                            if (i == 0)
                                _detail.Edit();
                            else
                                _detail.Append();

                            SetFieldValueByObjProps(_detail, inputDetailData[i]);

                            _detail.Post();
                        }
                    }

                    // 4. Save and return
                    BizObject.Save();
                    BizObject.Close();

                    retVal.Status = true;
                }
                else
                {
                    retVal.ErrorMsg = CommonMsg.LoginFailed;
                }
            }
            catch (Exception ex)
            {
                retVal.ErrorMsg = ex.ToString();
            }

            return retVal;
        }

        public ReturnObj GenericEdit<T, U>(string bizObjectName, object searchKey, T inputMainData, string mainDataSetStr = "MainDataSet", List<U> inputDetailData = null, string detailDataSetStr = null)
        {
            ReturnObj retVal = new ReturnObj();

            dynamic BizObject, _main, _detail;

            try
            {
                if (CheckLogin().Status)
                {
                    // 1. Initialize BizObject
                    BizObject = ComServer.BizObjects.Find(bizObjectName);

                    if (BizObject == null)
                    {
                        retVal.ErrorMsg = "BizObject not found";
                        return retVal;
                    }

                    // 2. Initialize DataSets
                    _main = BizObject.DataSets.Find(mainDataSetStr);
                    _detail = string.IsNullOrEmpty(detailDataSetStr) ? null : BizObject.DataSets.Find(detailDataSetStr);

                    if (_main == null)
                    {
                        retVal.ErrorMsg = "MainDataSet not found";
                        return retVal;
                    }

                    if (_detail == null && !string.IsNullOrEmpty(detailDataSetStr))
                    {
                        retVal.ErrorMsg = "DetailDataSet not found";
                        return retVal;
                    }

                    // 3. Return error msg if search key is null
                    if (searchKey == null)
                    {
                        retVal.ErrorMsg = "Search key cannot be null in edit mode";
                        return retVal;
                    }

                    // 4. Update
                    // 4.1 Set Params Key
                    string errorParams = SetParamsByObjProps(BizObject, searchKey);

                    if (errorParams != null)
                    {
                        retVal.ErrorMsg = $"Params key set error, probably target not found: {errorParams}";
                        return retVal;
                    }

                    // 4.2 Open
                    BizObject.Open();
                    BizObject.Edit();

                    // 4.3 Edit MainDataSet
                    SetFieldValueByObjProps(_main, inputMainData);

                    // 4.4 Edit Detail DataSet
                    while (_detail.RecordCount > 0)
                    {
                        _detail.First();
                        _detail.Delete();
                    }

                    foreach (object input in inputDetailData)
                    {
                        _detail.Append();

                        SetFieldValueByObjProps(_detail, input);

                        _detail.Post();
                    }

                    // 5. Save and return
                    BizObject.Save();
                    BizObject.Close();

                    retVal.Status = true;
                }
                else
                {
                    retVal.ErrorMsg = CommonMsg.LoginFailed;
                }
            }
            catch (Exception ex)
            {
                retVal.ErrorMsg = ex.ToString();
            }

            return retVal;
        }

        public ReturnObj GenericDelete(string bizObjectName, object searchKey)
        {
            ReturnObj retVal = new ReturnObj();
            dynamic BizObject;

            try
            {
                // 1. Initialize BizObject
                BizObject = ComServer.BizObjects.Find(bizObjectName);

                if (BizObject == null)
                {
                    retVal.ErrorMsg = "BizObject not found";
                    return retVal;
                }

                // 2. Set Params Key
                string errorParams = SetParamsByObjProps(BizObject, searchKey);
                if (errorParams != null)
                {
                    retVal.ErrorMsg = $"Params key set error, probably target not found: {errorParams}";
                    return retVal;
                }

                // 3. Delete
                BizObject.Open();
                BizObject.Delete();
                BizObject.Close();

                retVal.Status = true;
            }
            catch (Exception ex)
            {
                retVal.ErrorMsg = ex.ToString();
            }

            return retVal;
        }

        public ReturnObj_Data<bool> GenericCheckDataExist(string mainQuery)
        {
            ReturnObj_Data<bool> retVal = new ReturnObj_Data<bool>();
            dynamic _dataSet;
            try
            {
                _dataSet = GetDynamicDataByQuery(mainQuery);

                if (_dataSet != null)
                {
                    retVal.Data = _dataSet.RecordCount > 0 ? true : false;
                    retVal.Status = true;
                }
                else
                {
                    retVal.ErrorMsg = "Query data set return null";
                    retVal.Status = false;
                }
            }
            catch (Exception ex)
            {
                retVal.ErrorMsg = ex.ToString();
            }

            return retVal;
        }

        public ReturnObj_GetNextRunningNum GenericGetNextDocNo(string docType, string description, int stateSet = 1)
        {
            ReturnObj_GetNextRunningNum retVal = new ReturnObj_GetNextRunningNum();

            dynamic _dataSet;
            string _sqlQuery = "SELECT A.*, B.NEXTNUMBER FROM SY_DOCNO A ";
            _sqlQuery += "INNER JOIN SY_DOCNO_DTL B ON (A.DOCKEY=B.PARENTKEY) ";
            _sqlQuery += $"WHERE A.STATESET={stateSet} ";
            _sqlQuery += string.IsNullOrEmpty(docType) ? "" : $"AND A.DOCTYPE='{docType}' ";
            _sqlQuery += string.IsNullOrEmpty(description) ? "" : $"AND A.DESCRIPTION='{description}' ";

            try
            {
                _dataSet = GetDynamicDataByQuery(_sqlQuery);

                if (_dataSet != null && _dataSet.RecordCount > 0)
                {
                    retVal.Description = _dataSet.FindField("DESCRIPTION").AsString;
                    retVal.Format = _dataSet.FindField("Format").AsString;
                    retVal.NextRunningNum = _dataSet.FindField("NEXTNUMBER").AsFloat;
                    retVal.Status = true;
                }
                else
                {
                    retVal.ErrorMsg = CommonMsg.DataNotFound;
                }
            }
            catch (Exception ex)
            {
                retVal.ErrorMsg = ex.ToString();
            }

            return retVal;
        }

        public ReturnObj_Data<T> GenericGetLastPK<T>(string tableName, string pkName)
        {
            ReturnObj_Data<T> retVal = new ReturnObj_Data<T>();

            dynamic _dataSet;
            string _sqlQuery = $"SELECT FIRST 1 {pkName} FROM {tableName} ORDER BY {pkName} DESC";

            try
            {
                if (CheckLogin().Status)
                {
                    _dataSet = GetDynamicDataByQuery(_sqlQuery);

                    if (_dataSet != null && _dataSet.RecordCount > 0)
                    {
                        var pkVal = _dataSet.FindField(pkName).Value;

                        retVal.Data = Convert.ChangeType(pkVal, typeof(T));
                        retVal.Status = true;
                    }
                    else
                    {
                        retVal.ErrorMsg = "Data not found";
                    }
                }
                else
                {
                    retVal.ErrorMsg = CommonMsg.LoginFailed;
                }
            }
            catch (Exception ex)
            {
                retVal.ErrorMsg = ex.ToString();
            }

            return retVal;
        }
        #endregion

        #region Private
        private List<T> GetListByQuery_Dynamic<T>(string sqlQuery)
        {
            // 1. Declaration
            List<T> retDataList = null;
            dynamic _dataSet;

            // 2. Get Data
            _dataSet = GetDynamicDataByQuery(sqlQuery);

            // 3. Conver Data
            retDataList = Util.DynamicToList<T>(_dataSet);

            return retDataList;
        }

        private List<T> GetListByQuery_XmlStr<T>(string sqlQuery)
        {
            // 1. Declaration
            List<T> retDataList = null;
            dynamic _dataSet;

            // 2. Get Data
            _dataSet = GetDynamicDataByQuery(sqlQuery);

            // 3. Conver Data
            retDataList = Util.XMLStrToList<T>(_dataSet.XMLData);

            return retDataList;
        }

        private DataTable GetDataTableByQuery(string sqlQuery)
        {
            // 1. Declaration
            DataTable dataTbl = null;
            dynamic _dataSet;

            // 2. Get Data
            _dataSet = GetDynamicDataByQuery(sqlQuery);

            // 3. Convert Data
            dataTbl = Util.DynamicToDataTable(_dataSet, "TBL");

            return dataTbl;
        }

        private dynamic GetDynamicDataByQuery(string sqlQuery)
        {
            // 1. Declaration
            dynamic _dataSet = null;

            // 2. Execute Query
            _dataSet = ComServer.DBManager.NewDataSet(sqlQuery);

            return _dataSet;
        }

        private void SetFieldValueByObjProps(dynamic dataSet, object inputParams)
        {
            Type type = inputParams.GetType();

            foreach (PropertyInfo prop in type.GetProperties())
            {
                Type propType = prop.PropertyType;
                object propValue = prop.GetValue(inputParams);

                if (propValue == null)
                    continue;

                if (propType == typeof(string))
                    dataSet.FindField(prop.Name).AsString = propValue;
                else
                    dataSet.FindField(prop.Name).value = propValue;
            }
        }

        private string SetParamsByObjProps(dynamic BizObject, object inputParamsObj)
        {
            Type type = inputParamsObj.GetType();
            dynamic _docKey;

            foreach (PropertyInfo prop in type.GetProperties())
            {
                _docKey = BizObject.FindKeyByRef(prop.Name, prop.GetValue(inputParamsObj));

                if (_docKey != null && !Convert.IsDBNull(_docKey))
                    BizObject.Params.Find(prop.Name).Value = _docKey;
                else
                    return prop.Name;
            }

            return null;
        }

        private void FreeBiz(object BizObj)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(BizObj);
        }
        #endregion
    }
}
