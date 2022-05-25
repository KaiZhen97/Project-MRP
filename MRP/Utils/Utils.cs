using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MRP
{
    public static class Utils
    {
        public static int __defaultTimeout = 5000;

        public static void PromptInfo(Page page, string title, string message, string position = "topRight", Nullable<int> timeout_ms = null)
        {
            PromptMessage(page, PromptMessageType.Info, title, message, position, timeout_ms == null ? __defaultTimeout : timeout_ms);
        }

        public static void PromptAlert(Page page, string title, string message, string position = "topRight", Nullable<int> timeout_ms = null)
        {
            PromptMessage(page, PromptMessageType.Alert, title, message, position, timeout_ms == null ? __defaultTimeout : timeout_ms);
        }

        public static void PromptPass(Page page, string title, string message, string position = "topRight", Nullable<int> timeout_ms = null)
        {
            PromptMessage(page, PromptMessageType.Pass, title, message, position, timeout_ms == null ? __defaultTimeout : timeout_ms);
        }

        public static void PromptError(Page page, string title, string message, string position = "topRight", Nullable<int> timeout_ms = null)
        {
            PromptMessage(page, PromptMessageType.Error, title, message, position, timeout_ms == null ? __defaultTimeout : timeout_ms);
        }

        public static void PromptMessage(Page page, PromptMessageType type, string title, string message, string position = "topRight", Nullable<int> timeout_ms = 0)
        {
            var methodName = "ShowInfo";

            switch(type)
            {
                case PromptMessageType.Info:
                    methodName = "ShowInfo";
                    break;
                case PromptMessageType.Alert:
                    methodName = "ShowAlert";
                    break;
                case PromptMessageType.Pass:
                    methodName = "ShowPass";
                    break;
                case PromptMessageType.Error:
                    methodName = "ShowError";
                    break;

                default:
                    methodName = "alert";
                    break;
            }

            string script = $"window.onload=function() {{ {methodName}('{title}', '{message}', '{position}', {timeout_ms}); }}";

            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true);
        }
    }

    public enum PromptMessageType
    {
        Info = 1,
        Alert = 2,
        Pass = 3,
        Error = 4
    }
}