using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.ChromaticAdaptation;
using Colourful.Colors;
using Colourful.Conversion;
using NUnit.Framework;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="XYZColor"/>-<see cref="LabColor"/> conversions.
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
        /// Tests conversion from <see cref="XYZColor"/> (<see cref="Illuminants.D65"/>) to <see cref="LabColor"/>.
        /// </summary>
        [Test]
        [TestCase(0.95047, 1, 1.08883, 100, 0, 0)]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0.95047, 0, 0, 0, 431.0345, 0)]
        [TestCase(0, 1, 0, 100, -431.0345, 172.4138)]
        [TestCase(0, 0, 1.08883, 0, 0, -172.4138)]
        [TestCase(0.216938, 0.150041, 0.048850, 45.6398, 39.8753, 35.2091)]
        public void Convert_XYZ_to_Lab(double x, double y, double z, double l, double a, double b)
        {
            // arrange
            var input = new XYZColor(x, y, z);
            var converter = new XYZToLabConverter(Illuminants.D65);

            // act
            XYZColor intermediate = input;
            LabColor output = converter.Convert(intermediate);

            // assert
            Assert.That(output.L, Is.EqualTo(l).Using(DoubleComparerLabPrecision));
            Assert.That(output.a, Is.EqualTo(a).Using(DoubleComparerLabPrecision));
            Assert.That(output.b, Is.EqualTo(b).Using(DoubleComparerLabPrecision));
        }

        /// <summary>
        /// Tests conversion from <see cref="LabColor"/> to <see cref="XYZColor"/> (<see cref="Illuminants.D65"/>).
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
        public void Convert_Lab_to_XYZ(double l, double a, double b, double x, double y, double z)
        {
            // arrange
            var input = new LabColor(l, a, b, Illuminants.D65);
            var converter = new LabToXYZConverter();

            // act
            XYZColor output = converter.Convert(input);

            // assert
            Assert.That(output.X, Is.EqualTo(x).Using(DoubleComparerXYZPrecision));
            Assert.That(output.Y, Is.EqualTo(y).Using(DoubleComparerXYZPrecision));
            Assert.That(output.Z, Is.EqualTo(z).Using(DoubleComparerXYZPrecision));
        }
    }
}