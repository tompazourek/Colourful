using System.Collections.Generic;
using Colourful.Internals;
using Colourful.Tests.Assertions;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests
{
    public class CompandingTest
    {
        private static readonly IEqualityComparer<double> DoubleComparer = new DoubleRoundingComparer(precision: 4);

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
            var actual = companding.ConvertToNonLinear(in input);
            var loopback = companding.ConvertToLinear(in actual);

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
            var actual = companding.ConvertToNonLinear(in input);
            var loopback = companding.ConvertToLinear(in actual);

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
            var actual = companding.ConvertToNonLinear(in input);
            var loopback = companding.ConvertToLinear(in actual);

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
            var companding = new GammaCompanding(gamma: 2.2);

            // action
            var actual = companding.ConvertToNonLinear(in input);
            var loopback = companding.ConvertToLinear(in actual);

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
            var actual = companding.ConvertToNonLinear(in input);
            var loopback = companding.ConvertToLinear(in actual);

            // assert
            Assert.Equal(expected, actual, DoubleComparer);
            Assert.Equal(input, loopback, DoubleComparer);
        }

        [Fact]
        public void ComplexCompandingEqualityTest()
        {
            var compandings1 = new List<ICompanding>
            {
                new LCompanding(),
                new Rec2020Companding(),
                new Rec709Companding(),
                new sRGBCompanding(),
                new GammaCompanding(gamma: 2.0),
                new GammaCompanding(gamma: 3.0),
            };

            // same as above, separate instances
            var compandings2 = new List<ICompanding>
            {
                new LCompanding(),
                new Rec2020Companding(),
                new Rec709Companding(),
                new sRGBCompanding(),
                new GammaCompanding(gamma: 2.0),
                new GammaCompanding(gamma: 3.0),
            };

            // check that same are the same and different are different
            for (var i = 0; i < compandings1.Count; i++)
            {
                var c1 = compandings1[i];

                for (var j = 0; j < compandings2.Count; j++)
                {
                    var c2 = compandings2[j];

                    if (i == j)
                    {
                        CustomAssert.EqualsWithHashCode(c1, c2);
                    }
                    else
                    {
                        CustomAssert.NotEqualsWithHashCode(c1, c2);
                    }
                }

                CustomAssert.NotEqualsWithHashCode(c1, (ICompanding)null);
            }
        }
    }
}
