using Colourful.Tests.Assertions;
using Xunit;

namespace Colourful.Tests;

public class xyYColorTest
{
    [Fact]
    public void Equals_Same()
    {
        var first = new xyYColor(x: .1, y: .205, luminance: .45445);
        var second = new xyYColor(x: .1, y: .205, luminance: .45445);
        CustomAssert.EqualsWithHashCode(first, second);
    }

    [Fact]
    public void Equals_Different()
    {
        var first = new xyYColor(x: .2, y: .205, luminance: .45445);
        var second = new xyYColor(x: .1, y: .205, luminance: .45445);
        CustomAssert.NotEqualsWithHashCode(first, second);
    }

    [Fact]
    public void VectorCtor()
    {
        var first = new xyYColor(x: .2, y: .205, luminance: .45445);
        var vector = new[] { .2, .205, .45445 };
        var second = new xyYColor(vector);
        CustomAssert.EqualsWithHashCode(first, second);
        Assert.Equal(vector, second.Vector);
    }

    [Fact]
    public void ToString_Simple()
    {
        var color = new xyYColor(x: .1, y: .205, luminance: .45445);
        Assert.Equal("xyY [x=0.1, y=0.21, Y=0.45]", color.ToString());
    }

    [Fact]
    public void Dctor()
    {
        const double x1 = .1;
        const double y1 = .205;
        const double luminance1 = .45445;
        var (x2, y2, luminance2) = new xyYColor(x1, y1, luminance1);
        Assert.Equal(x1, x2);
        Assert.Equal(y1, y2);
        Assert.Equal(luminance1, luminance2);
    }
}
