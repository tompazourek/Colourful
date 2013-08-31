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
    /// Tests RGB-XYZ conversion
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
    /// </remarks>
    [TestFixture]
    public class RGBToXYZConversionTest
    {
        private static readonly IComparer<double> DoubleComparer = new DoubleRoundingComparer(6);

        /// <summary>
        /// Tests conversion from RGB (default sRGB workspace) to XYZ (D65)
        /// </summary>
        [Test]
        [TestCase(1, 1, 1, 0.950470, 1.000000, 1.088830)]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(1, 0, 0, 0.412456, 0.212673, 0.019334)]
        [TestCase(0, 1, 0, 0.357576, 0.715152, 0.119192)]
        [TestCase(0, 0, 1, 0.180437, 0.072175, 0.950304)]
        [TestCase(0.754902, 0.501961, 0.100000, 0.297676, 0.267854, 0.045504)]
        public void Convert_sRGB_to_XYZ(double r, double g, double b, double x, double y, double z)
        {
            // arrange
            var input = new RGBColor(r, g, b);

            // act
            XYZColor output = input.ToXYZ();

            // assert
            Assert.That(output.X, Is.EqualTo(x).Using(DoubleComparer));
            Assert.That(output.Y, Is.EqualTo(y).Using(DoubleComparer));
            Assert.That(output.Z, Is.EqualTo(z).Using(DoubleComparer));
        }
    }
}