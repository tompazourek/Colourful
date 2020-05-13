//using System.Collections.Generic;
//using Colourful.Conversion;
//using Colourful.Implementation.Conversion;
//using Xunit;

//#pragma warning disable 1574

//namespace Colourful.Tests
//{
//    /// <summary>
//    /// Tests <see cref="ColourfulConverter.Adapt" /> methods.
//    /// Test data generated using:
//    /// http://www.brucelindbloom.com/index.html?ChromAdaptCalc.html
//    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
//    /// </summary>
//    public class ColorConverterAdaptTest
//    {
//        private static readonly IEqualityComparer<double> DoubleRoundingComparer = new DoubleRoundingComparer(4);
//        private static readonly IEqualityComparer<double> DoublePrecisionComparer = new DoublePrecisionComparer(4);

//        [Theory]
//        [InlineData(0, 0, 0, 0, 0, 0)]
//        [InlineData(1, 1, 1, 1, 1, 1)]
//        [InlineData(0.206162, 0.260277, 0.746717, 0.220000, 0.130000, 0.780000)]
//        public void Adapt_RGB_WideGamutRGB_To_sRGB(double r1, double g1, double b1, double r2, double g2, double b2)
//        {
//            // arrange
//            var input = new RGBColor(in r1, in g1, in b1, RGBWorkingSpaces.WideGamutRGB);
//            var expectedOutput = new RGBColor(in r2, in g2, in b2, RGBWorkingSpaces.sRGB);
//            var converter = new ColourfulConverter { TargetRGBWorkingSpace = RGBWorkingSpaces.sRGB };

//            // action
//            var output = converter.Adapt(in input);

//            // assert
//            Assert.Equal(expectedOutput.WorkingSpace, output.WorkingSpace);
//            Assert.Equal(expectedOutput.R, output.R, DoubleRoundingComparer);
//            Assert.Equal(expectedOutput.G, output.G, DoubleRoundingComparer);
//            Assert.Equal(expectedOutput.B, output.B, DoubleRoundingComparer);
//        }

//        [Theory]
//        [InlineData(0, 0, 0, 0, 0, 0)]
//        [InlineData(1, 1, 1, 1, 1, 1)]
//        [InlineData(0.220000, 0.130000, 0.780000, 0.206162, 0.260277, 0.746717)]
//        public void Adapt_RGB_sRGB_To_WideGamutRGB(double r1, double g1, double b1, double r2, double g2, double b2)
//        {
//            // arrange
//            var input = new RGBColor(in r1, in g1, in b1, RGBWorkingSpaces.sRGB);
//            var expectedOutput = new RGBColor(in r2, in g2, in b2, RGBWorkingSpaces.WideGamutRGB);
//            var converter = new ColourfulConverter { TargetRGBWorkingSpace = RGBWorkingSpaces.WideGamutRGB };

//            // action
//            var output = converter.Adapt(in input);

//            // assert
//            Assert.Equal(expectedOutput.WorkingSpace, output.WorkingSpace);
//            Assert.Equal(expectedOutput.R, output.R, DoubleRoundingComparer);
//            Assert.Equal(expectedOutput.G, output.G, DoubleRoundingComparer);
//            Assert.Equal(expectedOutput.B, output.B, DoubleRoundingComparer);
//        }

//        [Theory]
//        [InlineData(0, 0, 0, 0, 0, 0)]
//        [InlineData(22, 33, 1, 22.269869, 32.841164, 1.633926)]
//        public void Adapt_Lab_D65_To_D50(double l1, double a1, double b1, double l2, double a2, double b2)
//        {
//            // arrange
//            var input = new LabColor(in l1, in a1, in b1, in Illuminants.D65);
//            var expectedOutput = new LabColor(in l2, in a2, in b2);
//            var converter = new ColourfulConverter { TargetLabWhitePoint = Illuminants.D50 };

//            // action
//            var output = converter.Adapt(in input);

//            // assert
//            Assert.Equal(expectedOutput.L, output.L, DoublePrecisionComparer);
//            Assert.Equal(expectedOutput.a, output.a, DoublePrecisionComparer);
//            Assert.Equal(expectedOutput.b, output.b, DoublePrecisionComparer);
//        }

