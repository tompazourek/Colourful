using System;
using System.Collections.Generic;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="JzazbzColor" />-<see cref="JzCzhzColor" /> conversions.
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// https://observablehq.com/@jrus/jzazbz
    /// </remarks>
    public class JzazbzAndJzCzhzConversionTest
    {
        private static readonly IEqualityComparer<double> DoubleComparer = new DoubleRoundingComparer(precision: 8);

        /// <summary>
        /// Tests conversion
        /// from <see cref="JzazbzColor" />
        /// to <see cref="JzCzhzColor" />.
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, -8.077935669463161e-26, 2.5849394142282115e-26, 0)]
        [InlineData(1, 1, 1, 0.9999999999999939, 1.4142135623731162, 0.7853981633974273 * 180 / Math.PI)]
        [InlineData(.5, .5, .5, 0.49999999999998834, 0.7071067811865418, 0.7853981633974352 * 180 / Math.PI)]
        [InlineData(.21321, -.45401, -.93646, 0.21320999999999796, 1.0407124538987635, -2.0222224319240194 * 180 / Math.PI + 360)]
        public void Convert_Jzazbz_to_JzCzhz(double jz,
            double az,
            double bz,
            double jhz,
            double cz,
            double hz)
        {
            // arrange
            var input = new JzazbzColor(jz, az, bz);
            var converter = new ConverterBuilder()
                .FromJzazbz()
                .ToJzCzhz()
                .Build();

            // act
            var output = converter.Convert(input);

            // assert
            Assert.Equal(jhz, output.Jz, DoubleComparer);
            Assert.Equal(cz, output.Cz, DoubleComparer);
            Assert.Equal(hz, output.hz, DoubleComparer);
        }

        /// <summary>
        /// Tests conversion
        /// from <see cref="JzazbzColor" />
        /// to <see cref="JzCzhzColor" />.
        /// </summary>
        [Theory]
        [InlineData(0, 0, 0, -8.077935669463161e-26, 2.5849394142282115e-26, 0)]
        [InlineData(1, 1, 1, 0.9999999999999939, 1.4142135623731162, 0.7853981633974273 * 180 / Math.PI)]
        [InlineData(.5, .5, .5, 0.49999999999998834, 0.7071067811865418, 0.7853981633974352 * 180 / Math.PI)]
        [InlineData(.21321, -.45401, -.93646, 0.21320999999999796, 1.0407124538987635, -2.0222224319240194 * 180 / Math.PI + 360)]
        public void Convert_JzCzhz_to_Jzazbz(double jz,
            double az,
            double bz,
            double jhz,
            double cz,
            double hz)
        {
            // arrange
            var input = new JzCzhzColor(jhz, cz, hz);
            var converter = new ConverterBuilder()
                .FromJzCzhz()
                .ToJzazbz()
                .Build();

            // act
            var output = converter.Convert(input);

            // assert
            Assert.Equal(jz, output.Jz, DoubleComparer);
            Assert.Equal(az, output.az, DoubleComparer);
            Assert.Equal(bz, output.bz, DoubleComparer);
        }
    }
}
