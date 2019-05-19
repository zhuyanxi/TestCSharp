using System;
using System.Collections.Generic;
using System.Text;

namespace DelegateAndEvent
{
    public delegate void GreetingDelegate(string name);

    public class GreetingManager
    {
        public event GreetingDelegate MakeGreet;

        public void GreetPeople(string name)
        {
            MakeGreet(name);
        }
        public void EnglishGreeting(string name)
        {
            Console.WriteLine($"Morning, {name}");
        }
        public void ChineseGreeting(string name)
        {
            Console.WriteLine($"早上好, {name}");
        }
    }
}
