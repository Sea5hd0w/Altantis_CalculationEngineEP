using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Altantis_CalculationEngineEP.Mapper
{
    public static class MapperRawData
    {
        public static Business.RawData DAOToBusiness(DAO.RawData dao)
        {
            try
            {
                var jo = JObject.Parse(dao.Data);
                DateTime dt = DateTime.ParseExact(jo["MetricDate"].ToString(), "dd/MM/yyyy HH:mm:ss", null);
                Double d = Double.Parse(jo["MetricValue"].ToString(), CultureInfo.CreateSpecificCulture("en-EN"));

                return new Business.RawData(jo["MacAddress"].ToString(), dt, jo["SensorType"].ToString(), jo["Name"].ToString(), d);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}