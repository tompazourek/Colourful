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
    /// Tests XYZ-LAB conversions
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
    /// </remarks>
    [TestFixture]
    public class XYZAndLabConversionTest
    {
        private static readonly IComparer<double> DoubleComparerLabPrecision = new DoubleRoundingComparer(4);
        private static readonly IComparer<double> DoubleComparerXYZPrecision = new DoubleRoundingComparer(6);

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
            Assert.That(output.L, Is.EqualTo(l).Using(DoubleComparerLabPrecision));
            Assert.That(output.a, Is.EqualTo(a).Using(DoubleComparerLabPrecision));
            Assert.That(output.b, Is.EqualTo(b).Using(DoubleComparerLabPrecision));
        }

        /// <summary>
        /// Tests conversion from L*a*b* to XYZ (D65)
        /// </summary>
        [Test]
        [TestCase(100, 0, 0, 0.95047, 1, 1.08883)]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0, 431.0345, 0, 0.95047, 0, 0)]
        [TestCase(100, -431.0345, 172.4138, 0, 1, 0)]
        [TestCase(0, 0, -172.4138, 0, 0, 1.08883)]
        [TestCase(45.6398, 39.8753, 35.2091, 0.216938, 0.150041, 0.048850)]
        [TestCase(77.1234, -40.1235, 78.1120, 0.358530, 0.517372, 0.076273)]
        [TestCase(10, -400, 20, 0, 0.011260, 0)]
        public void Convert_LAB_to_XYZ(double l, double a, double b, double x, double y, double z)
        {
            // arrange
            var input = new LabColor(l, a, b);

            // act
            XYZColor output = input.ToXYZ(Illuminants.D65);

            // assert
            Assert.That(output.X, Is.EqualTo(x).Using(DoubleComparerXYZPrecision));
            Assert.That(output.Y, Is.EqualTo(y).Using(DoubleComparerXYZPrecision));
            Assert.That(output.Z, Is.EqualTo(z).Using(DoubleComparerXYZPrecision));
        }
    }
}