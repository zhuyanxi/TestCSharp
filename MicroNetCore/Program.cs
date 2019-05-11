using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MicroNetCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            //HttpListener httpListener = new HttpListener();
            //httpListener.Prefixes.Add("http://localhost:5000/");
            //httpListener.Start();
            //while (true)
            //{
            //    var context = await httpListener.GetContextAsync();
            //    await context.Response.OutputStream.WriteAsync(Encoding.UTF8.GetBytes("hello world"));
            //    context.Response.Close();
            //}


            ////IServer server = new HttpListenerServer(new string[] { "http://localhost:5000" });
            IServer server = new HttpListenerServer();
            //async Task FooBar(HttpContext httpContext)
            //{
            //    await httpContext.Response.WriteAsync("fooBar");
            //}
            var handler = new ApplicationBuilder()
                .Use(FooMiddleware)
                .Use(BarMiddleware)
                .Use(BazMiddleware)
                .Build();
            await server.StartAsync(handler);


            //NameValueCollection myCol = new NameValueCollection();
            //myCol.Add("red", "rojo");//如果键值red相同结果合并 rojo,rouge    
            //myCol.Add("green", "verde");
            //myCol.Add("blue", "azul");
            //myCol.Add("red", "rouge");
            ////myCol["red"] = "dd";

            //foreach (string key in myCol.Keys)
            //{
            //    Console.WriteLine("{0}:{1}", key, myCol[key]);
            //}
            //Console.ReadLine();
        }

        static RequestDelegate FooMiddleware(RequestDelegate next)
        {
            return async context =>
            {
                await context.Response.WriteAsync("foo=>");
            };
        }

        static RequestDelegate BarMiddleware(RequestDelegate next)
        {
            return async context =>
            {
                await context.Response.WriteAsync("bar=>");
            };
        }

        static RequestDelegate BazMiddleware(RequestDelegate next)
        {
            return async context =>
            {
                await context.Response.WriteAsync("baz=>");
            };
        }
    }
}
