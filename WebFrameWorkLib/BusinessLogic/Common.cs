using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using WebFrameWorkLib.Models;

namespace WebFrameWorkLib.BusinessLogic
{
    public class Common
    {
        //retrieve message from xml file using resource name
        LogError logError = new LogError();
        private bool invalid = false;

        public string CustomMsg(string resourcesName)
        {
            try
            {
                string pathCommonMsg = (System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:///", "");
                pathCommonMsg = pathCommonMsg.Replace("/", "\\");
                pathCommonMsg = pathCommonMsg.Substring(0, pathCommonMsg.LastIndexOf("bin")) + "\\SystemMessage.xml";

                XDocument doc = XDocument.Load(pathCommonMsg);

                string element = string.Empty;
                element = doc.Element("Msg").Element("CustomMsg").Element(resourcesName).Value;
                return element;
            }
            catch (Exception ex)
            {

                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return string.Empty;
            }
        }

        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

        public string convertObjToJsonString(dynamic dbObj)
        {
            try
            {
                string contructStr = JsonConvert.SerializeObject(dbObj, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                
                
                return contructStr;
            }
            catch (Exception ex)
            {
                logError.LogErrorDb("Error", System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), ex.ToString());
                return "";
            }
        }

        public Guid extractUserID(HttpRequestMessage Request) {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userId = principal.Claims.Where(c => c.Type == "UserID").SingleOrDefault().Value;
            Guid userGuid = new Guid(userId);

            return userGuid;
        }

        public string extractUserRole(HttpRequestMessage Request)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var role = principal.Claims.Where(c => c.Type == "RoleDesc").SingleOrDefault().Value;

            return role;
        }

        public Dictionary<string, string> GetQueryStrings(HttpRequestMessage request)
        {
            return request.GetQueryNameValuePairs()
                          .ToDictionary(kv => kv.Key, kv => kv.Value,
                               StringComparer.OrdinalIgnoreCase);
        }
    }
}