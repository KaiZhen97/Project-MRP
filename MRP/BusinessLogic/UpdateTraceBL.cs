using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using MRP.Models;
using WebFrameWorkLib.BusinessLogic;
using MRP.Properties;
using MRP.Dal;

namespace MRP.BusinessLogic
{
    public class UpdateTraceBL
    {
        private UpdateTraceDal updateTraceDal = new UpdateTraceDal();
        private WebRequestApi webReqApi = new WebRequestApi();


        public HttpResponseMessage postAddUpdateTraceRFQ(RequestParameter.inputEditRFQ input, HttpRequestMessage request)
        {
            try
            {
                var addData = updateTraceDal.postAddUpdateTraceRFQ(input, request);

                if (!addData)
                    return webReqApi.returnBad(Resources.UPDATE_FAILED, request);
                return webReqApi.returnOk(Resources.UPDATE_SUCCESS, request);
            }
            catch(Exception ex)
            {
                return webReqApi.returnBad(Resources.UPDATE_FAILED, request);
            }
        }
    }
}