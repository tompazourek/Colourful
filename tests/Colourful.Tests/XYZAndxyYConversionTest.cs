using System.Collections.Generic;
using Xunit;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="XYZColor" />-<see cref="xyYColor" /> conversions.
    /// </summary>
    public class XYZAndxyYConversionTest
    {
        private static readonly IEqualityComparer<double> DoubleComparer = new DoubleRoundingComparer(precision: 4);

        /// <summary>
        /// Data from: http://www.brucelindbloom.com/index.html?ColorCalculator.html
        /// </summary>
        public static readonly IEnumerable<object[]> TestData = new[]
        {
            // X, Y, Z, x, y, Y
            new object[] { 0, 0, 0, 0, 0, 0 },
            new object[] { 0.436075, 0.222504, 0.013932, 0.648427, 0.330856, 0.222504 },
            new object[] { 0.964220, 1.000000, 0.825210, 0.345669, 0.358496, 1.000000 },
            new object[] { 0.434119, 0.356820, 0.369447, 0.374116, 0.307501, 0.356820 }
        };

        [Theory]
        [MemberData(nameof(TestData))]
        [InlineData(0, 0, 0, 0.538842, 0.000000, 0.000000)]
        public void Convert_xyY_to_XYZ(double xyzX, double xyzY, double xyzZ, double x, double y, double Y)
        {
            // arrange
            var input = new xyYColor(in x, in y, in Y);
            var converter = new ConverterBuilder()
                .FromxyY()
                .ToXYZ()
                .Build();

            // act
            var output = converter.Convert(in input);

            // assert
            Assert.Equal(xyzX, output.X, DoubleComparer);
            Assert.Equal(xyzY, output.Y, DoubleComparer);
            Assert.Equal(xyzZ, output.Z, DoubleComparer);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        [InlineData(0.231809, 0, 0.077528, 0.749374, 0.000000, 0.000000)]
        public void Convert_XYZ_to_xyY(double xyzX, double xyzY, double xyzZ, double x, double y, double Y)
        {
            // arrange
            var input = new XYZColor(in xyzX, in xyzY, in xyzZ);
            var converter = new ConverterBuilder()
                .FromXYZ()
                .ToxyY()
                .Build();

            // act
            var output = converter.Convert(in input);

            // assert
            Assert.Equal(x, output.x, DoubleComparer);
            Assert.Equal(y, output.y, DoubleComparer);
            Assert.Equal(Y, output.Luminance, DoubleComparer);
        }
    }
}