using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MathNet.Numerics.Statistics;

namespace CalculationEngine.StatisticalAnalysis
{
    public class ThirdQuartile : IStrategy
    {
        public double AnalyseStatisticalSeries(IEnumerable<double> raw)
        {
            try {
                List<double> raw_data = new List<double>(raw);
                raw_data.Sort();
                return raw_data[Convert.ToInt32(Math.Round((raw_data.Count / 4.0)*3))];
            }
            catch { return 0; }
        }
    }
}
