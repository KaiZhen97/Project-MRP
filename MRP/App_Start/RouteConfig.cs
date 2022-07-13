using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace MRP
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //var settings = new FriendlyUrlSettings();
            //settings.AutoRedirectMode = RedirectMode.Permanent;
            //routes.EnableFriendlyUrls(settings);

            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //        name: "Default",
            //        url: "{controller}/{action}/{id}",
            //        defaults: new { action = "Index", id = UrlParameter.Optional }
            //    );
            #region UAM
            routes.MapPageRoute("AuditTableSetup", "Views/Administrator/AuditTableSetup", "~/Views/Administrator/AuditTableSetup.aspx");

            routes.MapPageRoute("Department", "Views/Administrator/Department", "~/Views/Administrator/DepartmentSetup.aspx");

            routes.MapPageRoute("Company", "Views/Administrator/Company", "~/Views/Administrator/CompanySetup.aspx");

            routes.MapPageRoute("ModuleSetup", "Views/Administrator/ModuleSetup", "~/Views/Administrator/ModuleSetup.aspx");

            routes.MapPageRoute("PlatformSetup", "Views/Administrator/PlatformSetup", "~/Views/Administrator/PlatformSetup.aspx");

            routes.MapPageRoute("RoleSetup", "Views/Administrator/RoleSetup", "~/Views/Administrator/RoleSetup.aspx");

            routes.MapPageRoute("TabSetup", "Views/Administrator/TabSetup", "~/Views/Administrator/TabSetup.aspx");

            routes.MapPageRoute("UserSetup", "Views/Administrator/UserSetup", "~/Views/Administrator/UserSetup.aspx");

            routes.MapPageRoute("TeamSetup", "Views/Administrator/TeamSetup", "~/Views/Administrator/TeamSetup.aspx");
            #endregion

            routes.MapPageRoute("Login", "Login", "~/Login.aspx");

            #region Purchasing RFQ Management
            routes.MapPageRoute("RaiseRFQFromDraft", "Views/PurchasingRFQManagement/RaiseRFQ/{ID}", "~/Views/PurchasingRFQManagement/RaiseRFQ.aspx");

            routes.MapPageRoute("RaiseRFQ", "Views/PurchasingRFQManagement/RaiseRFQ", "~/Views/PurchasingRFQManagement/RaiseRFQ.aspx");

            routes.MapPageRoute("RFQList", "Views/PurchasingRFQManagement/RFQList", "~/Views/PurchasingRFQManagement/RFQList.aspx");

            routes.MapPageRoute("RFQOnHand", "Views/PurchasingRFQManagement/RFQOnHand", "~/Views/PurchasingRFQManagement/RFQOnHand.aspx");

            routes.MapPageRoute("RFQDetails", "Views/PurchasingRFQManagement/RFQDetails/{ID}", "~/Views/PurchasingRFQManagement/RFQDetails.aspx");

            routes.MapPageRoute("AssignPurchaser", "Views/PurchasingRFQManagement/AssignPurchaser", "~/Views/PurchasingRFQManagement/AssignPurchaser.aspx");
            #endregion

            #region Item Library Management
            routes.MapPageRoute("ILM_ItemLibraryList", "Views/ItemLibraryManagement/ItemLibraryList", "~/Views/ItemLibraryManagement/ItemLibraryList.aspx");

            routes.MapPageRoute("ILM_AddNewItem", "Views/ItemLibraryManagement/AddNewItem", "~/Views/ItemLibraryManagement/AddNewItem.aspx");

            routes.MapPageRoute("ILM_EditItem", "Views/ItemLibraryManagement/EditItem/{Mode}/{ID}", "~/Views/ItemLibraryManagement/AddNewItem.aspx");

            routes.MapPageRoute("ILM_DuplicateItem", "Views/ItemLibraryManagement/AddNewItem/{Mode}/{ID}", "~/Views/ItemLibraryManagement/AddNewItem.aspx");

            routes.MapPageRoute("ILM_ItemDetails", "Views/ItemLibraryManagement/ItemDetails/{ID}", "~/Views/ItemLibraryManagement/ItemDetails.aspx");

            routes.MapPageRoute("ILM_CategorySetup", "Views/ItemLibraryManagement/CategorySetup", "~/Views/ItemLibraryManagement/CategorySetup.aspx");

            routes.MapPageRoute("ILM_SupplierSetup", "Views/ItemLibraryManagement/SupplierSetup", "~/Views/ItemLibraryManagement/SupplierSetup.aspx");
            #endregion

        }
    }
}
