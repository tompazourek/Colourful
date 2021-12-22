using System.Collections.Generic;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests;

public class xyAndXYZConversionTest
{
    private static readonly IEqualityComparer<double> DoubleComparer = new DoubleRoundingComparer(precision: 6);

    [Theory]
    [InlineData(0.95047, 1, 1.08883, 0.3127266146810121, 0.32902313032606195)]
    public void Convert_XYZ_to_xy(double xyzX, double xyzY, double xyzZ, double x, double y)
    {
        // arrange
        var input = new XYZColor(in xyzX, in xyzY, in xyzZ);
        var converter = new ConverterBuilder()
            .FromXYZ()
            .Toxy()
            .Build();

        // act
        var output = converter.Convert(in input);

        // assert
        Assert.Equal(x, output.x, DoubleComparer);
        Assert.Equal(y, output.y, DoubleComparer);
    }

    [Theory]
    [InlineData(0.95047, 1, 1.08883, 0.3127266146810121, 0.32902313032606195)]
    public void Convert_xy_to_XYZ(double xyzX, double xyzY, double xyzZ, double x, double y)
    {
        // arrange
        var input = new xyChromaticity(in x, in y);
        var converter = new ConverterBuilder()
            .Fromxy()
            .ToXYZ()
            .Build();

        // act
        var output = converter.Convert(in input);

        // assert
        Assert.Equal(xyzX, output.X, DoubleComparer);
        Assert.Equal(xyzY, output.Y, DoubleComparer);
        Assert.Equal(xyzZ, output.Z, DoubleComparer);
    }
}
