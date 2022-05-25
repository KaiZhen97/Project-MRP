using WebFrameWorkLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFrameWorkLib.Database;

namespace WebFrameWorkLib.Models
{
    public class DataTables
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<User> data { get; set; }
    }

    public class DataTablesRole
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<UAMUserRole> data { get; set; }
    }

    public class DataTablesUAMModule
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<UAMModule> data { get; set; }
    }

    public class DataTablesUAMUserGroup
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<V_UAMUserGroup> data { get; set; }
    }

    public class DataTablesUAMModuleChildList
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<V_ChildModuleList> data { get; set; }
    }

    public class DataTablesUAMRoleModuleList
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<WebApiParameter.OutputRoleModule> data { get; set; }
    }

    public class DataTablesUAMRoleMenuList
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<V_RoleMenuList> data { get; set; }
    }

    public class DataTablesAuditTableList
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<WebApiParameter.OutputAuditTable> data { get; set; }
    }

    public class DataTablesSelectedAuditTableList
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<AuditLogTable> data { get; set; }
    }

    public class DataTablesPlatform
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<UAMPlatform> data { get; set; }
    }

}