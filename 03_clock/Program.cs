/*
3.
Визначити клас Годинник(години, хвилини), що дозволяє встановити(скасувати будильник). 
Метод Tick() виконує перехід на наступну хвилину та ініціює подію "Задзвонив будильник".
Завдання виконати з використання стандартного делегату EventHandler<MyArgs>. 
Визначити свій клас(похідний від EventArgs) для збереження параметрів події(повідомлення чи(і) ін.).
 */

using System;

namespace _03_clock
{
    class MyArgs : EventArgs
    {
        string message;

        public MyArgs(string message)
        {
            this.message = message;
        }

        public string Message
        {
            get { return message; }
        }
    }

    class Clock
    {
        public event EventHandler<MyArgs> alarmclock;
        uint hour = 0;
        uint minutes = 0;

        public Clock(uint hour = 0, uint minutes = 0)
        {
            this.Hour = hour;
            this.Minutes = minutes;
        }

        public uint Hour
        {
            get { return hour; }
            set { hour = value < 24 ? value : 0; }
        }

        public uint Minutes
        {
            get { return minutes; }
            set { minutes = value < 60 ? value : 0; }
        }

        public void Tick(uint h, uint m) //параметрами передається час спрацювання будильника
        {
            ++minutes;

            if (minutes == 60)
            {
                minutes = 0;
                ++hour;
                if (hour == 24)
                    hour = 0;
            }

            if(hour == h && minutes == m)
                OnAlarm(new MyArgs("Dr-r-r-r-r-r!\n"));
        }

        protected virtual void OnAlarm(MyArgs eargs)
        {
            if (alarmclock != null)
                alarmclock(this, eargs);
        }
    }

    class MyEventArgs
    {
        static void Main(string[] args)
        {

            Clock clock = new Clock(6, 30);
            clock.alarmclock += OnCatchAlarm;

            do
            {
                clock.Tick(7, 0);
                Console.WriteLine("{0} : {1, 2}", clock.Hour, clock.Minutes);
                System.Threading.Thread.Sleep(100);
            } while (!Console.KeyAvailable); 
        }

        static void OnCatchAlarm(object sender, MyArgs eargs)
        {
            Console.WriteLine("Alarm! " + eargs.Message);
        }
    }
}
