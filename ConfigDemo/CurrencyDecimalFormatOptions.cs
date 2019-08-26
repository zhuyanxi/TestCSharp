using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigDemo
{
    /// <summary>
    /// 定义货币格式
    /// </summary>
    public class CurrencyDecimalFormatOptions
    {
        //public CurrencyDecimalFormatOptions(IConfiguration configuration)
        //{
        //    Digits = int.Parse(configuration["Digits"]);
        //    Symbol = configuration["Symbol"];
        //}

        public int Digits { get; set; }
        public string Symbol { get; set; }
    }
}