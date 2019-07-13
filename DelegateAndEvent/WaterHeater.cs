using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 采用Observer设计模式定义一个电热水器
/// 包含热水器、警报器、显示器
/// </summary>


namespace DelegateAndEvent
{
    //被监视对象：热水器Heater
    public class Heater
    {
        private int temperature;
        public string type = "Realfire 001";
        public string area = "China Xian";

        public delegate void BoiledEventHandler(object sender, BoiledEventArgs e);
        public event BoiledEventHandler Boiled;

        //定义BoiledEventArgs类，传递给Observer所感兴趣的信息
        public class BoiledEventArgs : EventArgs
        {
            public readonly int _temperature;
            public BoiledEventArgs(int temperature)
            {
                _temperature = temperature;
            }
        }

        //可以供继承自Heater的类重写，以便继承类拒绝其它对象对它的监视
        protected virtual void OnBoiled(BoiledEventArgs e)
        {
            Boiled?.Invoke(this, e);
        }

        public void BoilWater()
        {
            for (int i = 0; i <= 100; i++)
            {
                temperature = i;
                if (temperature > 95)
                {
                    //建立BoiledEventArgs对象
                    BoiledEventArgs e = new BoiledEventArgs(temperature);
                    OnBoiled(e);
                }
            }
        }
    }

    //监视着1：警报器Alarm
    public class Alarm
    {
        public void MakeAlert(object sender, Heater.BoiledEventArgs e)
        {
            Heater h = (Heater)sender;
            Console.WriteLine($"Display: {h.area} - {h.type}");
            Console.WriteLine($"Alarm: Di Di Di, water already {e._temperature} degrees");
            Console.WriteLine();
        }
    }

    //监视着2：显示器Display
    public class Display
    {
        public void ShowMsg(object sender, Heater.BoiledEventArgs e)
        {
            Heater h = (Heater)sender;
            Console.WriteLine($"Display: {h.area} - {h.type}");
            Console.WriteLine($"Display: The water is boiling up very quickly, current temperature: {e._temperature} degrees");
            Console.WriteLine();
        }
    }
}
