using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;
using NUnit.Framework;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests XYZ-LAB conversion
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
    /// </remarks>
    [TestFixture]
    public class XYZToLabConversionTest
    {
        private static readonly IComparer<double> DoubleComparer = new DoubleRoundingComparer(4);

        /// <summary>
        /// Tests conversion from XYZ (D65) to L*a*b*
        /// </summary>
        [Test]
        [TestCase(0.95047, 1, 1.08883, 100, 0, 0)]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0.95047, 0, 0, 0, 431.0345, 0)]
        [TestCase(0, 1, 0, 100, -431.0345, 172.4138)]
        [TestCase(0, 0, 1.08883, 0, 0, -172.4138)]
        [TestCase(0.216938, 0.150041, 0.048850, 45.6398, 39.8753, 35.2091)]
        public void Convert_XYZ_to_LAB(double x, double y, double z, double l, double a, double b)
        {
            // arrange
            var input = new XYZColor(x, y, z, Illuminants.D65);

            // act
            LabColor output = input.ToLab();

            // assert
            Assert.That(output.L, Is.EqualTo(l).Using(DoubleComparer));
            Assert.That(output.a, Is.EqualTo(a).Using(DoubleComparer));
            Assert.That(output.b, Is.EqualTo(b).Using(DoubleComparer));
        }
    }
}