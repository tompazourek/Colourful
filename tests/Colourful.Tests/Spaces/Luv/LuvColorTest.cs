using Colourful.Tests.Assertions;
using Xunit;

namespace Colourful.Tests
{
    public class LuvColorTest
    {
        [Fact]
        public void Equals_Same()
        {
            var first = new LuvColor(l: 10, u: 20.5, v: 45.445);
            var second = new LuvColor(l: 10, u: 20.5, v: 45.445);
            CustomAssert.EqualsWithHashCode(first, second);
        }
        
        [Fact]
        public void Equals_Different()
        {
            var first = new LuvColor(l: 11, u: 20.5, v: 45.445);
            var second = new LuvColor(l: 10, u: 20.5, v: 45.445);
            CustomAssert.NotEqualsWithHashCode(first, second);
        }

        [Fact]
        public void VectorCtor()
        {
            var first = new LuvColor(l: 10, u: 20.5, v: 45.445);
            var vector = new[] { 10, 20.5, 45.445 };
            var second = new LuvColor(vector);
            CustomAssert.EqualsWithHashCode(first, second);
            Assert.Equal(vector, second.Vector);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new LuvColor(l: 10, u: 20.5, v: 45.445);
            Assert.Equal("Luv [L=10, u=20.5, v=45.45]", color.ToString());
        }

        [Fact]
        public void Dctor()
        {
            const double l1 = 10;
            const double u1 = 20.5;
            const double v1 = 45.445;
            var (l2, u2, v2) = new LuvColor(l1, u1, v1);
            Assert.Equal(l1, l2);
            Assert.Equal(u1, u2);
            Assert.Equal(v1, v2);
        }
    }
}