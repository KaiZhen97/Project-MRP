using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MRP.Dal;
using MRP.Database;
using WebFrameWorkLib.BusinessLogic;
using System.Net.Http;
using MRP.Models;
using System.Web.Http.ModelBinding;
using MRP.Properties;
using WebFrameWorkLib.Models;
using System.Net;

namespace MRP.BusinessLogic
{
    public class PRBL
    {
        private MRPEntities dbContext = new MRPEntities();
        private PRDal prDal = new PRDal();
        private WebRequestApi webReqApi = new WebRequestApi();
        private SystemMessage systemMessage = new SystemMessage();
        private ExtractModelStateMsg extractModelStateMsg = new ExtractModelStateMsg();
    }
}