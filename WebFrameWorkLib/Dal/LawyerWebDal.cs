using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFrameWorkLib.BusinessLogic;
using WebFrameWorkLib.Database;
using WebFrameWorkLib.Models;

namespace WebFrameWorkLib.Dal
{
    class LawyerWebDal
    {
        private FrameWorkEntities dbContext = new FrameWorkEntities();
        private LogError logError = new LogError();
        private AuditBL auditBL = new AuditBL();
        private Common common = new Common();

        public List<User> getUserList()
        {

            try
            {
                List<User> userList = new List<User>();

                Guid superAdminID = new Guid("4353A43C-C5E7-4767-9690-D7C51CD3DC11");
                Guid adminStaffRoleID = new Guid("465FCE16-FDC2-4858-B3B8-3D7DC522BB8E");
                Guid lawyerRoleID = new Guid("5E2A9803-E702-410E-9FC3-709F2BE033D4");
                var usersProfiles = dbContext.V_UserList.Where(c => c.AccessID != superAdminID && c.RoleID != adminStaffRoleID && c.RoleID != lawyerRoleID);

                if (usersProfiles != null)
                {
                    foreach (var profile in usersProfiles)
                    {
                        User user = new User();
                        user.AccessId = profile.AccessID.ToString();
                        user.Name = profile.StaffName;
                        user.LoginId = profile.LoginID;
                        user.Designation = profile.Designation;
                        user.EmailAddress = profile.EmailAddress;
                        user.PhoneNum = profile.PhoneNum;
                        user.StaffNumber = profile.StaffNumber;
                        user.Role = profile.Description;
                        if (profile.AccessStatus == 0)
                            user.Status = "Active";
                        else
                            user.Status = "Inactive";

                        userList.Add(user);
                    }
                }

                return userList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }
    }
}
