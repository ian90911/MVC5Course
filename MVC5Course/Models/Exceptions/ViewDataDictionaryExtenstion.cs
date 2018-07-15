using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Models.Exceptions
{
    public static class ViewDataDictionaryExtenstion
    {
        public static string GetModelStateValue(this WebViewPage wvp,string key)
        {
            return wvp.ViewContext.ViewData.ModelState[key]?.Value.AttemptedValue;
        }
    }
}