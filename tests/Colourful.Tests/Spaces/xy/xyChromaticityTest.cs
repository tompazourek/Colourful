using Colourful.Tests.Assertions;
using Xunit;

namespace Colourful.Tests
{
    public class xyChromaticityTest
    {
        [Fact]
        public void Equals_Same()
        {
            var first = new xyChromaticity(x: .1, y: .205);
            var second = new xyChromaticity(x: .1, y: .205);
            CustomAssert.EqualsWithHashCode(first, second);
        }

        [Fact]
        public void Equals_Different()
        {
            var first = new xyChromaticity(x: .2, y: .205);
            var second = new xyChromaticity(x: .1, y: .205);
            CustomAssert.NotEqualsWithHashCode(first, second);
        }

        [Fact]
        public void VectorCtor()
        {
            var first = new xyChromaticity(x: .1, y: .205);
            var vector = new[] { .1, .205 };
            var second = new xyChromaticity(vector);
            CustomAssert.EqualsWithHashCode(first, second);
            Assert.Equal(vector, second.Vector);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new xyChromaticity(x: .1, y: .205);
            Assert.Equal("xy [x=0.1, y=0.21]", color.ToString());
        }

        [Fact]
        public void Dctor()
        {
            const double x1 = 10;
            const double y1 = 20.5;
            var (x2, y2) = new xyChromaticity(x1, y1);
            Assert.Equal(x1, x2);
            Assert.Equal(y1, y2);
        }
    }
}