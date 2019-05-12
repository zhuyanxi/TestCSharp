using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroNetCore
{
    public interface IApplicationBuilder
    {
        IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);
        RequestDelegate Build();
    }

    public class ApplicationBuilder : IApplicationBuilder
    {
        private readonly List<Func<RequestDelegate, RequestDelegate>> _middlewares = new List<Func<RequestDelegate, RequestDelegate>>();

        public RequestDelegate Build()
        {
            //_middlewares.Reverse();

            //return httpContext =>
            //{
            //    RequestDelegate next = _ => { _.Response.StatusCode = 404; return Task.CompletedTask; };

            //    foreach (var middleware in _middlewares)
            //    {
            //        next += middleware(next);
            //    }
            //    return next(httpContext);
            //};

            RequestDelegate next = context =>
            {
                context.Response.StatusCode = 404;
                return Task.CompletedTask;
            };
            foreach (var middleware in _middlewares)
            {
                next += middleware(next);
            }
            return next;
        }

        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}
