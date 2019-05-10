using System;
using System.Collections.Generic;
using System.Text;

namespace TestDecoratorPattern
{
    public class StuDecoratorHomework : BaseDecorator
    {
        public StuDecoratorHomework(AbstractStudent student) : base(student) { }

        public override void Study()
        {
            base.Study();
            Console.Write($"{id} --- {name} --- homework after study\n");
        }
    }
}
