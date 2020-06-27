using Colourful.Tests.Assertions;
using Xunit;

namespace Colourful.Tests
{
    public class RGBColorTest
    {
        [Fact]
        public void Equals_Same()
        {
            var first = new RGBColor(r: .1, g: .205, b: .45445);
            var second = new RGBColor(r: .1, g: .205, b: .45445);
            CustomAssert.EqualsWithHashCode(first, second);
        }

        [Fact]
        public void Equals_Different()
        {
            var first = new RGBColor(r: .2, g: .205, b: .45445);
            var second = new RGBColor(r: .1, g: .205, b: .45445);
            CustomAssert.NotEqualsWithHashCode(first, second);
        }
        
        [Fact]
        public void VectorCtor()
        {
            var first = new RGBColor(r: .2, g: .205, b: .45445);
            var vector = new[] { .2, .205, .45445 };
            var second = new RGBColor(vector);
            CustomAssert.EqualsWithHashCode(first, second);
            Assert.Equal(vector, second.Vector);
        }
        
        [Fact]
        public void FromGreyCtor()
        {
            var first = new RGBColor(r: .4, g: .4, b: .4);
            var second = RGBColor.FromGrey(.4);
            CustomAssert.EqualsWithHashCode(first, second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new RGBColor(r: .1, g: .205, b: .45445);
            Assert.Equal("RGB [R=0.1, G=0.21, B=0.45]", color.ToString());
        }
    }
}