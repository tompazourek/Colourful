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
    /// Tests <see cref="XYZColor"/>-<see cref="LabColor"/> conversions.
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
    /// </remarks>
    [TestFixture]
    public class XYZAndHunterLabConversionTest
    {
        private static readonly IComparer<double> DoubleComparerLabPrecision = new DoubleRoundingComparer(3);
        private static readonly IComparer<double> DoubleComparerXYZPrecision = new DoubleRoundingComparer(3);

        /// <summary>
        /// Tests conversion from <see cref="HunterLabColor"/> to <see cref="XYZColor"/> (Illuminant C)
        /// </summary>
        /// [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(100, 0, 0, 0.98074, 1, 1.18232)] // C white point is HunerLab 100, 0, 0
        public void Convert_HunterLab_to_XYZ(double l, double a, double b, double x, double y, double z)
        {
            // arrange
            var input = new HunterLabColor(l, a, b);
            var converter = new ColorConverter { WhitePoint = Illuminants.C };

            // act
            XYZColor output = converter.ToXYZ(input);

            // assert
            Assert.That(output.X, Is.EqualTo(x).Using(DoubleComparerXYZPrecision));
            Assert.That(output.Y, Is.EqualTo(y).Using(DoubleComparerXYZPrecision));
            Assert.That(output.Z, Is.EqualTo(z).Using(DoubleComparerXYZPrecision));
        }

        /// <summary>
        /// Tests conversion from <see cref="XYZColor"/> (Illuminant C) to <see cref="HunterLabColor"/>.
        /// </summary>
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0.98074, 1, 1.18232, 100, 0, 0)] // C white point is HunerLab 100, 0, 0
        public void Convert_XYZ_to_HunterLab(double x, double y, double z, double l, double a, double b)
        {
            // arrange
            var input = new XYZColor(x, y, z);
            var converter = new ColorConverter { WhitePoint = Illuminants.C };

            // act
            HunterLabColor output = converter.ToHunterLab(input);

            // assert
            Assert.That(output.L, Is.EqualTo(l).Using(DoubleComparerLabPrecision));
            Assert.That(output.a, Is.EqualTo(a).Using(DoubleComparerLabPrecision));
            Assert.That(output.b, Is.EqualTo(b).Using(DoubleComparerLabPrecision));
        }

        /// <summary>
        /// Tests conversion from <see cref="HunterLabColor"/> to <see cref="XYZColor"/> (Illuminant C)
        /// </summary>
        /// [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(100, 0, 0, 0.95047, 1, 1.08883)] // D65 white point is HunerLab 100, 0, 0 (adaptation to C performed)
        public void Convert_HunterLab_to_XYZ_D65(double l, double a, double b, double x, double y, double z)
        {
            // arrange
            var input = new HunterLabColor(l, a, b);
            var converter = new ColorConverter { WhitePoint = Illuminants.D65 };

            // act
            XYZColor output = converter.ToXYZ(input);

            // assert
            Assert.That(output.X, Is.EqualTo(x).Using(DoubleComparerXYZPrecision));
            Assert.That(output.Y, Is.EqualTo(y).Using(DoubleComparerXYZPrecision));
            Assert.That(output.Z, Is.EqualTo(z).Using(DoubleComparerXYZPrecision));
        }

        /// <summary>
        /// Tests conversion from <see cref="XYZColor"/> (Illuminant C) to <see cref="HunterLabColor"/>.
        /// </summary>
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0.95047, 1, 1.08883, 100, 0, 0)] // D65 white point is HunerLab 100, 0, 0 (adaptation to C performed)
        public void Convert_XYZ_D65_to_HunterLab(double x, double y, double z, double l, double a, double b)
        {
            // arrange
            var input = new XYZColor(x, y, z);
            var converter = new ColorConverter { WhitePoint = Illuminants.D65 };

            // act
            HunterLabColor output = converter.ToHunterLab(input);

            // assert
            Assert.That(output.L, Is.EqualTo(l).Using(DoubleComparerLabPrecision));
            Assert.That(output.a, Is.EqualTo(a).Using(DoubleComparerLabPrecision));
            Assert.That(output.b, Is.EqualTo(b).Using(DoubleComparerLabPrecision));
        }
    }
}