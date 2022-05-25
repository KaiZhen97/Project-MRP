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
    public class ItemLibraryBL
    {
        private MRPEntities dbContext = new MRPEntities();
        private ItemLibraryDal itemLibraryDal = new ItemLibraryDal();
        private WebRequestApi webReqApi = new WebRequestApi();
        private SystemMessage systemMessage = new SystemMessage();
        private ExtractModelStateMsg extractModelStateMsg = new ExtractModelStateMsg();

        #region ItemLibrary
        public HttpResponseMessage postItemLibraryByID(RequestParameter.inputID input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var data = itemLibraryDal.postItemLibraryByID(input);

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

        public HttpResponseMessage postAddItemLibrary(RequestParameter.inputAddItemLibrary input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var addData = itemLibraryDal.postAddItemLibrary(input, request);

                    if (!addData)
                        return webReqApi.returnBad(Resources.ADD_FAILED, request);

                    return webReqApi.returnOk(Resources.ADD_SUCCESS, request);
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

        public HttpResponseMessage postEditItemLibrary(RequestParameter.inputEditItemLibrary input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var editData = itemLibraryDal.postEditItemLibrary(input, request);

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

        public HttpResponseMessage postDeleteItemLibrary(RequestParameter.inputID input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var deleteData = itemLibraryDal.postDeleteItemLibrary(input, request);

                    if (!deleteData)
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


        #region Category
        public HttpResponseMessage getActiveCategoryByID(RequestParameter.inputID input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var data = itemLibraryDal.getActiveCategoryByID(input);

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

        public HttpResponseMessage postAddNewCategory(RequestParameter.inputAddCategory input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var addData = itemLibraryDal.postAddNewCategory(input, request);

                    if (!addData)
                        return webReqApi.returnBad(Resources.ADD_FAILED, request);

                    return webReqApi.returnOk(Resources.ADD_SUCCESS, request);
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

        public HttpResponseMessage postEditCategory(RequestParameter.inputEditCategory input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var editData = itemLibraryDal.postEditCategory(input, request);

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

        public HttpResponseMessage postDeleteCategory(RequestParameter.inputDeleteCategory input, ModelStateDictionary modelState, HttpRequestMessage request)
        {
            try
            {
                if (modelState.IsValid)
                {
                    var deleteData = itemLibraryDal.postDeleteCategory(input, request);

                    if (!deleteData)
                        return webReqApi.returnBad(Resources.DELETE_FAILED, request);

                    return webReqApi.returnOk(Resources.DELETE_SUCCESS, request);
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
    }
}