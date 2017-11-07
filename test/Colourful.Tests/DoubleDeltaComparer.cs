using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Colourful.Tests
{
    /// <summary>
    /// Compares two doubles using delta difference.
    /// </summary>
    public class DoubleDeltaComparer : IComparer<double>
    {
        /// <param name="delta"><see cref="Delta"/></param>
        public DoubleDeltaComparer(double delta)
        {
            Delta = delta;
        }

        /// <summary>
        /// Smallest allowed difference
        /// </summary>
        public double Delta { get; }

        public int Compare(double x, double y)
        {
            double actualDifference = Math.Abs(x - y);

            int result = actualDifference > Delta
                ? Comparer<double>.Default.Compare(x, y)
                : 0;

            return result;
        }
    }
}