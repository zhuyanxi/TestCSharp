using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MicroNetCore
{
    public interface IServer
    {
        Task StartAsync(RequestDelegate handler);
    }

    public class HttpListenerFeature : IHttpRequestFeature, IHttpResponseFeature
    {

        private readonly HttpListenerContext _context;

        public HttpListenerFeature(HttpListenerContext context) => _context = context;
        Uri IHttpRequestFeature.Url => _context.Request.Url;

        NameValueCollection IHttpRequestFeature.Headers => _context.Request.Headers;

        NameValueCollection IHttpResponseFeature.Headers => _context.Response.Headers;

        Stream IHttpRequestFeature.Body => _context.Request.InputStream;

        Stream IHttpResponseFeature.Body => _context.Response.OutputStream;

        int IHttpResponseFeature.StatusCode
        {
            get => _context.Response.StatusCode;
            set => _context.Response.StatusCode = value;
        }
    }

    public class HttpListenerServer : IServer
    {
        private readonly HttpListener _httpListener;
        private readonly string[] _urls;
        public HttpListenerServer(params string[] urls)
        {
            _httpListener = new HttpListener();
            _urls = urls.Any() ? urls : new string[] { "http://localhost:5000/" };
        }

        public async Task StartAsync(RequestDelegate handler)
        {
            Array.ForEach(_urls, url => _httpListener.Prefixes.Add(url));
            _httpListener.Start();
            Console.WriteLine($"Server {typeof(HttpListenerServer).Name} open, start listening:{string.Join(";", _urls)}");
            while (true)
            {
                var listenerContext = await _httpListener.GetContextAsync();
                var feature = new HttpListenerFeature(listenerContext);
                var features = new FeatureCollection().Set<IHttpRequestFeature>(feature).Set<IHttpResponseFeature>(feature);
                var httpContext = new HttpContext(features);
                await handler(httpContext);
                listenerContext.Response.Close();
            }
        }
    }
}
