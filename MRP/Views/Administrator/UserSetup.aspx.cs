using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MRP.Models;
using WebFrameWorkLib.BusinessLogic;
using WebFrameWorkLib.Database;

namespace MRP.Views.Administrator
{
    public partial class UserSetup : System.Web.UI.Page
    {
        private FrameWorkEntities dbContext = new FrameWorkEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            loadComboBox();
        }

        protected void loadComboBox()
        {
            // Load Status ComboBox
            List<DropdownItem> statusDS = new List<DropdownItem>();
            statusDS.Add(new DropdownItem() { ID = "", Val = "Select" });
            foreach (KeyValuePair<string, string> kvp in LibEnumDescription.GetValuesIntAndDescription(typeof(LibSystemEnum.userStatus)))
            {
                statusDS.Add(new DropdownItem() { ID = kvp.Key, Val = kvp.Value });
            }
            selectAddNewUser_Status.DataSource = statusDS;
            selectAddNewUser_Status.DataTextField = "Val";
            selectAddNewUser_Status.DataValueField = "ID";
            selectAddNewUser_Status.DataBind();

            ////load role combobox
            Guid superAdminRoleID = new Guid("DFA724BD-F4D9-467B-BBC6-220249BA512D");
            var roleStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.roleStatus>("Active"));
            var deleteStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.roleDeleteStatus>("NotDeleted"));
            var roleList = dbContext.UAMUserRoles.Where(c => c.RoleStatus == roleStatus && c.Deleted == deleteStatus && c.RoleID != superAdminRoleID).OrderBy(c => c.RoleName).ToList();
            List<DropdownItem> roleDS = new List<DropdownItem>();
            roleDS.Add(new DropdownItem() { ID = "", Val = "Select" });
            foreach (var role in roleList)
            {
                roleDS.Add(new DropdownItem() { ID = role.RoleID.ToString(), Val = role.RoleName });
            }

            selectAddNewUser_Roles.DataSource = roleDS;
            selectAddNewUser_Roles.DataTextField = "Val";
            selectAddNewUser_Roles.DataValueField = "ID";
            selectAddNewUser_Roles.DataBind();

            ////load user group combobox
            var userGroupDeleteStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.userGroupDeleteStatus>("NotDeleted"));
            var userGroupList = dbContext.UAMUserGroups.Where(c => c.Deleted == userGroupDeleteStatus).OrderBy(c => c.UserGroupName).ToList();
            List<DropdownItem> userGroupDS = new List<DropdownItem>();
            userGroupDS.Add(new DropdownItem() { ID = "", Val = "Select" });
            foreach (var userGroup in userGroupList)
            {
                userGroupDS.Add(new DropdownItem() { ID = userGroup.UserGroupID.ToString(), Val = userGroup.UserGroupName });
            }
            selectAddNewUser_UserTeam.DataSource = userGroupDS;
            selectAddNewUser_UserTeam.DataTextField = "Val";
            selectAddNewUser_UserTeam.DataValueField = "ID";
            selectAddNewUser_UserTeam.DataBind();

            ////load department combobox
            var departmentList = dbContext.Departments.OrderBy(c => c.DepartmentName).ToList();
            List<DropdownItem> departmentDS = new List<DropdownItem>();
            departmentDS.Add(new DropdownItem() { ID = "", Val = "Select" });
            foreach (var department in departmentList)
            {
                departmentDS.Add(new DropdownItem() { ID = department.ID.ToString(), Val = department.DepartmentName });
            }
            selectAddNewUser_Department.DataSource = departmentDS;
            selectAddNewUser_Department.DataTextField = "Val";
            selectAddNewUser_Department.DataValueField = "ID";
            selectAddNewUser_Department.DataBind();
        }
    }
}