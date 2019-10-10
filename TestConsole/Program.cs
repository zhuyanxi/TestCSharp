using System;
using System.Reflection;
using System.Threading;

namespace TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Test1();
            //Test2();

            PrintThreadStatus("Main Info");

            //Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        private static void PrintThreadStatus(string name)
        {
            Console.WriteLine($"========={name}==========");

            var currentID = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Current ThreadID:{currentID}");

            var backThreadExist = Thread.CurrentThread.IsBackground;
            Console.WriteLine($"Is Back Thread existed:{backThreadExist}");

            int workerThread;
            int ioThread;
            ThreadPool.GetMaxThreads(out workerThread, out ioThread);
            Console.WriteLine($"Max worker thread number:{workerThread}");
            Console.WriteLine($"Max IO thread number:{ioThread}");

            int workerThreadAvailable;
            int ioThreadAvailable;
            ThreadPool.GetAvailableThreads(out workerThreadAvailable, out ioThreadAvailable);
            Console.WriteLine($"Available worker thread number:{workerThreadAvailable}");
            Console.WriteLine($"Available IO thread number:{ioThreadAvailable}");

            Console.WriteLine($"========={name}==========");
        }

        /// <summary>
        /// 测试静态字段、静态构造函数、非静态字段和非静态构造函数的执行顺序
        /// </summary>
        private static void Test1()
        {
            B1 b = new B1();
        }

        /// <summary>
        /// 测试AppDomain作用域
        /// </summary>
        private static void Test2()
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