using System;
using System.Collections.Generic;
using System.Text;

namespace TestDecoratorPattern
{
    public class StuDecoratorPreview : BaseDecorator
    {
        public StuDecoratorPreview(AbstractStudent student) : base(student) { }

        public override void Study()
        {
            Console.Write($"{id} --- {name} --- preview before study\n");
            base.Study();
        }
    }
}
