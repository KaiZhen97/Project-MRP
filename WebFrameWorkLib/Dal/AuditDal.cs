using WebFrameWorkLib.BusinessLogic;
using WebFrameWorkLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using WebFrameWorkLib.Database;

namespace WebFrameWorkLib.Dal
{
    public class AuditDal
    {
        private FrameWorkEntities dbContext = new FrameWorkEntities();
        private LogError logError = new LogError();
        private Common common = new Common();

        public void accessAudit(Guid accessID, string desc)
        {
            try
            {
                AuditSecurityLog auditSecurityLog = new AuditSecurityLog();
                auditSecurityLog.AuditSecID = Guid.NewGuid();
                auditSecurityLog.AccessID = accessID;
                auditSecurityLog.LogDesc = desc;
                auditSecurityLog.LogCreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                dbContext.AuditSecurityLogs.Add(auditSecurityLog);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
            }
        }

        public void auditAddDelete(Guid accessID, string desc, string uniqueKey, string objDeleted, string tableName)
        {
            try
            {
                if (tableAuditControl(tableName))
                {
                    AuditActivityLog auditActivityLog = new AuditActivityLog();
                    auditActivityLog.AuditActID = Guid.NewGuid();
                    auditActivityLog.AccessID = accessID;
                    auditActivityLog.LogDesc = desc;
                    auditActivityLog.LogCreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                    auditActivityLog.UniqueKey = uniqueKey;

                    if (objDeleted != null)
                        auditActivityLog.OldVal = objDeleted;

                    dbContext.AuditActivityLogs.Add(auditActivityLog);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
            }
        }

        public void auditUpdate(Guid accessID, string desc, string uniqueKey, string oldObj, string newObj, string tableName)
        {
            try
            {
                if (tableAuditControl(tableName))
                {
                    AuditActivityLog auditActivityLog = new AuditActivityLog();
                    auditActivityLog.AuditActID = Guid.NewGuid();
                    auditActivityLog.AccessID = accessID;
                    auditActivityLog.LogDesc = desc;
                    auditActivityLog.LogCreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                    auditActivityLog.UniqueKey = uniqueKey;

                    if (oldObj != null)
                        auditActivityLog.OldVal = oldObj;

                    if (newObj != null)
                        auditActivityLog.NewVal = newObj;

                    dbContext.AuditActivityLogs.Add(auditActivityLog);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
            }
        }

        public bool tableAuditControl(string tableName)
        {
            try
            {
                var auditTable = dbContext.AuditLogTables.Where(c => c.TableName == tableName).FirstOrDefault();

                if (auditTable != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public List<WebApiParameter.OutputAuditTable> getTableNameList()
        {
            try
            {
                List<WebApiParameter.OutputAuditTable> tableNameList = new List<WebApiParameter.OutputAuditTable>();

                List<string> results = dbContext.Database.SqlQuery<string>("SELECT name FROM sys.tables ORDER BY name").ToList();

                if (results != null)
                {
                    foreach (var table in results)
                    {
                        WebApiParameter.OutputAuditTable outputAuditTAble = new WebApiParameter.OutputAuditTable();
                        outputAuditTAble.TableName = table;
                        tableNameList.Add(outputAuditTAble);
                    }
                }

                return tableNameList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public List<AuditLogTable> getSelectedTableNameList()
        {
            try
            {
                List<AuditLogTable> tableNameList = new List<AuditLogTable>();

                var auditLogList = dbContext.AuditLogTables.ToList();

                return auditLogList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postEditAuditTable(WebApiParameter.InputEditAuditTable inputEditAuditTable, HttpRequestMessage Request)
        {
            try
            {
                Guid userGuid = common.extractUserID(Request);

                string[] tableNameArr = inputEditAuditTable.tableName.Split(',');

                //delete all the table name in audit table
                var auditTable = dbContext.AuditLogTables;
                dbContext.AuditLogTables.RemoveRange(auditTable);
                dbContext.SaveChanges();

                //insert new table setting to table
                foreach (var item in tableNameArr)
                {
                    AuditLogTable auditLogTable = new AuditLogTable();
                    auditLogTable.TableID = Guid.NewGuid();
                    auditLogTable.TableName = item;
                    auditLogTable.AuditFlag = 1;//always one 
                    dbContext.AuditLogTables.Add(auditLogTable);
                    dbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }
    }
}