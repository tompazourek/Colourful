using System.Collections.Generic;
using Colourful.Tests.Assertions;
using Colourful.Tests.Comparers;
using Xunit;

namespace Colourful.Tests
{
    public class LinearRGBColorTest
    {
        private static readonly IEqualityComparer<double> DoubleComparer = new DoubleRoundingComparer(precision: 12);

        [Fact]
        public void Equals_Same()
        {
            var first = new LinearRGBColor(r: .1, g: .205, b: .45445);
            var second = new LinearRGBColor(r: .1, g: .205, b: .45445);
            CustomAssert.EqualsWithHashCode(first, second);
        }

        [Fact]
        public void Equals_Different()
        {
            var first = new LinearRGBColor(r: .2, g: .205, b: .45445);
            var second = new LinearRGBColor(r: .1, g: .205, b: .45445);
            CustomAssert.NotEqualsWithHashCode(first, second);
        }

        [Fact]
        public void VectorCtor()
        {
            var first = new LinearRGBColor(r: .2, g: .205, b: .45445);
            var vector = new[] { .2, .205, .45445 };
            var second = new LinearRGBColor(vector);
            CustomAssert.EqualsWithHashCode(first, second);
            Assert.Equal(vector, second.Vector);
        }

        [Fact]
        public void FromGrayCtor()
        {
            var first = new LinearRGBColor(r: .4, g: .4, b: .4);
            var second = LinearRGBColor.FromGray(.4);
            CustomAssert.EqualsWithHashCode(first, second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new LinearRGBColor(r: .1, g: .205, b: .45445);
            Assert.Equal("LinearRGB [R=0.1, G=0.21, B=0.45]", color.ToString());
        }

        [Fact]
        public void Dctor()
        {
            const double r1 = .1;
            const double g1 = .205;
            const double b1 = .45445;
            var (r2, g2, b2) = new LinearRGBColor(r1, g1, b1);
            Assert.Equal(r1, r2);
            Assert.Equal(g1, g2);
            Assert.Equal(b1, b2);
        }

        [Fact]
        public void Clamp()
        {
            var (r, g, b) = new LinearRGBColor(1.1, -0.5, 0.01).Clamp();
            Assert.Equal(1, r);
            Assert.Equal(0, g);
            Assert.Equal(0.01, b);
        }

        [Fact]
        public void NormalizeIntensity()
        {
            var (r, g, b) = new LinearRGBColor(1.282323002024544, 0.92870160729369322, 0.55886769485605214).NormalizeIntensity();
            Assert.Equal(1, r);
            Assert.Equal(0.72423375844264681, g, DoubleComparer);
            Assert.Equal(0.4358244326692311, b, DoubleComparer);
        }
    }
}
