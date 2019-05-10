using System;

namespace TestDecoratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractStudent free = new StudentFree
            {
                id = "123",
                name = "zhuyx"
            };
            //free = new StuDecoratorVideo(free);
            //free = new StuDecoratorHomework(free);
            //free = new StuDecoratorAnswer(free);

            //free = new StuDecoratorPreview(free);
            //free = new StuDecoratorPrepare(free);

            free.Study();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
