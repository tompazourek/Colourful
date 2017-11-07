#region License

// Copyright (C) Tomáš Pažourek, 2014
// All rights reserved.
// 
// Distributed under MIT license as a part of project Colourful.
// https://github.com/tompazourek/Colourful

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Colourful.Conversion;
using NUnit.Framework;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="XYZColor"/>-<see cref="xyYColor"/> conversions.
    /// </summary>
    [TestFixture]
    public class XYZAndxyYConversionTest
    {
        private static readonly IComparer<double> DoubleComparer = new DoubleRoundingComparer(4);

        /// <summary>
        /// Data from: http://www.brucelindbloom.com/index.html?ColorCalculator.html
        /// </summary>
        private static readonly object[] TestData =
            {
                // X, Y, Z, x, y, Y
                new object[] { 0, 0, 0, 0, 0, 0 },
                new object[] { 0.436075, 0.222504, 0.013932, 0.648427, 0.330856, 0.222504 },
                new object[] { 0.964220, 1.000000, 0.825210, 0.345669, 0.358496, 1.000000 },
                new object[] { 0.434119, 0.356820, 0.369447, 0.374116, 0.307501, 0.356820 },
            };

        [Test]
        [TestCaseSource(nameof(TestData))]
        [TestCase(0, 0, 0, 0.538842, 0.000000, 0.000000)]
        public void Convert_xyY_to_XYZ(double xyzX, double xyzY, double xyzZ, double x, double y, double Y)
        {
            // arrange
            var input = new xyYColor(x, y, Y);
            var converter = new ColourfulConverter();

            // act
            XYZColor output = converter.ToXYZ(input);

            // assert
            Assert.That(output.X, Is.EqualTo(xyzX).Using(DoubleComparer));
            Assert.That(output.Y, Is.EqualTo(xyzY).Using(DoubleComparer));
            Assert.That(output.Z, Is.EqualTo(xyzZ).Using(DoubleComparer));
        }

        [Test]
        [TestCaseSource(nameof(TestData))]
        [TestCase(0.231809, 0, 0.077528, 0.749374, 0.000000, 0.000000)]
        public void Convert_XYZ_to_xyY(double xyzX, double xyzY, double xyzZ, double x, double y, double Y)
        {
            // arrange
            var input = new XYZColor(xyzX, xyzY, xyzZ);
            var converter = new ColourfulConverter();

            // act
            xyYColor output = converter.ToxyY(input);

            // assert
            Assert.That(output.x, Is.EqualTo(x).Using(DoubleComparer));
            Assert.That(output.y, Is.EqualTo(y).Using(DoubleComparer));
            Assert.That(output.Luminance, Is.EqualTo(Y).Using(DoubleComparer));
        }

        
#if (DYNAMIC)

        [Test]
        [TestCaseSource(nameof(TestData))]
        [TestCase(0, 0, 0, 0.538842, 0.000000, 0.000000)]
        public void Convert_xyY_as_vector_to_XYZ(double xyzX, double xyzY, double xyzZ, double x, double y, double Y)
        {
            // arrange
            IColorVector input = new xyYColor(x, y, Y);

            var converter = new ColourfulConverter();

            // act
            XYZColor output = converter.ToXYZ(input);

            // assert
            Assert.That(output.X, Is.EqualTo(xyzX).Using(DoubleComparer));
            Assert.That(output.Y, Is.EqualTo(xyzY).Using(DoubleComparer));
            Assert.That(output.Z, Is.EqualTo(xyzZ).Using(DoubleComparer));
        }

        [Test]
        [TestCase(0.538842, 0.000000, 0.000000)]
        public void Convert_XYZ_as_vector_to_XYZ(double x, double y, double z)
        {
            // arrange
            IColorVector input = new XYZColor(x, y, z);

            var converter = new ColourfulConverter();

            // act
            XYZColor output = converter.ToXYZ(input);

            // assert
            Assert.That(output.X, Is.EqualTo(x).Using(DoubleComparer));
            Assert.That(output.Y, Is.EqualTo(y).Using(DoubleComparer));
            Assert.That(output.Z, Is.EqualTo(z).Using(DoubleComparer));
        }

#endif
    }
}