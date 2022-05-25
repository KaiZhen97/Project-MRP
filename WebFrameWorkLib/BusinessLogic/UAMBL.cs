using WebFrameWorkLib.Dal;
using WebFrameWorkLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.ModelBinding;

namespace WebFrameWorkLib.BusinessLogic
{
    public class UAMBL
    {
        private UAMDal uamDal = new UAMDal();
        private SystemMessage systemMessage = new SystemMessage();
        private Common common = new Common();
        private ExtractModelStateMsg extractModelStateMsg = new ExtractModelStateMsg();

        #region role
        public HttpResponseMessage postAddRole(WebApiParameter.InputAddRole inputAddRole, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var verifyRoleName = uamDal.verifyRoleName(inputAddRole.roleName, null);

                    if (verifyRoleName)
                    {
                        var addRole = uamDal.postAddRole(inputAddRole, request);

                        if (addRole)
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
                        systemMessage.Message = rawMsg.Replace("@0", inputAddRole.roleName);
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

        public HttpResponseMessage postRoleDetailsByID(WebApiParameter.InputRoleDetails inputRoleDetails, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var role = uamDal.postRoleDetailsByID(inputRoleDetails);

                    if (role!=null)
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, role);
                        return response;
                    }
                    else
                    {
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

        public HttpResponseMessage postEditRole(WebApiParameter.InputEditRole inputEditRole, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var verifyRoleName = uamDal.verifyRoleName(inputEditRole.roleName, inputEditRole.roleID);

                    if (verifyRoleName)
                    {
                        var editRole = uamDal.postEditRole(inputEditRole, request);

                        if (editRole)
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
                        systemMessage.Message = rawMsg.Replace("@0", inputEditRole.roleName);
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

        public HttpResponseMessage postDeleteRole(WebApiParameter.InputDeleteRole inputDeleteRole, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var verifyRoleInuse = uamDal.verifyRoleInUSe(inputDeleteRole);

                    if (verifyRoleInuse)
                    {
                        var deleteRole = uamDal.postDeleteRole(inputDeleteRole, request);

                        if (deleteRole)
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
        #endregion

        #region tab
        public HttpResponseMessage postAddTab(WebApiParameter.InputAddTab inputAddTab, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var verifyTabName = uamDal.verifyTabName(inputAddTab.tabName, null);

                    if (verifyTabName)
                    {
                        var addTab = uamDal.postAddTab(inputAddTab, request);

                        if (addTab)
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
                        systemMessage.Message = rawMsg.Replace("@0", inputAddTab.tabName);
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

        public HttpResponseMessage postTabDetailsByID(WebApiParameter.InputTabDetails inputTabDetails, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var module = uamDal.postTabDetailsByID(inputTabDetails);

                    if (module != null)
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, module);
                        return response;
                    }
                    else
                    {
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

        public HttpResponseMessage postEditTab(WebApiParameter.InputEditTab inputEditTab, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var verifyTabName = uamDal.verifyTabName(inputEditTab.tabName, inputEditTab.moduleID);

                    if (verifyTabName)
                    {
                        var editTab = uamDal.postEditTab(inputEditTab, request);

                        if (editTab)
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
                        systemMessage.Message = rawMsg.Replace("@0", inputEditTab.tabName);
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

        public HttpResponseMessage postDeleteTab(WebApiParameter.InputDeleteTab inputDeleteTab, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var verifyTabInUse = uamDal.verifyTabInUSe(inputDeleteTab);

                    if (verifyTabInUse)
                    {
                        var deleteTab = uamDal.postDeleteTab(inputDeleteTab, request);

                        if (deleteTab)
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
        #endregion

        #region Module
        public HttpResponseMessage postAddModule(WebApiParameter.InputAddModule inputAddModule, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var verifyModuleName = uamDal.verifyModuleName(inputAddModule.moduleName, null);

                    if (verifyModuleName)
                    {
                        var addTab = uamDal.postAddModule(inputAddModule, request);

                        if (addTab)
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
                        systemMessage.Message = rawMsg.Replace("@0", inputAddModule.moduleName);
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

        public HttpResponseMessage postModuleDetailsByID(WebApiParameter.InputModuleDetails inputModuleDetails, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var module = uamDal.postModuleDetailsByID(inputModuleDetails);

                    if (module != null)
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, module);
                        return response;
                    }
                    else
                    {
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

        public HttpResponseMessage postModuleActionDetailsByID(WebApiParameter.InputModuleActionDetails inputModuleActionDetails, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var module = uamDal.postModuleActionDetailsByID(inputModuleActionDetails);

                    if (module != null)
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, module);
                        return response;
                    }
                    else
                    {
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

        public HttpResponseMessage postEditModule(WebApiParameter.InputEditModule inputEditModule, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var verifyModuleName = uamDal.verifyModuleName(inputEditModule.moduleName, inputEditModule.moduleID);

                    if (verifyModuleName)
                    {
                        var ediModuleb = uamDal.postEditModule(inputEditModule, request);

                        if (ediModuleb)
                        {
                            systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.UPDATE_SUCCESS);
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, systemMessage);
                            return response;
                        }
                        else
                        {
                            systemMessage.Message = common.CustomMsg(WebFrameWorkLib.Properties.Resource.UPDATE_SUCCESS);
                            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                            return response;
                        }

                    }
                    else
                    {
                        //return a message for reason of fail
                        string rawMsg = common.CustomMsg(WebFrameWorkLib.Properties.Resource.DATA_EXIST);
                        systemMessage.Message = rawMsg.Replace("@0", inputEditModule.moduleName);
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

        public HttpResponseMessage postDeleteModule(WebApiParameter.InputDeleteModule inputDeleteModule, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var deleteModule = uamDal.postDeleteModule(inputDeleteModule, request);

                    if (deleteModule)
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
        #endregion

        #region Role Module
        public HttpResponseMessage postEditRoleModule(WebApiParameter.InputEditRoleModule inputEditRoleModule, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var editRoleModule = uamDal.postEditRoleModule(inputEditRoleModule, request);

                    if (editRoleModule)
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

        public HttpResponseMessage postRoleModuleDetailsByID(WebApiParameter.InputRoleModuleDetails inputRoleModuleDetails, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var module = uamDal.postRoleModuleDetailsByID(inputRoleModuleDetails);

                    if (module != null)
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, module);
                        return response;
                    }
                    else
                    {
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
        #endregion

        #region platform
        public HttpResponseMessage postEditPlatformRole(WebApiParameter.InputEditPlatformRole inputEditPlatformRole, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var editPlatformRole = uamDal.postEditPlatformRole(inputEditPlatformRole, request);

                    if (editPlatformRole)
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

        public HttpResponseMessage postPlatformRoleByID(WebApiParameter.InputPlatformRole inputPlatformRole, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var module = uamDal.postPlatformRoleByID(inputPlatformRole);

                    if (module != null)
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, module);
                        return response;
                    }
                    else
                    {
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
        #endregion

        #region user group
        public HttpResponseMessage postAddUserGroup(WebApiParameter.InputAddUserGroup inputAddUserGroup, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var verifyUserGorupName = uamDal.verifyUserGroupName(inputAddUserGroup.userGroupName, null);

                    if (verifyUserGorupName)
                    {
                        var addUSerGroup = uamDal.postAddUserGroup(inputAddUserGroup, request);

                        if (addUSerGroup)
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
                        systemMessage.Message = rawMsg.Replace("@0", inputAddUserGroup.userGroupName);
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

        public HttpResponseMessage postUserGroupDetailsByID(WebApiParameter.InputUserGroupDetails InputUserGroupDetails, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var userGroup = uamDal.postUserGroupDetailsByID(InputUserGroupDetails);

                    if (userGroup != null)
                    {
                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, userGroup);
                        return response;
                    }
                    else
                    {
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

        public HttpResponseMessage postEditUserGroup(WebApiParameter.InputEditUserGroup inputEditUserGroup, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var verifyUserGroupName = uamDal.verifyUserGroupName(inputEditUserGroup.userGroupName, inputEditUserGroup.userGroupID);

                    if (verifyUserGroupName)
                    {
                        var editUserGroup = uamDal.postEditUserGroup(inputEditUserGroup, request);

                        if (editUserGroup)
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
                        systemMessage.Message = rawMsg.Replace("@0", inputEditUserGroup.userGroupName);
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

        public HttpResponseMessage postDeleteUserGroup(WebApiParameter.InputDeleteUserGroup inputDeleteUserGroup, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var verifyUserGroupInUse = uamDal.verifyUserGroupInUse(inputDeleteUserGroup);

                    if (verifyUserGroupInUse)
                    {
                        var deleteUserGroup = uamDal.postDeleteUserGroup(inputDeleteUserGroup, request);

                        if (deleteUserGroup)
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
        #endregion

        #region access right
        public HttpResponseMessage postCheckLinkAccessRight(WebApiParameter.InputUrl input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();

                if (!modelState.IsValid)
                {
                    //return a message for reason of fail
                    systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                    response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }

                var checkLinkAccessRight = uamDal.postCheckLinkAccessRight(input, request);

                if (!checkLinkAccessRight)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, new { urlStatus = checkLinkAccessRight });
                    return response;
                }

                response = request.CreateResponse(HttpStatusCode.OK, new { urlStatus = checkLinkAccessRight });
                return response;
            }
            catch (Exception ex)
            {
                //return a message for reason of fail
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "error" + ex.Message.ToString());
                return response;
            }
        }
        #endregion
    }
}