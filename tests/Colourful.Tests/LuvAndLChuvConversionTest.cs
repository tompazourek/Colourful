using System.Collections.Generic;
using Colourful.Conversion;
using Xunit;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="LuvColor" />-<see cref="LChuvColor" /> conversions.
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
    /// </remarks>
    public class LuvAndLChuvConversionTest
    {
        private static readonly IEqualityComparer<double> DoubleComparer = new DoubleRoundingComparer(4);

        private static ColourfulConverter Converter => new ColourfulConverter();

        /// <summary>
        /// Tests conversion from <see cref="LChuvColor" /> to <see cref="LuvColor" />.
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(54.2917, 106.8391, 40.8526, 54.2917, 80.8125, 69.8851)]
        [InlineData(100, 0, 0, 100, 0, 0)]
        [InlineData(100, 50, 180, 100, -50, 0)]
        [InlineData(10, 36.0555, 56.3099, 10, 20, 30)]
        [InlineData(10, 36.0555, 123.6901, 10, -20, 30)]
        [InlineData(10, 36.0555, 303.6901, 10, 20, -30)]
        [InlineData(10, 36.0555, 236.3099, 10, -20, -30)]
        public void Convert_LChuv_to_Luv(double l, double c, double h, double l2, double u, double v)
        {
            // arrange
            var input = new LChuvColor(in l, in c, in h);

            // act
            var output = Converter.ToLuv(in input);

            // assert
            Assert.Equal(output.L, l2, DoubleComparer);
            Assert.Equal(output.u, u, DoubleComparer);
            Assert.Equal(output.v, v, DoubleComparer);
        }

        /// <summary>
        /// Tests conversion from <see cref="LuvColor" /> to <see cref="LChuvColor" />.
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(54.2917, 80.8125, 69.8851, 54.2917, 106.8391, 40.8526)]
        [InlineData(100, 0, 0, 100, 0, 0)]
        [InlineData(100, -50, 0, 100, 50, 180)]
        [InlineData(10, 20, 30, 10, 36.0555, 56.3099)]
        [InlineData(10, -20, 30, 10, 36.0555, 123.6901)]
        [InlineData(10, 20, -30, 10, 36.0555, 303.6901)]
        [InlineData(10, -20, -30, 10, 36.0555, 236.3099)]
        [InlineData(37.3511, 24.1720, 16.0684, 37.3511, 29.0255, 33.6141)]
        public void Convert_Luv_to_LChuv(double l, double u, double v, double l2, double c, double h)
        {
            // arrange
            var input = new LuvColor(in l, in u, in v);

            // act
            var output = Converter.ToLChuv(in input);

            // assert
            Assert.Equal(output.L, l2, DoubleComparer);
            Assert.Equal(output.C, c, DoubleComparer);
            Assert.Equal(output.h, h, DoubleComparer);
        }
    }
}