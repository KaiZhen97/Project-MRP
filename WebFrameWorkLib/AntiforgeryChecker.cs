using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFrameWorkLib.BusinessLogic
{
    public static class AntiforgeryChecker
    {
        public static void Check(Page page, HiddenField antiforgery)
        {
            string antiforgerySetting = ConfigurationManager.AppSettings["antiforgery"];

            if (antiforgerySetting == "0")
            {
                return;
            }

            if (!page.IsPostBack)
            {
                Guid antiforgeryToken = Guid.NewGuid();
                page.Session["AntiforgeryToken"] = antiforgeryToken;
                antiforgery.Value = antiforgeryToken.ToString();
            }
            else
            {
                Guid stored = (Guid)page.Session["AntiforgeryToken"];
                Guid sent = new Guid(antiforgery.Value);
                if (sent != stored)
                {
                    throw new Exception("XSRF Attack Detected!");
                }
            }
        }
    }
}