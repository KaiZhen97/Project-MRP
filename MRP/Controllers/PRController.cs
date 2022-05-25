using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using MRP.BusinessLogic;
using MRP.Dal;
using MRP.Models;
using MRP.Database;

namespace MRP.Controllers
{
    [EnableCors("*", "*", "*")]
    [Authorize]
    public class PRController : ApiController
    {
        private PRBL itemLibraryBL = new PRBL();
        private PRDal itemLibraryDal = new PRDal();

        [HttpGet]
        public HttpResponseMessage getInvovledPRList()
        {
            var data = itemLibraryDal.getInvovledPRList(Request);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);

            return response;
        }
    }
}
