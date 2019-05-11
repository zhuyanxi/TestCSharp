using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MicroNetCore
{
    public class HttpContext
    {
        public HttpContext(IFeatureCollection features)
        {
            Request = new HttpRequest(features);
            Response = new HttpResponse(features);
        }

        public HttpRequest Request { get; set; }
        public HttpResponse Response { get; set; }
    }

    public class HttpRequest
    {
        private readonly IHttpRequestFeature _httpRequestFeature;
        public HttpRequest(IFeatureCollection features)
        {
            _httpRequestFeature = features.Get<IHttpRequestFeature>();
        }

        public Uri Url => _httpRequestFeature.Url;
        public NameValueCollection Header => _httpRequestFeature.Headers;
        public Stream body => _httpRequestFeature.Body;
    }

    public class HttpResponse
    {
        private readonly IHttpResponseFeature _httpResponseFeature;
        public HttpResponse(IFeatureCollection features)
        {
            _httpResponseFeature = features.Get<IHttpResponseFeature>();
        }

        public NameValueCollection Header => _httpResponseFeature.Headers;
        public Stream body => _httpResponseFeature.Body;
        public int StatusCode
        {
            get => _httpResponseFeature.StatusCode;
            set => _httpResponseFeature.StatusCode = value;
        }
    }

    public static partial class Extensions
    {
        public static Task WriteAsync(this HttpResponse response, string content)
        {
            var buffer = Encoding.UTF8.GetBytes(content);
            return response.body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
