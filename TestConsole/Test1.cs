using System;

namespace TestConsole
{
    public class A1
    {
        public A1(string text)
        {
            Console.WriteLine(text);
        }
    }

    public class B1
    {
        public static A1 a1 = new A1("a1");
        public A1 a2 = new A1("a2");

        static B1()
        {
            a1 = new A1("a3");
        }

        public B1()
        {
            a2 = new A1("a4");
        }
    }
}
