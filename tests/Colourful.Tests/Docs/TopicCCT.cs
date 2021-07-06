using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Colourful.Tests.Comparers;
using Xunit;

// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable ConvertToConstant.Local

namespace Colourful.Tests.Docs
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class TopicCCT
    {
        private static readonly IEqualityComparer<double> DoubleComparer = new DoubleRoundingComparer(precision: 6);

        [Fact]
        public void HowToComputeColorFromCCT()
        {
            double temperature = 3000; // in K
            xyChromaticity chromaticity = CCTConverter.GetChromaticityOfCCT(temperature);

            // asserts
            Assert.Equal(0.43657898148148144, chromaticity.x, DoubleComparer);
            Assert.Equal(0.4041745241092054, chromaticity.y, DoubleComparer);

            IColorConverter<xyChromaticity, RGBColor> converter = new ConverterBuilder().Fromxy(Illuminants.D65).ToRGB(RGBWorkingSpaces.sRGB).Build();
            RGBColor color = converter.Convert(chromaticity);

            // asserts
            Assert.Equal(1.282323002024544, color.R, DoubleComparer);
            Assert.Equal(0.92870160729369322, color.G, DoubleComparer);
            Assert.Equal(0.55886769485605214, color.B, DoubleComparer);

            color = color.NormalizeIntensity();

            // asserts
            Assert.Equal(1, color.R, DoubleComparer);
            Assert.Equal(0.72423375844264681, color.G, DoubleComparer);
            Assert.Equal(0.4358244326692311, color.B, DoubleComparer);

            color.ToRGB8Bit(out var r, out var g, out var b);

            // asserts
            Assert.Equal(255, r);
            Assert.Equal(185, g);
            Assert.Equal(111, b);
        }

        [Fact]
        public void HowToComputeTemperatureOfAColor()
        {
            RGBColor color = RGBColor.FromRGB8Bit(255, 121, 0);
            IColorConverter<RGBColor, xyChromaticity> converter = new ConverterBuilder().FromRGB(RGBWorkingSpaces.sRGB).Toxy(Illuminants.D65).Build();
            xyChromaticity chromaticity = converter.Convert(color); // xy [x=0.55, y=0.4]

            // asserts
            Assert.Equal(0.55117772083302841, chromaticity.x, DoubleComparer);
            Assert.Equal(0.40053533933847751, chromaticity.y, DoubleComparer);

            double temperature = CCTConverter.GetCCTOfChromaticity(chromaticity); // 1293 K

            // asserts
            Assert.Equal(1293.0206041090441, temperature, DoubleComparer);
        }
    }
}
