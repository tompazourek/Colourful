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
using Colourful.Implementation;
using NUnit.Framework;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="XYZColor"/>-<see cref="LuvColor"/> conversions.
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
    /// </remarks>
    [TestFixture]
    public class XYZAndLuvConversionTest
    {
        private static readonly IComparer<double> DoubleComparerLuvPrecision = new DoubleRoundingComparer(4);
        private static readonly IComparer<double> DoubleComparerXYZPrecision = new DoubleRoundingComparer(6);

        /// <summary>
        /// Tests conversion from <see cref="LuvColor"/> to <see cref="XYZColor"/> (<see cref="Illuminants.D65"/>).
        /// </summary>
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0, 100, 50, 0, 0, 0)]
        [TestCase(0.1, 100, 50, 0.000493, 0.000111, 0)]
        [TestCase(70.0000, 86.3525, 2.8240, 0.569310, 0.407494, 0.365843)]
        [TestCase(10.0000, -1.2345, -10.0000, 0.012191, 0.011260, 0.025939)]
        [TestCase(100, 0, 0, 0.950470, 1.000000, 1.088830)]
        [TestCase(1, 1, 1, 0.001255, 0.001107, 0.000137)]
        public void Convert_Luv_to_XYZ(double l, double u, double v, double x, double y, double z)
        {
            // arrange
            var input = new LuvColor(l, u, v, Illuminants.D65);
            var converter = new ColorConverter { WhitePoint = Illuminants.D65, TargetLuvWhitePoint = Illuminants.D65 };

            // act
            XYZColor output = converter.ToXYZ(input);

            // assert
            Assert.That(output.X, Is.EqualTo(x).Using(DoubleComparerXYZPrecision));
            Assert.That(output.Y, Is.EqualTo(y).Using(DoubleComparerXYZPrecision));
            Assert.That(output.Z, Is.EqualTo(z).Using(DoubleComparerXYZPrecision));
        }

        /// <summary>
        /// Tests conversion from <see cref="XYZColor"/> (<see cref="Illuminants.D65"/>) to <see cref="LuvColor"/>.
        /// </summary>
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0.000493, 0.000111, 0, 0.1003, 0.9332, -0.0070)]
        [TestCase(0.569310, 0.407494, 0.365843, 70.0000, 86.3524, 2.8240)]
        [TestCase(0.012191, 0.011260, 0.025939, 9.9998, -1.2343, -9.9999)]
        [TestCase(0.950470, 1.000000, 1.088830, 100, 0, 0)]
        [TestCase(0.001255, 0.001107, 0.000137, 0.9999, 0.9998, 1.0004)]
        public void Convert_XYZ_to_Luv(double x, double y, double z, double l, double u, double v)
        {
            // arrange
            var input = new XYZColor(x, y, z);
            var converter = new ColorConverter { WhitePoint = Illuminants.D65, TargetLuvWhitePoint = Illuminants.D65 };

            // act
            LuvColor output = converter.ToLuv(input);

            // assert
            Assert.That(output.L, Is.EqualTo(l).Using(DoubleComparerLuvPrecision));
            Assert.That(output.u, Is.EqualTo(u).Using(DoubleComparerLuvPrecision));
            Assert.That(output.v, Is.EqualTo(v).Using(DoubleComparerLuvPrecision));
        }
    }
}