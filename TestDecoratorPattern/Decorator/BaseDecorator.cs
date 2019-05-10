using System;
using System.Collections.Generic;
using System.Text;

namespace TestDecoratorPattern
{
    public class BaseDecorator : AbstractStudent
    {
        private readonly AbstractStudent _student;

        public BaseDecorator(AbstractStudent student)
        {
            _student = student;
            id = student.id;
            name = student.name;
        }

        public override void Study() => _student.Study();
        //{
        //    _student.Study();
        //    //Console.Write($"*******{id}---{name}--- Decorate class ***********\n");
        //}
    }
}
