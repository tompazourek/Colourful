using System.Diagnostics.CodeAnalysis;
using Xunit;

// ReSharper disable UnusedVariable
// ReSharper disable RedundantNameQualifier

namespace Colourful.Tests.Docs
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SpacesLuv
    {
        [Fact]
        public void SamplesLuv()
        {
            // red
            var c1 = new LuvColor(53.04, 140.97, 26.21);

            // white
            var c2 = new LuvColor(100, 0, 0);

            // gray
            var c3 = new LuvColor(53.39, 0, 0);

            // black
            var c4 = new LuvColor(0, 0, 0);
        }

        [Fact]
        public void RgbToLuv()
        {
            var inputRgb = new RGBColor(0.937, 0.2, 0.251);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var rgbToLuv = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToLuv(rgbWorkingSpace.WhitePoint).Build();
            var outputLuv = rgbToLuv.Convert(inputRgb); // Luv [L=53.04, u=140.97, v=26.21]

            Assert.Equal(53.04052291460343, outputLuv.L);
            Assert.Equal(140.97101114543253, outputLuv.u);
            Assert.Equal(26.20801783223351, outputLuv.v);
        }

        [Fact]
        public void LuvToRgb()
        {
            var inputLuv = new LuvColor(53.04, 140.97, 26.21);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var luvToRgb = new ConverterBuilder().FromLuv(rgbWorkingSpace.WhitePoint).ToRGB(rgbWorkingSpace).Build();
            var outputRgb = luvToRgb.Convert(inputLuv); // RGB [R=0.94, G=0.2, B=0.25]

            Assert.Equal(0.9369894149662827, outputRgb.R);
            Assert.Equal(0.20000252466899038, outputRgb.G);
            Assert.Equal(0.2509774344647836, outputRgb.B);
        }

        [Fact]
        public void SamplesLChuv()
        {
            // red
            var c1 = new LChuvColor(53.04, 143.39, 10.53);

            // white
            var c2 = new LChuvColor(100, 0, 0);

            // gray
            var c3 = new LChuvColor(53.39, 0, 0);

            // black
            var c4 = new LChuvColor(0, 0, 0);
        }

        [Fact]
        public void RgbToLChuv()
        {
            var inputRgb = new RGBColor(0.937, 0.2, 0.251);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var rgbToLChuv = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToLChuv(rgbWorkingSpace.WhitePoint).Build();
            var outputLChuv = rgbToLChuv.Convert(inputRgb); // LChuv [L=53.04, C=143.39, h=10.53]

            Assert.Equal(53.040522914603429, outputLChuv.L);
            Assert.Equal(143.38649232776544, outputLChuv.C);
            Assert.Equal(10.531661841237254, outputLChuv.h);
        }

        [Fact]
        public void LChuvToRgb()
        {
            var inputLChuv = new LChuvColor(53.04, 143.39, 10.53);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var lChuvToRgb = new ConverterBuilder().FromLChuv(rgbWorkingSpace.WhitePoint).ToRGB(rgbWorkingSpace).Build();
            var outputRgb = lChuvToRgb.Convert(inputLChuv); // RGB [R=0.94, G=0.2, B=0.25]

            Assert.Equal(0.9370059187037388, outputRgb.R);
            Assert.Equal(0.19996423944042457, outputRgb.G);
            Assert.Equal(0.2510189456133648, outputRgb.B);
        }
    }
}
