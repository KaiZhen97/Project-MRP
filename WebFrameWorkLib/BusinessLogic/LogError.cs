using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFrameWorkLib.Database;

namespace WebFrameWorkLib.BusinessLogic
{
    public class LogError
    {
        private FrameWorkEntities dbContext = new FrameWorkEntities();

        public void LogErrorDb(string level, string logger, string thread, string message)
        {
            try
            {
                ErrorLog errLog = new ErrorLog();
                errLog.ErrLogDate = DateTime.Now;
                errLog.ErrLogLevel = level;
                errLog.ErrLogLogger = logger;
                errLog.ErrLogThread = thread;
                errLog.ErrLogMessage = message;

                dbContext.ErrorLogs.Add(errLog);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                string inner = ex.InnerException != null ? ex.InnerException.ToString() : ex.ToString();
            }
        }
    }
}