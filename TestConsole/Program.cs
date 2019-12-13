using System;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Test1();
            //Test2();

            //PrintThreadStatus("Main Info");

            //checked
            //{
            //    int n = int.MaxValue;
            //    n++;
            //    Console.WriteLine(n);
            //}

            TestCallWSL();

            Console.ReadLine();
        }

        private static void TestCallWSL()
        {
            Console.WriteLine("Enter command to execute on your Ubuntu GNU/Linux");
            var commandToExecute = Console.ReadLine();

            // if command is null use 'ifconfig' for demo purposes
            if (string.IsNullOrWhiteSpace(commandToExecute))
            {
                commandToExecute = "ipconfig";
            }

            // Execute wsl command
            using (var wslCom = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    //FileName = @"C:\Windows\System32\bash.exe",
                    FileName = @"cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            })
            {
                wslCom.StandardInput.WriteLine($"{commandToExecute}&exit");
                wslCom.StandardInput.AutoFlush = true;
                string output = wslCom.StandardOutput.ReadToEnd();

                wslCom.WaitForExit();
                wslCom.Close();

                Console.WriteLine(output);

                //wslCom.Start();
                //Thread.Sleep(500);

                ////wslCom.StandardInput.Flush();
                ////wslCom.StandardInput.Close();

                //wslCom.WaitForExit(5000);

                //Console.WriteLine(wslCom.StandardOutput.ReadToEnd());
            }
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