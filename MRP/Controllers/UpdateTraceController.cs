using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MRP.Models;
using MRP.BusinessLogic;

namespace MRP.Controllers
{
    public class UpdateTraceController : ApiController
    {
        private UpdateTraceBL updateTraceBL = new UpdateTraceBL();

        public HttpResponseMessage postAddUpdateTraceRFQ (RequestParameter.inputEditRFQ input)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response = updateTraceBL.postAddUpdateTraceRFQ(input, Request);
            return response;
        }

    }
}
