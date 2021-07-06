using System.Collections.Generic;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="XYZColor" />-<see cref="LMSColor" /> conversions.
    /// </summary>
    public class XYZAndLMSConversionTest
    {
        private static readonly IEqualityComparer<double> DoubleComparerLMSPrecision = new DoubleRoundingComparer(precision: 10);
        private static readonly IEqualityComparer<double> DoubleComparerXYZPrecision = new DoubleRoundingComparer(precision: 10);

        /// <summary>
        /// Tests conversion from <see cref="LMSColor" /> to <see cref="XYZColor" /> (<see cref="Illuminants.D65" />).
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        public void Convert_LMS_to_XYZ(double l,
            double m,
            double s,
            double x,
            double y,
            double z)
        {
            // arrange
            var input = new LMSColor(in l, in m, in s);
            var converter = new ConverterBuilder()
                .FromLMS()
                .ToXYZ()
                .Build();

            // act
            var output = converter.Convert(in input);

            // assert
            Assert.Equal(x, output.X, DoubleComparerXYZPrecision);
            Assert.Equal(y, output.Y, DoubleComparerXYZPrecision);
            Assert.Equal(z, output.Z, DoubleComparerXYZPrecision);
        }

        /// <summary>
        /// Tests conversion from <see cref="XYZColor" /> (<see cref="Illuminants.D65" />) to <see cref="LMSColor" />.
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        public void Convert_XYZ_to_LMS(double x,
            double y,
            double z,
            double l,
            double m,
            double s)
        {
            // arrange
            var input = new XYZColor(in x, in y, in z);
            var converter = new ConverterBuilder()
                .FromXYZ()
                .ToLMS()
                .Build();

            // act
            var output = converter.Convert(in input);

            // assert
            Assert.Equal(l, output.L, DoubleComparerLMSPrecision);
            Assert.Equal(m, output.M, DoubleComparerLMSPrecision);
            Assert.Equal(s, output.S, DoubleComparerLMSPrecision);
        }
    }
}
