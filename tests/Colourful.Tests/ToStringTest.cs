using Xunit;

namespace Colourful.Tests
{
    public class ToStringTest
    {
        [Fact]
        public void xyChromaticity()
        {
            var xyChromaticity = new xyChromaticity(x: 1, y: 0.445);
            Assert.Equal("xy [x=1, y=0.45]", xyChromaticity.ToString());
        }

        [Fact]
        public void HunterLabColor()
        {
            var color = new HunterLabColor(l: 10, a: 20.5, b: 45.445);
            Assert.Equal("HunterLab [L=10, a=20.5, b=45.45]", color.ToString());
        }

        [Fact]
        public void LabColor()
        {
            var color = new LabColor(l: 10, a: 20.5, b: 45.445);
            Assert.Equal("Lab [L=10, a=20.5, b=45.45]", color.ToString());
        }

        [Fact]
        public void LChabColor()
        {
            var color = new LChabColor(l: 10, c: 20.5, h: 45.445);
            Assert.Equal("LChab [L=10, C=20.5, h=45.45]", color.ToString());
        }

        [Fact]
        public void RGBColor()
        {
            var color = new RGBColor(r: 0, g: 0.5, b: 0.445);
            Assert.Equal("RGB [R=0, G=0.5, B=0.45]", color.ToString());
        }

        [Fact]
        public void XYZColor()
        {
            var color = new XYZColor(x: 0, y: 0.5, z: 0.445);
            Assert.Equal("XYZ [X=0, Y=0.5, Z=0.45]", color.ToString());
        }
    }
}