using System.Diagnostics.CodeAnalysis;
using Xunit;

// ReSharper disable UnusedVariable
// ReSharper disable RedundantNameQualifier

namespace Colourful.Tests.Docs
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SpacesLms
    {
        [Fact]
        public void Samples()
        {
            // red
            var c1 = new LMSColor(0.3823, 0.0811, 0.07162);
            
            // white (D65)
            var c2 = new LMSColor(0.9414, 1.0404, 1.0895);

            // gray (D65)
            var c3 = new LMSColor(0.2015, 0.2227, 0.2332);

            // black
            var c4 = new LMSColor(0, 0, 0);
        }
       
        [Fact]
        public void RgbToLms()
        {
            var inputRgb = new RGBColor(0.937, 0.2, 0.251);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var rgbToLms = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToLMS(rgbWorkingSpace.WhitePoint).Build();
            var outputLms = rgbToLms.Convert(inputRgb); // LMS [L=0.38, M=0.08, S=0.07]

            Assert.Equal(0.38232219686835633, outputLms.L);
            Assert.Equal(0.08106809316471357, outputLms.M);
            Assert.Equal(0.071624422292454, outputLms.S);
        }

        [Fact]
        public void LmsToRgb()
        {
            var inputLms = new LMSColor(0.3823, 0.0811, 0.07162);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var lmsToRgb = new ConverterBuilder().FromLMS(rgbWorkingSpace.WhitePoint).ToRGB(rgbWorkingSpace).Build();
            var outputRgb = lmsToRgb.Convert(inputLms); // RGB [R=0.94, G=0.2, B=0.25]

            Assert.Equal(0.9369533174707416, outputRgb.R);
            Assert.Equal(0.2001250260835063, outputRgb.G);
            Assert.Equal(0.2509854069250157, outputRgb.B);
        }
    }
}
