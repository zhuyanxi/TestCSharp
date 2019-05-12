using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroNetCore
{

    public class QuxMiddleware
    {
        private readonly RequestDelegate _next;
        public QuxMiddleware() { }
        public QuxMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("qux=>");
            //await _next(context);
        }
    }

    public static partial class Extensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder application, Type type)
        {
            //RequestDelegate next = context =>
            //{
            //    context.Response.StatusCode = 404;
            //    return Task.CompletedTask;
            //};
            //var qux = (QuxMiddleware)Activator.CreateInstance(type, next);
            var qux = (QuxMiddleware)Activator.CreateInstance(type);
            //RequestDelegate next = context =>
            //{
            //    context.Response.StatusCode = 404;
            //    return qux.InvokeAsync(context);
            //};
            return application.Use(request =>
            {
                return context =>
                {
                    context.Response.StatusCode = 404;
                    return qux.InvokeAsync(context);
                };
            });
        }

        public static IApplicationBuilder UseMiddleware<T>(this IApplicationBuilder application) where T : class
        {
            return application.UseMiddleware(typeof(T));
        }

        public static IApplicationBuilder UseQux(this IApplicationBuilder builder)
        {
            //return builder.Use(QuxMiddleware);
            return builder.UseMiddleware<QuxMiddleware>();
        }
    }
}
