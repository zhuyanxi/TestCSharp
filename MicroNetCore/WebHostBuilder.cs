using System;
using System.Collections.Generic;
using System.Text;

namespace MicroNetCore
{
    public interface IWebHostBuilder
    {
        IWebHostBuilder UseServer(IServer server);
        IWebHostBuilder Configure(Action<IApplicationBuilder> configure);
        IWebHost Build();
    }

    public class WebHostBuilder : IWebHostBuilder
    {
        private readonly List<Action<IApplicationBuilder>> _configure = new List<Action<IApplicationBuilder>>();
        private IServer _server;

        public IWebHost Build()
        {
            var builder = new ApplicationBuilder();
            foreach (var config in _configure)
            {
                config(builder);
            }
            return new WebHost(_server, builder.Build());
        }

        public IWebHostBuilder Configure(Action<IApplicationBuilder> configure)
        {
            _configure.Add(configure);
            return this;
        }

        public IWebHostBuilder UseServer(IServer server)
        {
            _server = server;
            return this;
        }
    }

    public static partial class Extensions
    {
        public static IWebHostBuilder UseHttpListener(this IWebHostBuilder builder, params string[] urls)
        {
            return builder.UseServer(new HttpListenerServer(urls));
        }
    }
}
