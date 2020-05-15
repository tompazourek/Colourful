using System.Collections.Generic;
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
    public class XYZAndHunterLabConversionTest
    {
        private static readonly IEqualityComparer<double> DoubleComparerLabPrecision = new DoubleRoundingComparer(precision: 3);
        private static readonly IEqualityComparer<double> DoubleComparerXYZPrecision = new DoubleRoundingComparer(precision: 3);

        /// <summary>
        /// Tests conversion from <see cref="HunterLabColor" /> to <see cref="XYZColor" /> (Illuminant C)
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(100, 0, 0, 0.98074, 1, 1.18232)] // C white point is HunerLab 100, 0, 0
        public void Convert_HunterLab_to_XYZ(double l, double a, double b, double x, double y, double z)
        {
            // arrange
            var input = new HunterLabColor(in l, in a, in b);
            var converter = new ConverterBuilder()
                .FromHunterLab(Illuminants.C)
                .ToXYZ(Illuminants.C)
                .Build();

            // act
            var output = converter.Convert(in input);

            // assert
            Assert.Equal(x, output.X, DoubleComparerXYZPrecision);
            Assert.Equal(y, output.Y, DoubleComparerXYZPrecision);
            Assert.Equal(z, output.Z, DoubleComparerXYZPrecision);
        }

        /// <summary>
        /// Tests conversion from <see cref="XYZColor" /> (Illuminant C) to <see cref="HunterLabColor" />.
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0.98074, 1, 1.18232, 100, 0, 0)] // C white point is HunerLab 100, 0, 0
        public void Convert_XYZ_to_HunterLab(double x, double y, double z, double l, double a, double b)
        {
            // arrange
            var input = new XYZColor(in x, in y, in z);
            var converter = new ConverterBuilder()
                .FromXYZ(Illuminants.C)
                .ToHunterLab(Illuminants.C)
                .Build();

            // act
            var output = converter.Convert(in input);

            // assert
            Assert.Equal(l, output.L, DoubleComparerLabPrecision);
            Assert.Equal(a, output.a, DoubleComparerLabPrecision);
            Assert.Equal(b, output.b, DoubleComparerLabPrecision);
        }

        /// <summary>
        /// Tests conversion from <see cref="HunterLabColor" /> to <see cref="XYZColor" /> (Illuminant C)
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(100, 0, 0, 0.95047, 1, 1.08883)] // D65 white point is HunerLab 100, 0, 0 (adaptation to C performed)
        public void Convert_HunterLab_to_XYZ_D65(double l, double a, double b, double x, double y, double z)
        {
            // arrange
            var input = new HunterLabColor(in l, in a, in b);
            var converter = new ConverterBuilder()
                .FromHunterLab(Illuminants.C)
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
        /// Tests conversion from <see cref="XYZColor" /> (Illuminant C) to <see cref="HunterLabColor" />.
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0.95047, 1, 1.08883, 100, 0, 0)] // D65 white point is HunerLab 100, 0, 0 (adaptation to C performed)
        public void Convert_XYZ_D65_to_HunterLab(double x, double y, double z, double l, double a, double b)
        {
            // arrange
            var input = new XYZColor(in x, in y, in z);
            var converter = new ConverterBuilder()
                .FromXYZ(Illuminants.D65)
                .ToHunterLab(Illuminants.C)
                .Build();

            // act
            var output = converter.Convert(in input);

            // assert
            Assert.Equal(l, output.L, DoubleComparerLabPrecision);
            Assert.Equal(a, output.a, DoubleComparerLabPrecision);
            Assert.Equal(b, output.b, DoubleComparerLabPrecision);
        }
    }
}