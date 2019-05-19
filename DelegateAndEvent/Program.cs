using System;

namespace DelegateAndEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            //GreetingDelegate delegate1, delegate2;
            //delegate1 = EnglishGreeting;
            //delegate2 = ChineseGreeting;
            //GreetPeople("zhuyx", delegate1);
            //GreetPeople("朱沿熹", delegate2);

            //GreetingManager gm = new GreetingManager();
            //gm.MakeGreet += gm.EnglishGreeting;
            //gm.MakeGreet += gm.ChineseGreeting;
            //gm.GreetPeople("zhuyx");

            Heater ht = new Heater();
            Alarm alarm = new Alarm();
            Display display = new Display();
            ht.Boiled += alarm.MakeAlert;
            ht.Boiled += (new Alarm()).MakeAlert;
            ht.Boiled += display.ShowMsg;
            ht.BoilWater();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }


    }
}
