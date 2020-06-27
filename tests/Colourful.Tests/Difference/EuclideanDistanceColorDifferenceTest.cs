using System.Collections.Generic;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests
{
    public class EuclideanDistanceColorDifferenceTest
    {
        private static readonly IEqualityComparer<double> DoubleComparerLabRounding = new DoubleRoundingComparer(precision: 5);

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(-0.2, .5, 1, 0, 0, 0.17, 0.989394)]
        public void ComputeDifference(double a1, double a2, double a3, double b1, double b2, double b3, double expectedDeltaE)
        {
            // arrange
            var x = new RGBColor(in a1, in a2, in a3);
            var y = new RGBColor(in b1, in b2, in b3);

            // act
            var deltaE = new EuclideanDistanceColorDifference<RGBColor>().ComputeDifference(in x, in y);

            // assert
            Assert.Equal(expectedDeltaE, deltaE, DoubleComparerLabRounding);
        }
    }
}