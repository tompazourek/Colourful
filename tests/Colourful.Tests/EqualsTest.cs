using Xunit;

namespace Colourful.Tests
{
    public class EqualsTest
    {
        [Fact]
        public void xyChromaticity()
        {
            var first = new xyChromaticity(x: 1, y: 0.445);
            var second = new xyChromaticity(x: 1, y: 0.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void HunterLabColor()
        {
            var first = new HunterLabColor(l: 10, a: 20.5, b: 45.445);
            var second = new HunterLabColor(l: 10, a: 20.5, b: 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void LabColor()
        {
            var first = new LabColor(l: 10, a: 20.5, b: 45.445);
            var second = new LabColor(l: 10, a: 20.5, b: 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void LChabColor()
        {
            var first = new LChabColor(l: 10, c: 20.5, h: 45.445);
            var second = new LChabColor(l: 10, c: 20.5, h: 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void LuvColor()
        {
            var first = new LuvColor(l: 10, u: 20.5, v: 45.445);
            var second = new LuvColor(l: 10, u: 20.5, v: 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void LChuvColor()
        {
            var first = new LChuvColor(l: 10, c: 20.5, h: 45.445);
            var second = new LChuvColor(l: 10, c: 20.5, h: 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void RGBColor()
        {
            var first = new RGBColor(r: 0, g: 0.5, b: 0.445);
            var second = new RGBColor(r: 0, g: 0.5, b: 0.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void LinearRGBColor()
        {
            var first = new LinearRGBColor(r: 0, g: 0.5, b: 0.445);
            var second = new LinearRGBColor(r: 0, g: 0.5, b: 0.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void LMSColor()
        {
            var first = new LMSColor(l: 0, m: 0.5, s: 0.445);
            var second = new LMSColor(l: 0, m: 0.5, s: 0.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void xyYColor()
        {
            var first = new xyYColor(x: 0, y: 0.5, Y: 0.445);
            var second = new xyYColor(x: 0, y: 0.5, Y: 0.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void XYZColor()
        {
            var first = new XYZColor(x: 0, y: 0.5, z: 0.445);
            var second = new XYZColor(x: 0, y: 0.5, z: 0.445);
            Assert.Equal(first, (object)second);
        }
    }
}