using System.Collections.Generic;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="XYZColor" />-<see cref="LuvColor" /> conversions.
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
    /// </remarks>
    public class XYZAndLuvConversionTest
    {
        private static readonly IEqualityComparer<double> DoubleComparerLuvPrecision = new DoubleRoundingComparer(precision: 4);
        private static readonly IEqualityComparer<double> DoubleComparerXYZPrecision = new DoubleRoundingComparer(precision: 6);

        /// <summary>
        /// Tests conversion from <see cref="LuvColor" /> to <see cref="XYZColor" /> (<see cref="Illuminants.D65" />).
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0, 100, 50, 0, 0, 0)]
        [InlineData(0.1, 100, 50, 0.000493, 0.000111, 0)]
        [InlineData(70.0000, 86.3525, 2.8240, 0.569310, 0.407494, 0.365843)]
        [InlineData(10.0000, -1.2345, -10.0000, 0.012191, 0.011260, 0.025939)]
        [InlineData(100, 0, 0, 0.950470, 1.000000, 1.088830)]
        [InlineData(1, 1, 1, 0.001255, 0.001107, 0.000137)]
        public void Convert_Luv_to_XYZ(double l,
            double u,
            double v,
            double x,
            double y,
            double z)
        {
            // arrange
            var input = new LuvColor(in l, in u, in v);
            var converter = new ConverterBuilder()
                .FromLuv(Illuminants.D65)
                .ToXYZ(Illuminants.D65)
                .Build();

            // act
            var output = converter.Convert(in input);

            // assert
            Assert.Equal(x, output.X, DoubleComparerXYZPrecision);
            Assert.Equal(y, output.Y, DoubleComparerXYZPrecision);
            Assert.Equal(z, output.Z, DoubleComparerXYZPrecision);
        }

        /// <summary>
        /// Tests conversion from <see cref="XYZColor" /> (<see cref="Illuminants.D65" />) to <see cref="LuvColor" />.
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0.000493, 0.000111, 0, 0.1003, 0.9332, -0.0070)]
        [InlineData(0.569310, 0.407494, 0.365843, 70.0000, 86.3524, 2.8240)]
        [InlineData(0.012191, 0.011260, 0.025939, 9.9998, -1.2343, -9.9999)]
        [InlineData(0.950470, 1.000000, 1.088830, 100, 0, 0)]
        [InlineData(0.001255, 0.001107, 0.000137, 0.9999, 0.9998, 1.0004)]
        public void Convert_XYZ_to_Luv(double x,
            double y,
            double z,
            double l,
            double u,
            double v)
        {
            // arrange
            var input = new XYZColor(in x, in y, in z);
            var converter = new ConverterBuilder()
                .FromXYZ(Illuminants.D65)
                .ToLuv(Illuminants.D65)
                .Build();

            // act
            var output = converter.Convert(in input);

            // assert
            Assert.Equal(l, output.L, DoubleComparerLuvPrecision);
            Assert.Equal(u, output.u, DoubleComparerLuvPrecision);
            Assert.Equal(v, output.v, DoubleComparerLuvPrecision);
        }
    }
}
