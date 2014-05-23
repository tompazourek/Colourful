using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Implementation
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
        public int Precision { get; private set; }

        public int Compare(double x, double y)
        {
            double xp = Math.Round(x, Precision, MidpointRounding.AwayFromZero);
            double yp = Math.Round(y, Precision, MidpointRounding.AwayFromZero);

            int result = Comparer<double>.Default.Compare(xp, yp);
            return result;
        }
    }
}