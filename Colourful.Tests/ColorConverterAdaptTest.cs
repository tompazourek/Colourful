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
using System.Threading.Tasks;
using Colourful.ChromaticAdaptation;
using Colourful.Conversion;
using Colourful.Implementation;
using NUnit.Framework;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="ColorConverter.Adapt" /> methods.
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ChromAdaptCalc.html
    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
    /// </summary>
    [TestFixture]
    public class ColorConverterAdaptTest
    {
        private static readonly IComparer<double> DoubleRoundingComparer = new DoubleRoundingComparer(4);
        private static readonly IComparer<double> DoublePrecisionComparer = new DoublePrecisionComparer(4);

        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(1, 1, 1, 1, 1, 1)]
        [TestCase(0.206162, 0.260277, 0.746717, 0.220000, 0.130000, 0.780000)]
        public void Adapt_RGB_WideGamutRGB_To_sRGB(double r1, double g1, double b1, double r2, double g2, double b2)
        {
            // arrange
            var input = new RGBColor(r1, g1, b1, RGBWorkingSpaces.WideGamutRGB);
            var expectedOutput = new RGBColor(r2, g2, b2, RGBWorkingSpaces.sRGB);
            var converter = new ColorConverter { TargetRGBWorkingSpace = RGBWorkingSpaces.sRGB };

            // action
            RGBColor output = converter.Adapt(input);

            // assert
            Assert.AreEqual(expectedOutput.WorkingSpace, output.WorkingSpace);
            Assert.That(output.R, Is.EqualTo(expectedOutput.R).Using(DoubleRoundingComparer));
            Assert.That(output.G, Is.EqualTo(expectedOutput.G).Using(DoubleRoundingComparer));
            Assert.That(output.B, Is.EqualTo(expectedOutput.B).Using(DoubleRoundingComparer));
        }

        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(1, 1, 1, 1, 1, 1)]
        [TestCase(0.220000, 0.130000, 0.780000, 0.206162, 0.260277, 0.746717)]
        public void Adapt_RGB_sRGB_To_WideGamutRGB(double r1, double g1, double b1, double r2, double g2, double b2)
        {
            // arrange
            var input = new RGBColor(r1, g1, b1, RGBWorkingSpaces.sRGB);
            var expectedOutput = new RGBColor(r2, g2, b2, RGBWorkingSpaces.WideGamutRGB);
            var converter = new ColorConverter { TargetRGBWorkingSpace = RGBWorkingSpaces.WideGamutRGB };

            // action
            RGBColor output = converter.Adapt(input);

            // assert
            Assert.AreEqual(expectedOutput.WorkingSpace, output.WorkingSpace);
            Assert.That(output.R, Is.EqualTo(expectedOutput.R).Using(DoubleRoundingComparer));
            Assert.That(output.G, Is.EqualTo(expectedOutput.G).Using(DoubleRoundingComparer));
            Assert.That(output.B, Is.EqualTo(expectedOutput.B).Using(DoubleRoundingComparer));
        }

        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(22, 33, 1, 22.269869, 32.841164, 1.633926)]
        public void Adapt_Lab_D65_To_D50(double l1, double a1, double b1, double l2, double a2, double b2)
        {
            // arrange
            var input = new LabColor(l1, a1, b1, Illuminants.D65);
            var expectedOutput = new LabColor(l2, a2, b2);
            var converter = new ColorConverter { TargetLabWhitePoint = Illuminants.D50 };

            // action
            LabColor output = converter.Adapt(input);

            // assert
            Assert.That(output.L, Is.EqualTo(expectedOutput.L).Using(DoublePrecisionComparer));
            Assert.That(output.a, Is.EqualTo(expectedOutput.a).Using(DoublePrecisionComparer));
            Assert.That(output.b, Is.EqualTo(expectedOutput.b).Using(DoublePrecisionComparer));
        }

        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(2, 3, 4, 1.978956, 2.967544, 3.121752)]
        public void Adapt_LChab_D50_To_D65(double l1, double c1, double h1, double l2, double c2, double h2)
        {
            // arrange
            var input = new LChabColor(l1, c1, h1, Illuminants.D50);
            var expectedOutput = new LChabColor(l2, c2, h2);
            var converter = new ColorConverter { TargetLabWhitePoint = Illuminants.D65 };

            // action
            LChabColor output = converter.Adapt(input);

            // assert
            Assert.That(output.L, Is.EqualTo(expectedOutput.L).Using(DoubleRoundingComparer));
            Assert.That(output.C, Is.EqualTo(expectedOutput.C).Using(DoubleRoundingComparer));
            Assert.That(output.h, Is.EqualTo(expectedOutput.h).Using(DoubleRoundingComparer));
        }

        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0.5, 0.5, 0.5, 0.510286, 0.501489, 0.378970)]
        public void Adapt_XYZ_D65_To_D50_Bradford(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            // arrange
            var input = new XYZColor(x1, y1, z1);
            var expectedOutput = new XYZColor(x2, y2, z2);
            var converter = new ColorConverter
                {
                    ChromaticAdaptation = new BradfordChromaticAdaptation(),
                    WhitePoint = Illuminants.D50
                };

            // action
            XYZColor output = converter.Adapt(input, Illuminants.D65);

            // assert
            Assert.That(output.X, Is.EqualTo(expectedOutput.X).Using(DoubleRoundingComparer));
            Assert.That(output.Y, Is.EqualTo(expectedOutput.Y).Using(DoubleRoundingComparer));
            Assert.That(output.Z, Is.EqualTo(expectedOutput.Z).Using(DoubleRoundingComparer));
        }

        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0.5, 0.5, 0.5, 0.509591, 0.500204, 0.378942)]
        public void Adapt_XYZ_D65_To_D50_VonKries(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            // arrange
            var input = new XYZColor(x1, y1, z1);
            var expectedOutput = new XYZColor(x2, y2, z2);
            var converter = new ColorConverter
                {
                    ChromaticAdaptation = new VonKriesChromaticAdaptation(),
                    WhitePoint = Illuminants.D50
                };

            // action
            XYZColor output = converter.Adapt(input, Illuminants.D65);

            // assert
            Assert.That(output.X, Is.EqualTo(expectedOutput.X).Using(DoubleRoundingComparer));
            Assert.That(output.Y, Is.EqualTo(expectedOutput.Y).Using(DoubleRoundingComparer));
            Assert.That(output.Z, Is.EqualTo(expectedOutput.Z).Using(DoubleRoundingComparer));
        }

        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0.5, 0.5, 0.5, 0.507233, 0.500000, 0.378943)]
        public void Adapt_XYZ_D65_To_D50_XYZScaling(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            // arrange
            var input = new XYZColor(x1, y1, z1);
            var expectedOutput = new XYZColor(x2, y2, z2);
            var converter = new ColorConverter
                {
                    ChromaticAdaptation = new XYZScaling(),
                    WhitePoint = Illuminants.D50
                };

            // action
            XYZColor output = converter.Adapt(input, Illuminants.D65);

            // assert
            Assert.That(output.X, Is.EqualTo(expectedOutput.X).Using(DoubleRoundingComparer));
            Assert.That(output.Y, Is.EqualTo(expectedOutput.Y).Using(DoubleRoundingComparer));
            Assert.That(output.Z, Is.EqualTo(expectedOutput.Z).Using(DoubleRoundingComparer));
        }
    }
}