using System;
using System.Collections.Generic;

namespace Colourful.Tests
{
    /// <summary>
    /// Compares two doubles using delta difference.
    /// </summary>
    public class DoubleDeltaComparer : IComparer<double>, IEqualityComparer<double>
    {
        public DoubleDeltaComparer(in double delta)
        {
            Delta = delta;
        }

        /// <summary>
        /// Smallest allowed difference.
        /// </summary>
        public double Delta { get; }

        public int Compare(double x, double y)
        {
            var actualDifference = Math.Abs(x - y);

            var result = actualDifference > Delta
                ? Comparer<double>.Default.Compare(x, y)
                : 0;

            return result;
        }

        public bool Equals(double x, double y) => Compare(x, y) == 0;

        public int GetHashCode(double obj) => throw new NotSupportedException();
    }
}