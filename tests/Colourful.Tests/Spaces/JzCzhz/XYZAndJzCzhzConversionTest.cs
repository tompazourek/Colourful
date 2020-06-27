using System;
using System.Collections.Generic;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="XYZColor" />-<see cref="JzCzhzColor" /> conversions.
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// https://observablehq.com/@jrus/jzazbz
    /// </remarks>
    public class XYZAndJzCzhzConversionTest
    {
        private static readonly IEqualityComparer<double> DoubleComparer = new DoubleRoundingComparer(precision: 8);

        /// <summary>
        /// Tests conversion
        /// from <see cref="XYZColor" />
        /// to <see cref="JzCzhzColor" />.
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, -8.077935669463161e-26, 2.5849394142282115e-26, 3.141592653589793 * 180 / Math.PI)]
        [InlineData(10000 / 10000d, 10000 / 10000d, 10000 / 10000d, 0.9962177641398566, 0.023243401709370465, 0.6942355713504587 * 180 / Math.PI)]
        [InlineData(3333 / 10000d, 5555 / 10000d, 1111 / 10000d, 0.7306836888937366, 0.2283688525264617, 2.178752906908651 * 180 / Math.PI)]
        [InlineData(7000 / 10000d, 20 / 10000d, 9500 / 10000d, 0.6118957788657648, 0.6637894734869301, -0.4015277024972921 * 180 / Math.PI + 360)]
        public void Convert_XYZ_to_JzCzhz(double x, double y, double z, double jz, double cz, double hz)
        {
            // arrange
            var input = new XYZColor(x, y, z);
            var converter = new ConverterBuilder()
                .FromXYZ(Illuminants.D65)
                .ToJzCzhz(Illuminants.D65)
                .Build();

            // act
            var output = converter.Convert(input);

            // assert
            Assert.Equal(jz, output.Jz, DoubleComparer);
            Assert.Equal(cz, output.Cz, DoubleComparer);
            Assert.Equal(hz, output.hz, DoubleComparer);
        }

        /// <summary>
        /// Tests conversion
        /// from <see cref="XYZColor" />
        /// to <see cref="JzCzhzColor" />.
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, -8.077935669463161e-26, 2.5849394142282115e-26, 3.141592653589793 * 180 / Math.PI)]
        [InlineData(10000 / 10000d, 10000 / 10000d, 10000 / 10000d, 0.9962177641398566, 0.023243401709370465, 0.6942355713504587 * 180 / Math.PI)]
        [InlineData(3333 / 10000d, 5555 / 10000d, 1111 / 10000d, 0.7306836888937366, 0.2283688525264617, 2.178752906908651 * 180 / Math.PI)]
        [InlineData(7000 / 10000d, 20 / 10000d, 9500 / 10000d, 0.6118957788657648, 0.6637894734869301, -0.4015277024972921 * 180 / Math.PI + 360)]
        public void Convert_JzCzhz_to_XYZ(double x, double y, double z, double jz, double cz, double hz)
        {
            // arrange
            var input = new JzCzhzColor(jz, cz, hz);
            var converter = new ConverterBuilder()
                .FromJzCzhz(Illuminants.D65)
                .ToXYZ(Illuminants.D65)
                .Build();

            // act
            var output = converter.Convert(input);

            // assert
            Assert.Equal(x, output.X, DoubleComparer);
            Assert.Equal(y, output.Y, DoubleComparer);
            Assert.Equal(z, output.Z, DoubleComparer);
        }
    }
}