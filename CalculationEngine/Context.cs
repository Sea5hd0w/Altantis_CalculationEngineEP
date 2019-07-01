using System;
using System.Collections.Generic;
using System.Text;

namespace CalculationEngine
{
    public class Context
    {
        public IStrategy strategie { get; set; }

        public double StatisticalAnalysis(IEnumerable<double> raw)
        {
            return strategie.AnalyseStatisticalSeries(raw);
        }
    }
}