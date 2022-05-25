using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using WebFrameWorkLib.BusinessLogic;

namespace MRP
{
    public partial class SiteMaster : MasterPage
    {
        private WebRequestApi webRequestApi = new WebRequestApi();

        protected void Page_Load(object sender, EventArgs e)
        {
            var tokenString = "";
            if (Session["accessToken"] == null)
                Response.Redirect("~/Login");
            else
                tokenString = Session["accessToken"].ToString();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!this.IsPostBack)
            {
                Session["Reset"] = true;
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
            }
            

            loadAccess(tokenString);
        }

        protected void loadAccess(string token)
        {
            string _commonUserPage = WebConfigurationManager.AppSettings["commonUserPage"];
            var _arrayUserPageList = _commonUserPage.Split('|');
            string _link = HttpContext.Current.Request.Path.Replace("/", "");

            if (!_arrayUserPageList.Contains(_link))
            {
                string methodURL = "api/UAM/postCheckLinkAccessRight";
                string apiMethod = "POST";
                string postDataStr = "{\"CurrentURL\": \"" + HttpContext.Current.Request.Url.AbsoluteUri.ToString() + "\"}";
                string contentType = "application/json";
                string alias = ConfigurationManager.AppSettings["alias"];

                WebRequestApiResponse webRequestApiResponse = webRequestApi.sendWebRequest(token, methodURL, postDataStr, apiMethod, contentType, alias);

                dynamic jsonObject = webRequestApiResponse.responseJson;

                if (jsonObject == null)
                    Response.Redirect("~/Error", true);

                if (jsonObject["urlStatus"] != null)
                {
                    bool access = jsonObject["urlStatus"];

                    if (!access)
                        Response.Redirect("~/Error", true);
                }
            }
        }
    }

}