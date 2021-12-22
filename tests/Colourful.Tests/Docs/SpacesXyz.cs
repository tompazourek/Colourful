using System.Diagnostics.CodeAnalysis;
using Xunit;

// ReSharper disable UnusedVariable
// ReSharper disable RedundantNameQualifier

namespace Colourful.Tests.Docs;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class SpacesXyz
{
    [Fact]
    public void Samples()
    {
        // red
        var c1 = new XYZColor(0.3769, 0.2108, 0.0694);

        // white (relative to D65)
        var c2 = new XYZColor(0.95047, 1, 1.08883);

        // gray (relative to D65)
        var c3 = new XYZColor(0.2034, 0.2140, 0.2331);

        // black (relative to D65)
        var c4 = new XYZColor(0, 0, 0);
    }

    [Fact]
    public void RgbToXyz()
    {
        var inputRgb = new RGBColor(0.937, 0.2, 0.251);
        var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

        var rgbToXyz = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToXYZ(rgbWorkingSpace.WhitePoint).Build();
        var outputXyz = rgbToXyz.Convert(inputRgb); // XYZ [X=0.38, Y=0.21, Z=0.07]

        Assert.Equal(0.37688512026549464, outputXyz.X);
        Assert.Equal(0.2108328349690765, outputXyz.Y);
        Assert.Equal(0.06935279750340714, outputXyz.Z);
    }

    [Fact]
    public void XyzToRgb()
    {
        var inputXyz = new XYZColor(0.3769, 0.2108, 0.0694);
        var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

        var xyzToRgb = new ConverterBuilder().FromXYZ(rgbWorkingSpace.WhitePoint).ToRGB(rgbWorkingSpace).Build();
        var outputRgb = xyzToRgb.Convert(inputXyz); // RGB [R=0.94, G=0.2, B=0.25]

        Assert.Equal(0.9370360111592494, outputRgb.R);
        Assert.Equal(0.19976214977119267, outputRgb.G);
        Assert.Equal(0.2511427538450153, outputRgb.B);
    }
}
