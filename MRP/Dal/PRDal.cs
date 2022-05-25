using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFrameWorkLib.BusinessLogic;
using MRP.Database;
using MRP.Models;
using System.Net.Http;
using MRP.Models;

namespace MRP.Dal
{
    public class PRDal
    {
        private MRPEntities dbContext = new MRPEntities();
        private LogError logError = new LogError();
        private Common common = new Common();
        private AuditBL auditBL = new AuditBL();

        public List<V_PRList_Watcher> getInvovledPRList(HttpRequestMessage request)
        {
            try
            {
                Guid userid = common.extractUserID(request);

                List<V_PRList_Watcher> data = dbContext.V_PRList_Watcher
                    .Where(c => c.CreatedBy == userid || c.Watchers_AccessID == userid)
                    .AsEnumerable()
                    .Distinct(new PRListWatcherComparer())
                    .ToList();

                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }
    }
}