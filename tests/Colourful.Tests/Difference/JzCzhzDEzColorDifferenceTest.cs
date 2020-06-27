using System;
using System.Collections.Generic;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests
{
    public class JzCzhzDEzColorDifferenceTest
    {
        private static readonly IEqualityComparer<double> DoubleComparer = new DoubleRoundingComparer(precision: 8);

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(.33, .44, 2, .55, .66, 3, 0.603152071000162)]
        [InlineData(.5421, 0, .001, 0.1, .23, 0, 0.49834968646523703)]
        public void ComputeDifference(double jz1, double cz1, double hz1Rad, double jz2, double cz2, double hz2Rad, double expectedDeltaE)
        {
            // arrange
            var hz1 = hz1Rad * 180 / Math.PI;
            var hz2 = hz2Rad * 180 / Math.PI;
            var x = new JzCzhzColor(in jz1, in cz1, in hz1);
            var y = new JzCzhzColor(in jz2, in cz2, in hz2);

            // act
            var deltaE = new JzCzhzDEzColorDifference().ComputeDifference(in x, in y);

            // assert
            Assert.Equal(expectedDeltaE, deltaE, DoubleComparer);
        }
    }
}