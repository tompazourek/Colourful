using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;
using Colourful.Difference;
using NUnit.Framework;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests color difference formulas
    /// </summary>
    [TestFixture]
    public class ColorDifferenceFormulasTest
    {
        private static readonly IComparer<double> DoubleComparerLabPrecision = new DoublePrecisionComparer(4);

        /// <summary>
        /// Tests <see cref="Difference.CIE76ColorDifference"/>
        /// </summary>
        /// <remarks>
        /// Test data generated using:
        /// http://www.brucelindbloom.com/index.html?ColorDifferenceCalc.html
        /// </remarks>
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0, 0)]
        [TestCase(100, 0, 0, 0, 0, 0, 100)]
        [TestCase(100, -50, 50, 20, 10, -20, 122.06555)]
        [TestCase(10.3454, 3.2151, -189.1230, 51.7781, -22.5151, 1.0001, 196.27915)]
        public void CIE76ColorDifference(double l1, double a1, double b1, double l2, double a2, double b2, double expectedDeltaE)
        {
            // arrange
            var x = new LabColor(l1, a1, b1);
            var y = new LabColor(l2, a2, b2);

            // act
            var deltaE = new CIE76ColorDifference().ComputeDifference(x, y);

            // assert
            Assert.That(deltaE, Is.EqualTo(expectedDeltaE).Using(DoubleComparerLabPrecision));
        }
    }
}