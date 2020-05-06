using System;
using System.Collections.Generic;

namespace Colourful.Tests
{
    /// <summary>
    /// Compares two doubles and rounds to specific number of fractional digits.
    /// </summary>
    public class DoubleRoundingComparer : IComparer<double>, IEqualityComparer<double>
    {
        public DoubleRoundingComparer(in int precision)
        {
            Precision = precision;
        }

        /// <summary>
        /// Number of fractional digits
        /// </summary>
        public int Precision { get; }

        public int Compare(double x, double y)
        {
            var xp = Math.Round(x, Precision, MidpointRounding.AwayFromZero);
            var yp = Math.Round(y, Precision, MidpointRounding.AwayFromZero);

            var result = Comparer<double>.Default.Compare(xp, yp);
            return result;
        }

        public bool Equals(double x, double y) => Compare(x, y) == 0;

        public int GetHashCode(double obj) => throw new NotSupportedException();
    }
}