using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFrameWorkLib.BusinessLogic;
using MRP.Database;
using System.IO;
using MRP.Models;
using System.Net.Http;
using WebFrameWorkLib.Database;



namespace MRP.Dal
{
    public class RFQDal
    {
        private MRPEntities dbContext = new MRPEntities();
        private FrameWorkEntities dbContextFrameWork = new FrameWorkEntities();
        private LogError logError = new LogError();
        private Common common = new Common();
        private AuditBL auditBL = new AuditBL();

        public V_RFQList postRFQByID(RequestParameter.inputID input)
        {
            try
            {
                V_RFQList data = dbContext.V_RFQList.Where(c => c.ID == input.ID).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public List<V_UserDetail> postWatcherEmailByID(RequestParameter.inputID input)
        {
            try
            {
                List<Watcher> data = dbContext.Watchers.Where(c => c.RFQID == input.ID).ToList();
                List<V_UserDetail> detailList = new List<V_UserDetail>();
                foreach (var d in data)
                {
                    detailList.Add(dbContextFrameWork.V_UserDetail.Where(c => c.AccessID == d.Watchers_AccessID).FirstOrDefault());
                }

                return detailList;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public V_RFQList postClosedRFQByID(RequestParameter.inputID input)
        {
            try
            {
                V_RFQList data = dbContext.V_RFQList.Where(c => c.ID == input.ID && c.RFQ_StatusID == 0).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        //public List<V_RFQAttachmentsList> postAttachmentByID(RequestParameter.inputID input)
        //{
        //    try
        //    {
        //        List<V_RFQAttachmentsList> data = new List<V_RFQAttachmentsList>();
        //        data = dbContext.V_RFQAttachmentsList.Where(c => c.RFQID == input.ID).ToList();
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
        //            System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
        //        return null;
        //    }
        //}


        #region RFQList
        //public List<V_RFQList> getActiveRFQList(HttpRequestMessage request)
        //{
        //    try
        //    {
        //        Guid user = common.extractUserID(request);
        //        var data = dbContext.V_RFQList.Where(c => c.CancelDate == null && c.IsDraft == 0 && (c.CreatedBy == user || c.Watchers_AccessID == user))
        //            .AsEnumerable()
        //            .Distinct(new V_RFQListComparer())
        //            .ToList();

        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
        //            System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
        //        return null;
        //    }
        //}

        public List<V_RFQList> getDraftRFQList(HttpRequestMessage request)
        {
            try
            {
                Guid user = new Guid();
                user = common.extractUserID(request);
                var data = dbContext.V_RFQList.Where(c => c.CancelDate == null && c.IsDraft == 1 && c.CreatedBy == user).ToList();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postDeleteRFQ(RequestParameter.inputDeleteRFQ input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                RFQ RFQ = dbContext.RFQs.Where(c => c.ID == input.ID).FirstOrDefault();

                if (RFQ == null)
                    return false;

                // Change target Item Library status to deleted
                RFQ.CancelRemark = input.CancelRemark;
                RFQ.LastUpdatedDate = DateTime.Now;
                RFQ.LastUpdatedBy = userGuid;
                RFQ.CancelDate = DateTime.Now;
                RFQ.CancelBy = userGuid;

                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postDeleteRFQDraft(RequestParameter.inputID input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                RFQ RFQ = dbContext.RFQs.Where(c => c.ID == input.ID).FirstOrDefault();

                if (RFQ == null)
                    return false;

                // Change target Item Library status to deleted
                RFQ.LastUpdatedDate = DateTime.Now;
                RFQ.LastUpdatedBy = userGuid;
                RFQ.CancelDate = DateTime.Now;
                RFQ.CancelBy = userGuid;

                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }
        #endregion


        #region Assignment
        public List<V_RFQList> getPendingAssignmentList(HttpRequestMessage request)
        {
            try
            {
                var data = dbContext.V_RFQList.Where(c => c.CancelDate == null && c.IsDraft == 0 && (c.Purchaser1 == null || c.Purchaser1 == Guid.Empty)).ToList();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool postSubmitAssignmentRFQ(RequestParameter.inputSubmitPendingRFQList input, HttpRequestMessage request)
        {
            try
            {
                foreach (RequestParameter.inputSubmitPendingRFQ pendingRFQ in input.submitList)
                {
                    var editRFQ = dbContext.RFQs.Where(c => c.RFQNo == pendingRFQ.value).FirstOrDefault();

                    editRFQ.Purchaser1 = pendingRFQ.purchaser[0].value;
                    editRFQ.Purchaser2 = pendingRFQ.purchaser[1].value;
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

        public List<V_RFQList> getAssignedPurchaser(HttpRequestMessage request)
        {
            try
            {
                var data = dbContext.V_RFQList.Where(c => c.CancelDate == null && c.IsDraft == 0 && c.Purchaser1StaffName != null).ToList();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        #endregion


        #region RaiseRFQ
        public bool postAddRFQDraftWithUpload(RequestParameter.inputAddRFQ input, List<string> filenameList,
            string pathString, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);
                // Create app key for IPN Counter purpose
                RFQ newRFQ = new RFQ();

                newRFQ.RFQ_StatusID = 1;
                newRFQ.Title = input.Title;
                newRFQ.Description = input.Description;
                newRFQ.Purchaser1 = string.IsNullOrEmpty(input.Purchaser1) ? Guid.Empty : new Guid(input.Purchaser1);
                newRFQ.Purchaser2 = string.IsNullOrEmpty(input.Purchaser2) ? Guid.Empty : new Guid(input.Purchaser2);
                newRFQ.Req_CancelApprv = input.Req_CancelAppr;
                newRFQ.CancelApprv = input.CancelApprv;
                newRFQ.IsDraft = 1;
                newRFQ.CreatedBy = userGuid;
                newRFQ.CreatedDate = DateTime.Now;
                newRFQ.LastUpdatedBy = userGuid;
                newRFQ.LastUpdatedDate = DateTime.Now;
                //newRFQ.tempWatcher = input.tempWatcher;

                dbContext.RFQs.Add(newRFQ);

                #region audit
                //auditBL.auditUpdate(userGuid, "new RFQ", newRFQ.ID.ToString(),
                //    oldEqValue, common.convertObjToJsonString(newRFQ), "RFQ");
                #endregion

                var RFQUploadList = new List<Attachment>();

                foreach (var filename in filenameList)
                {
                    Attachment _RFQFileModel = new Attachment();
                    _RFQFileModel.RFQID = newRFQ.ID;
                    _RFQFileModel.AttachmentName = filename;
                    _RFQFileModel.FilePath = pathString;
                    _RFQFileModel.IsConfidential = 0;
                    _RFQFileModel.CreatedDate = DateTime.Now;
                    _RFQFileModel.CreatedBy = userGuid;

                    RFQUploadList.Add(_RFQFileModel);
                }

                #region audit
                auditBL.auditAddDelete(userGuid, "add new RFQ", null, null, "RFQ");
                #endregion

                dbContext.Attachments.AddRange(RFQUploadList);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postAddRFQWithUpload(RequestParameter.inputAddRFQ input, List<string> filenameList,
            string pathString, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                // Create app key for IPN Counter purpose
                Guid appKey = Guid.NewGuid();

                AppRFQ newAppRFQ = new AppRFQ();

                newAppRFQ.AppKey = appKey;
                newAppRFQ.RequestDate = DateTime.Now;

                RFQ newRFQ = new RFQ();

                newRFQ.RFQ_StatusID = 1;
                newRFQ.Title = input.Title;
                newRFQ.Description = input.Description;
                newRFQ.Purchaser1 = string.IsNullOrEmpty(input.Purchaser1) ? Guid.Empty : new Guid(input.Purchaser1);
                newRFQ.Purchaser2 = string.IsNullOrEmpty(input.Purchaser2) ? Guid.Empty : new Guid(input.Purchaser2);
                newRFQ.Req_CancelApprv = input.Req_CancelAppr;
                newRFQ.CancelApprv = input.CancelApprv;
                newRFQ.IsDraft = 0;
                newRFQ.CreatedBy = userGuid;
                newRFQ.CreatedDate = DateTime.Now;
                newRFQ.LastUpdatedBy = userGuid;
                newRFQ.LastUpdatedDate = DateTime.Now;
                newRFQ.AppKey = appKey;

                dbContext.RFQs.Add(newRFQ);

                #region audit
                //auditBL.auditUpdate(userGuid, "new RFQ", newRFQ.ID.ToString(),
                //    oldEqValue, common.convertObjToJsonString(newRFQ), "RFQ");
                #endregion



                var RFQUploadList = new List<Attachment>();

                foreach (var filename in filenameList)
                {
                    Attachment _RFQFileModel = new Attachment();
                    _RFQFileModel.RFQID = newRFQ.ID;
                    _RFQFileModel.AttachmentName = filename;
                    _RFQFileModel.FilePath = pathString;
                    _RFQFileModel.IsConfidential = 0;
                    _RFQFileModel.CreatedDate = DateTime.Now;
                    _RFQFileModel.CreatedBy = userGuid;

                    RFQUploadList.Add(_RFQFileModel);
                }

                List<AppWatcher> appWatcherList = new List<AppWatcher>();
                foreach (var data in input.Watchers)
                {
                    V_UserDetail watcher = dbContextFrameWork.V_UserDetail.Where(c => c.EmailAddress == data).FirstOrDefault();
                    AppWatcher newAppWatcher = new AppWatcher();
                    newAppWatcher.Watcher_AccessID = watcher.AccessID;
                    newAppWatcher.AppKey = appKey;
                    appWatcherList.Add(newAppWatcher);
                }

                #region audit
                auditBL.auditAddDelete(userGuid, "add new RFQ", null, null, "RFQRFQ");
                #endregion

                dbContext.AppWatchers.AddRange(appWatcherList);
                dbContext.Attachments.AddRange(RFQUploadList);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postEditRFQDraftWithUpload(RequestParameter.inputEditRFQ input, List<string> filenameList,
            string pathString, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                // edit RFQ draft with input
                RFQ newRFQ = dbContext.RFQs.Where(c => c.ID == input.ID).FirstOrDefault();

                newRFQ.RFQ_StatusID = 1;
                newRFQ.Title = input.Title;
                newRFQ.Description = input.Description;
                newRFQ.Purchaser1 = string.IsNullOrEmpty(input.Purchaser1) ? Guid.Empty : new Guid(input.Purchaser1);
                newRFQ.Purchaser2 = string.IsNullOrEmpty(input.Purchaser2) ? Guid.Empty : new Guid(input.Purchaser2);
                newRFQ.Req_CancelApprv = input.Req_CancelAppr;
                newRFQ.CancelApprv = input.CancelApprv;
                newRFQ.IsDraft = 1;
                newRFQ.LastUpdatedBy = userGuid;
                newRFQ.LastUpdatedDate = DateTime.Now;
                //newRFQ.tempWatcher = input.tempWatcher;

                var RFQUploadList = new List<Attachment>();

                foreach (var filename in filenameList)
                {
                    Attachment _RFQFileModel = new Attachment();
                    _RFQFileModel.RFQID = newRFQ.ID;
                    _RFQFileModel.AttachmentName = filename;
                    _RFQFileModel.FilePath = pathString;
                    _RFQFileModel.IsConfidential = 0;
                    _RFQFileModel.CreatedDate = DateTime.Now;
                    _RFQFileModel.CreatedBy = userGuid;

                    RFQUploadList.Add(_RFQFileModel);
                }

                #region audit
                auditBL.auditAddDelete(userGuid, "add new RFQ", null, null, "RFQ");
                #endregion

                dbContext.Attachments.AddRange(RFQUploadList);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postSubmitDraftRFQWithUpload(RequestParameter.inputEditRFQ input, List<string> filenameList,
            string pathString, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                // Create app key for IPN Counter purpose
                Guid appKey = Guid.NewGuid();

                AppRFQ newAppRFQ = new AppRFQ();

                newAppRFQ.AppKey = appKey;
                newAppRFQ.RequestDate = DateTime.Now;

                RFQ newRFQ = dbContext.RFQs.Where(c => c.ID == input.ID).FirstOrDefault();

                newRFQ.RFQ_StatusID = 1;
                newRFQ.Title = input.Title;
                newRFQ.Description = input.Description;
                newRFQ.Purchaser1 = string.IsNullOrEmpty(input.Purchaser1) ? Guid.Empty : new Guid(input.Purchaser1);
                newRFQ.Purchaser2 = string.IsNullOrEmpty(input.Purchaser2) ? Guid.Empty : new Guid(input.Purchaser2);
                newRFQ.Req_CancelApprv = input.Req_CancelAppr;
                newRFQ.CancelApprv = input.CancelApprv;
                newRFQ.IsDraft = 0;
                newRFQ.LastUpdatedBy = userGuid;
                newRFQ.LastUpdatedDate = DateTime.Now;
                newRFQ.AppKey = appKey;

                #region audit
                //auditBL.auditUpdate(userGuid, "new RFQ", newRFQ.ID.ToString(),
                //    oldEqValue, common.convertObjToJsonString(newRFQ), "RFQ");
                #endregion

                var RFQUploadList = new List<Attachment>();

                foreach (var filename in filenameList)
                {
                    Attachment _RFQFileModel = new Attachment();
                    _RFQFileModel.RFQID = newRFQ.ID;
                    _RFQFileModel.AttachmentName = filename;
                    _RFQFileModel.FilePath = pathString;
                    _RFQFileModel.IsConfidential = 0;
                    _RFQFileModel.CreatedDate = DateTime.Now;
                    _RFQFileModel.CreatedBy = userGuid;

                    RFQUploadList.Add(_RFQFileModel);
                }

                List<AppWatcher> appWatcherList = new List<AppWatcher>();
                foreach (var data in input.Watchers)
                {
                    V_UserDetail watcher = dbContextFrameWork.V_UserDetail.Where(c => c.EmailAddress == data).FirstOrDefault();
                    AppWatcher newAppWatcher = new AppWatcher();
                    newAppWatcher.Watcher_AccessID = watcher.AccessID;
                    newAppWatcher.AppKey = appKey;
                    appWatcherList.Add(newAppWatcher);
                }


                #region audit
                auditBL.auditAddDelete(userGuid, "add new RFQ", null, null, "RFQ");
                #endregion

                dbContext.AppWatchers.AddRange(appWatcherList);
                dbContext.Attachments.AddRange(RFQUploadList);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        public bool postDeleteAttachment(RequestParameter.inputEditRFQ input, HttpRequestMessage request)
        {
            try
            {

                for (int i = 0; i < input.CanceledFilesList.Count; i++)
                {
                    Guid userGuid = common.extractUserID(request);
                    DateTime datetimeNow = DateTime.Now;

                    int x = Convert.ToInt32(input.CanceledFilesList[i]);

                    Attachment RFQAttachmentModel = dbContext.Attachments.Where(c => c.ID == x).FirstOrDefault();
                    if (RFQAttachmentModel != null)
                    {
                        var oldValue = common.convertObjToJsonString(RFQAttachmentModel);
                        var randDel = "Del" + new Random().Next(1000, 9999) + "_";
                        var movedFile = RFQAttachmentModel.FilePath + "\\" + RFQAttachmentModel.AttachmentName;

                        if (File.Exists(movedFile))
                            File.Move(movedFile, RFQAttachmentModel.FilePath + "\\" + randDel + RFQAttachmentModel.AttachmentName);

                        dbContext.Attachments.Remove(RFQAttachmentModel);
                    }

                    //#region audit
                    //auditBL.auditUpdate(userGuid, "delete RFQ Attachment", RFQAttachmentModel.ID.ToString(), oldValue,
                    //    common.convertObjToJsonString(RFQAttachmentModel), "RFQAttachmentModel");
                    //#endregion
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

        public bool postSubmitDraftRFQ(RequestParameter.inputEditRFQ input, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = new Guid();
                userGuid = common.extractUserID(request);

                RFQ RFQ = dbContext.RFQs.Where(c => c.ID == input.ID).FirstOrDefault();

                if (RFQ == null)
                    return false;

                // Edit Item Library
                var editRFQ = dbContext.RFQs.Where(c => c.ID == input.ID).FirstOrDefault();

                editRFQ.RFQ_StatusID = 1;
                editRFQ.Title = input.Title;
                editRFQ.Description = input.Description;
                editRFQ.Purchaser1 = string.IsNullOrEmpty(input.Purchaser1) ? Guid.Empty : new Guid(input.Purchaser1);
                editRFQ.Purchaser2 = string.IsNullOrEmpty(input.Purchaser2) ? Guid.Empty : new Guid(input.Purchaser2);
                editRFQ.Req_CancelApprv = input.Req_CancelAppr;
                editRFQ.CancelApprv = input.CancelApprv;
                editRFQ.IsDraft = 0;
                editRFQ.LastUpdatedBy = userGuid;
                editRFQ.LastUpdatedDate = DateTime.Now;

                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }
        public bool postAddWatcher(RequestParameter.inputAddWatcher input)
        {
            try
            {
                V_UserDetail watcher = dbContextFrameWork.V_UserDetail.Where(c => c.EmailAddress == input.email).FirstOrDefault();
                Watcher newWatcher = new Watcher();
                newWatcher.Watchers_AccessID = watcher.AccessID;
                newWatcher.RFQID = input.ID;

                dbContext.Watchers.Add(newWatcher);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        #endregion


        #region RFQDetails
        public List<V_UpdateHistoryTrace> postUpdateHistoryTrace(RequestParameter.inputID input)
        {
            try
            {
                List<V_UpdateHistoryTrace> data = dbContext.V_UpdateHistoryTrace.Where(c => c.RFQID == input.ID && c.UpdateTraceTypeID == 2).ToList();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }

        public bool checkRFQInitialization(RequestParameter.inputID input, List<V_UpdateHistoryTrace> dataList)
        {
            foreach (var data in dataList)
            {
                if (data.UpdateTraceActionID == 4)
                {
                    return true;
                }
            }
            return false;
        }

        public bool postEditRFQWithUpload(RequestParameter.inputEditRFQ input, List<string> filenameList,
            string pathString, HttpRequestMessage request)
        {
            try
            {
                Guid userGuid = common.extractUserID(request);

                // edit RFQ draft with input
                RFQ newRFQ = dbContext.RFQs.Where(c => c.ID == input.ID).FirstOrDefault();
                Guid appKey = Guid.NewGuid();

                newRFQ.RFQ_StatusID = 1;
                newRFQ.Title = input.Title;
                newRFQ.Description = input.Description;
                newRFQ.Purchaser1 = string.IsNullOrEmpty(input.Purchaser1) ? Guid.Empty : new Guid(input.Purchaser1);
                newRFQ.Purchaser2 = string.IsNullOrEmpty(input.Purchaser2) ? Guid.Empty : new Guid(input.Purchaser2);
                newRFQ.Req_CancelApprv = input.Req_CancelAppr;
                newRFQ.CancelApprv = input.CancelApprv;
                newRFQ.IsDraft = 0;
                newRFQ.LastUpdatedBy = userGuid;
                newRFQ.LastUpdatedDate = DateTime.Now;

                var RFQUploadList = new List<Attachment>();

                foreach (var filename in filenameList)
                {
                    Attachment _RFQFileModel = new Attachment();
                    _RFQFileModel.RFQID = newRFQ.ID;
                    _RFQFileModel.AttachmentName = filename;
                    _RFQFileModel.FilePath = pathString;
                    _RFQFileModel.IsConfidential = 0;
                    _RFQFileModel.CreatedDate = DateTime.Now;
                    _RFQFileModel.CreatedBy = userGuid;

                    RFQUploadList.Add(_RFQFileModel);
                }


                #region audit
                auditBL.auditAddDelete(userGuid, "add new Calibration Files", null, null, "CalibrationFile");
                #endregion

                dbContext.Attachments.AddRange(RFQUploadList);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return false;
            }
        }

        #endregion

        #region
        public List<V_RFQList> getRFQOnHandList(HttpRequestMessage request)
        {
            try
            {
                Guid user = common.extractUserID(request);
                var data = dbContext.V_RFQList.Where(c => c.Purchaser1 == user && c.CancelDate == null).ToList();
                return data;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return null;
            }
        }
        #endregion

    }
}