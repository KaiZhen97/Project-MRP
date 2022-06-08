using WebFrameWorkLib.BusinessLogic;
using WebFrameWorkLib.Database;
using WebFrameWorkLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;

namespace WebFrameWorkLib.Dal
{
    public class UserDal
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
                var usersProfiles = dbContext.V_UserList.Where(c => c.AccessID != superAdminID);

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
                        var userStatusActive = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.userAccessStatus>("Active"));
                        if (profile.AccessStatus == userStatusActive)
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


        //public List<V_UserDetail> getPurchasingList()
        //{
        //    try
        //    {
        //        //Guid userGuid = common.extractUserID(Request);

        //        //var User = dbContext.UAMUsers;
        //        //List<V_UserDetail> PurchasingList = new List<V_UserDetail>();
        //        var PurchasingList = dbContext.V_UserDetail.Where(c => c.DepartmentID == 22).ToList();

        //        return PurchasingList;
        //    }
        //    catch (Exception ex)
        //    {
        //        logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
        //            System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
        //        return null;
        //    }
        //}



        public List<V_UserDetail> getPurchasingList(HttpRequestMessage Request)
        {
            try
            {
                //Guid userGuid = common.extractUserID(Request);

                //var User = dbContext.UAMUsers;
                //List<V_UserDetail> PurchasingList = new List<V_UserDetail>();
                var PurchasingList = dbContext.V_UserDetail.Where(c => c.DepartmentID == 22).ToList();

                return PurchasingList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }


        //public List<V_UserDetail> getPurchasingList(HttpRequestMessage Request)
        //{
        //    try
        //    {
        //        //Guid userGuid = common.extractUserID(Request);

        //        //var User = dbContext.UAMUsers;
        //        List<V_UserDetail> PurchasingList = new List<V_UserDetail>();
        //        var UserDetail = dbContext.V_UserDetail.Where(c => c.DepartmentID == 22).ToList();

        //            return PurchasingList;

        //        //if (UserDetail != null)
        //        //{
        //        //    return PurchasingList;
        //        //}

        //            //UserProfiles = dbContext.UAMUserProfiles.AccessID().FirstOrDefault();
        //            //var 

        //            //UAMUserProfile UserProfile = new UAMUserProfile();

        //        //var AccessID = dbContext.UAMUsers.Join(dbContext.UAMUserProfiles,
        //        //    //s => s.DepartmentID,
        //        //    d => d.AccessID,
        //        //    (dbContext.UAMUsers, dbContext.UAMUserProfiles) => new
        //        //    {
        //        //        //DepartmentID = dbContext.UAMUserProfiles.DepartmentID,
        //        //        AccessID1 = dbContext.UAMUserProfiles.AccessID,
        //        //        AccessID2 = dbContext.UAMUsers.AccessID
        //        //    });

        //        //var AccessID = from u in dbContext.UAMUsers
        //        //               join p in dbContext.UAMUserProfiles
        //        //               on u.dbContext.UAMUsers.AccessID euqals p.dbContext.UAMUserProfiles.AccessID
        //        //    select new
        //        //    {
        //        //        dbContext.UAMUsers.AcecessID = u.AccessID,
        //        //        dbContext.UAMUserProfiles.AccessID = p.AccessID
        //        //    };


        //        //if (UserProfiles != null && User != null)
        //        //{
        //        //    return userGuid;
        //        //    return null;
        //        //}


        //        //var EmployeeProfileList = dbContext.V_EmployeeProfileList.Where(c => c.DepartmentID == 22 || c.DepartmentName == "ISYS Purchasing").FirstOrDefault();

        //        //if (EmployeeProfileList != null)
        //        //{

        //        //    return EmployeeProfileList;
        //        //}

        //        //var Department = dbContext.UAMUserProfiles.()

        //        //else
        //        //{

        //        //    return null;
        //        //}

        //        //return PurchasingList.AccessID;

        //        //return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
        //            System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
        //        return null;
        //    }
        //}



        public UAMUser getUserByIDAndPwd(string loginID, string password, string loginType)
        {

            try
            {
                DataEncryption dataEncryption = new DataEncryption();
                var hashedPassword = DataEncryption.GetEncrypt(password);

                UAMUser userProfile = new UAMUser();
                List<UAMPlatformLoginRole> PlatformLoginRole = null;
                List<string> roleIdList = new List<string>();

                var platformId = dbContext.UAMPlatforms.Where(c => c.PlatformName == loginType).FirstOrDefault().PlatformID;

                if (platformId == null)
                {
                    auditBL.accessAudit(Guid.Empty, "Invalid Login - invalid platform " + loginID);
                    return null;
                }
                else
                {
                    PlatformLoginRole = dbContext.UAMPlatformLoginRoles.Where(c => c.PlatformID == platformId).ToList();
                }

                if (PlatformLoginRole != null)
                {
                    foreach (var loginRole in PlatformLoginRole)
                    {
                        roleIdList.Add(loginRole.RoleID.ToString());
                    }
                }
                else
                {
                    auditBL.accessAudit(Guid.Empty, "Invalid Login - role is not allowed " + loginID);
                    return null;
                }

                var deleteStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.userDeleteStatus>("NotDeleted"));
                var accessStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.userAccessStatus>("Active"));
                userProfile = dbContext.UAMUsers.Where(c => c.LoginID == loginID && c.Deleted == deleteStatus && roleIdList.Contains(c.RoleID.ToString()) && c.AccessStatus == accessStatus).FirstOrDefault<UAMUser>();

                if (userProfile != null)
                {
                    if (DataEncryption.VerifyHash(hashedPassword, userProfile.Password))
                    {
                        return userProfile;
                    }
                    else
                    {
                        auditBL.accessAudit(Guid.Empty, "Invalid Login - invalid password " + loginID);
                        return null;
                    }
                }
                else
                {
                    auditBL.accessAudit(Guid.Empty, "Invalid Login - invalid user login " + loginID);
                    return null;
                }
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public UAMUser getUserProfileUsingToken(HttpRequestMessage Request)
        {

            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var customClaimValue = principal.Claims.Where(c => c.Type == "UserID").Single().Value;

                if (customClaimValue != null)
                {
                    Guid userID = new Guid(customClaimValue);
                    var deleteStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.userDeleteStatus>("NotDeleted"));
                    var userProfile = dbContext.UAMUsers.Where(c => c.AccessID == userID && c.Deleted == deleteStatus).FirstOrDefault();

                    if (userProfile != null)
                    {
                        return userProfile;
                    }
                    else
                    {
                        return null;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public UAMUserProfile getProfileDetails(HttpRequestMessage Request)
        {

            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var customClaimValue = principal.Claims.Where(c => c.Type == "UserID").Single().Value;

                if (customClaimValue != null)
                {
                    Guid userID = new Guid(customClaimValue);
                    var deleteStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.userDeleteStatus>("NotDeleted"));
                    var userProfile = dbContext.UAMUserProfiles.Where(c => c.AccessID == userID && c.Deleted == deleteStatus).FirstOrDefault();

                    if (userProfile != null)
                    {
                        return userProfile;
                    }
                    else
                    {
                        return null;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public V_UserDetail postUserDetails(WebApiParameter.InputGetUserDetail inputGetUserDetail, HttpRequestMessage Request)
        {

            try
            {
                Guid userID = new Guid(inputGetUserDetail.userId);
                var userDetail = dbContext.V_UserDetail.Where(c => c.AccessID == userID).FirstOrDefault();

                if (userDetail != null)
                {
                    return userDetail;
                }

                return null;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postUpdateUser(WebApiParameter.InputUpdateUser inputUpdateUser, HttpRequestMessage Request)
        {

            try
            {
                using (var dbTransaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        Guid userGuid = common.extractUserID(Request);

                        Guid accessID = new Guid(inputUpdateUser.userID);
                        //update tbluamuser
                        var user = dbContext.UAMUsers.Where(c => c.AccessID == accessID).FirstOrDefault();

                        if (user != null)
                        {
                            var oldUserValue = common.convertObjToJsonString(user);
                            user.LoginID = inputUpdateUser.loginID;
                            user.AccessStatus = Convert.ToInt16(inputUpdateUser.status);
                            user.RoleID = new Guid(inputUpdateUser.role);

                            if (inputUpdateUser.password != "")
                                user.Password = inputUpdateUser.password;

                            if (inputUpdateUser.userGroupID != "")
                                user.UserGroupID = new Guid(inputUpdateUser.userGroupID);

                            dbContext.SaveChanges();

                            #region audit
                            auditBL.auditUpdate(userGuid, "edit user", user.AccessID.ToString(), oldUserValue, common.convertObjToJsonString(user), "UAMUser");
                            if (inputUpdateUser.password != "")
                                auditBL.accessAudit(userGuid, "change user password - " + user.LoginID);
                            #endregion

                            //update tbluamuserprofile
                            var userProfile = dbContext.UAMUserProfiles.Where(c => c.UserProfileID == user.UserProfileID).FirstOrDefault();
                            if (userProfile != null)
                            {
                                var oldUserProfileValue = common.convertObjToJsonString(userProfile);
                                userProfile.StaffNumber = inputUpdateUser.staffNumber;
                                userProfile.StaffName = inputUpdateUser.staffName;
                                userProfile.PhoneNum = inputUpdateUser.telno;
                                userProfile.Designation = inputUpdateUser.designation;
                                userProfile.DepartmentID = Convert.ToInt16(inputUpdateUser.departmentID);
                                dbContext.SaveChanges();

                                #region audit
                                auditBL.auditUpdate(userGuid, "edit user profile", userProfile.UserProfileID.ToString(), oldUserProfileValue, common.convertObjToJsonString(userProfile), "UAMUserProfile");
                                #endregion

                                //update email
                                var userEmail = dbContext.UAMSMMEmailLists.Where(c => c.EmailID == userProfile.EmailID).FirstOrDefault();
                                if (userEmail != null)
                                {
                                    var oldEmailValue = common.convertObjToJsonString(userEmail);
                                    userEmail.EmailAddress = inputUpdateUser.email;
                                    dbContext.SaveChanges();

                                    #region audit
                                    auditBL.auditUpdate(userGuid, "edit user email", userEmail.EmailID.ToString(), oldEmailValue, common.convertObjToJsonString(userEmail), "UAMSMMEmailList");
                                    #endregion
                                }
                            }
                        }

                        dbTransaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postAddUser(WebApiParameter.InputAddUser inputAddUser, HttpRequestMessage Request)
        {

            try
            {
                using (var dbContextTransaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        Guid userGuid = new Guid();
                        if (Request.RequestUri.AbsolutePath != "/api/User/postRegistration")
                        {
                            userGuid = common.extractUserID(Request);
                        }

                        Guid newAccessID = Guid.NewGuid();
                        Guid newProfileID = Guid.NewGuid();
                        Guid newEmailID = Guid.NewGuid();
                        var deleteStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.userDeleteStatus>("NotDeleted"));
                        var activeStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.userAccessStatus>("Active"));
                        //Add tbluamuser
                        UAMUser user = new UAMUser();
                        user.AccessID = newAccessID;
                        user.LoginID = inputAddUser.loginID;
                        user.Password = inputAddUser.password;
                        user.RoleID = new Guid(inputAddUser.role);
                        user.AccessStatus = activeStatus;
                        user.CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                        user.Deleted = deleteStatus;
                        user.UserProfileID = newProfileID;
                        user.NumInvalidLogin = 0;

                        if (inputAddUser.userGroupID != "")
                            user.UserGroupID = new Guid(inputAddUser.userGroupID);

                        dbContext.UAMUsers.Add(user);

                        #region audit
                        if (userGuid == Guid.Empty)
                            auditBL.auditAddDelete(Guid.Empty, "add new user from register page", user.AccessID.ToString(), common.convertObjToJsonString(user), "UAMUser");
                        else
                            auditBL.auditAddDelete(userGuid, "add new user", user.AccessID.ToString(), common.convertObjToJsonString(user), "UAMUser");
                        #endregion

                        var emailDeleteStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.emailDeleteStatus>("NotDeleted"));
                        var emailStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.emailStatus>("Active"));
                        //Add tbluamemail
                        UAMSMMEmailList email = new UAMSMMEmailList();
                        email.EmailID = newEmailID;
                        email.RecipientName = inputAddUser.staffName;
                        email.EmailAddress = inputAddUser.email;
                        email.EmailStatus = emailStatus;
                        email.Deleted = emailDeleteStatus;
                        email.CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss"); ;
                        email.BranchID = "HQ";
                        email.AccessID = newAccessID;
                        dbContext.UAMSMMEmailLists.Add(email);

                        #region audit
                        if (userGuid == Guid.Empty)
                            auditBL.auditAddDelete(Guid.Empty, "add new user email from register page", user.AccessID.ToString(), common.convertObjToJsonString(user), "UAMSMMEmailList");
                        else
                            auditBL.auditAddDelete(userGuid, "add new user email", email.EmailID.ToString(), common.convertObjToJsonString(email), "UAMSMMEmailList");
                        #endregion

                        var userProfileDeleteStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.userProfileDeleteStatus>("NotDeleted"));
                        var userProfileStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.userProfileStatus>("Active"));
                        //Add tbluamuserprofile
                        UAMUserProfile profile = new UAMUserProfile();
                        profile.UserProfileID = newProfileID;
                        profile.StaffNumber = inputAddUser.staffNumber;
                        profile.StaffName = inputAddUser.staffName;
                        profile.StaffName = inputAddUser.staffName;
                        profile.EmailID = newEmailID;
                        profile.UserProfileStatus = userProfileStatus;
                        profile.CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                        profile.AccessID = newAccessID;
                        profile.Deleted = userProfileDeleteStatus;
                        profile.BranchID = "HQ";
                        profile.PhoneNum = inputAddUser.telno;
                        profile.Designation = inputAddUser.designation;
                        profile.DepartmentID = Convert.ToInt16(inputAddUser.departmentID);

                        dbContext.UAMUserProfiles.Add(profile);
                        dbContext.SaveChanges();
                        dbContextTransaction.Commit();

                        #region audit
                        if (userGuid == Guid.Empty)
                            auditBL.auditAddDelete(Guid.Empty, "add new user profile from register page", user.AccessID.ToString(), common.convertObjToJsonString(user), "UAMUserProfile");
                        else
                            auditBL.auditAddDelete(userGuid, "add new user profile", profile.UserProfileID.ToString(), common.convertObjToJsonString(profile), "UAMUserProfile");
                        #endregion

                        return true;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool verifyExistingLoginID(string loginID, string accessID)
        {

            try
            {
                UAMUser userProfile = new UAMUser();
                var userProfileDeleteStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.userProfileDeleteStatus>("NotDeleted"));

                if (accessID != null)
                {
                    Guid accessGuid = new Guid(accessID);
                    userProfile = dbContext.UAMUsers.Where(c => c.LoginID == loginID && c.Deleted == userProfileDeleteStatus && c.AccessID != accessGuid).FirstOrDefault<UAMUser>();
                }
                else
                {
                    userProfile = dbContext.UAMUsers.Where(c => c.LoginID == loginID && c.Deleted == userProfileDeleteStatus).FirstOrDefault<UAMUser>();
                }


                if (userProfile != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postDeleteUser(WebApiParameter.InputDeleteUser inputDeleteUser, HttpRequestMessage Request)
        {

            try
            {
                Guid userGuid = common.extractUserID(Request);

                Guid userID = new Guid(inputDeleteUser.userID);
                var user = dbContext.UAMUsers.Where(c => c.AccessID == userID).FirstOrDefault();
                var userProfileDeleteStatus = Convert.ToInt16(LibEnumDescription.GetValueFromDescription<LibSystemEnum.userProfileDeleteStatus>("Deleted"));
                if (user != null)
                {
                    user.Deleted = userProfileDeleteStatus;
                    dbContext.SaveChanges();

                    #region audit
                    auditBL.auditAddDelete(userGuid, "delete user", user.AccessID.ToString(), common.convertObjToJsonString(user), "UAMUser");
                    #endregion

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postUserLogout(HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);

                #region audit
                auditBL.accessAudit(userGuid, "Logout");
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public string getUserRoleDesc(Guid roleID)
        {

            try
            {
                var roleDesc = dbContext.UAMUserRoles.Where(c => c.RoleID == roleID).FirstOrDefault();

                if (roleDesc != null)
                {
                    return roleDesc.RoleName;
                }
                else
                {
                    return "Anonymous";
                }
            }
            catch
            {
                return "Anonymous";
            }
        }

        public bool postChangePassword(WebApiParameter.InputChangePassword inputChangePassword, HttpRequestMessage Request)
        {

            try
            {
                Guid userGuid = common.extractUserID(Request);

                var user = dbContext.UAMUsers.Where(c => c.AccessID == userGuid).FirstOrDefault();

                if (user != null)
                {
                    var oldUser = common.convertObjToJsonString(user);
                    user.Password = inputChangePassword.newPassword;
                    dbContext.SaveChanges();


                    #region audit
                    auditBL.auditUpdate(userGuid, "change password", userGuid.ToString(), oldUser, common.convertObjToJsonString(user), "UAMUser");
                    auditBL.accessAudit(userGuid, "user change password");
                    #endregion

                }
                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool verifyOldPassword(WebApiParameter.InputChangePassword inputChangePassword, HttpRequestMessage Request)
        {

            try
            {
                Guid userGuid = common.extractUserID(Request);

                //verify the old password
                var user = dbContext.UAMUsers.Where(c => c.AccessID == userGuid).FirstOrDefault();

                if (user != null)
                {
                    if (DataEncryption.VerifyHash(inputChangePassword.oldPassword, user.Password))
                        return true;
                    else
                        return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postVerifyRegistered(WebApiParameter.InputVerifyStaffNum inputVerifyStaffNum, HttpRequestMessage Request)
        {

            try
            {
                //Guid userGuid = common.extractUserID(Request);
                var user = dbContext.UAMUserProfiles.Where(c => c.StaffNumber == inputVerifyStaffNum.verifyStaffNum).FirstOrDefault();

                if (user != null)
                {
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;

            }
        }

        public bool verifyEmployeeProfile(WebApiParameter.InputVerifyStaffNum inputVerifyStaffNum, HttpRequestMessage Request)
        {

            try
            {
                //Guid userGuid = common.extractUserID(Request);
                var userProfileByStaffNumber = dbContext.EmployeeProfiles.Where(c => c.StaffNumber == inputVerifyStaffNum.verifyStaffNum && (c.ICNumber == inputVerifyStaffNum.IdentityNumber || c.PassportNumber == inputVerifyStaffNum.IdentityNumber)).FirstOrDefault();

                if (userProfileByStaffNumber == null)
                {
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;

            }
        }

        public bool postUserDeviceInfo(WebApiParameter.InputUserDeviceInfo requestField, HttpRequestMessage Request)
        {

            try
            {
                //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                //var customClaimValue = principal.Claims.Where(c => c.Type == "UserID").Single().Value;

                //if (customClaimValue != null)
                //{
                //Guid userID = new Guid(customClaimValue);
                //var userDevice = dbContext.TblUserDevices.Where(c => c.AccessID == userID).FirstOrDefault();

                //if (userDevice != null)
                //{
                //    userDevice.DvcToken = requestField.deviceToken;
                //    userDevice.Model = requestField.model;
                //    userDevice.OSVersion = requestField.osVersion;
                //    userDevice.UpdateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                //    dbContext.SaveChanges();

                //    return true;
                //}
                //else
                //{
                //    TblUserDevice tblUserDevice = new TblUserDevice();
                //    tblUserDevice.DvcID = Guid.NewGuid();
                //    tblUserDevice.Model = requestField.model;
                //    tblUserDevice.OSVersion = requestField.osVersion;
                //    tblUserDevice.DvcToken = requestField.deviceToken;
                //    tblUserDevice.AccessID = userID;
                //    tblUserDevice.CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                //    tblUserDevice.UpdateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                //    dbContext.TblUserDevices.Add(tblUserDevice);
                //    dbContext.SaveChanges();
                //    return true;
                //}
                //}
                //else
                //{
                return false;
                //}
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public WebApiParameter.OutUserProfile getProfile(HttpRequestMessage Request)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var role = principal.Claims.Where(c => c.Type == "RoleDesc").SingleOrDefault().Value;
            var userId = principal.Claims.Where(c => c.Type == "UserID").SingleOrDefault().Value;
            Guid userGuid = new Guid(userId);

            try
            {
                var userProfile = dbContext.V_UserList.Where(c => c.AccessID == userGuid).FirstOrDefault();

                if (userProfile != null)
                {
                    WebApiParameter.OutUserProfile outUserProfile = new WebApiParameter.OutUserProfile();
                    outUserProfile.userName = userProfile.StaffName;
                    outUserProfile.phoneno = userProfile.PhoneNum;
                    outUserProfile.emailAddress = userProfile.EmailAddress;

                    return outUserProfile;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postLogout(HttpRequestMessage Request)
        {

            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                var customClaimValue = principal.Claims.Where(c => c.Type == "UserID").Single().Value;

                if (customClaimValue != null)
                {
                    Guid userID = new Guid(customClaimValue);
                    //var userDevice = dbContext.TblUserDevices.Where(c => c.AccessID == userID).FirstOrDefault();

                    //if (userDevice != null)
                    //{
                    //    userDevice.DvcToken = "";
                    //    userDevice.UpdateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                    //    dbContext.SaveChanges();

                    //    return true;
                    //}
                }
                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }
    }
}