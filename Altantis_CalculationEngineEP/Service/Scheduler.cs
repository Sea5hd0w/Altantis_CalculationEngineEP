using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Threading;

namespace Altantis_CalculationEngineEP.Service
{
    public class Scheduler : IObservable
    {
        private static readonly Lazy<Scheduler> _lazy = new Lazy<Scheduler>(() => new Scheduler());
        public static Scheduler Instance { get { return _lazy.Value; } }

        private List<IObserver> Observers { get; set; }

        public string Status { get; set; }
        private string ConnectionString { get; set; }

        public Dictionary<string, Business.CacheDataDeviceSensor> Cache { get; set; }

        public bool Run { get; set; }
        public Thread Clock { get; set; }

        public DateTime startTime;

        public Scheduler()
        {
            Cache = new Dictionary<string, Business.CacheDataDeviceSensor>();
            Observers = new List<IObserver>();
            Run = false;
            startTime = DateTime.Now;

            LoadConfig();
            if (Status == "") LoadTasks();
            if (Status == "") Start();
        }

        private void LoadConfig()
        {
            Status = "";
            try
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("config.json")
                    .Build();
                ConnectionString = configuration.GetConnectionString("DataBase");
            }
            catch { Status = "🔴 - EndPoint Down - Config file Error"; }
        }

        private void LoadTasks()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    IEnumerable<DAO.CalculType> ECalculTypes = connection.Query<DAO.CalculType>("select * from CalculType");
                    
                    foreach(DAO.CalculType calculTypes in ECalculTypes)
                    {
                        if (!Cache.ContainsKey(calculTypes.SensorType))
                        {
                            Business.CacheDataDeviceSensor temp = new Business.CacheDataDeviceSensor(calculTypes, this);
                            Cache.Add(
                                calculTypes.SensorType,
                                temp
                            );
                        }
                        else Cache[calculTypes.SensorType].AddCalculType(calculTypes);
                    }
                    connection.Close();
                }
            }
            catch
            {
                Status = "🔴 - EndPoint Down - Fail to connect to the DataBase" ;
            }
        }

        private void Start()
        {
            Status = "🔵 - EndPoint Up - Scheduler running";
            Run = true;

            Clock = new Thread(this.RunClock);
            Clock.Start();
        }

        private void RunClock()
        {
            DateTime now;
            TimeSpan wait;

            while (Run)
            {
                now = DateTime.Now;
                wait = (this.TruncateToMinuteStart(now) - now);
                Thread.Sleep(wait);
                NotifyObservers();
            }
        }

        public DateTime TruncateToMinuteStart(DateTime dt)
        {
            DateTime temp = dt.AddMinutes(1);
            return new DateTime(temp.Year, temp.Month, temp.Day, temp.Hour, temp.Minute, 0);
        }

        public void AssignRawDataToCache(string raw)
        {
            Business.RawData temp = Mapper.MapperRawData.DAOToBusiness(new DAO.RawData(raw));
            if(temp != null)
            {
                if (Cache.ContainsKey(temp.Name))
                {
                    Cache[temp.Name].AddRawData(temp);
                }
            }
        }

        public void PersistDAO(List<DAO.CalculatedMetric> daos)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO [Atlantis_CalculatedMetrics].[dbo].[CalculatedMetric] (CreatedAt, Value, CalculTypeId, DeviceId) VALUES(@CreatedAt, @Value, @CalculTypeId, @DeviceId)", daos);
            }
        }

        public void RegisterObserver(IObserver observer)
        {
            this.Observers.Add(observer);
        }

        public void UnRegisterObserver(IObserver observer)
        {
            this.Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver o in Observers) new Thread(o.Notify).Start();
        }
    }
}