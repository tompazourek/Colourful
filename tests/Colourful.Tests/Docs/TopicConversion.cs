using Xunit;

namespace Colourful.Tests.Docs
{
    public class TopicConversion
    {
        [Fact]
        public void Examples()
        {
            Assert.Throws<MissingConversionMetadataException>(() =>
            {
                var rgbToXyz = new ConverterBuilder().FromRGB().ToXYZ().Build();
            });

            Assert.Throws<MissingConversionMetadataException>(() =>
            {
                var xyzToRgb = new ConverterBuilder().FromXYZ().ToRGB().Build();
            });

            Assert.Throws<MissingConversionMetadataException>(() =>
            {
                // assumes the source color is sRGB (which has the D65 white point), but we don't know the target white point, so we get MissingConversionMetadataException
                var converter1 = new ConverterBuilder().FromRGB().ToXYZ().Build();
            });

            // assumes the source color is sRGB (which has the D65 white point), target will be XYZ with the D65 white point, same as sRGB, so we won't need to perform chromatic adaptation
            var converter2 = new ConverterBuilder().FromRGB().ToXYZ(Illuminants.D65).Build();

            // assumes the source color is sRGB (which has the D65 white point), target will be XYZ with the D50 white point, chromatic adaptation from D65 to D50 will be performed during the conversion 
            var converter3 = new ConverterBuilder().FromRGB().ToXYZ(Illuminants.D50).Build();

            // assumes the source color is ProPhoto RGB (which has D50 white point), target will be XYZ with the D50 white point, because both white points are the same, no chromatic adaptation will be performed
            var converter4 = new ConverterBuilder().FromRGB(RGBWorkingSpaces.ProPhotoRGB).ToXYZ(Illuminants.D50).Build();

            // create the converter once (e.g. store it in a field somewhere)
            var _rgbToXyz = new ConverterBuilder().FromRGB().ToXYZ(Illuminants.D65).Build();

            // use it for conversion
            var rgbColor1 = new RGBColor(1, 0, 0.5);
            var xyzColor1 = _rgbToXyz.Convert(rgbColor1);

            // not creating another converter, re-using the same
            var rgbColor2 = new RGBColor(0.7, 0.2, 1);
            var xyzColor2 = _rgbToXyz.Convert(rgbColor2);

            var xyzD50ToD65 = new ConverterBuilder().FromXYZ(Illuminants.D50).ToXYZ(Illuminants.D65).Build();
            var xyzAdapted = xyzD50ToD65.Convert(new XYZColor(0.5, 0.5, 0.5));

            var labD50ToD65 = new ConverterBuilder().FromLab(Illuminants.D50).ToLab(Illuminants.D65).Build();
            var labAdapted = labD50ToD65.Convert(new LabColor(50, -30, 75));

            var sRgbToAdobe = new ConverterBuilder().FromRGB(RGBWorkingSpaces.sRGB).ToRGB(RGBWorkingSpaces.AdobeRGB1998).Build();
            var rgbAdapted = sRgbToAdobe.Convert(new RGBColor(0.25, 0.65, 0.1));

            // adapt a XYZ color relative to D65 to a custom white point
            var xyzCustomWhitePoint = new XYZColor(0.95, 0.54, 0.72);
            var xyzCustomWhitePointAdapter = new ConverterBuilder().FromXYZ(Illuminants.D65).ToXYZ(xyzCustomWhitePoint).Build();
            var xyzCustomAdapted = xyzCustomWhitePointAdapter.Convert(new XYZColor(0.5, 0.5, 0.5));

            // Default; Bradford chromatic adaptation transform matrix (used in CMCCAT97)
            var matrix1 = new ConverterBuilder();

            // Von Kries chromatic adaptation transform matrix (Hunt-Pointer-Estevez adjusted for D65).
            var matrix2 = new ConverterBuilder(LMSTransformationMatrix.VonKriesHPEAdjusted);

            // Von Kries chromatic adaptation transform matrix (Hunt-Pointer-Estevez for equal energy).
            var matrix3 = new ConverterBuilder(LMSTransformationMatrix.VonKriesHPE);

            // XYZ scaling chromatic adaptation transform matrix.
            var matrix4 = new ConverterBuilder(LMSTransformationMatrix.XYZScaling);

            // Spectral sharpening and the Bradford transform.
            var matrix5 = new ConverterBuilder(LMSTransformationMatrix.BradfordSharp);

            // CMCCAT2000 (fitted from all available color data sets).
            var matrix6 = new ConverterBuilder(LMSTransformationMatrix.CMCCAT2000);

            // CAT02 (optimized for minimizing CIELAB differences).
            var matrix7 = new ConverterBuilder(LMSTransformationMatrix.CAT02);

            // custom LMS transformation matrix
            double[,] myCustomMatrix =
            {
                { 1.2694, -0.0988, -0.1706 },
                { -0.8364, 1.8006, 0.0357 },
                { 0.0297, -0.0315, 1.0018 },
            };
            new ConverterBuilder(myCustomMatrix);
        }
    }
}
