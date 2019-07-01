using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MathNet.Numerics.Statistics;

namespace CalculationEngine.StatisticalAnalysis
{
    public class Minimum : IStrategy
    {
        public double AnalyseStatisticalSeries(IEnumerable<double> raw)
        {
            try { return raw.Min(); }
            catch { return 0; }
        }
    }
}
