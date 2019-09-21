using System;
namespace TestConsole
{
    /// <summary>
    /// 无法应付多线程环境
    /// </summary>
    public class TestSingleton1
    {
        private TestSingleton1()
        {
        }

        private static TestSingleton1 singleton = null;
        public static TestSingleton1 CreateInstance()
        {
            if (singleton == null)
            {
                singleton = new TestSingleton1();
            }
            return singleton;
        }
    }

    /// <summary>
    /// 解决了多线程问题，但每次获取实例都因为lock的缘故较为耗时
    /// </summary>
    public class TestSingleton2
    {
        private TestSingleton2()
        {
        }

        private static TestSingleton2 singleton = null;
        private static readonly object mutext = new object();
        public static TestSingleton2 CreateInstance()
        {
            lock (mutext)
            {
                if (singleton == null)
                {
                    singleton = new TestSingleton2();
                }
            }
            return singleton;
        }
    }

    /// <summary>
    /// 解决多线程问题，并在已经有singleton实例时直接返回该实例而不进入lock区域，大幅提高了运行效率
    /// 但是代码较为复杂
    /// </summary>
    public class TestSingleton3
    {
        private TestSingleton3()
        {
        }

        private static TestSingleton3 singleton = null;
        private static readonly object mutext = new object();
        public static TestSingleton3 CreateInstance()
        {
            if (singleton == null)
            {
                lock (mutext)
                {
                    if (singleton == null)
                    {
                        singleton = new TestSingleton3();
                    }
                }
            }
            return singleton;
        }
    }

    /// <summary>
    /// 使用静态私有变量只初始化一次变量
    /// 劣势：一旦开始使用类的任何方法，就会初始化对象，但有时并不一定要一开始就使用对象实例。
    /// </summary>
    public class TestSingleton4
    {
        private TestSingleton4() { }

        private static TestSingleton4 singleton = new TestSingleton4();
        public static TestSingleton4 CreateInstance()
        {
            return singleton;
        }
    }

    public class TestSingleton5
    {
        private TestSingleton5() { }

        public static TestSingleton5 CreateInstance()
        {
            return Sub.singleton;
        }

        private class Sub
        {
            static Sub() { }
            internal static readonly TestSingleton5 singleton = new TestSingleton5();
        }
    }
}
