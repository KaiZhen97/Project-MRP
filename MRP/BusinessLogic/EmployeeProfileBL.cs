using MRP.Dal;
using MRP.Models;
using MRP.Properties;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.ModelBinding;
using WebFrameWorkLib.BusinessLogic;
using WebFrameWorkLib.Database;

namespace MRP.BusinessLogic
{
    public class EmployeeProfileBL
    {
        private FrameWorkEntities dbContext = new FrameWorkEntities();
        private EmployeeProfileDal EmployeeProfileDal = new EmployeeProfileDal();
        private WebRequestApi webReqApi = new WebRequestApi();

        public HttpResponseMessage postAddEmployeeProfile(RequestParameter.inputAddEmployeeProfile input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (!modelState.IsValid)
                    return webReqApi.returnBad(request, modelState);

                //verify white space
                if (input.StaffNumber.Contains(" ") == true)
                    return webReqApi.returnBad(Resources.VERIFY_WHITE_SPACE, request);

                if (input.ICNumber == "" && input.PassportNumber == "")
                    return webReqApi.returnBad(Resources.NO_IDENTITY_DATA, request);

                if (input.ICNumber != "" && input.PassportNumber != "")
                    return webReqApi.returnBad(Resources.IDENTITY_FAILED, request);

                var verifyEmployeeProfile = EmployeeProfileDal.verifyEmployeeProfile(input, null);

                if (!verifyEmployeeProfile)
                    return webReqApi.returnBad(Resources.DATA_EXIST, request, input.EmployeeName);

                var addData = EmployeeProfileDal.addEmployeeProfile(input, request);

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

        public HttpResponseMessage postEditEmployeeProfile(RequestParameter.inputEditEmployeeProfile input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (!modelState.IsValid)
                    return webReqApi.returnBad(request, modelState);

                //verify white space
                if (input.StaffNumber.Contains(" ") == true)
                    return webReqApi.returnBad(Resources.VERIFY_WHITE_SPACE, request);

                if (input.ICNumber == "" && input.PassportNumber == "")
                    return webReqApi.returnBad(Resources.NO_IDENTITY_DATA, request);

                if (input.ICNumber != "" && input.PassportNumber != "")
                    return webReqApi.returnBad(Resources.IDENTITY_FAILED, request);

                var verifyEmployeeProfile = EmployeeProfileDal.verifyEmployeeProfile(null, input);

                if (!verifyEmployeeProfile)
                    return webReqApi.returnBad(Resources.DATA_EXIST, request, input.EmployeeName);
                //try
                //var verifyEmployeeProfile = EmployeeProfileDal.verifyEmployeeProfile(null, input);

                //if (!verifyEmployeeProfile)
                //    return webReqApi.returnBad(Resources.DATA_EXIST, request, input.EmployeeName);
                //try

                var editData = EmployeeProfileDal.editEmployeeProfile(input, request);

                if (!editData)
                    return webReqApi.returnBad(Resources.UPDATE_FAILED, request);

                return webReqApi.returnOk(Resources.UPDATE_SUCCESS, request);
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }

        public HttpResponseMessage postDeleteEmployeeProfile(RequestParameter.inputID input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (!modelState.IsValid)
                    return webReqApi.returnBad(request, modelState);

                var deleteData = EmployeeProfileDal.deleteEmployeeProfile(input, request);

                if (!deleteData)
                    return webReqApi.returnBad(Resources.DELETE_FAILED, request);

                return webReqApi.returnOk(Resources.DELETE_SUCCESS, request);
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }

        public HttpResponseMessage postEmployeeProfileByID(RequestParameter.inputID input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (!modelState.IsValid)
                    return webReqApi.returnBad(request, modelState);

                var data = EmployeeProfileDal.postEmployeeProfileByID(input);

                if (data == null)
                    return webReqApi.returnBad(Resources.NO_DATA, request);

                return webReqApi.returnOk(request, data);
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(request, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(request, "Unexpected Error");
            }
        }

        // for employee edit their details

        //public HttpResponseMessage postEditSingleEmployeeProfile(RequestParameter.inputEditUpdateEmployeeProfile input, ModelStateDictionary modelState, HttpRequestMessage request)
        //{
        //    try
        //    {
        //        if (!modelState.IsValid)
        //            return webReqApi.returnBad(request, modelState);

        //        //if (input.ICNumber == "" && input.PassportNumber == "")
        //        //    return webReqApi.returnBad(Resources.NO_IDENTITY_DATA, request);

        //        //if (input.ICNumber != "" && input.PassportNumber != "")
        //        //    return webReqApi.returnBad(Resources.IDENTITY_FAILED, request);

        //        var verifyEmployeeProfile = EmployeeProfileDal.verifyUpdateEmployeeProfile(null, input);

        //        if (!verifyEmployeeProfile)
        //            return webReqApi.returnBad(Resources.DATA_EXIST, request, input.EmployeeName);

        //        var editData = EmployeeProfileDal.editSingleEmployeeProfile(input, request);

        //        if (!editData)
        //            return webReqApi.returnBad(Resources.UPDATE_FAILED, request);

        //        return webReqApi.returnOk(Resources.UPDATE_SUCCESS, request);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message != null)
        //            return webReqApi.returnUnexpected(request, ex.Message.ToString());
        //        else
        //            return webReqApi.returnUnexpected(request, "Unexpected Error");
        //    }
        //}

        //cert upload file type
        public bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".pdf":
                    return true;
                case ".doc":
                    return true;
                case ".docx":
                    return true;
                case ".xls":
                    return true;
                case ".xlsx":
                    return true;
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        #region photo upload
        public HttpResponseMessage postPhotoUploadFile(MultipartMemoryStreamProvider provider, HttpRequestMessage requestMsg)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                var EmployeeID = httpRequest.Params["EmployeeID"];
                int hasheddate = DateTime.Now.GetHashCode();

                //check request contain a file
                if (httpRequest.Files.Count == 0)
                    return webReqApi.returnBad("No file uploaded", requestMsg);

                //check file extension to prevent hacker upload exe files
                var checkFileExtension = CheckPhotoFileType(postedFile.FileName);

                if (!checkFileExtension)
                    return webReqApi.returnBad("File type not allowed", requestMsg);

                //create a unique name with datestr to revent duplicated file name
                //string changed_name = hasheddate.ToString() + "_" + postedFile.FileName;

                //remove special character
                string fileName = postedFile.FileName;
                string originalFileName = postedFile.FileName;
                //foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                //{
                //    fileName = fileName.Replace(c, '_');
                //}

                // Any special or invalid characters will be changed to _(underscore)
                fileName = hasSpecialChar(fileName);

                //create a unique name with datestr to revent duplicated file name
                string changed_name = hasheddate.ToString() + "_" + fileName.Replace(" ", "");

                //save into detination
                var filePath = HttpContext.Current.Server.MapPath("~/UploadedFile/ProfilePhoto/" + changed_name);
                postedFile.SaveAs(filePath);

                //save the file name into database after save successfully. Call the dal function here
                var addData = EmployeeProfileDal.photoUploadFile(EmployeeID, changed_name);

                if (!addData)
                    return webReqApi.returnBad(Resources.ADD_FAILED, requestMsg);

                return webReqApi.returnOk("File Uploaded Successfully", requestMsg);
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                    return webReqApi.returnUnexpected(requestMsg, ex.Message.ToString());
                else
                    return webReqApi.returnUnexpected(requestMsg, "Unexpected Error");
            }
        }

        public bool CheckPhotoFileType(string fileName)
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
                default:
                    return false;
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
        #endregion
    }
}