using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Colourful.Tests
{
    public class EqualsTest
    {

        [Fact]
        public void ChromaticityCoordinates()
        {
            var first = new xyChromaticityCoordinates(1, 0.445);
            var second = new xyChromaticityCoordinates(1, 0.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void HunterLabColor()
        {
            var first = new HunterLabColor(10, 20.5, 45.445);
            var second = new HunterLabColor(10, 20.5, 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void LabColor()
        {
            var first = new LabColor(10, 20.5, 45.445);
            var second = new LabColor(10, 20.5, 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void LChabColor()
        {
            var first = new LChabColor(10, 20.5, 45.445);
            var second = new LChabColor(10, 20.5, 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void LuvColor()
        {
            var first = new LuvColor(10, 20.5, 45.445);
            var second = new LuvColor(10, 20.5, 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void LChuvColor()
        {
            var first = new LChuvColor(10, 20.5, 45.445);
            var second = new LChuvColor(10, 20.5, 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void RGBColor()
        {
            var first = new RGBColor(0, 0.5, 0.445);
            var second = new RGBColor(0, 0.5, 0.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void LinearRGBColor()
        {
            var first = new LinearRGBColor(0, 0.5, 0.445);
            var second = new LinearRGBColor(0, 0.5, 0.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void LMSColor()
        {
            var first = new LMSColor(0, 0.5, 0.445);
            var second = new LMSColor(0, 0.5, 0.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void xyYColor()
        {
            var first = new xyYColor(0, 0.5, 0.445);
            var second = new xyYColor(0, 0.5, 0.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void XYZColor()
        {
            var first = new XYZColor(0, 0.5, 0.445);
            var second = new XYZColor(0, 0.5, 0.445);
            Assert.Equal(first, (object)second);
        }
    }
}
