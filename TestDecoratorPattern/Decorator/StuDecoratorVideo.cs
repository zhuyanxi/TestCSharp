using System;
using System.Collections.Generic;
using System.Text;

namespace TestDecoratorPattern
{
    public class StuDecoratorVideo : BaseDecorator
    {
        public StuDecoratorVideo(AbstractStudent student) : base(student) { }

        public override void Study()
        {
            base.Study();
            Console.Write($"{id} --- {name} --- video after study\n");
        }
    }
}
