using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MRP.Views.PurchasingRFQManagement
{
    public partial class RaiseRFQ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int RFQID = Convert.ToInt32(Page.RouteData.Values["RFQID"]);
            Console.WriteLine(RFQID);
        }
    }
}