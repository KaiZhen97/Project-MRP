using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFrameWorkLib.BusinessLogic;
using MRP.Database;
using System.IO;
using MRP.Models;
using System.Net.Http;

namespace MRP.Dal
{
    public class UpdateTraceDal
    {
        private MRPEntities dbContext = new MRPEntities();
        private LogError logError = new LogError();
        private Common common = new Common();
        private AuditBL auditBL = new AuditBL();

        public bool postAddUpdateTraceRFQ(RequestParameter.inputEditRFQ input, HttpRequestMessage request) 
        {
            try
            {
                Guid userGuid = common.extractUserID(request);
                RFQ editRFQ = dbContext.RFQs.Where(c => c.ID == input.ID).FirstOrDefault();
                UpdateTrace newUpdateTrace = new UpdateTrace();


                newUpdateTrace.RFQID = input.ID;
                newUpdateTrace.UpdateTraceActionID = input.TraceAction;
                newUpdateTrace.UpdateTraceTypeID = 2;
                newUpdateTrace.UpdateRemark = input.Description;
                newUpdateTrace.CreatedDate = DateTime.Now;
                newUpdateTrace.CreatedBy = userGuid;

                dbContext.UpdateTraces.Add(newUpdateTrace);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}