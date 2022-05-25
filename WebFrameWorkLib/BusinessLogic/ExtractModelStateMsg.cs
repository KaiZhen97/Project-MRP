using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace WebFrameWorkLib.BusinessLogic
{
    public class ExtractModelStateMsg
    {
        public string GetErrorMessageForKey(ModelStateDictionary modelState)
        {
            foreach (var key in modelState.Keys)
            {
                if (modelState[key].Errors.Count != 0)
                {
                    return !string.IsNullOrEmpty(modelState[key].Errors[0].ErrorMessage) ? 
                        modelState[key].Errors[0].ErrorMessage.ToString()
                        :
                        modelState[key].Errors[0].Exception.Message;
                }
            }

            return "";
        }
    }
}