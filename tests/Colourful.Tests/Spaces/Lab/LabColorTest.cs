using Colourful.Tests.Assertions;
using Xunit;

namespace Colourful.Tests;

public class LabColorTest
{
    [Fact]
    public void Equals_Same()
    {
        var first = new LabColor(l: 10, a: 20.5, b: 45.445);
        var second = new LabColor(l: 10, a: 20.5, b: 45.445);
        CustomAssert.EqualsWithHashCode(first, second);
    }

    [Fact]
    public void Equals_Different()
    {
        var first = new LabColor(l: 11, a: 20.5, b: 45.445);
        var second = new LabColor(l: 10, a: 20.5, b: 45.445);
        CustomAssert.NotEqualsWithHashCode(first, second);
    }

    [Fact]
    public void VectorCtor()
    {
        var first = new LabColor(l: 10, a: 20.5, b: 45.445);
        var vector = new[] { 10, 20.5, 45.445 };
        var second = new LabColor(vector);
        CustomAssert.EqualsWithHashCode(first, second);
        Assert.Equal(vector, second.Vector);
    }

    [Fact]
    public void ToString_Simple()
    {
        var color = new LabColor(l: 10, a: 20.5, b: 45.445);
        Assert.Equal("Lab [L=10, a=20.5, b=45.45]", color.ToString());
    }

    [Fact]
    public void Dctor()
    {
        const double l1 = 10;
        const double a1 = 20.5;
        const double b1 = 45.445;
        var (l2, a2, b2) = new LabColor(l1, a1, b1);
        Assert.Equal(l1, l2);
        Assert.Equal(a1, a2);
        Assert.Equal(b1, b2);
    }
}
