﻿#region License

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
    /// Tests <see cref="LabColor"/>-<see cref="LChabColor"/> conversions.
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
    /// </remarks>
    [TestFixture]
    public class LabAndLChabConversionTest
    {
        private static readonly IComparer<double> DoubleComparer = new DoubleRoundingComparer(4);

        private static readonly ColourfulConverter Converter = new ColourfulConverter();

        /// <summary>
        /// Tests conversion from <see cref="LChabColor"/> to <see cref="LabColor"/>.
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
        public void Convert_LCHab_to_Lab(double l, double c, double h, double l2, double a, double b)
        {
            // arrange
            var input = new LChabColor(l, c, h);

            // act
            LabColor output = Converter.ToLab(input);

            // assert
            Assert.That(output.L, Is.EqualTo(l2).Using(DoubleComparer));
            Assert.That(output.a, Is.EqualTo(a).Using(DoubleComparer));
            Assert.That(output.b, Is.EqualTo(b).Using(DoubleComparer));
        }

        /// <summary>
        /// Tests conversion from <see cref="LabColor"/> to <see cref="LChabColor"/>.
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
        public void Convert_Lab_to_LCHab(double l, double a, double b, double l2, double c, double h)
        {
            // arrange
            var input = new LabColor(l, a, b);

            // act
            LChabColor output = Converter.ToLChab(input);

            // assert
            Assert.That(output.L, Is.EqualTo(l2).Using(DoubleComparer));
            Assert.That(output.C, Is.EqualTo(c).Using(DoubleComparer));
            Assert.That(output.h, Is.EqualTo(h).Using(DoubleComparer));
        }
    }
}