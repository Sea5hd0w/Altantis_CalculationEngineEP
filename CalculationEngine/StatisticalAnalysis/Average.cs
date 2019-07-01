using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculationEngine.StatisticalAnalysis
{
    public class Average : IStrategy
    {
        public double AnalyseStatisticalSeries(IEnumerable<double> raw)
        {
            try { return raw.Average(); }
            catch { return 0; }
        }
    }
}
