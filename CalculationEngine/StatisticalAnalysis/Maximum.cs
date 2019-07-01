using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MathNet.Numerics.Statistics;

namespace CalculationEngine.StatisticalAnalysis
{
    public class Maximum : IStrategy
    {
        public double AnalyseStatisticalSeries(IEnumerable<double> raw)
        {
            try { return raw.Max(); }
            catch { return 0; }
        }
    }
}
