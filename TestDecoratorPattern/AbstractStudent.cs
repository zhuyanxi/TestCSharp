using System;
using System.Collections.Generic;
using System.Text;

namespace TestDecoratorPattern
{
    public abstract class AbstractStudent
    {
        public string id { get; set; }
        public string name { get; set; }
        public abstract void Study();
    }
}
