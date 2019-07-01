using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using CalculationEngine;

namespace Test
{
    class Program
    {
        public long Tic_second { get; set; }
        public long Tic_minute { get; set; }
        public long Tic_hour { get; set; }
        public long Tic_day { get; set; }
        public long Tic_week { get; set; }

        static void Main(string[] args)
        {
            Program temp = new Program();

            DateTime now;
            TimeSpan wait;

            while (true)
            {
                now = DateTime.Now;
                wait = (temp.TruncateToMinuteStart(now) - now);
                Thread.Sleep(wait);

                //Console.WriteLine(temp.Tic_minute + " " + temp.Tic_hour + " " + temp.Tic_day + " " + temp.Tic_week + " - " + wait);

                new Thread(temp.Notify2).Start();
            }
        }

        public DateTime TruncateToSecondStart(DateTime dt)
        {
            DateTime temp = dt.AddSeconds(1);
            return new DateTime(temp.Year, temp.Month, temp.Day, temp.Hour, temp.Minute, temp.Second);
        }

        public DateTime TruncateToMinuteStart(DateTime dt)
        {
            DateTime temp = dt.AddMinutes(1);
            return new DateTime(temp.Year, temp.Month, temp.Day, temp.Hour, temp.Minute,0);
        }


        public void Notify()
        {
            if (Tic_second == 59)
            {
                if (Tic_minute == 59)
                {
                    if (Tic_hour == 23)
                    {
                        if (Tic_day == 6)
                        {
                            Tic_week++;
                        }
                        else Tic_day++;
                        Tic_hour = 0;
                    }
                    else Tic_hour++;
                    Tic_minute = 0;
                }
                else Tic_minute++;
                Tic_second = 0;
            }
            else Tic_second++;

            Console.WriteLine(Tic_second + " " + Tic_minute + " " + Tic_hour + " " + Tic_day + " " + Tic_week + " - " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond);
        }

        public void Notify2()
        {
                if (Tic_minute == 59)
                {
                    if (Tic_hour == 23)
                    {
                        if (Tic_day == 6)
                        {
                            Tic_week++;
                        }
                        else Tic_day++;
                        Tic_hour = 0;
                    }
                    else Tic_hour++;
                    Tic_minute = 0;
                }
                else Tic_minute++;

            Console.WriteLine(Tic_second + " " + Tic_minute + " " + Tic_hour + " " + Tic_day + " " + Tic_week + " - " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond);
        }

    }
}
