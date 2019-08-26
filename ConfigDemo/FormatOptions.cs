using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDemo
{
    /// <summary>
    /// 针对不同数据类型的格式设置
    /// </summary>
    public class FormatOptions
    {
        //public FormatOptions(IConfiguration configuration)
        //{
        //    DateTime = new DateTimeFormatOptions(configuration.GetSection("DateTime"));
        //    CurrencyDecimal = new CurrencyDecimalFormatOptions(configuration.GetSection("CurrencyDecimal"));
        //}

        public DateTimeFormatOptions DateTime { get; set; }
        public CurrencyDecimalFormatOptions CurrencyDecimal { get; set; }
    }
}