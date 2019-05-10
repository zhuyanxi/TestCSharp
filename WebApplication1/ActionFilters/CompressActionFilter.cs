using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1
{
    public class CompressActionFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string acceptEncoding = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(acceptEncoding))
            {
                return;
            }
            if (acceptEncoding.ToLower().Contains("gzip"))
            {
                var response = filterContext.HttpContext.Response;
                response.AddHeader("Content-Encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
        }
    }
}