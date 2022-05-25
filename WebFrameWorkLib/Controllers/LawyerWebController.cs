using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebFrameWorkLib.BusinessLogic;
using WebFrameWorkLib.Dal;
using WebFrameWorkLib.Models;

namespace WebFrameWorkLib.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LawyerWebController : ApiController
    {
        private LawyerWebDal lawyerWebDal = new LawyerWebDal();
        private UserDal UserDal = new UserDal();
        private Common common = new Common();

        [HttpPost]
        [Authorize]
        public HttpResponseMessage getUserList()
        {
            var role = common.extractUserRole(Request);

            DataTables data = new DataTables();

            if (role == "Admin Staff")
            {
                List<User> userList = lawyerWebDal.getUserList();

                data.data = userList;
                //data.status = "success";
                data.draw = 1;
                data.recordsFiltered = userList.Count;
                data.recordsTotal = userList.Count;

                HttpResponseMessage response = new HttpResponseMessage();
                response = Request.CreateResponse(HttpStatusCode.OK, data);
                return response;
            }
            else
            {
                List<User> userList = UserDal.getUserList();

                data.data = userList;
                //data.status = "success";
                data.draw = 1;
                data.recordsFiltered = userList.Count;
                data.recordsTotal = userList.Count;

                HttpResponseMessage response = new HttpResponseMessage();
                response = Request.CreateResponse(HttpStatusCode.OK, data);
                return response;
            }
            
        }
    }
}
