using System.Collections.Generic;
using Colourful.Conversion;
using Xunit;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="RGBColor" />-<see cref="XYZColor" /> conversions.
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
    /// </remarks>
    public class RGBAndXYZConversionTest
    {
        private static readonly IEqualityComparer<double> DoubleComparer = new DoubleRoundingComparer(6);

        /// <summary>
        /// Tests conversion
        /// from <see cref="XYZColor" /> (<see cref="Illuminants.D50" />)
        /// to <see cref="RGBColor" /> (<see cref="RGBColor.DefaultWorkingSpace">default sRGB working space</see>).
        /// </summary>
        [Theory]
        [InlineData(0.96422, 1.00000, 0.82521, 1, 1, 1)]
        [InlineData(0.00000, 1.00000, 0.00000, 0, 1, 0)]
        [InlineData(0.96422, 0.00000, 0.00000, 1, 0, 0.292064)]
        [InlineData(0.00000, 0.00000, 0.82521, 0, 0.181415, 1)]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0.297676, 0.267854, 0.045504, 0.720315, 0.509999, 0.168112)]
        public void Convert_XYZ_D50_to_sRGB(double x, double y, double z, double r, double g, double b)
        {
            // arange
            var input = new XYZColor(in x, in y, in z);
            var converter = new ColourfulConverter { WhitePoint = Illuminants.D50, TargetRGBWorkingSpace = RGBWorkingSpaces.sRGB };

            // act
            var output = converter.ToRGB(in input);

            // assert
            Assert.Equal(output.WorkingSpace, RGBColor.DefaultWorkingSpace);
            Assert.Equal(output.R, r, DoubleComparer);
            Assert.Equal(output.G, g, DoubleComparer);
            Assert.Equal(output.B, b, DoubleComparer);
        }

        /// <summary>
        /// Tests conversion
        /// from <see cref="XYZColor" /> (<see cref="Illuminants.D65" />)
        /// to <see cref="RGBColor" /> (<see cref="RGBColor.DefaultWorkingSpace">default sRGB working space</see>).
        /// </summary>
        [Theory]
        [InlineData(0.950470, 1.000000, 1.088830, 1, 1, 1)]
        [InlineData(0, 1.000000, 0, 0, 1, 0)]
        [InlineData(0.950470, 0, 0, 1, 0, 0.254967)]
        [InlineData(0, 0, 1.088830, 0, 0.235458, 1)]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0.297676, 0.267854, 0.045504, 0.754903, 0.501961, 0.099998)]
        public void Convert_XYZ_D65_to_sRGB(double x, double y, double z, double r, double g, double b)
        {
            // arange
            var input = new XYZColor(in x, in y, in z);
            var converter = new ColourfulConverter { WhitePoint = Illuminants.D65, TargetRGBWorkingSpace = RGBWorkingSpaces.sRGB };

            // act
            var output = converter.ToRGB(in input);

            // assert
            Assert.Equal(output.WorkingSpace, RGBColor.DefaultWorkingSpace);
            Assert.Equal(output.R, r, DoubleComparer);
            Assert.Equal(output.G, g, DoubleComparer);
            Assert.Equal(output.B, b, DoubleComparer);
        }

        /// <summary>
        /// Tests conversion
        /// from <see cref="RGBColor" /> (<see cref="RGBColor.DefaultWorkingSpace">default sRGB working space</see>)
        /// to <see cref="XYZColor" /> (<see cref="Illuminants.D50" />).
        /// </summary>
        [Theory]
        [InlineData(1, 1, 1, 0.964220, 1.000000, 0.825210)]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0.436075, 0.222504, 0.013932)]
        [InlineData(0, 1, 0, 0.385065, 0.716879, 0.097105)]
        [InlineData(0, 0, 1, 0.143080, 0.060617, 0.714173)]
        [InlineData(0.754902, 0.501961, 0.100000, 0.315757, 0.273323, 0.035506)]
        public void Convert_sRGB_to_XYZ_D50(double r, double g, double b, double x, double y, double z)
        {
            // arrange
            var input = new RGBColor(in r, in g, in b);
            var converter = new ColourfulConverter { WhitePoint = Illuminants.D50 };

            // act
            var output = converter.ToXYZ(in input);

            // assert
            Assert.Equal(output.X, x, DoubleComparer);
            Assert.Equal(output.Y, y, DoubleComparer);
            Assert.Equal(output.Z, z, DoubleComparer);
        }

        /// <summary>
        /// Tests conversion
        /// from <see cref="RGBColor" /> (<see cref="RGBColor.DefaultWorkingSpace">default sRGB working space</see>)
        /// to <see cref="XYZColor" /> (<see cref="Illuminants.D65" />).
        /// </summary>
        [Theory]
        [InlineData(1, 1, 1, 0.950470, 1.000000, 1.088830)]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0.412456, 0.212673, 0.019334)]
        [InlineData(0, 1, 0, 0.357576, 0.715152, 0.119192)]
        [InlineData(0, 0, 1, 0.180437, 0.072175, 0.950304)]
        [InlineData(0.754902, 0.501961, 0.100000, 0.297676, 0.267854, 0.045504)]
        public void Convert_sRGB_to_XYZ_D65(double r, double g, double b, double x, double y, double z)
        {
            // arrange
            var input = new RGBColor(in r, in g, in b);
            var converter = new ColourfulConverter { WhitePoint = Illuminants.D65 };

            // act
            var output = converter.ToXYZ(in input);

            // assert
            Assert.Equal(output.X, x, DoubleComparer);
            Assert.Equal(output.Y, y, DoubleComparer);
            Assert.Equal(output.Z, z, DoubleComparer);
        }
    }
}