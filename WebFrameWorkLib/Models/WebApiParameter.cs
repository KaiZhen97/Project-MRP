using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFrameWorkLib.Database;
using System.Linq;
using System.Web;

namespace WebFrameWorkLib.Models
{
    public class WebApiParameter
    {
        public class OutUserProfile
        {
            public string userName { get; set; }
            public string phoneno { get; set; }
            public string emailAddress { get; set; }
        }

        public class InputUserDeviceInfo
        {
            public string model { get; set; }
            public string osVersion { get; set; }
            public string deviceToken { get; set; }
        }

        public class InputGetUserDetail
        {
            public string userId { get; set; }
        }

        public class InputGetProductList
        { 
            public string lastSyncDate { get; set; }
        }

        public class InputNewOrder
        {
            [Required]
            public string customerID { get; set; }
            [Required]
            public List<OutputOrderItem> orderItem { get; set; }
        }

        public class InputEditOrder
        {
            [Required]
            public string orderID { get; set; }
            [Required]
            public string status { get; set; }
        }

        public class InputUpdateUser
        {
            [Required]
            public string userID { get; set; }
            [Required]
            [MaxLength(20)]
            public string loginID { get; set; }
            [Required]
            [MaxLength(20)]
            public string staffNumber { get; set; }
            [Required]
            [MaxLength(200)]
            public string staffName { get; set; }
            [Required]
            [MaxLength(200)]
            public string email { get; set; }
            [Required]
            [MaxLength(20)]
            public string telno { get; set; }
            [Required]
            [MaxLength(36)]
            public string role { get; set; }
            [Required]
            [MaxLength(1)]
            public string status { get; set; }
            public string password { get; set; }
            [MaxLength(36)]
            public string userGroupID { get; set; }
            [MaxLength(11)]
            public string departmentID { get; set; }
            [Required]
            [MaxLength(50)]
            public string designation { get; set; }
        }

        public class InputChangePassword
        {
            [Required]
            public string oldPassword { get; set; }
            [Required]
            public string newPassword { get; set; }
        }

        public class InputVerifyStaffNum
        {
            [Required, MaxLength(100)]
            public string verifyStaffNum { get; set; }
            [Required, MaxLength(50)]
            public string IdentityNumber { get; set; }
        }

        public class InputRegistration
        {
            [Required, MaxLength(50)]
            public string confirmRegistrationPassword { get; set; }
            [Required]
            public string staffNumber { get; set; }
        }

        public class InputPostOrderDetail
        {
            [Required]
            public string orderID { get; set; }
        }

        public class InputAddUser
        {
            [Required]
            [MaxLength(20)]
            public string loginID { get; set; }
            [Required]
            [MaxLength(20)]
            public string staffNumber { get; set; }
            [Required]
            [MaxLength(200)]
            public string staffName { get; set; }
            [Required]
            [MaxLength(200)]
            public string email { get; set; }
            [Required]
            [MaxLength(20)]
            public string telno { get; set; }
            [Required]
            [MaxLength(36)]
            public string role { get; set; }
            [Required]
            [MaxLength(1)]
            public string status { get; set; }
            [Required]
            public string password { get; set; }
            [MaxLength(36)]
            public string userGroupID { get; set; }
            [MaxLength(11)]
            public string departmentID { get; set; }
            [Required]
            [MaxLength(50)]
            public string designation { get; set; }
        }

        public class InputDeleteUser
        {
            [Required]
            [MaxLength(36)]
            public string userID { get; set; }
        }

        public class InputDeleteRole
        {
            [Required]
            [MaxLength(36)]
            public string roleID { get; set; }
        }

        public class InputDeleteTab
        {
            [Required]
            [MaxLength(36)]
            public string moduleID { get; set; }
        }

        public class InputDeleteUserGroup
        {
            [Required]
            [MaxLength(36)]
            public string userGroupID { get; set; }
        }

        public class InputDeleteModule
        {
            [Required]
            [MaxLength(36)]
            public string moduleID { get; set; }
        }

        public class InputAddRole
        {
            [Required]
            [MaxLength(50)]
            public string roleName { get; set; }
            [Required]
            [MaxLength(200)]
            public string roleDesc { get; set; }
            [Required]
            public string roleStatus { get; set; }

        }

        public class InputAddTab
        {
            [Required]
            [MaxLength(100)]
            public string tabName { get; set; }
            [MaxLength(200)]
            public string tabLink { get; set; }
            [Required]
            [MaxLength(3)]
            public string sequence { get; set; }
            [Required]
            [MaxLength(1)]
            public string status { get; set; }
            [MaxLength(50)]
            public string htmlID { get; set; }
            [MaxLength(100)]
            public string htmlIcon { get; set; }
        }

        public class InputAddModule
        {
            [Required]
            [MaxLength(100)]
            public string moduleName { get; set; }
            [Required]
            [MaxLength(200)]
            public string moduleLink { get; set; }
            [Required]
            [MaxLength(3)]
            public string sequence { get; set; }
            [Required]
            [MaxLength(1)]
            public string status { get; set; }
            [Required]
            [MaxLength(36)]
            public string parentTabID { get; set; }
            public string actions { get; set; }
            [MaxLength(30)]
            public string groupName { get; set; }
            [MaxLength(3)]
            public string groupSequence { get; set; }
            [MaxLength(50)]
            public string htmlID { get; set; }
            [MaxLength(100)]
            public string htmlIcon { get; set; }

        }

