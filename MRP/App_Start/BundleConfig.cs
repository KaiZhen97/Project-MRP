using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace MRP
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Bundle Collection

            #region Script
            string[] commonScripts = new string[]
            {
                "~/Scripts/jquery-3.0.0.min.js",
                "~/Scripts/bootstrap.bundle.min.js",
                "~/Plugins/Pace/pace.min.js",
            };

            string[] componentScript = new string[]
            {
                "~/Js/Component/CheckBox.js",
                "~/Js/Component/Drawer.js",
            };

            string[] dataTablesScript = new string[]
            {
                "~/Plugins/DataTables/datatables.js",
            };

            string[] contextMenuScript = new string[]
            {
                "~/Plugins/jQuery-ContextMenu/jquery.contextMenu.js",
            };
            #endregion

            #region Style
            string[] commonStyle = new string[]
            {
                "~/Content/bootstrap.min.css",
                "~/Plugins/FontAwesome/css/all.min.css",
                "~/Plugins/Pace/pace.css",
                "~/Style/Component/Fonts.css",
            };

            string[] componentStyle = new string[]
            {
                "~/Style/Component/Flex.css",
                "~/Style/Component/Table.css",
                "~/Style/Component/Button.css",
                "~/Style/Component/ScrollBar.css",
                "~/Style/Component/CheckBox.css",
                "~/Style/Component/Select.css",
                "~/Style/Component/Input.css",
                "~/Style/Component/Drawer.css",
            };

            string[] dataTablesStyle = new string[]
            {
                "~/Plugins/DataTables/datatables.css",
                "~/Style/Plugins/DataTables.css"
            };

            string[] contextMenuStyle = new string[]
            {
                "~/Plugins/jQuery-ContextMenu/jquery.contextMenu.css",
                "~/Style/Plugins/ContextMenu.css"
            };
            #endregion

            #endregion

            #region Script Bundle

            #region Plugins
            bundles.Add(new ScriptBundle("~/scriptBundle/iziToast")
                .Include("~/Plugins/iziToast/iziToast.min.js")
                .Include("~/Js/Plugins/iziToastCust.js"));
            #endregion

            bundles.Add(new ScriptBundle("~/scriptBundle/Master")
                .Include(commonScripts)
                .Include("~/Js/SiteMaster.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/Component")
                .Include(componentScript));

            bundles.Add(new ScriptBundle("~/scriptBundle/Login")
                .Include(commonScripts)
                .Include("~/Js/Login.js"));

            #region Administrator
            bundles.Add(new ScriptBundle("~/scriptBundle/AuditSetup")
                .Include("~/Js/Administrator/AuditSetup.min.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/ModuleSetup")
                .Include(dataTablesScript)
                .Include("~/Js/Administrator/ModuleSetup.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/PlatformSetup")
                .Include(dataTablesScript)
                .Include(contextMenuScript)
                .Include("~/Js/Administrator/PlatformSetup.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/RoleSetup")
                .Include(dataTablesScript)
                .Include(contextMenuScript)
                .Include("~/Js/Administrator/RoleSetup.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/TabSetup")
                .Include(dataTablesScript)
                .Include(contextMenuScript)
                .Include("~/Js/Administrator/TabSetup.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/UserSetup")
                .Include(dataTablesScript)
                .Include(contextMenuScript)
                .Include("~/Js/Administrator/UserSetup.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/CompanySetup")
                .Include(dataTablesScript)
                .Include(contextMenuScript)
                .Include("~/Js/Administrator/CompanySetup.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/DepartmentSetup")
                .Include(dataTablesScript)
                .Include(contextMenuScript)
                .Include("~/Js/Administrator/DepartmentSetup.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/TeamSetup")
                .Include(dataTablesScript)
                .Include(contextMenuScript)
                .Include("~/Js/Administrator/TeamSetup.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/FileTypeAttachmentSetup")
                .Include(dataTablesScript)
                .Include(contextMenuScript)
                .Include("~/Js/Administrator/FileTypeAttachmentSetup.js"));
            #endregion

            #region RFQ
            bundles.Add(new ScriptBundle("~/scriptBundle/RFQ")
                .Include(dataTablesScript)
                .Include(contextMenuScript)
                .Include("~/Js/PurchasingRFQManagement/RFQ-1.0.0.min.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/RaiseRFQ")
                .Include("~/Js/PurchasingRFQManagement/RaiseRFQ-1.0.0.min.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/RFQOnHand")
                .Include(dataTablesScript)
                .Include(contextMenuScript)
                .Include("~/Js/PurchasingRFQManagement/RFQOnHand-1.0.0.min.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/RFQDetails")
                .Include(dataTablesScript)
                .Include("~/Js/PurchasingRFQManagement/RFQDetails-1.0.0.min.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/AssignPurchaser")
                .Include(dataTablesScript)
                .Include("~/Js/PurchasingRFQManagement/AssignPurchaser-1.0.0.min.js"));
            #endregion


            #region ItemLibrary
            bundles.Add(new ScriptBundle("~/scriptBundle/ILM/ItemLibraryList")
                .Include(dataTablesScript)
                .Include(contextMenuScript)
                //.Include("~/Js/ItemLibraryManagement/ItemLibraryList-1.0.0.min.js"));
            .Include("~/Js/ItemLibraryManagement/ItemLibraryList-1.0.0.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/ILM/AddNewItem")
                .Include(contextMenuScript)
                //.Include("~/Js/ItemLibraryManagement/AddNewPage-1.0.0.min.js"));
                .Include("~/Js/ItemLibraryManagement/AddNewItem-1.0.0.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/ILM/EditItem")
                .Include(contextMenuScript)
                .Include("~/Js/ItemLibraryManagement/EditItem.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/ILM/ItemDetails")
                .Include(contextMenuScript)
                .Include("~/Js/ItemLibraryManagement/ItemDetails.js"));

            bundles.Add(new ScriptBundle("~/scriptBundle/ILM/CategorySetup")
                .Include(dataTablesScript)
                .Include(contextMenuScript)
                //.Include("~/Js/ItemLibraryManagement/CategorySetup-1.0.0.min.js"));
                .Include("~/Js/ItemLibraryManagement/CategorySetup-1.0.0.js"));
            #endregion
            #endregion

            #region Style Bundle

            #region Plugins
            bundles.Add(new StyleBundle("~/styleBundle/iziToast")
                .Include("~/Plugins/iziToast/iziToast.min.css")
                .Include("~/Style/Plugins/iziToastCust.css"));
            #endregion

            bundles.Add(new StyleBundle("~/styleBundle/Component")
                .Include(componentStyle));

            bundles.Add(new StyleBundle("~/styleBundle/Master")
                .Include(commonStyle)
                .Include("~/Style/SiteMaster.css"));

            bundles.Add(new StyleBundle("~/styleBundle/Login")
                .Include(commonStyle)
                .Include("~/Style/Login.css"));

            bundles.Add(new StyleBundle("~/styleBundle/RFQ")
                .Include(commonStyle)
                .Include(contextMenuStyle)
                .Include(dataTablesStyle));

            bundles.Add(new StyleBundle("~/styleBundle/RFQOnHand")
                .Include(contextMenuStyle)
                .Include(dataTablesStyle));

            bundles.Add(new StyleBundle("~/styleBundle/RFQDetails")
                .Include(commonStyle)
                .Include(dataTablesStyle));

            bundles.Add(new StyleBundle("~/styleBundle/AssignPurchaser")
                .Include(commonStyle)
                .Include(dataTablesStyle));

            bundles.Add(new StyleBundle("~/styleBundle/RaiseRFQ")
                .Include(commonStyle));

            #region Administrator
            bundles.Add(new StyleBundle("~/styleBundle/AuditSetup")
                .Include("~/Style/Administrator/AuditSetup.css"));

            bundles.Add(new StyleBundle("~/styleBundle/ModuleSetup")
                .Include(dataTablesStyle)
                .Include("~/Style/Administrator/ModuleSetup.css"));

            bundles.Add(new StyleBundle("~/styleBundle/PlatformSetup")
                .Include(dataTablesStyle)
                .Include(contextMenuStyle)
                .Include("~/Style/Administrator/PlatformSetup.css"));

            bundles.Add(new StyleBundle("~/styleBundle/RoleSetup")
                .Include(dataTablesStyle)
                .Include(contextMenuStyle)
                .Include("~/Style/Administrator/RoleSetup.css"));

            bundles.Add(new StyleBundle("~/styleBundle/TabSetup")
                .Include(dataTablesStyle)
                .Include(contextMenuStyle)
                .Include("~/Style/Administrator/TabSetup.css"));

            bundles.Add(new StyleBundle("~/styleBundle/UserSetup")
                .Include(dataTablesStyle)
                .Include(contextMenuStyle)
                .Include("~/Style/Administrator/UserSetup.css"));

            bundles.Add(new StyleBundle("~/styleBundle/CompanySetup")
                .Include(dataTablesStyle)
                .Include(contextMenuStyle)
                .Include("~/Style/Administrator/CompanySetup.css"));

            bundles.Add(new StyleBundle("~/styleBundle/DepartmentSetup")
                .Include(dataTablesStyle)
                .Include(contextMenuStyle)
                .Include("~/Style/Administrator/DepartmentSetup.css"));

            bundles.Add(new StyleBundle("~/styleBundle/TeamSetup")
                .Include(dataTablesStyle)
                .Include(contextMenuStyle)
                .Include("~/Style/Administrator/TeamSetup.css"));

            bundles.Add(new StyleBundle("~/styleBundle/FileTypeAttachmentSetup")
                .Include(dataTablesStyle)
                .Include(contextMenuStyle)
                .Include("~/Style/Administrator/FileTypeAttachmentSetup.css"));
            #endregion

            #region Item Library Management
            bundles.Add(new StyleBundle("~/styleBundle/ILM/ItemLibraryList")
                .Include(contextMenuStyle)
                .Include(dataTablesStyle)
                .Include("~/Style/ItemLibraryManagement/ItemLibraryList.css"));

            bundles.Add(new StyleBundle("~/styleBundle/ILM/AddNewItem")
                .Include(contextMenuStyle)
                .Include(dataTablesStyle)
                .Include("~/Style/ItemLibraryManagement/AddNewItem.css"));
            
            bundles.Add(new StyleBundle("~/styleBundle/ILM/EditItem")
                .Include(dataTablesStyle)
                .Include(contextMenuStyle)
                .Include("~/Style/ItemLibraryManagement/EditItem.css"));

            bundles.Add(new StyleBundle("~/styleBundle/ILM/ItemDetails")
                .Include(dataTablesStyle)
                .Include(contextMenuStyle)
                .Include("~/Style/ItemLibraryManagement/ItemDetails.css"));

            bundles.Add(new StyleBundle("~/styleBundle/ILM/ItemDetails")
                .Include(dataTablesStyle)
                .Include(contextMenuStyle)
                .Include("~/Style/ItemLibraryManagement/ItemDetails.css"));

            bundles.Add(new StyleBundle("~/styleBundle/ILM/CategorySetup")
                .Include(contextMenuStyle)
                .Include(dataTablesStyle)
                .Include("~/Style/ItemLibraryManagement/CategorySetup.css"));


            #endregion
            #endregion
        }
    }
}