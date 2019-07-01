using Altantis_CalculationEngineEP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altantis_CalculationEngineEP.Business
{
    public class CacheDataDeviceSensor : Service.IObserver
    {
        public long Tic_minute { get; set; }
        public long Tic_hour { get; set; }
        public long Tic_day { get; set; }
        public long Tic_week { get; set; }


        public Dictionary<string, DAO.CalculType> Type { get; set; }
        public Dictionary<string, Dictionary<string, double>> ResultCalculType { get; set; }
        /// <summary>
        /// Dictionary<MacAddress, Dictionary<Period,List<Business.RawData>>>
        /// </summary>
        public Dictionary<string, Dictionary<string,List<Business.RawData>>> Cache { get; set; }

        public CacheDataDeviceSensor(DAO.CalculType type, IObservable observable)
        {
            Tic_minute = 0;
            Tic_hour = 0;
            Tic_day = 0;
            Tic_week = 0;

            Type = new Dictionary<string, DAO.CalculType>();
            ResultCalculType = new Dictionary<string, Dictionary<string, double>>();
            Type.Add(type.Period ,type);
            Cache = new Dictionary<string, Dictionary<string, List<Business.RawData>>>();
            observable.RegisterObserver(this);
        }
        
        public void AddRawData(Business.RawData raw)
        {
            foreach(KeyValuePair<string, DAO.CalculType> kv in Type)
            {
                if (!ResultCalculType.ContainsKey(raw.MacAddress)) ResultCalculType.Add(raw.MacAddress, new Dictionary<string, double>());
                if (!ResultCalculType[raw.MacAddress].ContainsKey(kv.Value.Period)) ResultCalculType[raw.MacAddress].Add(kv.Value.Period, 0);
                double temp = ResultCalculType[raw.MacAddress][kv.Value.Period];

                if (!Cache.ContainsKey(raw.MacAddress)) Cache.Add(raw.MacAddress, new Dictionary<string, List<RawData>>());
                if (!Cache[raw.MacAddress].ContainsKey(kv.Value.Period)) Cache[raw.MacAddress].Add(kv.Value.Period, new List<RawData>());
                Cache[raw.MacAddress][kv.Value.Period].Add(raw);

                temp = temp + raw.MetricValue;

                if (Cache[raw.MacAddress][kv.Value.Period].Count - 1 >= kv.Value.PeriodSecondCount)
                {
                    temp = temp - Cache[raw.MacAddress][kv.Value.Period][0].MetricValue;
                    Cache[raw.MacAddress][kv.Value.Period].RemoveAt(0);
                }
                ResultCalculType[raw.MacAddress][kv.Value.Period] = temp;
            }
        }

        public double GetCalculated(string macAddress, string period)
        {
            if (ResultCalculType.ContainsKey(macAddress))
            {
                if (ResultCalculType[macAddress].ContainsKey(period))
                {
                    return ResultCalculType[macAddress][period] / Cache[macAddress][period].Count;
                }
                else return 0;
            }
            else return 0;
        }

        public Dictionary<string, Dictionary<string, double>> testc()
        {
            //Console.WriteLine(Cache.First().Value["week"].Count + " - " + ResultCalculType.First().Value["week"] + " : " + ResultCalculType.First().Value["week"] / Cache.First().Value["week"].Count);
            //Console.WriteLine(Cache.First().Value["day"].Count + " - " + ResultCalculType.First().Value["day"] + " : " + ResultCalculType.First().Value["day"] / Cache.First().Value["day"].Count);
            //Console.WriteLine(Cache.First().Value["hour"].Count + " - " + ResultCalculType.First().Value["hour"] + " : " + ResultCalculType.First().Value["hour"] / Cache.First().Value["hour"].Count);
            //Console.WriteLine(Cache.First().Value["minute"].Count + " - " + ResultCalculType.First().Value["minute"] + " : " + ResultCalculType.First().Value["minute"] / Cache.First().Value["minute"].Count);
            return ResultCalculType;
        }

        public void AddCalculType(DAO.CalculType calcul)
        {
            Type.Add(calcul.Period, calcul);
        }

        public void Notify()
        {
            if (Tic_minute == 59) {
                if (Tic_hour == 23) {
                    if (Tic_day == 6) {
                        Tic_week++;
                        Scheduler.Instance.PersistDAO(Mapper.MapperCalculatedMetric.BusinessToListDAO(this, "week"));
                    }
                    else Tic_day++;
                    Tic_hour = 0;
                    Scheduler.Instance.PersistDAO(Mapper.MapperCalculatedMetric.BusinessToListDAO(this, "day"));
                }
                else Tic_hour++;
                Tic_minute = 0;
                Scheduler.Instance.PersistDAO(Mapper.MapperCalculatedMetric.BusinessToListDAO(this, "hour"));
            }
            else Tic_minute++;
            Scheduler.Instance.PersistDAO(Mapper.MapperCalculatedMetric.BusinessToListDAO(this, "minute"));
        }
    }
}
