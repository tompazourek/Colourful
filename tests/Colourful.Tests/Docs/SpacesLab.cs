using System.Diagnostics.CodeAnalysis;
using Xunit;

// ReSharper disable UnusedVariable
// ReSharper disable RedundantNameQualifier

namespace Colourful.Tests.Docs
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SpacesLab
    {
        [Fact]
        public void SamplesLab()
        {
            // red
            var c1 = new LabColor(53.9, 70.46, 41);

            // white
            var c2 = new LabColor(100, 0, 0);

            // gray
            var c3 = new LabColor(53.9, 0, 0);

            // black
            var c4 = new LabColor(0, 0, 0);
        }

        [Fact]
        public void RgbToLab()
        {
            var inputRgb = new RGBColor(0.937, 0.2, 0.251);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var rgbToLab = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToLab(Illuminants.D50).Build();
            var outputLab = rgbToLab.Convert(inputRgb); // Lab [L=53.9, a=70.46, b=41]

            Assert.Equal(53.8971420427677, outputLab.L);
            Assert.Equal(70.4560341801751, outputLab.a);
            Assert.Equal(40.999832380473279, outputLab.b);
        }

        [Fact]
        public void LabToRgb()
        {
            var inputLab = new LabColor(53.9, 70.46, 41);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var labToRgb = new ConverterBuilder().FromLab(Illuminants.D50).ToRGB(rgbWorkingSpace).Build();
            var outputRgb = labToRgb.Convert(inputLab); // RGB [R=0.94, G=0.2, B=0.25]

            Assert.Equal(0.9370552176858629, outputRgb.R);
            Assert.Equal(0.2000024500228637, outputRgb.G);
            Assert.Equal(0.25102657453857047, outputRgb.B);
        }

        [Fact]
        public void SamplesLChab()
        {
            // red
            var c1 = new LChabColor(53.9, 81.52, 30.2);

            // white
            var c2 = new LChabColor(100, 0, 0);

            // gray
            var c3 = new LChabColor(53.39, 0, 0);

            // black
            var c4 = new LChabColor(0, 0, 0);
        }

        [Fact]
        public void RgbToLChab()
        {
            var inputRgb = new RGBColor(0.937, 0.2, 0.251);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var rgbToLChab = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToLChab(Illuminants.D50).Build();
            var outputLChab = rgbToLChab.Convert(inputRgb); // LChab [L=53.9, C=81.52, h=30.2]

            Assert.Equal(53.8971420427677, outputLChab.L);
            Assert.Equal(81.51710868047827, outputLChab.C);
            Assert.Equal(30.196015762132554, outputLChab.h);
        }

        [Fact]
        public void LChabToRgb()
        {
            var inputLChab = new LChabColor(53.9, 81.52, 30.2);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var lChabToRgb = new ConverterBuilder().FromLChab(Illuminants.D50).ToRGB(rgbWorkingSpace).Build();
            var outputRgb = lChabToRgb.Convert(inputLChab); // RGB [R=0.94, G=0.2, B=0.25]

            Assert.Equal(0.9370373270715214, outputRgb.R);
            Assert.Equal(0.2000445268236402, outputRgb.G);
            Assert.Equal(0.2509818153127169, outputRgb.B);
        }


        [Fact]
        public void SamplesHunterLab()
        {
            // red
            var c1 = new HunterLabColor(46.1, 67.57, 22.58);

            // white
            var c2 = new HunterLabColor(100, 0, 0);

            // gray
            var c3 = new HunterLabColor(46.26, 0, 0);

            // black
            var c4 = new HunterLabColor(0, 0, 0);
        }

        [Fact]
        public void RgbToHunterLab()
        {
            var inputRgb = new RGBColor(0.937, 0.2, 0.251);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var rgbToHunterLab = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToHunterLab(Illuminants.C).Build();
            var outputHunterLab = rgbToHunterLab.Convert(inputRgb); // HunterLab [L=46.1, a=67.57, b=22.58]

            Assert.Equal(46.0952969853114, outputHunterLab.L);
            Assert.Equal(67.571834404459764, outputHunterLab.a);
            Assert.Equal(22.575202092155724, outputHunterLab.b);
        }

        [Fact]
        public void HunterLabToRgb()
        {
            var inputHunterLab = new HunterLabColor(46.1, 67.57, 22.58);
            var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

            var hunterLabToRgb = new ConverterBuilder().FromHunterLab(Illuminants.C).ToRGB(rgbWorkingSpace).Build();
            var outputRgb = hunterLabToRgb.Convert(inputHunterLab); // RGB [R=0.94, G=0.2, B=0.25]

            Assert.Equal(0.9370540094288826, outputRgb.R);
            Assert.Equal(0.20009009476086387, outputRgb.G);
            Assert.Equal(0.25097611676994847, outputRgb.B);
        }
    }
}
