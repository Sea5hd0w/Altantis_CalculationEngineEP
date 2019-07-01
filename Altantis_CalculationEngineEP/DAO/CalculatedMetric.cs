using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altantis_CalculationEngineEP.DAO
{
    public class CalculatedMetric
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Value { get; set; }
        public int CalculTypeId { get; set; }
        public string DeviceId { get; set; }

        public CalculatedMetric(int id, DateTime createdAt, double value, int calculTypeId, string deviceId)
        {
            Id = id;
            CreatedAt = createdAt;
            Value = value;
            CalculTypeId = calculTypeId;
            DeviceId = deviceId;
        }

        public CalculatedMetric(DateTime createdAt, double value, int calculTypeId, string deviceId)
        {
            CreatedAt = createdAt;
            Value = value;
            CalculTypeId = calculTypeId;
            DeviceId = deviceId;
        }
    }
}
