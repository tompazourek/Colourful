using System.Collections.Generic;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests;

/// <summary>
/// Tests <see cref="LabColor" />-<see cref="HunterLabColor" /> conversions.
/// </summary>
public class LabAndHunterLabConversionTest
{
    private static readonly IEqualityComparer<double> DoubleComparerHunterLabPrecision = new DoubleRoundingComparer(precision: 10);
    private static readonly IEqualityComparer<double> DoubleComparerLabPrecision = new DoubleRoundingComparer(precision: 10);

    /// <summary>
    /// Tests conversion from <see cref="HunterLabColor" /> to <see cref="LabColor" />.
    /// </summary>
    [Theory]
    [InlineData(0, 0, 0, 0, 0, 0)]
    [InlineData(100, 0, 0, 100, 0, 0)]
    public void Convert_HunterLab_to_Lab(double hl,
        double ha,
        double hb,
        double l,
        double a,
        double b)
    {
        // arrange
        var input = new HunterLabColor(in hl, in ha, in hb);
        var converter = new ConverterBuilder()
            .FromHunterLab()
            .ToLab()
            .Build();

        // act
        var output = converter.Convert(in input);

        // assert
        Assert.Equal(l, output.L, DoubleComparerLabPrecision);
        Assert.Equal(a, output.a, DoubleComparerLabPrecision);
        Assert.Equal(b, output.b, DoubleComparerLabPrecision);
    }

    /// <summary>
    /// Tests conversion from <see cref="LabColor" /> to <see cref="HunterLabColor" />.
    /// </summary>
    [Theory]
    [InlineData(0, 0, 0, 0, 0, 0)]
    [InlineData(100, 0, 0, 100, 0, 0)]
    public void Convert_Lab_to_HunterLab(double l,
        double a,
        double b,
        double hl,
        double ha,
        double hb)
    {
        // arrange
        var input = new LabColor(in l, in a, in b);
        var converter = new ConverterBuilder()
            .FromLab()
            .ToHunterLab()
            .Build();

        // act
        var output = converter.Convert(in input);

        // assert
        Assert.Equal(hl, output.L, DoubleComparerHunterLabPrecision);
        Assert.Equal(ha, output.a, DoubleComparerHunterLabPrecision);
        Assert.Equal(hb, output.b, DoubleComparerHunterLabPrecision);
    }
}
