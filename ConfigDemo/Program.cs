using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConfigDemo
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //var source = new Dictionary<string, string>
            //{
            //    ["longDatePattern"] = "dddd, MMMM d, yyyy",
            //    ["longTimePattern"] = "h:mm:ss tt",
            //    ["shortDatePattern"] = "M/d/yyyy",
            //    ["shortTimePattern"] = "h:mm tt"
            //};
            //IConfigurationRoot config = new ConfigurationBuilder()
            //    .Add(new MemoryConfigurationSource { InitialData = source })
            //    .Build();
            //var options = new DateTimeFormatOptions(config);

            var source = new Dictionary<string, string>
            {
                ["format:dateTime:longDatePattern"] = "dddd, MMMM d, yyyy",
                ["format:dateTime:longTimePattern"] = "h:mm:ss tt",
                ["format:dateTime:shortDatePattern"] = "M/d/yyyy",
                ["format:dateTime:shortTimePattern"] = "h:mm tt",

                ["format:currencyDecimal:digits"] = "2",
                ["format:currencyDecimal:symbol"] = "$",
            };

            //var configuration = new ConfigurationBuilder()
            //    .Add(new MemoryConfigurationSource { InitialData = source })
            //    .Build();
            //var options = new FormatOptions(configuration.GetSection("Format"));

            var currentPath = Directory.GetCurrentDirectory();//此方法获取的是dotnet命令运行的根目录,所以调试时会得到~\bin\Debug\netcoreapp2.2

#if DEBUG
            // 调试用代码
            Console.WriteLine("CurrentPath1:" + currentPath);
            var pathArr = currentPath.Split('\\').ToList();
            int count = pathArr.Count;
            pathArr.RemoveRange(count - 3, 3);
            currentPath = string.Join('\\', pathArr);
            Console.WriteLine("CurrentPath2:" + currentPath);
#endif

            var options = new ConfigurationBuilder()
                //.Add(new MemoryConfigurationSource { InitialData = source })//当添加了“appsettings.json”配置文件后就应当从配置文件读取数据源
                .SetBasePath(currentPath)
                .AddJsonFile("appsettings.json")//AddJsonFile位于库Microsoft.Extensions.Configuration.Json中，SetBasePath位于库Microsoft.Extensions.Configuration.FileExtensions（Json库依赖此库，不用单独安装）
                .Build()
                .GetSection("format")
                .Get<FormatOptions>();//要使用Get<T>扩展方法需要Nuget添加库Microsoft.Extensions.Configuration.Binder

            var dateTime = options.DateTime;
            var currencyDecimal = options.CurrencyDecimal;

            Console.WriteLine("**************************************************");
            Console.WriteLine("DateTime:");
            Console.WriteLine($"\tLongDatePattern: {dateTime.LongDatePattern}");
            Console.WriteLine($"\tLongTimePattern: {dateTime.LongTimePattern}");
            Console.WriteLine($"\tShortDatePattern: {dateTime.ShortDatePattern}");
            Console.WriteLine($"\tShortTimePattern: {dateTime.ShortTimePattern}");

            Console.WriteLine("CurrencyDecimal:");
            Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
            Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}");

            Console.WriteLine("Hello World!");
        }
    }
}