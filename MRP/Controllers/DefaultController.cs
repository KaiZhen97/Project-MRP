using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MRP.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage TestApi()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "OK");
        }
    }
}
