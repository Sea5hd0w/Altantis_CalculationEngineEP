using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altantis_CalculationEngineEP.Mapper
{
    public static class MapperCalculatedMetric
    {
        public static List<DAO.CalculatedMetric> BusinessToListDAO(Business.CacheDataDeviceSensor business, string period)
        {
            List<DAO.CalculatedMetric> temp = new List<DAO.CalculatedMetric>();

            foreach (KeyValuePair<string, Dictionary<string, double>> kv in business.ResultCalculType)
            {
                foreach(KeyValuePair<string, double> kv2 in kv.Value)
                {
                    if(kv2.Key == period)
                    {
                        DateTime d = DateTime.Now;
                        int c = business.Type[kv2.Key].Id;
                        temp.Add(new DAO.CalculatedMetric(d, kv2.Value, c, kv.Key));
                    }
                }
            }
            return temp;
        }
    }
}
