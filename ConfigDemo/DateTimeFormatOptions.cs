using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDemo
{
    /// <summary>
    /// 假设我们的应用程序需要通过配置来设定日期/时间的显示格式
    /// 该类的四个属性体现针对DateTime对象的四种显示格式
    /// </summary>
    public class DateTimeFormatOptions
    {
        //public DateTimeFormatOptions(IConfiguration configuration)
        //{
        //    LongDatePattern = configuration["LongDatePattern"];
        //    LongTimePattern = configuration["LongTimePattern"];
        //    ShortDatePattern = configuration["ShortDatePattern"];
        //    ShortTimePattern = configuration["ShortTimePattern"];
        //}

        public string LongDatePattern { get; set; }
        public string LongTimePattern { get; set; }
        public string ShortDatePattern { get; set; }
        public string ShortTimePattern { get; set; }
    }
}