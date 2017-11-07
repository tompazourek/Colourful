using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Colourful.Tests
{
    /// <summary>
    /// Compares two doubles and rounds to specific number of fractional digits.
    /// </summary>
    public class DoubleRoundingComparer : IComparer<double>
    {
        /// <param name="precision"><see cref="Precision"/></param>
        public DoubleRoundingComparer(int precision)
        {
            Precision = precision;
        }

        /// <summary>
        /// Number of fractional digits
        /// </summary>
        public int Precision { get; }

        public int Compare(double x, double y)
        {
            double xp = Math.Round(x, Precision, MidpointRounding.AwayFromZero);
            double yp = Math.Round(y, Precision, MidpointRounding.AwayFromZero);

            int result = Comparer<double>.Default.Compare(xp, yp);
            return result;
        }
    }
}