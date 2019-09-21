using System;
using System.Reflection;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test1();
            Test2();

            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// 测试静态字段、静态构造函数、非静态字段和非静态构造函数的执行顺序
        /// </summary>
        static void Test1()
        {
            B1 b = new B1();
        }

        /// <summary>
        /// 测试AppDomain作用域
        /// </summary>
        static void Test2()
        {
            string assembly = Assembly.GetEntryAssembly().FullName;
            AppDomain domain = AppDomain.CreateDomain("NewDomain");

            A2.Num = 10;
            string nameofA2 = typeof(A2).FullName;
            A2 a2 = domain.CreateInstanceAndUnwrap(assembly, nameofA2) as A2;
            a2.SetNo(20);
            Console.WriteLine($"Num in class A2 is {A2.Num}");

            B2.Num = 10;
            string nameofB2 = typeof(B2).FullName;
            B2 b2 = domain.CreateInstanceAndUnwrap(assembly, nameofB2) as B2;
            b2.SetNo(20);
            Console.WriteLine($"Num in class B2 is {B2.Num}");
        }
    }
}
