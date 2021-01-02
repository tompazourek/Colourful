using Colourful.Tests.Assertions;
using Xunit;

namespace Colourful.Tests
{
    public class XYZColorTest
    {
        [Fact]
        public void Equals_Same()
        {
            var first = new XYZColor(x: .1, y: .205, z: .45445);
            var second = new XYZColor(x: .1, y: .205, z: .45445);
            CustomAssert.EqualsWithHashCode(first, second);
        }

        [Fact]
        public void Equals_Different()
        {
            var first = new XYZColor(x: .2, y: .205, z: .45445);
            var second = new XYZColor(x: .1, y: .205, z: .45445);
            CustomAssert.NotEqualsWithHashCode(first, second);
        }

        [Fact]
        public void VectorCtor()
        {
            var first = new XYZColor(x: .2, y: .205, z: .45445);
            var vector = new[] { .2, .205, .45445 };
            var second = new XYZColor(vector);
            CustomAssert.EqualsWithHashCode(first, second);
            Assert.Equal(vector, second.Vector);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new XYZColor(x: .1, y: .205, z: .45445);
            Assert.Equal("XYZ [X=0.1, Y=0.21, Z=0.45]", color.ToString());
        }
        
        [Fact]
        public void Dctor()
        {
            const double x1 = .1;
            const double y1 = .205;
            const double z1 = .45445;
            var (x2, y2, z2) = new XYZColor(x1, y1, z1);
            Assert.Equal(x1, x2);
            Assert.Equal(y1, y2);
            Assert.Equal(z1, z2);
        }
    }
}