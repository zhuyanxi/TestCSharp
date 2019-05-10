using System;
using System.Collections.Generic;
using System.Text;

namespace TestDecoratorPattern
{
    public class StuDecoratorPrepare : BaseDecorator
    {
        public StuDecoratorPrepare(AbstractStudent student) : base(student) { }

        public override void Study()
        {
            Console.Write($"{id} --- {name} --- prepare before study\n");
            base.Study();
        }
    }
}
