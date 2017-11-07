using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.common.Helper.Extention
{
    public static class HtmlHelperExtention
    {
        public static string GetContent(this UrlHelper urlHelper, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
                return string.Empty;
            else
                return urlHelper.Content(contentPath);
        }
    }
}