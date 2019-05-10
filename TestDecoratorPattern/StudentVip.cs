using System;
using System.Collections.Generic;
using System.Text;

namespace TestDecoratorPattern
{
    public class StudentVip : AbstractStudent
    {
        public override void Study()
        {
            Console.WriteLine($"{id} --- {name} --- vip study");
        }
    }
}
