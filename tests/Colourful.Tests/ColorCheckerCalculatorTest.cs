using System.Collections.Generic;
using System.Linq;
using Xunit;
using DataRow = Colourful.Tests.ColorCheckerCalculatorData.Row;

namespace Colourful.Tests
{
    public class ColorCheckerCalculatorTest
    {
        public static readonly IEnumerable<object[]> TestData = ColorCheckerCalculatorData.GetData().Select(x => new[] { x });

        [Theory]
        [MemberData(nameof(TestData))]
        public void Convert_Lab_to_XYZ(DataRow row)
        {
            var converter = new ConverterBuilder()
                .FromLab(Illuminants.C)
                .ToXYZ(Illuminants.C)
                .Build();

            var inputLab = row.GetLabColor();
            var expectedXYZ = row.GetXYZColor();
            var actualXYZ = converter.Convert(in inputLab);

            Assert.Equal(expectedXYZ, actualXYZ, new ColorVectorComparer(new DoubleDeltaComparer(delta: 0.000001)));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Convert_Lab_to_RGB(DataRow row)
        {
            var converter = new ConverterBuilder()
                .FromLab(Illuminants.C)
                .ToRGB(RGBWorkingSpaces.sRGB)
                .Build();

            var inputLab = row.GetLabColor();
            var expectedRGB = row.GetRGBColor();
            var actualRGB = converter.Convert(in inputLab);

            Assert.Equal(expectedRGB, actualRGB, new ColorVectorComparer(((IComparer<double>)new DoubleDeltaComparer(delta: 0.00912)).CropRange(min: 0, max: 1)));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Convert_Lab_to_xyY(DataRow row)
        {
            var converter = new ConverterBuilder()
                .FromLab(Illuminants.C)
                .ToxyY(Illuminants.C)
                .Build();

            var inputLab = row.GetLabColor();
            var expectedxyY = row.GetxyYColor();
            var actualxyY = converter.Convert(in inputLab);

            Assert.Equal(expectedxyY, actualxyY, new ColorVectorComparer(new DoubleDeltaComparer(delta: 0.000001)));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Convert_Lab_to_LChab(DataRow row)
        {
            var converter = new ConverterBuilder()
                .FromLab(Illuminants.C)
                .ToLChab(Illuminants.C)
                .Build();

            var inputLab = row.GetLabColor();
            var expectedLChab = row.GetLChabColor();
            var actualLChab = converter.Convert(in inputLab);

            Assert.Equal(expectedLChab, actualLChab, new ColorVectorComparer(new DoubleDeltaComparer(delta: 0.00017)));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Convert_Lab_to_LChuv(DataRow row)
        {
            var converter = new ConverterBuilder()
                .FromLab(Illuminants.C)
                .ToLChuv(Illuminants.C)
                .Build();

            var inputLab = row.GetLabColor();
            var expectedLChuv = row.GetLChuvColor();
            var actualLChuv = converter.Convert(in inputLab);

            Assert.Equal(expectedLChuv, actualLChuv, new ColorVectorComparer(new DoubleDeltaComparer(delta: 0.00022)));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Convert_Lab_to_Luv(DataRow row)
        {
            var converter = new ConverterBuilder()
                .FromLab(Illuminants.C)
                .ToLuv(Illuminants.C)
                .Build();

            var inputLab = row.GetLabColor();
            var expectedLuv = row.GetLuvColor();
            var actualLuv = converter.Convert(in inputLab);

            Assert.Equal(expectedLuv, actualLuv, new ColorVectorComparer(new DoubleDeltaComparer(delta: 0.00000105)));
        }
    }
}