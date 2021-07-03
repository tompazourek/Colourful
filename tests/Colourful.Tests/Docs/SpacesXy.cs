using System.Diagnostics.CodeAnalysis;
using Xunit;

// ReSharper disable UnusedVariable
// ReSharper disable RedundantNameQualifier

namespace Colourful.Tests.Docs
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SpacesXy
    {
        [Fact]
        public void SamplesXy()
        {
            // red
            var c1 = new xyChromaticity(0.5736, 0.3209);

            // white/gray/black (relative to D65 illuminant)
            var c2 = new xyChromaticity(0.3127, 0.3290);
        }

        [Fact]
        public void SamplesXyy()
        {
            // red
            var c1 = new xyYColor(0.5736, 0.3209, 0.21);

            // white (relative to D65 illuminant)
            var c2 = new xyYColor(0.3127, 0.3290, 1);

            // gray (relative to D65 illuminant)
            var c3 = new xyYColor(0.3127, 0.3290, 0.21);

            // black (relative to D65 illuminant), note chromaticity doesn't matter here
            var c4 = new xyYColor(0.3127, 0.3290, 0);
        }

        [Fact]
        public void RgbToXy()
        {
            var inputRgb = new RGBColor(0.937, 0.2, 0.251);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var rgbToxy = new ConverterBuilder().FromRGB(rgbWorkingSpace).Toxy(rgbWorkingSpace.WhitePoint).Build();
            var outputXy = rgbToxy.Convert(inputRgb); // xy [x=0.57, y=0.32]

            Assert.Equal(0.5735837711464629, outputXy.x);
            Assert.Equal(0.32086778187972526, outputXy.y);
        }


        [Fact]
        public void XyToRGB()
        {
            var inputXy = new xyChromaticity(0.5736, 0.3209);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var xyToLinearRgb = new ConverterBuilder().Fromxy(rgbWorkingSpace.WhitePoint).ToLinearRGB(rgbWorkingSpace).Build();
            var linearRgb = xyToLinearRgb.Convert(inputXy); // LinearRGB [R=4.09, G=0.16, B=0.24]
            var normalizedLinearRgb = linearRgb.NormalizeIntensity(); // LinearRGB [R=1, G=0.04, B=0.06]

            var linearRgbToRgb = new ConverterBuilder().FromLinearRGB(rgbWorkingSpace).ToRGB(rgbWorkingSpace).Build();
            var outputRgb = linearRgbToRgb.Convert(normalizedLinearRgb); // RGB [R=1, G=0.22, B=0.27]

            Assert.Equal(4.09118633506919, linearRgb.R);
            Assert.Equal(0.1571363198987878, linearRgb.G);
            Assert.Equal(0.24301157278955215, linearRgb.B);

            Assert.Equal(0.9999999999999999, outputRgb.R);
            Assert.Equal(0.21628795305057674, outputRgb.G);
            Assert.Equal(0.2703317877324105, outputRgb.B);
        }
    }
}
