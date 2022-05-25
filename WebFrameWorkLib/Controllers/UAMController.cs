using WebFrameWorkLib.BusinessLogic;
using WebFrameWorkLib.Database;
using WebFrameWorkLib.Dal;
using WebFrameWorkLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebFrameWorkLib.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UAMController : ApiController
    {
        private UAMDal uamDal = new UAMDal();
        private UAMBL uamBL = new UAMBL();

        #region role 
        [HttpGet]
        [Authorize]
        public HttpResponseMessage getRoleList()
        {
            DataTablesRole data = new DataTablesRole();
            List<UAMUserRole> roleList = uamDal.getRoleList(Request);

            data.data = roleList;
            //data.status = "success";
            data.draw = 1;
            data.recordsFiltered = roleList.Count;
            data.recordsTotal = roleList.Count;

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postAddRole([FromBody]WebApiParameter.InputAddRole inputAddRole)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postAddRole(inputAddRole, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postRoleDetailsByID([FromBody]WebApiParameter.InputRoleDetails inputRoleDetails)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postRoleDetailsByID(inputRoleDetails, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postEditRole([FromBody]WebApiParameter.InputEditRole inputEditRole)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postEditRole(inputEditRole, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postDeleteRole([FromBody]WebApiParameter.InputDeleteRole inputDeleteRole)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postDeleteRole(inputDeleteRole, ModelState, Request);

            return response;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage getActiveRoleList([FromBody]WebApiParameter.InputDeleteRole inputDeleteRole)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            List<UAMUserRole> data = uamDal.getRoleList(Request);

            response = Request.CreateResponse(HttpStatusCode.OK, data);

            return response;
        }

        #endregion

        #region tab
        [HttpGet]
        [Authorize]
        public HttpResponseMessage getTabList()
        {
            DataTablesUAMModule data = new DataTablesUAMModule();
            List<UAMModule> tabList = uamDal.getTabList();

            data.data = tabList;
            //data.status = "success";
            data.draw = 1;
            data.recordsFiltered = tabList.Count;
            data.recordsTotal = tabList.Count;

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postAddTab([FromBody]WebApiParameter.InputAddTab inputAddTab)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postAddTab(inputAddTab, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postTabDetailsByID([FromBody]WebApiParameter.InputTabDetails inputTabDetails)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postTabDetailsByID(inputTabDetails, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postEditTab([FromBody]WebApiParameter.InputEditTab inputEditTab)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postEditTab(inputEditTab, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postDeleteTab([FromBody]WebApiParameter.InputDeleteTab inputDeleteTab)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postDeleteTab(inputDeleteTab, ModelState, Request);

            return response;
        }
        #endregion

        #region module
        [HttpGet]
        [Authorize]
        public HttpResponseMessage getModuleList()
        {
            DataTablesUAMModuleChildList data = new DataTablesUAMModuleChildList();
            List<V_ChildModuleList> moduleList = uamDal.getModuleList();

            data.data = moduleList;
            data.draw = 1;
            data.recordsFiltered = moduleList.Count;
            data.recordsTotal = moduleList.Count;

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postAddModule([FromBody]WebApiParameter.InputAddModule inputAddModule)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postAddModule(inputAddModule, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postModuleDetailsByID([FromBody]WebApiParameter.InputModuleDetails inputModuleDetails)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postModuleDetailsByID(inputModuleDetails, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postModuleActionDetailsByID([FromBody]WebApiParameter.InputModuleActionDetails inputModuleActionDetails)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postModuleActionDetailsByID(inputModuleActionDetails, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postEditModule([FromBody]WebApiParameter.InputEditModule inputEditModule)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postEditModule(inputEditModule, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postDeleteModule([FromBody]WebApiParameter.InputDeleteModule inputDeleteModule)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postDeleteModule(inputDeleteModule, ModelState, Request);

            return response;
        }
        #endregion

        #region role module
        [HttpGet]
        [Authorize]
        public HttpResponseMessage getRoleModuleList()
        {
            DataTablesUAMRoleModuleList data = new DataTablesUAMRoleModuleList();
            List<WebApiParameter.OutputRoleModule> roleModuleList = uamDal.getRoleModuleList();

            data.data = roleModuleList;
            data.draw = 1;
            data.recordsFiltered = roleModuleList.Count;
            data.recordsTotal = roleModuleList.Count;

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postEditRoleModule([FromBody]WebApiParameter.InputEditRoleModule inputEditRoleModule)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postEditRoleModule(inputEditRoleModule, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postRoleModuleDetailsByID([FromBody]WebApiParameter.InputRoleModuleDetails inputRoleModuleDetails)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postRoleModuleDetailsByID(inputRoleModuleDetails, ModelState, Request);

            return response;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage getRoleMenuList()
        {
            DataTablesUAMRoleMenuList data = new DataTablesUAMRoleMenuList();
            List<V_RoleMenuList> roleMenuList = uamDal.getRoleMenuList(Request);

            data.data = roleMenuList;
            data.draw = 1;
            data.recordsFiltered = roleMenuList.Count;
            data.recordsTotal = roleMenuList.Count;

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        #endregion

        #region platform
        [HttpGet]
        [Authorize]
        public HttpResponseMessage getPlatformList()
        {
            DataTablesPlatform data = new DataTablesPlatform();
            List<UAMPlatform> tabList = uamDal.getPlatformList();

            data.data = tabList;
            //data.status = "success";
            data.draw = 1;
            data.recordsFiltered = tabList.Count;
            data.recordsTotal = tabList.Count;

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postEditPlatformRole([FromBody]WebApiParameter.InputEditPlatformRole inputEditPlatformRole)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postEditPlatformRole(inputEditPlatformRole, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postPlatformRoleByID([FromBody]WebApiParameter.InputPlatformRole inputPlatformRole)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postPlatformRoleByID(inputPlatformRole, ModelState, Request);

            return response;
        }
        #endregion

        #region user group
        [HttpGet]
        [Authorize]
        public HttpResponseMessage getUserGroupList()
        {
            DataTablesUAMUserGroup data = new DataTablesUAMUserGroup();
            List<V_UAMUserGroup> userGroupList = uamDal.getUserGroupList();

            data.data = userGroupList;
            //data.status = "success";
            data.draw = 1;
            data.recordsFiltered = userGroupList.Count;
            data.recordsTotal = userGroupList.Count;

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postAddUserGroup([FromBody]WebApiParameter.InputAddUserGroup inputAddUserGroup)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postAddUserGroup(inputAddUserGroup, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postUserGroupDetailsByID([FromBody]WebApiParameter.InputUserGroupDetails inputUserGroupDetails)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postUserGroupDetailsByID(inputUserGroupDetails, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postEditUserGroup([FromBody]WebApiParameter.InputEditUserGroup inputEditUserGroup)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postEditUserGroup(inputEditUserGroup, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postDeleteUserGroup([FromBody]WebApiParameter.InputDeleteUserGroup inputDeleteUserGroup)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postDeleteUserGroup(inputDeleteUserGroup, ModelState, Request);

            return response;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage getActiveUserGroup([FromBody]WebApiParameter.InputDeleteUserGroup inputDeleteUserGroup)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            List<V_UAMUserGroup> data = uamDal.getUserGroupList();
            
            response = Request.CreateResponse(HttpStatusCode.OK, data);

            return response;
        }
        #endregion

        #region access right
        [HttpPost]
        [Authorize]
        public HttpResponseMessage postCheckLinkAccessRight([FromBody]WebApiParameter.InputUrl input)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = uamBL.postCheckLinkAccessRight(input, ModelState, Request);

            return response;
        }
        #endregion

        #region LibSystemEnum
        [HttpGet]
        [Authorize]
        public HttpResponseMessage getModuleStatus()
        {
            List<object> statusList = new List<object>();
            foreach (KeyValuePair<string, string> kvp in LibEnumDescription.GetValuesIntAndDescription(typeof(LibSystemEnum.moduleStatus)))
            {
                statusList.Add(new { ID = kvp.Key, Val = kvp.Value });
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, statusList);
            return response;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage getUserStatus()
        {
            List<object> statusList = new List<object>();
            foreach (KeyValuePair<string, string> kvp in LibEnumDescription.GetValuesIntAndDescription(typeof(LibSystemEnum.userStatus)))
            {
                statusList.Add(new { ID = kvp.Key, Val = kvp.Value });
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, statusList);
            return response;
        }
        #endregion
    }
}