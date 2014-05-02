using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.ChromaticAdaptation;
using Colourful.Colors;
using Colourful.Conversion;
using NUnit.Framework;

namespace Colourful.Tests
{
    [TestFixture]
    public class ToStringTest
    {
        [Test]
        public void LabColor()
        {
            var color = new LabColor(10, 20.5, 45.445);
            Assert.AreEqual("Lab [L=10, a=20.5, b=45.45]", color.ToString());
        }

        [Test]
        public void LChabColor()
        {
            var color = new LChabColor(10, 20.5, 45.445);
            Assert.AreEqual("LChab [L=10, C=20.5, h=45.45]", color.ToString());
        }

        [Test]
        public void RGBColor()
        {
            var color = new RGBColor(0, 0.5, 0.445);
            Assert.AreEqual("RGB [R=0, G=0.5, B=0.45]", color.ToString());
        }

        [Test]
        public void XYZColor()
        {
            var color = new XYZColor(0, 0.5, 0.445);
            Assert.AreEqual("XYZ [X=0, Y=0.5, Z=0.45]", color.ToString());
        }

        [Test]
        public void HunterLabColor()
        {
            var color = new HunterLabColor(10, 20.5, 45.445);
            Assert.AreEqual("HunterLab [L=10, a=20.5, b=45.45]", color.ToString());
        }

        [Test]
        public void ChromaticityCoordinates()
        {
            var coordinates = new ChromaticityCoordinates(1, 0.445);
            Assert.AreEqual("xy [x=1, y=0.45]", coordinates.ToString());
        }
    }
}
