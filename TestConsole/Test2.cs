using System;
namespace TestConsole
{
    [Serializable]
    public class A2 : MarshalByRefObject
    {
        public static int Num;
        public void SetNo(int value)
        {
            Num = value;
        }
    }

    [Serializable]
    public class B2
    {
        public static int Num;
        public void SetNo(int value)
        {
            Num = value;
        }
    }
}
