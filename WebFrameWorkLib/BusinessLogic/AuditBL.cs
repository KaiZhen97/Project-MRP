using WebFrameWorkLib.Dal;
using WebFrameWorkLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.ModelBinding;
using WebFrameWorkLib.Database;

namespace WebFrameWorkLib.BusinessLogic
{
    public class AuditBL
    {
        private FrameWorkEntities dbContext = new FrameWorkEntities();
        private LogError logError = new LogError();
        private AuditDal auditDal = new AuditDal();

        private SystemMessage systemMessage = new SystemMessage();
        private Common common = new Common();
        private ExtractModelStateMsg extractModelStateMsg = new ExtractModelStateMsg();

        public void accessAudit(Guid accessID, string desc)
        {
            auditDal.accessAudit(accessID, desc);
        }

        public void auditAddDelete(Guid accessID, string desc, string uniqueKey, string objDeleted, string tableName) {
            auditDal.auditAddDelete(accessID, desc, uniqueKey, objDeleted, tableName);
        }

        public void auditUpdate(Guid accessID, string desc, string uniqueKey, string oldObj, string newObj, string tableName)
        {
            auditDal.auditUpdate(accessID, desc, uniqueKey, oldObj, newObj, tableName);
        }

        public HttpResponseMessage postEditAuditTable(WebApiParameter.InputEditAuditTable inputEditAuditTable, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var editAuditTable = auditDal.postEditAuditTable(inputEditAuditTable, request);

                    if (editAuditTable)
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
    }
}