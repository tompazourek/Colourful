using System.Collections.Generic;
using Colourful.Internals;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="XYZColor" />-<see cref="LabColor" /> conversions.
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ColorCalculator.html
    /// </remarks>
    public class XYZAndLabConversionTest
    {
        private static readonly IEqualityComparer<double> DoubleComparerLabPrecision = new DoubleRoundingComparer(precision: 4);
        private static readonly IEqualityComparer<double> DoubleComparerXYZPrecision = new DoubleRoundingComparer(precision: 6);

        /// <summary>
        /// Tests conversion from <see cref="LabColor" /> to <see cref="XYZColor" /> (<see cref="Illuminants.D65" />).
        /// </summary>
        [Theory]
        [InlineData(100, 0, 0, 0.95047, 1, 1.08883)]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0, 431.0345, 0, 0.95047, 0, 0)]
        [InlineData(100, -431.0345, 172.4138, 0, 1, 0)]
        [InlineData(0, 0, -172.4138, 0, 0, 1.08883)]
        [InlineData(45.6398, 39.8753, 35.2091, 0.216938, 0.150041, 0.048850)]
        [InlineData(77.1234, -40.1235, 78.1120, 0.358530, 0.517372, 0.076273)]
        [InlineData(10, -400, 20, -0.08712414334331052, 0.011260, -0.0019286340563368727)]
        public void Convert_Lab_to_XYZ(double l,
            double a,
            double b,
            double x,
            double y,
            double z)
        {
            // arrange
            var input = new LabColor(in l, in a, in b);
            var converter = new ConverterBuilder()
                .FromLab(Illuminants.D65)
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
        /// Tests conversion from <see cref="XYZColor" /> (<see cref="Illuminants.D65" />) to <see cref="LabColor" />.
        /// </summary>
        [Theory]
        [InlineData(0.95047, 1, 1.08883, 100, 0, 0)]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0.95047, 0, 0, 0, 431.0345, 0)]
        [InlineData(0, 1, 0, 100, -431.0345, 172.4138)]
        [InlineData(0, 0, 1.08883, 0, 0, -172.4138)]
        [InlineData(0.216938, 0.150041, 0.048850, 45.6398, 39.8753, 35.2091)]
        public void Convert_XYZ_to_Lab(double x,
            double y,
            double z,
            double l,
            double a,
            double b)
        {
            // arrange
            var input = new XYZColor(in x, in y, in z);
            var converter = new ConverterBuilder()
                .FromXYZ(Illuminants.D65)
                .ToLab(Illuminants.D65)
                .Build();

            // act
            var output = converter.Convert(in input);

            // assert
            Assert.Equal(l, output.L, DoubleComparerLabPrecision);
            Assert.Equal(a, output.a, DoubleComparerLabPrecision);
            Assert.Equal(b, output.b, DoubleComparerLabPrecision);
        }

        /// <summary>
        /// Tests conversion from <see cref="XYZColor" /> to <see cref="LabColor" /> and checks that it requires a white point.
        /// </summary>
        [Fact]
        public void Convert_XYZ_to_Lab_RequiredWhitePoint()
        {
            var ex = Assert.Throws<MissingConversionMetadataException>(() => { new ConverterBuilder().FromXYZ().ToLab().Build(); });

            Assert.Equal(ex.Key, ConversionMetadataKeys.WhitePoint);
        }
    }
}
