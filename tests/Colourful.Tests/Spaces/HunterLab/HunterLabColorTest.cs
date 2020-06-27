using Colourful.Tests.Assertions;
using Xunit;

namespace Colourful.Tests
{
    public class HunterLabColorTest
    {
        [Fact]
        public void Equals_Same()
        {
            var first = new HunterLabColor(l: 10, a: 20.5, b: 45.445);
            var second = new HunterLabColor(l: 10, a: 20.5, b: 45.445);
            CustomAssert.EqualsWithHashCode(first, second);
        }
        
        [Fact]
        public void Equals_Different()
        {
            var first = new HunterLabColor(l: 11, a: 20.5, b: 45.445);
            var second = new HunterLabColor(l: 10, a: 20.5, b: 45.445);
            CustomAssert.NotEqualsWithHashCode(first, second);
        }

        [Fact]
        public void VectorCtor()
        {
            var first = new HunterLabColor(l: 10, a: 20.5, b: 45.445);
            var vector = new[] { 10, 20.5, 45.445 };
            var second = new HunterLabColor(vector);
            CustomAssert.EqualsWithHashCode(first, second);
            Assert.Equal(vector, second.Vector);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new HunterLabColor(l: 10, a: 20.5, b: 45.445);
            Assert.Equal("HunterLab [L=10, a=20.5, b=45.45]", color.ToString());
        }
    }
}