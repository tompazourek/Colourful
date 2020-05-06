using System.Collections.Generic;
using System.Linq;
using Colourful.Conversion;
using Xunit;
using DataRow = Colourful.Tests.ColorCheckerCalculatorData.Row;

namespace Colourful.Tests
{
    public class ColorCheckerCalculatorTest
    {
        public static readonly IEnumerable<object[]> TestData = ColorCheckerCalculatorData.GetData().Select(x => new[] { x });

        public ColourfulConverter Converter => new ColourfulConverter
        {
            WhitePoint = Illuminants.C,
            TargetRGBWorkingSpace = RGBWorkingSpaces.sRGB,
            TargetLabWhitePoint = Illuminants.C,
            TargetLuvWhitePoint = Illuminants.C,
            ChromaticAdaptation = new VonKriesChromaticAdaptation()
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void Convert_Lab_to_XYZ(DataRow row)
        {
            var inputLab = row.GetLabColor();
            var expectedXYZ = row.GetXYZColor();
            var actualXYZ = Converter.ToXYZ(in inputLab);

            Assert.Equal(expectedXYZ, actualXYZ, new ColorVectorComparer(new DoubleDeltaComparer(0.000001)));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Convert_Lab_to_RGB(DataRow row)
        {
            var inputLab = row.GetLabColor();
            var expectedRGB = row.GetRGBColor();
            var actualRGB = Converter.ToRGB(in inputLab);
            Assert.Equal(expectedRGB, actualRGB, new ColorVectorComparer(((IComparer<double>)new DoubleDeltaComparer(0.00912)).CropRange(0, 1)));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Convert_Lab_to_xyY(DataRow row)
        {
            var inputLab = row.GetLabColor();
            var expectedxyY = row.GetxyYColor();
            var actualxyY = Converter.ToxyY(in inputLab);

            Assert.Equal(expectedxyY, actualxyY, new ColorVectorComparer(new DoubleDeltaComparer(0.000001)));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Convert_Lab_to_LChab(DataRow row)
        {
            var inputLab = row.GetLabColor();
            var expectedLChab = row.GetLChabColor();
            var actualLChab = Converter.ToLChab(in inputLab);

            Assert.Equal(expectedLChab, actualLChab, new ColorVectorComparer(new DoubleDeltaComparer(0.00017)));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Convert_Lab_to_LChuv(DataRow row)
        {
            var inputLab = row.GetLabColor();
            var expectedLChuv = row.GetLChuvColor();
            var actualLChuv = Converter.ToLChuv(in inputLab);

            Assert.Equal(expectedLChuv, actualLChuv, new ColorVectorComparer(new DoubleDeltaComparer(0.00022)));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Convert_Lab_to_Luv(DataRow row)
        {
            var inputLab = row.GetLabColor();
            var expectedLuv = row.GetLuvColor();
            var actualLuv = Converter.ToLuv(in inputLab);

            Assert.Equal(expectedLuv, actualLuv, new ColorVectorComparer(new DoubleDeltaComparer(0.00000105)));
        }
    }
}