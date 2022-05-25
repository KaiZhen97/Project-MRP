using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrameWorkLib.BusinessLogic;

namespace MRP
{
    public partial class Login : System.Web.UI.Page
    {
        private WebRequestApi webRequestApi = new WebRequestApi();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["accessToken"] = null;
            AntiforgeryChecker.Check(this, antiforgery);
        }

        protected void Button_Login_ServerClick(object sender, EventArgs e)
        {
            UserLogin();
        }

        protected void UserLogin()
        {
            if (string.IsNullOrEmpty(Input_Username.Value))
            {
                PromptAlert("Login Validation Failed" ,"Username cannot be empty!");
                Output_ErrorMsg.InnerHtml = "Username cannot be empty.";

                Input_Username.Focus();
                return;
            }

            if (string.IsNullOrEmpty(Input_Password.Value))
            {
                PromptAlert("Login Validation Failed", "Password cannot be empty!");
                Output_ErrorMsg.InnerHtml = "Password cannot be empty.";

                Input_Password.Focus();
                return;
            }

            try
            {
                string token = "";
                string methodURL = "token";
                string postDataStr = "Username=" + Input_Username.Value;
                postDataStr += "&Password=" + Server.UrlEncode(Input_Password.Value);
                postDataStr += "&grant_type=password";
                postDataStr += "&custom=Web";
                string apiMethod = "POST";
                string contentType = "application/json";
                string alias = ConfigurationManager.AppSettings["alias"];

                WebRequestApiResponse webRequestApiResponse = webRequestApi.sendWebRequest(token, methodURL, postDataStr, apiMethod, contentType, alias);

                dynamic jsonObject = webRequestApiResponse.responseJson;

                if (jsonObject["access_token"] != null)
                {
                    Session["accessToken"] = jsonObject["access_token"].ToString();
                    //string accessSystem = getAccessRightModule(jsonObject["access_token"].ToString());
                    string firstPage = getAccessRightModule(jsonObject["access_token"].ToString());

                    if (firstPage != null)
                    {
                        Response.Redirect("/", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }
                    else
                    {
                        PromptAlert("Login Error", "Invalid login.");
                        Output_ErrorMsg.InnerHtml = "Invalid login.";
                    }
                }
                else
                {
                    PromptAlert("Login Error", "Invalid login.");
                    Output_ErrorMsg.InnerHtml = "Invalid login.";
                }
            }
            catch (Exception)
            {
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");

                PromptAlert("Login Error", "Invalid login.");
                Output_ErrorMsg.InnerHtml = "Invalid login.";
            }
        }

        protected string getAccessRightModule(string accessToken)
        {
            try
            {
                string token = accessToken;
                string methodURL = "api/UAM/getRoleMenuList";
                string apiMethod = "Get";
                string contentType = "application/json; charset=utf-8";
                string alias = ConfigurationManager.AppSettings["alias"];

                WebRequestApiResponse webRequestApiResponse = webRequestApi.sendWebRequest(token, methodURL, "", apiMethod, contentType, alias);

                dynamic jsonObject = webRequestApiResponse.responseJson;

                if (jsonObject["recordsTotal"] > 0)
                {
                    return "";
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //protected string getAccessRightSystem(string accessToken)
        //{
        //    try
        //    {
        //        string token = accessToken;
        //        string methodURL = "api/UAM/getSystemRoleMenuList";
        //        string apiMethod = "Get";
        //        string contentType = "application/json; charset=utf-8";
        //        string alias = ConfigurationManager.AppSettings["alias"];

        //        WebRequestApiResponse webRequestApiResponse = webRequestApi.sendWebRequest(token, methodURL, "", apiMethod, contentType, alias);

        //        dynamic jsonObject = webRequestApiResponse.responseJson;

        //        if (jsonObject["recordsTotal"] > 0)
        //        {
        //            return "";
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        private void PromptAlert(string title, string message)
        {
            Utils.PromptAlert(this, title, message, "topCenter");
        }
    }
}