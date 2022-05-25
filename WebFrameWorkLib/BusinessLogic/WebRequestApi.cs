using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Web.Script.Serialization;
using WebFrameWorkLib.Models;

namespace WebFrameWorkLib.BusinessLogic
{
    public class WebRequestApi
    {
        private WebRequestApiResponse webRequestApiResponse = new WebRequestApiResponse();
        private SystemMessage systemMessage = new SystemMessage();
        private Common common = new Common();
        private ExtractModelStateMsg extractModelStateMsg = new ExtractModelStateMsg();

        public WebRequestApiResponse sendWebRequest(string sessionToken, string methodURL, string postDataStr, string apiMethod, string contentType, string alias) {
            try
            {
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");

                var requestPath = strUrl + alias + methodURL;
                var request = (HttpWebRequest)WebRequest.Create(requestPath);

                var postData = postDataStr;
                var data = Encoding.UTF8.GetBytes(postData);
                
                request.Method = apiMethod;
                request.ContentType = contentType;
                request.ContentLength = data.Length;

                if (sessionToken!=null && sessionToken!="")
                    request.Headers[HttpRequestHeader.Authorization] = "Bearer " + sessionToken;

                if (apiMethod == "Post" || apiMethod == "POST")
                {
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }                

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                var serializer = new JavaScriptSerializer();
                dynamic jsonObject = serializer.Deserialize<dynamic>(responseString);

                webRequestApiResponse.responseStatus = true;
                webRequestApiResponse.responseJson = jsonObject;
                return webRequestApiResponse;
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            var responseString = reader.ReadToEnd();
                            var serializer = new JavaScriptSerializer();
                            dynamic jsonObject = serializer.Deserialize<dynamic>(responseString);

                            webRequestApiResponse.responseStatus = false;
                            webRequestApiResponse.responseJson = jsonObject;
                            return webRequestApiResponse;
                        }
                    }
                }
                return null;
            }
        }

        public HttpResponseMessage returnBad(string message, HttpRequestMessage request)
        {
            systemMessage.Message = common.CustomMsg(message);
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
            return response;
        }

        public HttpResponseMessage returnBad(string message, HttpRequestMessage request, string dataExist)
        {
            string rawMsg = common.CustomMsg(message);
            systemMessage.Message = rawMsg.Replace("@0", dataExist);
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
            return response;
        }

        public HttpResponseMessage returnBad(string message, HttpRequestMessage request, string dataExist1, string dataExist2)
        {
            string rawMsg = common.CustomMsg(message);
            systemMessage.Message = rawMsg.Replace("@0", dataExist1).Replace("@1", dataExist2);
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
            return response;
        }

        public HttpResponseMessage returnBad(HttpRequestMessage request, ModelStateDictionary modelState)
        {
            systemMessage.Message = extractModelStateMsg.GetErrorMessageForKey(modelState);
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.BadRequest, systemMessage);
            return response;
        }

        public HttpResponseMessage returnOk(string message, HttpRequestMessage request)
        {
            systemMessage.Message = common.CustomMsg(message);
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, systemMessage);
            return response;
        }

        public HttpResponseMessage returnOk(HttpRequestMessage request, Object data)
        {
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, data);
            return response;
        }

        public HttpResponseMessage returnUnexpected(HttpRequestMessage request, string message)
        {
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.InternalServerError, "error" + message);
            return response;
        }
    }

    public class WebRequestApiResponse {
        public bool responseStatus { get; set; }
        public dynamic responseJson { get; set; }
    }
}