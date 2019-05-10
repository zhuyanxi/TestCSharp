using System;
using System.Collections.Generic;
using System.Text;

namespace TestDecoratorPattern
{
    public class StudentFree : AbstractStudent
    {
        public override void Study() => Console.WriteLine($"{id} --- {name} --- free study");
        //{
        //    Console.WriteLine($"{id} --- {name} --- free study");
        //}
    }
}
