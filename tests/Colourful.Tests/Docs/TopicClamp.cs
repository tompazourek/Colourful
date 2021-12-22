using Xunit;

namespace Colourful.Tests.Docs;

public class TopicClamp
{
    [Fact]
    public void RangesOfChannelValuesAndClamping()
    {
        var x1 = (1.25).Clamp(0, 1);
        var x2 = (-0.5).Clamp(0, 1);
        var x3 = (0.75).Clamp(0, 1);

        // asserts
        Assert.Equal(1, x1);
        Assert.Equal(0, x2);
        Assert.Equal(0.75, x3);

        var arr1 = new[] { 1.25, -0.5, 0.75 }.Clamp(0, 1);

        // asserts
        Assert.Equal(new[] { 1, 0, 0.75 }, arr1);

        var arr2 = new[] { -50, 75, 1.25 }.Clamp(new double[] { 0, 0, 0 }, new double[] { 100, 100, 1 }); // { 0, 75, 1 }

        // asserts
        Assert.Equal(new double[] { 0, 75, 1 }, arr2);

        var color1 = new RGBColor(2, -3, 0.5);
        var color2 = color1.Clamp();

        // asserts
        Assert.Equal(new[] { 1, 0, 0.5 }, color2.Vector);

        var color3 = color1.NormalizeIntensity(); // RGB [R=1, G=0, B=0.25]

        // asserts
        Assert.Equal(new[] { 1, 0, 0.25 }, color3.Vector);

        // linear RGB
        var linearColor = new LinearRGBColor(2, -3, 0.5);
        var normalizedLinearColor = linearColor.NormalizeIntensity(); // LinearRGB [R=1, G=0, B=0.25]

        // asserts
        Assert.Equal(new[] { 1, 0, 0.25 }, normalizedLinearColor.Vector);
    }
}
