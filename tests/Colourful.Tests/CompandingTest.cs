using System.Collections.Generic;
using Colourful.Implementation.RGB;
using Xunit;

namespace Colourful.Tests
{
    public class CompandingTest
    {
        private static readonly IEqualityComparer<double> DoubleComparer = new DoubleRoundingComparer(4);

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(1.0, 1.0)]
        [InlineData(0.002, 0.02584)]
        [InlineData(0.18, 0.4613561295)]
        public void sRGBCompanding(double input, double expected)
        {
            // arrange
            var companding = new sRGBCompanding();

            // action
            var actual = companding.Companding(in input);
            var loopback = companding.InverseCompanding(in actual);

            // assert
            Assert.Equal(expected, actual, DoubleComparer);
            Assert.Equal(input, loopback, DoubleComparer);
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(1.0, 1.0)]
        [InlineData(0.002, 0.009)]
        [InlineData(0.18, 0.40900772886)]
        public void Rec709Companding(double input, double expected)
        {
            // arrange
            var companding = new Rec709Companding();

            // action
            var actual = companding.Companding(in input);
            var loopback = companding.InverseCompanding(in actual);

            // assert
            Assert.Equal(expected, actual, DoubleComparer);
            Assert.Equal(input, loopback, DoubleComparer);
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(1.0, 1.0)]
        [InlineData(0.002, 0.009)]
        [InlineData(0.18, 0.40884810889)]
        public void Rec2020Companding(double input, double expected)
        {
            // arrange
            var companding = new Rec2020Companding();

            // action
            var actual = companding.Companding(in input);
            var loopback = companding.InverseCompanding(in actual);

            // assert
            Assert.Equal(expected, actual, DoubleComparer);
            Assert.Equal(input, loopback, DoubleComparer);
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(1.0, 1.0)]
        [InlineData(0.002, 0.05931922284)]
        [InlineData(0.18, 0.45865644686)]
        public void GammaCompanding(double input, double expected)
        {
            // arrange
            var companding = new GammaCompanding(2.2);

            // action
            var actual = companding.Companding(in input);
            var loopback = companding.InverseCompanding(in actual);

            // assert
            Assert.Equal(expected, actual, DoubleComparer);
            Assert.Equal(input, loopback, DoubleComparer);
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(1.0, 1.0)]
        [InlineData(0.002, 0.01806592592)]
        [InlineData(0.18, 0.4949610761)]
        public void LCompanding(double input, double expected)
        {
            // arrange
            var companding = new LCompanding();

            // action
            var actual = companding.Companding(in input);
            var loopback = companding.InverseCompanding(in actual);

            // assert
            Assert.Equal(expected, actual, DoubleComparer);
            Assert.Equal(input, loopback, DoubleComparer);
        }
    }
}
