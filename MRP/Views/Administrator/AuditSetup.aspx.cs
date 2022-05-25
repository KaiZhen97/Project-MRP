using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrameWorkLib.BusinessLogic;
using WebFrameWorkLib.Database;

namespace MRP.Views.Administrator
{
    public partial class AuditSetup : Page
    {
        private WebRequestApi webRequestApi = new WebRequestApi();
        private FrameWorkEntities dbContext = new FrameWorkEntities();
        private LogError logError = new LogError();
        private Common common = new Common();

        protected void Page_Init(object sender, EventArgs e)
        {
            loadAuditTableData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AntiforgeryChecker.Check(this, antiforgery);

            if(IsPostBack)
            {
                Session["Loaded"] = true;
            }
        }

        protected void loadAuditTableData()
        {
            try
            {
                string token = Session["accessToken"].ToString();
                string methodURL = "api/Audit/getTableNameList";
                string postDataStr = "";
                string apiMethod = "GET";
                string contentType = "application/json";
                string alias = ConfigurationManager.AppSettings["alias"];

                WebRequestApiResponse webRequestApiResponse = webRequestApi.sendWebRequest(token, methodURL, postDataStr, apiMethod, contentType, alias);

                dynamic jsonObject = webRequestApiResponse.responseJson;

                if (jsonObject["data"] != null)
                {
                    //var serializer = new JavaScriptSerializer();
                    //dynamic firslvl = serializer.Deserialize<dynamic>(jsonObject["data"]);
                    var firstLevel = jsonObject["data"];
                    string construcStr = "";
                    var countfirstlevel = 0;

                    foreach (var parentModule in firstLevel)
                    {
                        var firstlevelChkboxName = "firstlvl" + countfirstlevel.ToString();

                        //construcStr = construcStr + "<tr>" +
                        //    "<td style=\"width:100px;text-align: center; vertical-align: middle;\">" +
                        //            "<input class=\"table-ckb\" type=\"checkbox\" value=\"" + parentModule["TableName"] + "\" name=\"" + firstlevelChkboxName + "\" id=\"" + firstlevelChkboxName + "\" />" +
                        //            "<label for=\"" + firstlevelChkboxName + "\"></label>" +
                        //        "</td>" +
                        //        "<td colspan=\"3\">" + parentModule["TableName"] + "</td>" +
                        //    "</tr>";

                        construcStr = construcStr +
                            $"<tr>" +
                                $"<td><input type='checkbox' value='{parentModule["TableName"]}' name='{firstlevelChkboxName}' id='{firstlevelChkboxName}'/></td>" +
                                $"<td>{parentModule["TableName"]}</td>" +
                            $"</tr>";

                        countfirstlevel++;
                    }

                    tableBody_AuditTable.InnerHtml = construcStr;
                }
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string tableName = "";

                tableName = hiddenChkBoxValue.Value;

                string token = Session["accessToken"].ToString();
                string methodURL = "api/Audit/postEditAuditTable";
                string postDataStr = "{"
                    + "\"tableName\": \"" + tableName + "\" "
                    + "}";
                string apiMethod = "POST";
                string contentType = "application/json";
                string alias = ConfigurationManager.AppSettings["alias"];

                WebRequestApiResponse webRequestApiResponse = webRequestApi.sendWebRequest(token, methodURL, postDataStr, apiMethod, contentType, alias);

                dynamic jsonObject = webRequestApiResponse.responseJson;

                if (jsonObject["Message"] != null)
                {
                    if (webRequestApiResponse.responseStatus == true)
                    {
                        Utils.PromptPass(this, "Save Successfully", jsonObject["Message"].ToString());
                    }
                    else
                    {
                        Utils.PromptError(this, "Save Error", jsonObject["Message"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());

                Utils.PromptError(this, "Save Error", ex.Message.ToString());
            }
        }
    }
}