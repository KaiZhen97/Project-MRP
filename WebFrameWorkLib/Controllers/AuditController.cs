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
    public class AuditController : ApiController
    {
        private AuditDal auditDal = new AuditDal();
        private AuditBL auditBL = new AuditBL();

        [HttpGet]
        [Authorize]
        public HttpResponseMessage getTableNameList()
        {
            DataTablesAuditTableList data = new DataTablesAuditTableList();
            List<WebApiParameter.OutputAuditTable> tableList = auditDal.getTableNameList();

            data.data = tableList;
            //data.status = "success";
            data.draw = 1;
            data.recordsFiltered = tableList.Count;
            data.recordsTotal = tableList.Count;

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpGet]
        [Authorize]
        public HttpResponseMessage getSelectedTableNameList()
        {
            DataTablesSelectedAuditTableList data = new DataTablesSelectedAuditTableList();
            List<AuditLogTable> tableList = auditDal.getSelectedTableNameList();

            data.data = tableList;
            data.draw = 1;
            data.recordsFiltered = tableList.Count;
            data.recordsTotal = tableList.Count;

            HttpResponseMessage response = new HttpResponseMessage();
            response = Request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        [HttpPost]
        [Authorize]
        public HttpResponseMessage postEditAuditTable([FromBody]WebApiParameter.InputEditAuditTable inputEditAuditTable)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response = auditBL.postEditAuditTable(inputEditAuditTable, ModelState, Request);

            return response;
        }
    }
}