//        [Theory]
//        [InlineData(0, 0, 0, 0, 0, 0)]
//        [InlineData(2, 3, 4, 1.978956, 2.967544, 3.121752)]
//        public void Adapt_LChab_D50_To_D65(double l1, double c1, double h1, double l2, double c2, double h2)
//        {
//            // arrange
//            var input = new LChabColor(in l1, in c1, in h1, in Illuminants.D50);
//            var expectedOutput = new LChabColor(in l2, in c2, in h2);
//            var converter = new ColourfulConverter { TargetLabWhitePoint = Illuminants.D65 };

//            // action
//            var output = converter.Adapt(in input);

//            // assert
//            Assert.Equal(expectedOutput.L, output.L, DoubleRoundingComparer);
//            Assert.Equal(expectedOutput.C, output.C, DoubleRoundingComparer);
//            Assert.Equal(expectedOutput.h, output.h, DoubleRoundingComparer);
//        }

//        [Theory]
//        [InlineData(0, 0, 0, 0, 0, 0)]
//        [InlineData(0.5, 0.5, 0.5, 0.510286, 0.501489, 0.378970)]
//        public void Adapt_XYZ_D65_To_D50_Bradford(double x1, double y1, double z1, double x2, double y2, double z2)
//        {
//            // arrange
//            var input = new XYZColor(in x1, in y1, in z1);
//            var expectedOutput = new XYZColor(in x2, in y2, in z2);
//            var converter = new ColourfulConverter
//            {
//                WhitePoint = Illuminants.D50
//            };

//            // action
//            var output = converter.Adapt(in input, in Illuminants.D65);

//            // assert
//            Assert.Equal(expectedOutput.X, output.X, DoubleRoundingComparer);
//            Assert.Equal(expectedOutput.Y, output.Y, DoubleRoundingComparer);
//            Assert.Equal(expectedOutput.Z, output.Z, DoubleRoundingComparer);
//        }

//        [Theory]
//        [InlineData(0, 0, 0, 0, 0, 0)]
//        [InlineData(0.5, 0.5, 0.5, 0.509591, 0.500204, 0.378942)]
//        public void Adapt_XYZ_D65_To_D50_VonKries(double x1, double y1, double z1, double x2, double y2, double z2)
//        {
//            // arrange
//            var input = new XYZColor(in x1, in y1, in z1);
//            var expectedOutput = new XYZColor(in x2, in y2, in z2);
//            var converter = new ColourfulConverter
//            {
//                ChromaticAdaptation = new VonKriesChromaticAdaptation(in LMSTransformationMatrix.VonKriesHPEAdjusted),
//                WhitePoint = Illuminants.D50,
//            };

//            // action
//            var output = converter.Adapt(in input, in Illuminants.D65);

//            // assert
//            Assert.Equal(expectedOutput.X, output.X, DoubleRoundingComparer);
//            Assert.Equal(expectedOutput.Y, output.Y, DoubleRoundingComparer);
//            Assert.Equal(expectedOutput.Z, output.Z, DoubleRoundingComparer);
//        }

//        [Theory]
//        [InlineData(0, 0, 0, 0, 0, 0)]
//        [InlineData(0.5, 0.5, 0.5, 0.507233, 0.500000, 0.378943)]
//        public void Adapt_XYZ_D65_To_D50_XYZScaling(double x1, double y1, double z1, double x2, double y2, double z2)
//        {
//            // arrange
//            var input = new XYZColor(in x1, in y1, in z1);
//            var expectedOutput = new XYZColor(in x2, in y2, in z2);
//            var converter = new ColourfulConverter
//            {
//                ChromaticAdaptation = new VonKriesChromaticAdaptation(in LMSTransformationMatrix.XYZScaling),
//                WhitePoint = Illuminants.D50,
//            };

//            // action
//            var output = converter.Adapt(in input, in Illuminants.D65);

//            // assert
//            Assert.Equal(expectedOutput.X, output.X, DoubleRoundingComparer);
//            Assert.Equal(expectedOutput.Y, output.Y, DoubleRoundingComparer);
//            Assert.Equal(expectedOutput.Z, output.Z, DoubleRoundingComparer);
//        }
//    }
//}