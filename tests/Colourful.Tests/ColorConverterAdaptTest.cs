using System.Collections.Generic;
using Colourful.Conversion;
using Colourful.Implementation.Conversion;
using Xunit;

#pragma warning disable 1574

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="ColourfulConverter.Adapt" /> methods.
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ChromAdaptCalc.html
    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
    /// </summary>
    public class ColorConverterAdaptTest
    {
        private static readonly IEqualityComparer<double> DoubleRoundingComparer = new DoubleRoundingComparer(4);
        private static readonly IEqualityComparer<double> DoublePrecisionComparer = new DoublePrecisionComparer(4);

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(1, 1, 1, 1, 1, 1)]
        [InlineData(0.206162, 0.260277, 0.746717, 0.220000, 0.130000, 0.780000)]
        public void Adapt_RGB_WideGamutRGB_To_sRGB(double r1, double g1, double b1, double r2, double g2, double b2)
        {
            // arrange
            var input = new RGBColor(r1, g1, b1, RGBWorkingSpaces.WideGamutRGB);
            var expectedOutput = new RGBColor(r2, g2, b2, RGBWorkingSpaces.sRGB);
            var converter = new ColourfulConverter { TargetRGBWorkingSpace = RGBWorkingSpaces.sRGB };

            // action
            var output = converter.Adapt(input);

            // assert
            Assert.Equal(expectedOutput.WorkingSpace, output.WorkingSpace);
            Assert.Equal(output.R, expectedOutput.R, DoubleRoundingComparer);
            Assert.Equal(output.G, expectedOutput.G, DoubleRoundingComparer);
            Assert.Equal(output.B, expectedOutput.B, DoubleRoundingComparer);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(1, 1, 1, 1, 1, 1)]
        [InlineData(0.220000, 0.130000, 0.780000, 0.206162, 0.260277, 0.746717)]
        public void Adapt_RGB_sRGB_To_WideGamutRGB(double r1, double g1, double b1, double r2, double g2, double b2)
        {
            // arrange
            var input = new RGBColor(r1, g1, b1, RGBWorkingSpaces.sRGB);
            var expectedOutput = new RGBColor(r2, g2, b2, RGBWorkingSpaces.WideGamutRGB);
            var converter = new ColourfulConverter { TargetRGBWorkingSpace = RGBWorkingSpaces.WideGamutRGB };

            // action
            var output = converter.Adapt(input);

            // assert
            Assert.Equal(expectedOutput.WorkingSpace, output.WorkingSpace);
            Assert.Equal(output.R, expectedOutput.R, DoubleRoundingComparer);
            Assert.Equal(output.G, expectedOutput.G, DoubleRoundingComparer);
            Assert.Equal(output.B, expectedOutput.B, DoubleRoundingComparer);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(22, 33, 1, 22.269869, 32.841164, 1.633926)]
        public void Adapt_Lab_D65_To_D50(double l1, double a1, double b1, double l2, double a2, double b2)
        {
            // arrange
            var input = new LabColor(l1, a1, b1, Illuminants.D65);
            var expectedOutput = new LabColor(l2, a2, b2);
            var converter = new ColourfulConverter { TargetLabWhitePoint = Illuminants.D50 };

            // action
            var output = converter.Adapt(input);

            // assert
            Assert.Equal(output.L, expectedOutput.L, DoublePrecisionComparer);
            Assert.Equal(output.a, expectedOutput.a, DoublePrecisionComparer);
            Assert.Equal(output.b, expectedOutput.b, DoublePrecisionComparer);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(2, 3, 4, 1.978956, 2.967544, 3.121752)]
        public void Adapt_LChab_D50_To_D65(double l1, double c1, double h1, double l2, double c2, double h2)
        {
            // arrange
            var input = new LChabColor(l1, c1, h1, Illuminants.D50);
            var expectedOutput = new LChabColor(l2, c2, h2);
            var converter = new ColourfulConverter { TargetLabWhitePoint = Illuminants.D65 };

            // action
            var output = converter.Adapt(input);

            // assert
            Assert.Equal(output.L, expectedOutput.L, DoubleRoundingComparer);
            Assert.Equal(output.C, expectedOutput.C, DoubleRoundingComparer);
            Assert.Equal(output.h, expectedOutput.h, DoubleRoundingComparer);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0.5, 0.5, 0.5, 0.510286, 0.501489, 0.378970)]
        public void Adapt_XYZ_D65_To_D50_Bradford(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            // arrange
            var input = new XYZColor(x1, y1, z1);
            var expectedOutput = new XYZColor(x2, y2, z2);
            var converter = new ColourfulConverter
            {
                WhitePoint = Illuminants.D50
            };

            // action
            var output = converter.Adapt(input, Illuminants.D65);

            // assert
            Assert.Equal(output.X, expectedOutput.X, DoubleRoundingComparer);
            Assert.Equal(output.Y, expectedOutput.Y, DoubleRoundingComparer);
            Assert.Equal(output.Z, expectedOutput.Z, DoubleRoundingComparer);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0.5, 0.5, 0.5, 0.509591, 0.500204, 0.378942)]
        public void Adapt_XYZ_D65_To_D50_VonKries(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            // arrange
            var input = new XYZColor(x1, y1, z1);
            var expectedOutput = new XYZColor(x2, y2, z2);
            var converter = new ColourfulConverter
            {
                ChromaticAdaptation = new VonKriesChromaticAdaptation(LMSTransformationMatrix.VonKriesHPEAdjusted),
                WhitePoint = Illuminants.D50
            };

            // action
            var output = converter.Adapt(input, Illuminants.D65);

            // assert
            Assert.Equal(output.X, expectedOutput.X, DoubleRoundingComparer);
            Assert.Equal(output.Y, expectedOutput.Y, DoubleRoundingComparer);
            Assert.Equal(output.Z, expectedOutput.Z, DoubleRoundingComparer);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0.5, 0.5, 0.5, 0.507233, 0.500000, 0.378943)]
        public void Adapt_XYZ_D65_To_D50_XYZScaling(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            // arrange
            var input = new XYZColor(x1, y1, z1);
            var expectedOutput = new XYZColor(x2, y2, z2);
            var converter = new ColourfulConverter
            {
                ChromaticAdaptation = new VonKriesChromaticAdaptation(LMSTransformationMatrix.XYZScaling),
                WhitePoint = Illuminants.D50
            };

            // action
            var output = converter.Adapt(input, Illuminants.D65);

            // assert
            Assert.Equal(output.X, expectedOutput.X, DoubleRoundingComparer);
            Assert.Equal(output.Y, expectedOutput.Y, DoubleRoundingComparer);
            Assert.Equal(output.Z, expectedOutput.Z, DoubleRoundingComparer);
        }
    }
}