using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebFrameWorkLib.Models;
using WebFrameWorkLib.Dal;
using WebFrameWorkLib.BusinessLogic;
using System.Web.Http.Cors;

namespace WebFrameWork.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        private UserDal userDal = new UserDal();
        private UserBL userBL = new UserBL();

        [HttpPost]
        [Authorize]
        public HttpResponseMessage getUserList() {
            DataTables data = new DataTables();
            List<User> userList = userDal.getUserList();

            data.data = userList;
            //data.status = "success";
            data.draw = 1;
            data.recordsFiltered = userList.Count;
            data.recordsTotal = userList.Count;

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage getUserProfileUsingToken()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = userBL.getUserProfile(Request);

            return response;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage getProfileDetails()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = userBL.getProfileDetails(Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postUserDetails([FromBody]WebApiParameter.InputGetUserDetail inputGetUserDetail)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = userBL.postUserDetails(inputGetUserDetail, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postUpdateUser([FromBody]WebApiParameter.InputUpdateUser inputUpdateUser)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = userBL.postUpdateUser(inputUpdateUser, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postAddUser([FromBody]WebApiParameter.InputAddUser inputAddUser)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = userBL.postAddUser(inputAddUser, ModelState, Request);

            return response;
        }


        [HttpPost]
        [Authorize]
        public HttpResponseMessage postDeleteUser([FromBody]WebApiParameter.InputDeleteUser inputDeleteUser)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = userBL.postDeleteUser(inputDeleteUser, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postUserLogout()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = userBL.postUserLogout(Request);

            return response;
        }

        [HttpPost]
        public HttpResponseMessage postVerifyStaffNum([FromBody]WebApiParameter.InputVerifyStaffNum inputVerifyStaffNum)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = userBL.postVerifyStaffName(inputVerifyStaffNum, ModelState, Request);

            return response;
        }

        [HttpPost]
        public HttpResponseMessage postRegistration([FromBody]WebApiParameter.InputRegistration inputRegistrationPassword)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = userBL.postNewRegistrationPassword(inputRegistrationPassword, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postChangePassword([FromBody]WebApiParameter.InputChangePassword inputChangePassword)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = userBL.postChangePassword(inputChangePassword, ModelState, Request);

            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postUserDeviceInfo([FromBody]WebApiParameter.InputUserDeviceInfo requestFields)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = userBL.postUserDeviceInfo(requestFields, ModelState, Request);

            return response;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage getProfile()
        {
            WebApiParameter.OutUserProfile outUserProfile = userDal.getProfile(Request);

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, outUserProfile);
            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postLogout()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = userBL.postLogout(Request);

            return response;
        }
    }
}