        public class InputAddUserGroup
        {
            [Required]
            [MaxLength(100)]
            public string userGroupName { get; set; }
            public int DepartmentID { get; set; }
        }

        public class InputEditModule
        {
            [Required]
            [MaxLength(36)]
            public string moduleID { get; set; }
            [Required]
            [MaxLength(100)]
            public string moduleName { get; set; }
            [Required]
            [MaxLength(200)]
            public string moduleLink { get; set; }
            [Required]
            [MaxLength(3)]
            public string sequence { get; set; }
            [Required]
            [MaxLength(1)]
            public string status { get; set; }
            [Required]
            [MaxLength(36)]
            public string parentTabID { get; set; }
            public string actions { get; set; }
            [MaxLength(30)]
            public string groupName { get; set; }
            [MaxLength(3)]
            public string groupSequence { get; set; }
            [MaxLength(50)]
            public string htmlID { get; set; }
            [MaxLength(100)]
            public string htmlIcon { get; set; }
        }

        public class InputRoleDetails
        {
            [Required]
            [MaxLength(36)]
            public string roleID { get; set; }
        }

        public class InputTabDetails
        {
            [Required]
            [MaxLength(36)]
            public string moduleID { get; set; }
        }

        public class InputUserGroupDetails
        {
            [Required]
            [MaxLength(36)]
            public string userGroupID { get; set; }
        }

        public class InputModuleDetails
        {
            [Required]
            [MaxLength(36)]
            public string moduleID { get; set; }
        }

        public class InputRoleModuleDetails
        {
            [Required]
            [MaxLength(36)]
            public string roleID { get; set; }
        }

        public class InputPlatformRole
        {
            [Required]
            [MaxLength(36)]
            public string platformID { get; set; }
        }

        public class InputModuleActionDetails
        {
            [Required]
            [MaxLength(36)]
            public string moduleID { get; set; }
        }

        public class InputEditRole
        {
            [Required]
            [MaxLength(36)]
            public string roleID { get; set; }
            [Required]
            [MaxLength(50)]
            public string roleName { get; set; }
            [Required]
            [MaxLength(200)]
            public string roleDesc { get; set; }
            [Required]
            public string status { get; set; }
        }

        public class InputEditTab
        {
            [Required]
            [MaxLength(36)]
            public string moduleID { get; set; }
            [Required]
            [MaxLength(100)]
            public string tabName { get; set; }
            public string tabLink { get; set; }
            [Required]
            [MaxLength(3)]
            public string sequence { get; set; }
            [Required]
            [MaxLength(1)]
            public string status { get; set; }
            [MaxLength(50)]
            public string htmlID { get; set; }
            [MaxLength(100)]
            public string htmlIcon { get; set; }
        }

        public class InputEditUserGroup
        {
            [Required]
            [MaxLength(36)]
            public string userGroupID { get; set; }
            [Required]
            [MaxLength(100)]
            public string userGroupName { get; set; }
            public int DepartmentID { get; set; }
        }

        public class InputEditRoleModule
        {
            [Required]
            [MaxLength(36)]
            public string roleID { get; set; }
            public string moduleStr { get; set; }
        }

        public class InputEditAuditTable
        {
            //[Required]
            public string tableName { get; set; }
        }

        public class InputEditPlatformRole
        {
            [Required]
            public string platformID { get; set; }
            [Required]
            public string roleID { get; set; }
        }

        public class OutputOrderItem
        {
            public string productID { get; set; }
            public string orderQty { get; set; }
            public string orderSinglePrice { get; set; }
        }

        public class OutputModuleList
        {
            public string parentModuleID { get; set; }
            public string parentModuleName { get; set; } 
            public List<UAMModule> childList { get; set; }
        }

        public class OutputRoleModule
        {
            public string ModuleID { get; set; }
            public string ModuleName { get; set; }
            public string DisplayName { get; set; }
            public string Description { get; set; }
            public string ModuleLink { get; set; }
            public string DisplaySequence { get; set; }
            public string GroupSequence { get; set; }
            public List<SubModule> subModuleList { get; set; }
        }

        public class OutputAuditTable
        {
            public string TableName { get; set; }
        }

        public class SubModule
        {
            public string ModuleID { get; set; }
            public string ModuleName { get; set; }
            public string DisplayName { get; set; }
            public string Description { get; set; }
            public string ModuleLink { get; set; }
            public string DisplaySequence { get; set; }
            public string GroupSequence { get; set; }
            public List<V_ModuleAction> ModuleActionList { get; set; }
        }

        public class DashboardYearlySales
        {
            public string year { get; set; }
        }

        public class InputUserToken
        {
            public string UserToken { get; set; }
        }

        public class InputUrl
        {
            [Required]
            public string CurrentURL { get; set; }
        }
    }
}