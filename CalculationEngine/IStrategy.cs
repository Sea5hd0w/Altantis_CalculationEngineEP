using System;
using System.Collections.Generic;
using System.Text;

namespace CalculationEngine
{
    public interface IStrategy
    {
        double AnalyseStatisticalSeries(IEnumerable<double> raw);
    }
}
