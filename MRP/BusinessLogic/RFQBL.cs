using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
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
    public class RFQBL
    {
        private MRPEntities dbContext = new MRPEntities();
        private RFQDal RFQDal = new RFQDal();
        private WebRequestApi webReqApi = new WebRequestApi();
        private SystemMessage systemMessage = new SystemMessage();
        private ExtractModelStateMsg extractModelStateMsg = new ExtractModelStateMsg();
        private UpdateTraceDal updateTraceDal = new UpdateTraceDal();


        #region RFQList
        public HttpResponseMessage postDeleteRFQ(RequestParameter.inputDeleteRFQ input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var editData = RFQDal.postDeleteRFQ(input, request);

                    if (!editData)
                        return webReqApi.returnBad(Resources.UPDATE_FAILED, request);

                    return webReqApi.returnOk(Resources.UPDATE_SUCCESS, request);
                }
                else
                {
                    systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }


            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }

        public HttpResponseMessage postDeleteRFQDraft(RequestParameter.inputID input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var editData = RFQDal.postDeleteRFQDraft(input, request);

                    if (!editData)
                        return webReqApi.returnBad(Resources.UPDATE_FAILED, request);

                    return webReqApi.returnOk(Resources.UPDATE_SUCCESS, request);
                }
                else
                {
                    systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }


            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }
        #endregion


        #region RaiseRFQ
        public HttpResponseMessage postAddRFQDraftWithUpload(MultipartMemoryStreamProvider provider, HttpRequestMessage request)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                string inputJson = httpRequest.Params["JsonString"];
                var inputRFQ = JsonConvert.DeserializeObject<inputRFQFileUpload>(inputJson);

                var input = new RequestParameter.inputAddRFQ();
                string errMsg = checkingInputRFQ(inputRFQ, out input);

                if (!string.IsNullOrEmpty(errMsg))
                    return webReqApi.returnBad("input error", request, errMsg);

                int hasheddate = DateTime.Now.GetHashCode();
                List<string> changedFilenameLsit = new List<string>();

                //check request contain a file
                //if (httpRequest.Files.Count == 0)
                //    //return webReqApi.returnBad(Resource.FILE_IS_REQUIRED, request);
                //    return webReqApi.returnBad("resoursefile", request);

                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    HttpPostedFile postedFile = httpRequest.Files[i];
                    //check file extension to prevent hacker upload exe files
                    var checkFileExtension = CheckFileType(postedFile.FileName);

                    if (!checkFileExtension)
                        return webReqApi.returnBad(Resources.INVALID_FILE_TYPE, request, postedFile.FileName);

                    //remove special character
                    string fileName = postedFile.FileName;
                    string originalFileName = postedFile.FileName;

                    // Any special or invalid characters will be changed to _(underscore)
                    fileName = hasSpecialChar(fileName);

                    //create a unique name with datestr to revent duplicated file name
                    string changed_name = hasheddate.ToString() + "_" + fileName.Replace(" ", "");

                    //save into destination
                    var filePath = HttpContext.Current.Server.MapPath("~/Attachment/RFQ/" + changed_name);
                    postedFile.SaveAs(filePath);

                    changedFilenameLsit.Add(changed_name);

                }

                //save the file name into database after save successfully. Call the dal function here
                var addData = RFQDal.postAddRFQDraftWithUpload(input, changedFilenameLsit,
                    AppDomain.CurrentDomain.BaseDirectory + "\\Attachment\\RFQ", request);

                if (!addData)
                    return webReqApi.returnBad(Resources.ADD_FAILED, request);

                return webReqApi.returnOk(Resources.ADD_SUCCESS, request);
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }

        public HttpResponseMessage postAddRFQWithUpload(MultipartMemoryStreamProvider provider, HttpRequestMessage request)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                string inputJson = httpRequest.Params["JsonString"];
                var inputRFQ = JsonConvert.DeserializeObject<inputRFQFileUpload>(inputJson);

                var input = new RequestParameter.inputAddRFQ();
                string errMsg = checkingInputRFQ(inputRFQ, out input);

                if (!string.IsNullOrEmpty(errMsg))
                    return webReqApi.returnBad("input error", request, errMsg);

                int hasheddate = DateTime.Now.GetHashCode();
                List<string> changedFilenameLsit = new List<string>();

                //check request contain a file
                if (httpRequest.Files.Count == 0)
                    return webReqApi.returnBad(Resources.FILE_IS_REQUIRED, request);

                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    HttpPostedFile postedFile = httpRequest.Files[i];
                    //check file extension to prevent hacker upload exe files
                    var checkFileExtension = CheckFileType(postedFile.FileName);

                    if (!checkFileExtension)
                        //return webReqApi.returnBad(Resource.INVALID_FILE_TYPE_2, request, postedFile.FileName);
                        return webReqApi.returnBad(Resources.INVALID_FILE_TYPE, request);

                    //remove special character
                    string fileName = postedFile.FileName;
                    string originalFileName = postedFile.FileName;

                    // Any special or invalid characters will be changed to _(underscore)
                    fileName = hasSpecialChar(fileName);

                    //create a unique name with datestr to revent duplicated file name
                    string changed_name = hasheddate.ToString() + "_" + fileName.Replace(" ", "");

                    //save into destination
                    var filePath = HttpContext.Current.Server.MapPath("~/Attachment/RFQ/" + changed_name);
                    postedFile.SaveAs(filePath);

                    changedFilenameLsit.Add(changed_name);
                }

                //save the file name into database after save successfully. Call the dal function here
                var addData = RFQDal.postAddRFQWithUpload(input, changedFilenameLsit,
                    AppDomain.CurrentDomain.BaseDirectory + "\\Attachment\\RFQ", request);

                if (!addData)
                    return webReqApi.returnBad(Resources.ADD_FAILED, request);

                return webReqApi.returnOk(Resources.ADD_SUCCESS, request);
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }

        public HttpResponseMessage postEditRFQDraftWithUpload(MultipartMemoryStreamProvider provider, HttpRequestMessage request)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                string inputJson = httpRequest.Params["JsonString"];
                var inputRFQ = JsonConvert.DeserializeObject<RequestParameter.inputEditRFQ>(inputJson);

                var input = new RequestParameter.inputEditRFQ();
                string errMsg = checkingEditRFQ(inputRFQ, out input);

                if (!string.IsNullOrEmpty(errMsg))
                    return webReqApi.returnBad("input error", request, errMsg);

                int hasheddate = DateTime.Now.GetHashCode();
                List<string> changedFilenameLsit = new List<string>();

                //check request contain a file
                //if (httpRequest.Files.Count == 0)
                //    //return webReqApi.returnBad(Resource.FILE_IS_REQUIRED, request);
                //    return webReqApi.returnBad("resoursefile", request);
                bool editData = true;
                if (input.CanceledFilesList.Count > 0)
                {
                    editData = RFQDal.postDeleteAttachment(input, request);
                }

                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    HttpPostedFile postedFile = httpRequest.Files[i];
                    //check file extension to prevent hacker upload exe files
                    var checkFileExtension = CheckFileType(postedFile.FileName);

                    if (!checkFileExtension)
                        return webReqApi.returnBad(Resources.INVALID_FILE_TYPE, request, postedFile.FileName);

                    //remove special character
                    string fileName = postedFile.FileName;
                    string originalFileName = postedFile.FileName;

                    //V_RFQAttachmentsList attachment = dbContext.V_RFQAttachmentsList.Where(c => c.RFQID == input.ID && c.AttachmentName == fileName).FirstOrDefault();
                    V_RFQAttachmentsList attachment = dbContext.V_RFQAttachmentsList.Where(c => c.ID == input.ID && c.AttachmentName == fileName).FirstOrDefault();

                    //check for duplicated file
                    if (attachment == null)
                    {
                        // Any special or invalid characters will be changed to _(underscore)
                        fileName = hasSpecialChar(fileName);

                        //create a unique name with datestr to revent duplicated file name
                        string changed_name = hasheddate.ToString() + "_" + fileName.Replace(" ", "");

                        //save into destination
                        var filePath = HttpContext.Current.Server.MapPath("~/Attachment/RFQ/" + changed_name);
                        postedFile.SaveAs(filePath);

                        changedFilenameLsit.Add(changed_name);
                    }

                }

                //save the file name into database after save successfully. Call the dal function here
                var addData = RFQDal.postEditRFQDraftWithUpload(input, changedFilenameLsit,
                    AppDomain.CurrentDomain.BaseDirectory + "\\Attachment\\RFQ", request);

                if (!addData && !editData)
                    return webReqApi.returnBad(Resources.ADD_FAILED, request);

                //save update history
                var addUpdateTrace = updateTraceDal.postAddUpdateTraceRFQ(input, request);

                if(!addUpdateTrace)
                    return webReqApi.returnBad(Resources.ADD_FAILED, request);

                return webReqApi.returnOk(Resources.ADD_SUCCESS, request);
                
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }

        public HttpResponseMessage postSubmitDraftRFQWithUpload(MultipartMemoryStreamProvider provider, HttpRequestMessage request)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                string inputJson = httpRequest.Params["JsonString"];
                var inputRFQ = JsonConvert.DeserializeObject<RequestParameter.inputEditRFQ>(inputJson);

                var input = new RequestParameter.inputEditRFQ();
                string errMsg = checkingEditRFQ(inputRFQ, out input);

                if (!string.IsNullOrEmpty(errMsg))
                    return webReqApi.returnBad("input error", request, errMsg);

                int hasheddate = DateTime.Now.GetHashCode();
                 List<string> changedFilenameLsit = new List<string>();

                //check request contain a file
                //if (httpRequest.Files.Count == 0)
                //    return webReqApi.returnBad(Resources.FILE_IS_REQUIRED, request);

                //check if user deletes files during editing
                bool editData = true;
                if (input.CanceledFilesList.Count > 0)
                {
                    editData = RFQDal.postDeleteAttachment(input, request);
                }

                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    HttpPostedFile postedFile = httpRequest.Files[i];
                    //check file extension to prevent hacker upload exe files
                    var checkFileExtension = CheckFileType(postedFile.FileName);

                    if (!checkFileExtension)
                        return webReqApi.returnBad(Resources.INVALID_FILE_TYPE, request, postedFile.FileName);

                    //remove special character
                    string fileName = postedFile.FileName;
                    string originalFileName = postedFile.FileName;

                    //V_RFQAttachmentsList attachment = dbContext.V_RFQAttachmentsList.Where(c => c.RFQID == input.ID && c.AttachmentName == fileName).FirstOrDefault();
                    V_RFQAttachmentsList attachment = dbContext.V_RFQAttachmentsList.Where(c => c.ID == input.ID && c.AttachmentName == fileName).FirstOrDefault();

                    //check for duplicated file
                    if (attachment == null)
                    {
                        // Any special or invalid characters will be changed to _(underscore)
                        fileName = hasSpecialChar(fileName);

                        //create a unique name with datestr to revent duplicated file name
                        string changed_name = hasheddate.ToString() + "_" + fileName.Replace(" ", "");

                        //save into destination
                        var filePath = HttpContext.Current.Server.MapPath("~/Attachment/RFQ/" + changed_name);
                        postedFile.SaveAs(filePath);

                        changedFilenameLsit.Add(changed_name);
                    }

                }

                //save the file name into database after save successfully. Call the dal function here
                var addData = RFQDal.postSubmitDraftRFQWithUpload(input, changedFilenameLsit,
                    AppDomain.CurrentDomain.BaseDirectory + "\\Attachment\\RFQ", request);

                if (!addData && !editData)
                    return webReqApi.returnBad(Resources.ADD_FAILED, request);

                //save update history
                var addUpdateTrace = updateTraceDal.postAddUpdateTraceRFQ(input, request);

                if (!addUpdateTrace)
                    return webReqApi.returnBad(Resources.ADD_FAILED, request);

                return webReqApi.returnOk(Resources.ADD_SUCCESS, request);
                
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }

        public static string hasSpecialChar(string fileName)
        {
            string ext = Path.GetExtension(fileName);

            fileName = fileName.Substring(0, fileName.Length - ext.Length);
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_, ";
            foreach (var item in specialChar)
            {
                if (item == '&')
                {
                    fileName = fileName.Replace(item.ToString(), "and");
                }
                if (fileName.Contains(item))
                {
                    fileName = fileName.Replace(item, '_');
                }
            }
            fileName = fileName + ext;

            return fileName;
        }

        public bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".gif":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".png":
                    return true;
                case ".pdf":
                    return true;
                case ".doc":
                    return true;
                case ".docx":
                    return true;
                //case ".xls":
                //    return true;
                //case ".xlsx":
                //    return true;
                //case ".ppt":
                //    return true;
                //case ".pptx":
                //    return true;
                default:
                    return false;
            }
        }

        private string checkingInputRFQ(inputRFQFileUpload input, out RequestParameter.inputAddRFQ outputRFQ)
        {
            try
            {
                outputRFQ = new RequestParameter.inputAddRFQ();

                if (string.IsNullOrEmpty(input.Title))
                    return "Title is required.";
                else
                {
                    outputRFQ.Title = input.Title;
                    outputRFQ.Watchers = input.Watchers;
                    outputRFQ.Description = input.Description;
                    outputRFQ.tempWatcher = input.tempWatcher;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
}
        }

        private string checkingEditRFQ(RequestParameter.inputEditRFQ input, out RequestParameter.inputEditRFQ outputRFQ)
        {
            try
            {
                outputRFQ = new RequestParameter.inputEditRFQ();

                if (string.IsNullOrEmpty(input.Title))
                    return "Title is required.";
                else
                {
                    outputRFQ.CanceledFilesList = input.CanceledFilesList;
                    outputRFQ.ID = input.ID;
                    outputRFQ.Title = input.Title;
                    outputRFQ.RFQ_StatusID = input.RFQ_StatusID;
                    outputRFQ.Description = input.Description;
                    outputRFQ.TraceAction = input.TraceAction;
                    outputRFQ.tempWatcher = input.tempWatcher;
                    outputRFQ.Watchers = input.Watchers;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private class inputRFQFileUpload
        {
            
            public string Title { get; set; }
            public string Description { get; set; }
            public List<string> Watchers { get; set; }
            public string tempWatcher { get; set; }
        }

        private class editRFQFileUpload
        {
            
            public ArrayList CanceledFiles { get; set; }
            public int ID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int TraceAction{ get; set; }
        }

        #endregion


        #region Assignment
        public HttpResponseMessage postSubmitAssignmentRFQ(RequestParameter.inputSubmitPendingRFQList input, HttpRequestMessage request)
        {
            try
            {
                for (var i = 0; i < input.submitList.Count; i++)
                {
                    if (input.submitList[i].purchaser[0].value == null || input.submitList[i].purchaser[0].value == Guid.Empty)
                    {
                        return webReqApi.returnBad(Resources.ADD_FAILED, request);
                    }
                }
                
                var addData = RFQDal.postSubmitAssignmentRFQ(input, request);

                if (!addData)
                    return webReqApi.returnBad(Resources.ADD_FAILED, request);

                return webReqApi.returnOk(Resources.ADD_SUCCESS, request);
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }
        #endregion


        #region RFQDetails

        public HttpResponseMessage postEditRFQWithUpload(MultipartMemoryStreamProvider provider, HttpRequestMessage request)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                string inputJson = httpRequest.Params["JsonString"];
                var inputRFQ = JsonConvert.DeserializeObject<RequestParameter.inputEditRFQ>(inputJson);

                var input = new RequestParameter.inputEditRFQ();
                string errMsg = checkingEditRFQ(inputRFQ, out input);

                if (!string.IsNullOrEmpty(errMsg))
                    return webReqApi.returnBad("input error", request, errMsg);

                int hasheddate = DateTime.Now.GetHashCode();
                List<string> changedFilenameLsit = new List<string>();

                bool editData = true;
                if (input.CanceledFilesList.Count > 0)
                {
                    editData = RFQDal.postDeleteAttachment(input, request);
                }

                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    HttpPostedFile postedFile = httpRequest.Files[i];
                    //check file extension to prevent hacker upload exe files
                    var checkFileExtension = CheckFileType(postedFile.FileName);

                    if (!checkFileExtension)
                        //return webReqApi.returnBad(Resource.INVALID_FILE_TYPE_2, request, postedFile.FileName);
                        return webReqApi.returnBad("resoursefile", request);

                    //remove special character
                    string fileName = postedFile.FileName;
                    string originalFileName = postedFile.FileName;

                    //V_RFQAttachmentsList attachment = dbContext.V_RFQAttachmentsList.Where(c => c.RFQID == input.ID && c.AttachmentName == fileName).FirstOrDefault();
                    V_RFQAttachmentsList attachment = dbContext.V_RFQAttachmentsList.Where(c => c.ID == input.ID && c.AttachmentName == fileName).FirstOrDefault();

                    //check for duplicated file
                    if (attachment == null)
                    {
                        // Any special or invalid characters will be changed to _(underscore)
                        fileName = hasSpecialChar(fileName);

                        //create a unique name with datestr to revent duplicated file name
                        string changed_name = hasheddate.ToString() + "_" + fileName.Replace(" ", "");

                        //save into destination
                        var filePath = HttpContext.Current.Server.MapPath("~/Attachment/RFQ/" + changed_name);
                        postedFile.SaveAs(filePath);

                        changedFilenameLsit.Add(changed_name);
                    }

                }

                //save the file name into database after save successfully. Call the dal function here
                var addData = RFQDal.postEditRFQWithUpload(input, changedFilenameLsit,
                    AppDomain.CurrentDomain.BaseDirectory + "\\Attachment\\RFQ", request);

                if (!addData && !editData)
                    return webReqApi.returnBad(Resources.ADD_FAILED, request);

                //save update history
                var addUpdateTrace = updateTraceDal.postAddUpdateTraceRFQ(input, request);

                if (!addUpdateTrace)
                    return webReqApi.returnBad(Resources.ADD_FAILED, request);

                return webReqApi.returnOk(Resources.ADD_SUCCESS, request);

            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }
        #endregion

        public HttpResponseMessage postRFQByID(RequestParameter.inputID input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var data = RFQDal.postRFQByID(input);

                    if (data == null)
                        return webReqApi.returnBad(Resources.NO_DATA, request);

                    return webReqApi.returnOk(request, data);
                }
                else
                {
                    systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }

        public HttpResponseMessage postWatcherEmailByID(RequestParameter.inputID input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var data = RFQDal.postWatcherEmailByID(input);

                    if (data == null)
                        return webReqApi.returnBad(Resources.NO_DATA, request);

                    return webReqApi.returnOk(request, data);
                }
                else
                {
                    systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }

        public HttpResponseMessage postSubmitDraftRFQ(RequestParameter.inputEditRFQ input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var editData = RFQDal.postSubmitDraftRFQ(input, request);

                    if (!editData)
                        return webReqApi.returnBad(Resources.UPDATE_FAILED, request);

                    return webReqApi.returnOk(Resources.UPDATE_SUCCESS, request);
                }
                else
                {
                    systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }      

        public HttpResponseMessage postClosedRFQByID(RequestParameter.inputID input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var data = RFQDal.postClosedRFQByID(input);

                    if (data == null)
                        return webReqApi.returnBad(Resources.NO_DATA, request);

                    return webReqApi.returnOk(request, data);
                }
                else
                {
                    systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                    return response;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }

        //public HttpResponseMessage postAttachmentByID(RequestParameter.inputID input, ModelStateDictionary modelState, HttpRequestMessage request)
        //{
        //    try
        //    {
        //        if (modelState.IsValid)
        //        {
        //            var data = RFQDal.postAttachmentByID(input);

        //            if (data == null)
        //                return webReqApi.returnBad(Resources.NO_DATA, request);

        //            return webReqApi.returnOk(request, data);
        //        }
        //        else
        //        {
        //            systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
        //            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message != null)
        //            return webReqApi.returnUnexpected(request, ex.Message.ToString());
        //        else
        //            return webReqApi.returnUnexpected(request, "Unexpected Error");
        //    }
        //}

        public HttpResponseMessage postAddWatcher (RequestParameter.inputAddWatcher input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            if (modelState.IsValid)
            {
                var data = RFQDal.postAddWatcher(input);

                if(!data)
                    return webReqApi.returnBad(Resources.NO_DATA, request);

                return webReqApi.returnOk(request, data);
            }
            else
            {
                systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
                return response;
            }
        }
    }
}

