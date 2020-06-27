using System.Collections.Generic;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests
{
    public class CIE94ColorDifferenceTest
    {
        private static readonly IEqualityComparer<double> DoubleComparerLabPrecision = new DoublePrecisionComparer(precision: 4);

        /// <summary>
        /// Tests <see cref="CIE94ColorDifference" /> for <see cref="CIE94ColorDifferenceApplication.GraphicArts" />
        /// </summary>
        /// <remarks>
        /// Test data generated using:
        /// http://www.brucelindbloom.com/index.html?ColorDifferenceCalc.html
        /// </remarks>
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(100, 0, 0, 0, 0, 0, 100)]
        [InlineData(100, -50, 50, 20, 10, -20, 89.358114)]
        [InlineData(10.3454, 3.2151, -189.1230, 51.7781, -22.5151, 1.0001, 51.361909)]
        public void ComputeDifference_GraphicArts(double l1, double a1, double b1, double l2, double a2, double b2, double expectedDeltaE)
        {
            // arrange
            var x = new LabColor(in l1, in a1, in b1);
            var y = new LabColor(in l2, in a2, in b2);

            // act
            var deltaE = new CIE94ColorDifference(CIE94ColorDifferenceApplication.GraphicArts).ComputeDifference(in x, in y);

            // assert
            Assert.Equal(expectedDeltaE, deltaE, DoubleComparerLabPrecision);
        }

        /// <summary>
        /// Tests <see cref="CIE94ColorDifference" /> for <see cref="CIE94ColorDifferenceApplication.Textiles" />
        /// </summary>
        /// <remarks>
        /// Test data generated using:
        /// http://www.brucelindbloom.com/index.html?ColorDifferenceCalc.html
        /// </remarks>
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(100, 0, 0, 0, 0, 0, 50)]
        [InlineData(100, -50, 50, 20, 10, -20, 57.247221)]
        [InlineData(10.3454, 3.2151, -189.1230, 51.7781, -22.5151, 1.0001, 37.179939)]
        public void ComputeDifference_Textiles(double l1, double a1, double b1, double l2, double a2, double b2, double expectedDeltaE)
        {
            // arrange
            var x = new LabColor(in l1, in a1, in b1);
            var y = new LabColor(in l2, in a2, in b2);

            // act
            var deltaE = new CIE94ColorDifference(CIE94ColorDifferenceApplication.Textiles).ComputeDifference(in x, in y);

            // assert
            Assert.Equal(expectedDeltaE, deltaE, DoubleComparerLabPrecision);
        }
    }
}