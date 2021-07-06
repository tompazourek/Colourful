using System.Diagnostics.CodeAnalysis;
using Xunit;

// ReSharper disable UnusedVariable
// ReSharper disable RedundantNameQualifier

namespace Colourful.Tests.Docs
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SpacesJzazbz
    {
        [Fact]
        public void SamplesJzazbz()
        {
            // red
            var c1 = new JzazbzColor(0.6004, 0.1864, 0.1541);

            // white (D65)
            var c2 = new JzazbzColor(0.9886, -0.0002, -0.0001);

            // gray (D65)
            var c3 = new JzazbzColor(0.5446, -0.0002, -0.0001);

            // black
            var c4 = new JzazbzColor(0, 0, 0);
        }

        [Fact]
        public void RgbToJzazbz()
        {
            var inputRgb = new RGBColor(0.937, 0.2, 0.251);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var rgbToJzazbz = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToJzazbz(rgbWorkingSpace.WhitePoint).Build();
            var outputJzazbz = rgbToJzazbz.Convert(inputRgb); // Jzazbz [Jz=0.6004, az=0.1864, bz=0.1541]

            Assert.Equal(0.60036649238701956, outputJzazbz.Jz);
            Assert.Equal(0.18643549584767377, outputJzazbz.az);
            Assert.Equal(0.15410295035569854, outputJzazbz.bz);
        }

        [Fact]
        public void JzazbzToRgb()
        {
            var inputJzazbz = new JzazbzColor(0.6004, 0.1864, 0.1541);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var jzazbzToRgb = new ConverterBuilder().FromJzazbz(rgbWorkingSpace.WhitePoint).ToRGB(rgbWorkingSpace).Build();
            var outputRgb = jzazbzToRgb.Convert(inputJzazbz); // RGB [R=0.94, G=0.2, B=0.25]

            Assert.Equal(0.9370129897742967, outputRgb.R);
            Assert.Equal(0.20017189959478499, outputRgb.G);
            Assert.Equal(0.2510271785857589, outputRgb.B);
        }

        [Fact]
        public void SamplesJzCzhz()
        {
            // red
            var c1 = new JzCzhzColor(0.6004, 0.2418, 39.58);

            // white (D65)
            var c2 = new JzCzhzColor(0.9886, 0.0003, 211.6);

            // gray (D65)
            var c3 = new JzCzhzColor(0.5446, 0.0003, 211.6);

            // black
            var c4 = new JzCzhzColor(0, 0, 0);
        }

        [Fact]
        public void RgbToJzCzhz()
        {
            var inputRgb = new RGBColor(0.937, 0.2, 0.251);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var rgbToJzCzhz = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToJzCzhz(rgbWorkingSpace.WhitePoint).Build();
            var outputJzCzhz = rgbToJzCzhz.Convert(inputRgb); // JzCzhz [Jz=0.6004, Cz=0.2418, hz=39.58]

            Assert.Equal(0.60036649238701956, outputJzCzhz.Jz);
            Assert.Equal(0.24187995663200137, outputJzCzhz.Cz);
            Assert.Equal(39.576290136882172, outputJzCzhz.hz);
        }

        [Fact]
        public void JzCzhzToRgb()
        {
            var inputJzCzhz = new JzCzhzColor(0.6004, 0.2418, 39.58);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var jzCzhzToRgb = new ConverterBuilder().FromJzCzhz(rgbWorkingSpace.WhitePoint).ToRGB(rgbWorkingSpace).Build();
            var outputRgb = jzCzhzToRgb.Convert(inputJzCzhz); // RGB [R=0.94, G=0.2, B=0.25]

            Assert.Equal(0.9369533175180143, outputRgb.R);
            Assert.Equal(0.20034445429157727, outputRgb.G);
            Assert.Equal(0.2511056385901133, outputRgb.B);
        }
    }
}
