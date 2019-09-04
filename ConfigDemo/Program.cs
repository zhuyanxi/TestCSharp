using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Primitives;
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
            VersionOne();
            VersionTwo();
        }

        private static void VersionOne()
        {
            var currentPath = CurrentPath();

            var options = new ConfigurationBuilder()
                //.Add(new MemoryConfigurationSource { InitialData = source })//当添加了“appsettings.json”配置文件后就应当从配置文件读取数据源
                .SetBasePath(currentPath)
                .AddJsonFile("appsettings.json")//AddJsonFile位于库Microsoft.Extensions.Configuration.Json中，SetBasePath位于库Microsoft.Extensions.Configuration.FileExtensions（Json库依赖此库，不用单独安装）
                .Build()
                .GetSection("format")
                .Get<FormatOptions>();//要使用Get<T>扩展方法需要Nuget添加库Microsoft.Extensions.Configuration.Binder

            var dateTime = options.DateTime;
            var currencyDecimal = options.CurrencyDecimal;

            Console.WriteLine("*****************Start Version One*****************");
            Console.WriteLine("DateTime:");
            Console.WriteLine($"\tLongDatePattern: {dateTime.LongDatePattern}");
            Console.WriteLine($"\tLongTimePattern: {dateTime.LongTimePattern}");
            Console.WriteLine($"\tShortDatePattern: {dateTime.ShortDatePattern}");
            Console.WriteLine($"\tShortTimePattern: {dateTime.ShortTimePattern}");

            Console.WriteLine("CurrencyDecimal:");
            Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
            Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}");

            Console.WriteLine("*****************End Version One*****************");
            Console.WriteLine("");
        }

        private static void VersionTwo()
        {
            var currentPath = CurrentPath();

            var config = new ConfigurationBuilder()
                .SetBasePath(currentPath)
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ChangeToken.OnChange(() => config.GetReloadToken(), () =>
            {
                var options = config.GetSection("format").Get<FormatOptions>();
                var dateTime = options.DateTime;
                var currencyDecimal = options.CurrencyDecimal;

                Console.WriteLine("*****************Start Version Two*****************");
                Console.WriteLine("DateTime:");
                Console.WriteLine($"\tLongDatePattern: {dateTime.LongDatePattern}");
                Console.WriteLine($"\tLongTimePattern: {dateTime.LongTimePattern}");
                Console.WriteLine($"\tShortDatePattern: {dateTime.ShortDatePattern}");
                Console.WriteLine($"\tShortTimePattern: {dateTime.ShortTimePattern}");

                Console.WriteLine("CurrencyDecimal:");
                Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
                Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}\n\n");
                Console.WriteLine("*****************End Version Two*****************");
                Console.WriteLine("");
            });
            Console.ReadKey();
        }

        private static string CurrentPath()
        {
            var currentPath = Directory.GetCurrentDirectory();//此方法获取的是dotnet命令运行的根目录,所以调试时会得到~\bin\Debug\netcoreapp2.2

#if DEBUG
            // 调试用代码
            Console.WriteLine("CurrentPath1:" + currentPath);
            List<string> pathArr = new List<string>();//996622

            var osInfo = Environment.OSVersion;
            if (osInfo.VersionString.Contains("Windows"))
            {
                pathArr = currentPath.Split('\\').ToList();
                int count = pathArr.Count;
                pathArr.RemoveRange(count - 3, 3);
                currentPath = string.Join('\\', pathArr);
            }
            else
            {
                pathArr = currentPath.Split('/').ToList();
                int count = pathArr.Count;
                pathArr.RemoveRange(count - 3, 3);
                currentPath = string.Join('/', pathArr);
            }

            Console.WriteLine("CurrentPath2:" + currentPath);
#endif
            return currentPath;
        }
    }
}