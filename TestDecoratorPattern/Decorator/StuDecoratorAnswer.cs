using System;
using System.Collections.Generic;
using System.Text;

namespace TestDecoratorPattern
{
    public class StuDecoratorAnswer : BaseDecorator
    {
        public StuDecoratorAnswer(AbstractStudent student) : base(student) { }

        public override void Study()
        {
            base.Study();
            Console.Write($"{id} --- {name} --- anwser after study\n");
        }
    }
}
