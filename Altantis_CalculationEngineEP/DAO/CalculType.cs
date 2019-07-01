using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altantis_CalculationEngineEP.DAO
{
    public class CalculType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Period { get; set; }
        public int PeriodSecondCount { get; set; }
        public string SensorType { get; set; }

        public CalculType(int id, string name, string description, string unit, string period, int periodSecondCount, string sensorType)
        {
            Id = id;
            Name = name;
            Description = description;
            Unit = unit;
            Period = period;
            PeriodSecondCount = periodSecondCount;
            SensorType = sensorType;
        }
    }
}
