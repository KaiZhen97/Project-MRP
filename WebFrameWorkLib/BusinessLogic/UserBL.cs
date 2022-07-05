using WebFrameWorkLib.Dal;
using WebFrameWorkLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.ModelBinding;
using WebFrameWorkLib.Database;
using static WebFrameWorkLib.Models.WebApiParameter;

namespace WebFrameWorkLib.BusinessLogic
{
    public class UserBL
    {
        private WebRequestApi webRequestApi = new WebRequestApi();
        private FrameWorkEntities dbContext = new FrameWorkEntities();
        private UserDal userDal = new UserDal();
        private SystemMessage systemMessage = new SystemMessage();
        private Common common = new Common();
        private ExtractModelStateMsg extractModelStateMsg = new ExtractModelStateMsg();
        private LogError logError = new LogError();

        public HttpResponseMessage getUserProfile(HttpRequestMessage request)
        {
            try
            {
                var userProfile = userDal.getUserProfileUsingToken(request);

                if (userProfile != null)
                {
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, userProfile);
                    return response;
                }
                else
                {
                    //return a message for reason of fail
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, "not found");
                    return response;
                }
            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "error" + ex.Message.ToString());
                return response;
            }
        }


        public HttpResponseMessage getPurchaserList(HttpRequestMessage request)
        {
            try
            {
                var PurchaserList = userDal.getPurchaserList(request);

                if (PurchaserList != null)
                {
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, PurchaserList);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadGateway, "not found");
                    return response;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "error" + ex.Message.ToString());
                return response;
            }
        }


        public HttpResponseMessage getProfileDetails(HttpRequestMessage request)
        {
            try
            {
                var userProfile = userDal.getProfileDetails(request);

                if (userProfile != null)
                {
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, userProfile);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, "not found");
                    return response;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "error" + ex.Message.ToString());
                return response;
            }
        }

        public HttpResponseMessage postUserDetails(WebApiParameter.InputGetUserDetail inputGetUserDetail, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var userDetail = userDal.postUserDetails(inputGetUserDetail, request);

                    if (userDetail != null)
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, userDetail);
                        return response;
                    }
                    else
                    {
                        //return a message for reason of fail
                        systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.NO_DATA);
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                        return response;
                    }
                }
                else
                {
                    //return a message for reason of fail
                    systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }
            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "error" + ex.Message.ToString());
                return response;
            }
        }

        public HttpResponseMessage postUpdateUser(WebApiParameter.InputUpdateUser inputUpdateUser, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    //verify loginid
                    var verifyLoginID = userDal.verifyExistingLoginID(inputUpdateUser.loginID, inputUpdateUser.userID);

                    if (verifyLoginID)
                    {
                        //update login and profile info if loginid is valid to use
                        var AddUSerProfile = userDal.postUpdateUser(inputUpdateUser, request);

                        if (AddUSerProfile)
                        {
                            systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.UPDATE_SUCCESS);
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, systemMessage);
                            return response;
                        }
                        else
                        {
                            systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.UPDATE_FAILED);
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                            return response;
                        }
                    }
                    else
                    {
                        //return a message for reason of fail
                        string rawMsg = common.CustomMsg(WebFrameWorkLib.Properties.Resource.DATA_EXIST);
                        systemMessage.Message = rawMsg.Replace("@0", inputUpdateUser.loginID);
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                        return response;
                    }
                }
                else
                {
                    //return a message for reason of fail
                    systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }
            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "error" + ex.Message.ToString());
                return response;
            }
        }

        public HttpResponseMessage postAddUser(WebApiParameter.InputAddUser inputAddUser, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    //verify loginid
                    var verifyLoginID = userDal.verifyExistingLoginID(inputAddUser.loginID, null);

                    if (verifyLoginID)
                    {
                        //update login and profile info if loginid is valid to use
                        var addUSerProfile = userDal.postAddUser(inputAddUser, request);

                        if (addUSerProfile)
                        {
                            systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.ADD_SUCCESS);
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, systemMessage);
                            return response;
                        }
                        else
                        {
                            systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.ADD_FAILED);
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                            return response;
                        }

                    }
                    else
                    {
                        //return a message for reason of fail
                        string rawMsg = common.CustomMsg(WebFrameWorkLib.Properties.Resource.DATA_EXIST);
                        systemMessage.Message = rawMsg.Replace("@0", inputAddUser.loginID);
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                        return response;
                    }
                }
                else
                {
                    //return a message for reason of fail
                    systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }
            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "error" + ex.Message.ToString());
                return response;
            }
        }

        public HttpResponseMessage postDeleteUser(WebApiParameter.InputDeleteUser inputDeleteUser, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var deleteUser = userDal.postDeleteUser(inputDeleteUser, request);

                    if (deleteUser)
                    {
                        systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.DELETE_SUCCESS);
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, systemMessage);
                        return response;
                    }
                    else
                    {
                        systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.DELETE_FAILED);
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                        return response;
                    }
                }
                else
                {
                    //return a message for reason of fail
                    systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }
            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "error" + ex.Message.ToString());
                return response;
            }
        }

        public HttpResponseMessage postUserLogout(HttpRequestMessage request)
        {
            try
            {
                var userLogout = userDal.postUserLogout(request);

                if (userLogout)
                {
                    systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.USER_LOGOUT);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, systemMessage);
                    return response;
                }
                else
                {
                    systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.USER_LOGOUT_FAILED);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }
            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "error" + ex.Message.ToString());
                return response;
            }
        }

        public HttpResponseMessage postChangePassword(WebApiParameter.InputChangePassword inputChangePassword, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    //verify loginid
                    var verifyOldPassword = userDal.verifyOldPassword(inputChangePassword, request);

                    if (verifyOldPassword)
                    {
                        //update login and profile info if loginid is valid to use
                        var updateUserPassword = userDal.postChangePassword(inputChangePassword, request);

                        if (updateUserPassword)
                        {
                            systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.PASSWORD_UPDATED);
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, systemMessage);
                            return response;
                        }
                        else
                        {
                            systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.UPDATE_FAILED);
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                            return response;
                        }

                    }
                    else
                    {
                        //return a message for reason of fail
                        systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.OLDPWD_INCORRECT);
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                        return response;
                    }
                }
                else
                {
                    //return a message for reason of fail
                    systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }
            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "error" + ex.Message.ToString());
                return response;
            }
        }

        public HttpResponseMessage postVerifyStaffName(WebApiParameter.InputVerifyStaffNum inputVerifyStaffNum, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();

                if (!modelState.IsValid)
                {
                    //return a message for reason of fail
                    systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.ID_STAFFNO_REQUIRED);
                    response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }

                //Check Staff Number exist in Tbl UserProfile (staff registered or not)
                var VerifyRegistered = userDal.postVerifyRegistered(inputVerifyStaffNum, request);

                if (!VerifyRegistered)
                {
                    //return a message for reason of fail user dah register
                    systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.IDENTITY_REGISTERED);
                    response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }

                //Check Stuff Number & IC Number in Employee Profile
                var verifyEmployeeProfile = userDal.verifyEmployeeProfile(inputVerifyStaffNum, request);

                if (!verifyEmployeeProfile)
                {
                    //return a message for reason of fail user tk ada dlm employee profile
                    systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.USER_NOT_IN_EMPLOYEELIST);
                    response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }

                //check ada space ke tak ada


                //semue success
                systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.VERIFY_REGISTER_SUCCESS);
                response = request.CreateResponse(HttpStatusCode.OK, verifyEmployeeProfile);
                return response;
            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "error" + ex.Message.ToString());
                return response;
            }
        }

        public HttpResponseMessage postNewRegistrationPassword(WebApiParameter.InputRegistration inputRegistrationPassword, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();

                if (!modelState.IsValid)
                {
                    //return a message for reason of fail
                    systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.OLDPWD_INCORRECT);
                    response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }

                var employeeProfile = dbContext.EmployeeProfiles.Where(c => c.StaffNumber == inputRegistrationPassword.staffNumber && c.Deleted == 0).FirstOrDefault();
                if (employeeProfile == null)
                {
                    //return a message for reason of fail
                    systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.OLDPWD_INCORRECT);
                    response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }

                else
                {
                    WebApiParameter.InputAddUser inputAddUser = new InputAddUser();
                    inputAddUser.departmentID = employeeProfile.DepartmentID.ToString();
                    inputAddUser.userGroupID = employeeProfile.UserGroupID.ToString();
                    inputAddUser.staffName = employeeProfile.EmployeeName;
                    inputAddUser.staffNumber = employeeProfile.StaffNumber;
                    inputAddUser.email = employeeProfile.EmailAddress;
                    inputAddUser.designation = employeeProfile.Designation;
                    inputAddUser.loginID = employeeProfile.StaffNumber;
                    inputAddUser.password = inputRegistrationPassword.confirmRegistrationPassword;
                    inputAddUser.status = 1.ToString();
                    inputAddUser.role = employeeProfile.RoleID.ToString();
                    inputAddUser.telno = employeeProfile.TelNumber;

                    var getPostAddUser = postAddUser(inputAddUser, modelState, request);
                    return getPostAddUser;

                }
            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "error" + ex.Message.ToString());
                return response;
            }
        }

        public HttpResponseMessage postUserDeviceInfo(WebApiParameter.InputUserDeviceInfo requestFields, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var saveDeviceInfo = userDal.postUserDeviceInfo(requestFields, request);

                    if (saveDeviceInfo)
                    {
                        systemMessage.Message = "success";
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                        return response;
                    }
                    else
                    {
                        //return a message for reason of fail
                        systemMessage.Message = "Failed";
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                        return response;
                    }
                }
                else
                {
                    //return a message for reason of fail
                    systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }

            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                systemMessage.Message = ex.Message.ToString();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, systemMessage);
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());

                return response;
            }
        }

        public HttpResponseMessage postLogout(HttpRequestMessage request)
        {
            try
            {
                var logoutUser = userDal.postLogout(request);

                if (logoutUser)
                {
                    systemMessage.Message = "success";
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, systemMessage);
                    return response;
                }
                else
                {
                    //return a message for reason of fail
                    systemMessage.Message = "Failed";
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }
            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                systemMessage.Message = ex.Message.ToString();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, systemMessage);
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());

                return response;
            }
        }
    }
}