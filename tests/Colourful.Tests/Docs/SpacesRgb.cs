using System.Diagnostics.CodeAnalysis;
using Xunit;

// ReSharper disable UnusedVariable
// ReSharper disable RedundantNameQualifier

namespace Colourful.Tests.Docs
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SpacesRgb
    {
        [Fact]
        public void SamplesRgb()
        {
            // red
            var c1 = new RGBColor(0.937, 0.2, 0.251);
            var c2 = RGBColor.FromRGB8Bit(239, 51, 64);
            var c3 = RGBColor.FromColor(System.Drawing.Color.FromArgb(239, 51, 64));

            // gray
            var c4 = new RGBColor(0.5, 0.5, 0.5);
            var c5 = RGBColor.FromGray(0.5);

            // white
            var c6 = new RGBColor(1, 1, 1);

            // black
            var c7 = new RGBColor(0, 0, 0);
        }

        [Fact]
        public void SamplesLinearRgb()
        {
            // red
            var c1 = new LinearRGBColor(0.863, 0.0331, 0.0513);

            // gray
            var c4 = new LinearRGBColor(0.5, 0.5, 0.5);
            var c5 = LinearRGBColor.FromGray(0.5);

            // white
            var c6 = new LinearRGBColor(1, 1, 1);

            // black
            var c7 = new LinearRGBColor(0, 0, 0);
        }

        [Fact]
        public void BlendRgbColors()
        {
            RGBColor color1 = RGBColor.FromRGB8Bit(255, 0, 0);
            RGBColor color2 = RGBColor.FromRGB8Bit(0, 0, 255);

            var rgbToLinear = new ConverterBuilder().FromRGB().ToLinearRGB().Build();

            LinearRGBColor linear1 = rgbToLinear.Convert(color1);
            LinearRGBColor linear2 = rgbToLinear.Convert(color2);

            var linearBlend = new LinearRGBColor(
                (linear1.R + linear2.R) / 2,
                (linear1.G + linear2.G) / 2,
                (linear1.B + linear2.B) / 2);

            var linearToRgb = new ConverterBuilder().FromLinearRGB().ToRGB().Build();

            RGBColor blend = linearToRgb.Convert(linearBlend);
            blend.ToRGB8Bit(out var r, out var g, out var b);

            Assert.Equal(188, r);
            Assert.Equal(0, g);
            Assert.Equal(188, b);
        }
    }
}
