using System.Collections.Generic;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="RGBColor" />-<see cref="JzazbzColor" /> conversions.
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// https://observablehq.com/@jrus/jzazbz
    /// </remarks>
    public class RGBAndJzazbzConversionTest
    {
        private static readonly IEqualityComparer<double> DoubleComparer = new DoubleRoundingComparer(precision: 3);

        /// <summary>
        /// Tests conversion
        /// from <see cref="RGBColor" /> (<see cref="RGBWorkingSpaces.sRGB">sRGB working space</see>)
        /// to <see cref="JzazbzColor" />.
        /// </summary>
        [Theory]
        [InlineData(215 / 255d, 183 / 255d, 251 / 255d, 0.8082452316139784, 0.03128405391849587, -0.07214170275247)]
        [InlineData(0, 0, 0, -8.077935669463161e-26, -2.5849394142282115e-26, 0)]
        [InlineData(1, 1, 1, 0.9886011804209902, -0.00024120640221192424, -0.00015051014313560707)]
        [InlineData(210 / 255d, 88 / 255d, 6 / 255d, 0.5724569969567214, 0.11371956333703165, 0.20847893937029804)]
        [InlineData(255 / 255d, 254 / 255d, 206 / 255d, 0.9655170100316091, -0.007751523182026876, 0.05434705677934293)]
        public void Convert_sRGB_to_Jzazbz(double r, double g, double b, double jz, double az, double bz)
        {
            // arrange
            var input = new RGBColor(r, g, b);
            var converter = new ConverterBuilder()
                .FromRGB()
                .ToJzazbz()
                .Build();

            // act
            var output = converter.Convert(input);

            // assert
            Assert.Equal(jz, output.Jz, DoubleComparer);
            Assert.Equal(az, output.az, DoubleComparer);
            Assert.Equal(bz, output.bz, DoubleComparer);
        }

        /// <summary>
        /// Tests conversion
        /// from <see cref="RGBColor" /> (<see cref="RGBWorkingSpaces.sRGB">sRGB working space</see>)
        /// to <see cref="JzazbzColor" />.
        /// </summary>
        [Theory]
        [InlineData(0.8082452316139784, 0.03128405391849587, -0.07214170275247, 0.8431372549019194, 0.7176470588235777, 0.9843137254902082)]
        [InlineData(-8.077935669463161e-26, -2.5849394142282115e-26, 0, 0, 0, 0)]
        [InlineData(0.9886011804209902, -0.00024120640221192424, -0.00015051014313560707, 0.9999999999998498, 1.0000000000000897, 0.9999999999999584)]
        [InlineData(0.9655170100316091, -0.007751523182026876, 0.05434705677934293, 1.000000000000253, 0.9960784313724732, 0.8078431372548578)]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0, .1, .1, double.NaN, double.NaN, double.NaN)]
        [InlineData(.5, -.1, .1, 0.26664213995884956, 0.5430249807908226, 0.27655883327102515)]
        public void Convert_Jzazbz_to_sRGB(double jz, double az, double bz, double r, double g, double b)
        {
            // arrange
            var input = new JzazbzColor(jz, az, bz);
            var converter = new ConverterBuilder()
                .FromJzazbz()
                .ToRGB()
                .Build();

            // act
            var output = converter.Convert(input);

            // assert
            Assert.Equal(r, output.R, DoubleComparer);
            Assert.Equal(g, output.G, DoubleComparer);
            Assert.Equal(b, output.B, DoubleComparer);
        }
    }
}