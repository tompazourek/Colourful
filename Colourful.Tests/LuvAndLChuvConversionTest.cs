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
    /// Tests <see cref="LuvColor"/>-<see cref="LChuvColor"/> conversions.
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
    /// </remarks>
    [TestFixture]
    public class LuvAndLChuvConversionTest
    {
        private static readonly IComparer<double> DoubleComparer = new DoubleRoundingComparer(4);

        private static readonly ColorConverter Converter = new ColorConverter();

        /// <summary>
        /// Tests conversion from <see cref="LChuvColor"/> to <see cref="LuvColor"/>.
        /// </summary>
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(54.2917, 106.8391, 40.8526, 54.2917, 80.8125, 69.8851)]
        [TestCase(100, 0, 0, 100, 0, 0)]
        [TestCase(100, 50, 180, 100, -50, 0)]
        [TestCase(10, 36.0555, 56.3099, 10, 20, 30)]
        [TestCase(10, 36.0555, 56.3099, 10, 20, 30)]
        [TestCase(10, 36.0555, 123.6901, 10, -20, 30)]
        [TestCase(10, 36.0555, 303.6901, 10, 20, -30)]
        [TestCase(10, 36.0555, 236.3099, 10, -20, -30)]
        public void Convert_LChuv_to_Luv(double l, double c, double h, double l2, double u, double v)
        {
            // arrange
            var input = new LChuvColor(l, c, h);

            // act
            LuvColor output = Converter.ToLuv(input);

            // assert
            Assert.That(output.L, Is.EqualTo(l2).Using(DoubleComparer));
            Assert.That(output.u, Is.EqualTo(u).Using(DoubleComparer));
            Assert.That(output.v, Is.EqualTo(v).Using(DoubleComparer));
        }

        /// <summary>
        /// Tests conversion from <see cref="LuvColor"/> to <see cref="LChuvColor"/>.
        /// </summary>
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(54.2917, 80.8125, 69.8851, 54.2917, 106.8391, 40.8526)]
        [TestCase(100, 0, 0, 100, 0, 0)]
        [TestCase(100, -50, 0, 100, 50, 180)]
        [TestCase(10, 20, 30, 10, 36.0555, 56.3099)]
        [TestCase(10, 20, 30, 10, 36.0555, 56.3099)]
        [TestCase(10, -20, 30, 10, 36.0555, 123.6901)]
        [TestCase(10, 20, -30, 10, 36.0555, 303.6901)]
        [TestCase(10, -20, -30, 10, 36.0555, 236.3099)]
        [TestCase(37.3511, 24.1720, 16.0684, 37.3511, 29.0255, 33.6141)]
        public void Convert_Luv_to_LChuv(double l, double u, double v, double l2, double c, double h)
        {
            // arrange
            var input = new LuvColor(l, u, v);

            // act
            LChuvColor output = Converter.ToLChuv(input);

            // assert
            Assert.That(output.L, Is.EqualTo(l2).Using(DoubleComparer));
            Assert.That(output.C, Is.EqualTo(c).Using(DoubleComparer));
            Assert.That(output.h, Is.EqualTo(h).Using(DoubleComparer));
        }
    }
}