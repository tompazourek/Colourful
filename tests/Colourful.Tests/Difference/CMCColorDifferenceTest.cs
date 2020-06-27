using System.Collections.Generic;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests
{
    public class CMCColorDifferenceTest
    {
        private static readonly IEqualityComparer<double> DoubleComparerLabPrecision = new DoublePrecisionComparer(precision: 4);

        /// <summary>
        /// Tests <see cref="CMCColorDifference" />
        /// </summary>
        /// <remarks>
        /// Test data generated using:
        /// http://www.brucelindbloom.com/index.html?ColorDifferenceCalc.html
        /// </remarks>
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0, 0)]
        [InlineData(100, 0, 0, 0, 0, 0, 67.480171, 33.740085)]
        [InlineData(100, -50, 50, 20, 10, -20, 66.320207, 47.038863)]
        [InlineData(10.3454, 3.2151, -189.1230, 51.7781, -22.5151, 1.0001, 98.577755, 69.187455)]
        [InlineData(41.41, 2.64, 4.15, 0, 0, 0, 42.570316, 21.769476)]
        public void ComputeDifference(double l1, double a1, double b1, double l2, double a2, double b2, double expectedDeltaE_imperceptibility, double expectedDeltaE_acceptability)
        {
            // arrange
            var x = new LabColor(in l1, in a1, in b1);
            var y = new LabColor(in l2, in a2, in b2);

            // act
            var deltaE_imperceptibility = new CMCColorDifference(CMCColorDifferenceThreshold.Imperceptibility).ComputeDifference(in x, in y);
            var deltaE_acceptability = new CMCColorDifference(CMCColorDifferenceThreshold.Acceptability).ComputeDifference(in x, in y);

            // assert
            Assert.Equal(expectedDeltaE_imperceptibility, deltaE_imperceptibility, DoubleComparerLabPrecision);
            Assert.Equal(expectedDeltaE_acceptability, deltaE_acceptability, DoubleComparerLabPrecision);
        }
    }
}