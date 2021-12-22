using System.Collections.Generic;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests;

public class CIE76ColorDifferenceTest
{
    private static readonly IEqualityComparer<double> DoubleComparerLabPrecision = new DoublePrecisionComparer(precision: 4);

    /// <summary>
    /// Tests <see cref="Colourful.CIE76ColorDifference" />
    /// </summary>
    /// <remarks>
    /// Test data generated using:
    /// http://www.brucelindbloom.com/index.html?ColorDifferenceCalc.html
    /// </remarks>
    [Theory]
    [InlineData(0, 0, 0, 0, 0, 0, 0)]
    [InlineData(100, 0, 0, 0, 0, 0, 100)]
    [InlineData(100, -50, 50, 20, 10, -20, 122.06555)]
    [InlineData(10.3454, 3.2151, -189.1230, 51.7781, -22.5151, 1.0001, 196.27915)]
    public void ComputeDifference(double l1,
        double a1,
        double b1,
        double l2,
        double a2,
        double b2,
        double expectedDeltaE)
    {
        // arrange
        var x = new LabColor(in l1, in a1, in b1);
        var y = new LabColor(in l2, in a2, in b2);

        // act
        var deltaE = new CIE76ColorDifference().ComputeDifference(in x, in y);

        // assert
        Assert.Equal(expectedDeltaE, deltaE, DoubleComparerLabPrecision);
    }
}
