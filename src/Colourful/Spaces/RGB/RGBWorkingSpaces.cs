using Colourful.Internals;

namespace Colourful
{
    /// <remarks>
    /// Chromaticity of primaries taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public static class RGBWorkingSpaces
    {
        /// <summary>
        /// sRGB.
        /// </summary>
        /// <remarks>
        /// Uses proper companding function, according to:
        /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
        /// </remarks>
        public static readonly RGBWorkingSpace sRGB = new RGBWorkingSpace(in Illuminants.D65, new sRGBCompanding(), new RGBPrimaries(new xyChromaticity(x: 0.6400, y: 0.3300), new xyChromaticity(x: 0.3000, y: 0.6000), new xyChromaticity(x: 0.1500, y: 0.0600)));

        /// <summary>
        /// Simplified sRGB (uses <see cref="GammaCompanding">gamma companding</see> instead of <see cref="sRGBCompanding" />).
        /// See also <see cref="sRGB" />.
        /// </summary>
        public static readonly RGBWorkingSpace sRGBSimplified = new RGBWorkingSpace(in Illuminants.D65, new GammaCompanding(gamma: 2.2), new RGBPrimaries(new xyChromaticity(x: 0.6400, y: 0.3300), new xyChromaticity(x: 0.3000, y: 0.6000), new xyChromaticity(x: 0.1500, y: 0.0600)));

        /// <summary>
        /// Rec. 709 (ITU-R Recommendation BT.709).
        /// </summary>
        public static readonly RGBWorkingSpace Rec709 = new RGBWorkingSpace(in Illuminants.D65, new Rec709Companding(), new RGBPrimaries(new xyChromaticity(x: 0.64, y: 0.33), new xyChromaticity(x: 0.30, y: 0.60), new xyChromaticity(x: 0.15, y: 0.06)));

        /// <summary>
        /// Rec. 2020 (ITU-R Recommendation BT.2020).
        /// </summary>
        public static readonly RGBWorkingSpace Rec2020 = new RGBWorkingSpace(in Illuminants.D65, new Rec2020Companding(), new RGBPrimaries(new xyChromaticity(x: 0.708, y: 0.292), new xyChromaticity(x: 0.170, y: 0.797), new xyChromaticity(x: 0.131, y: 0.046)));

        /// <summary>
        /// ECI RGB v2.
        /// </summary>
        public static readonly RGBWorkingSpace ECIRGBv2 = new RGBWorkingSpace(in Illuminants.D50, new LCompanding(), new RGBPrimaries(new xyChromaticity(x: 0.6700, y: 0.3300), new xyChromaticity(x: 0.2100, y: 0.7100), new xyChromaticity(x: 0.1400, y: 0.0800)));

        /// <summary>
        /// Adobe RGB (1998).
        /// </summary>
        public static readonly RGBWorkingSpace AdobeRGB1998 = new RGBWorkingSpace(in Illuminants.D65, new GammaCompanding(gamma: 2.2), new RGBPrimaries(new xyChromaticity(x: 0.6400, y: 0.3300), new xyChromaticity(x: 0.2100, y: 0.7100), new xyChromaticity(x: 0.1500, y: 0.0600)));

        /// <summary>
        /// Apple sRGB.
        /// </summary>
        public static readonly RGBWorkingSpace ApplesRGB = new RGBWorkingSpace(in Illuminants.D65, new GammaCompanding(gamma: 1.8), new RGBPrimaries(new xyChromaticity(x: 0.6250, y: 0.3400), new xyChromaticity(x: 0.2800, y: 0.5950), new xyChromaticity(x: 0.1550, y: 0.0700)));

        /// <summary>
        /// Best RGB.
        /// </summary>
        public static readonly RGBWorkingSpace BestRGB = new RGBWorkingSpace(in Illuminants.D50, new GammaCompanding(gamma: 2.2), new RGBPrimaries(new xyChromaticity(x: 0.7347, y: 0.2653), new xyChromaticity(x: 0.2150, y: 0.7750), new xyChromaticity(x: 0.1300, y: 0.0350)));

        /// <summary>
        /// Beta RGB.
        /// </summary>
        public static readonly RGBWorkingSpace BetaRGB = new RGBWorkingSpace(in Illuminants.D50, new GammaCompanding(gamma: 2.2), new RGBPrimaries(new xyChromaticity(x: 0.6888, y: 0.3112), new xyChromaticity(x: 0.1986, y: 0.7551), new xyChromaticity(x: 0.1265, y: 0.0352)));

        /// <summary>
        /// Bruce RGB.
        /// </summary>
        public static readonly RGBWorkingSpace BruceRGB = new RGBWorkingSpace(in Illuminants.D65, new GammaCompanding(gamma: 2.2), new RGBPrimaries(new xyChromaticity(x: 0.6400, y: 0.3300), new xyChromaticity(x: 0.2800, y: 0.6500), new xyChromaticity(x: 0.1500, y: 0.0600)));

        /// <summary>
        /// CIE RGB.
        /// </summary>
        public static readonly RGBWorkingSpace CIERGB = new RGBWorkingSpace(in Illuminants.E, new GammaCompanding(gamma: 2.2), new RGBPrimaries(new xyChromaticity(x: 0.7350, y: 0.2650), new xyChromaticity(x: 0.2740, y: 0.7170), new xyChromaticity(x: 0.1670, y: 0.0090)));

        /// <summary>
        /// ColorMatch RGB.
        /// </summary>
        public static readonly RGBWorkingSpace ColorMatchRGB = new RGBWorkingSpace(in Illuminants.D50, new GammaCompanding(gamma: 1.8), new RGBPrimaries(new xyChromaticity(x: 0.6300, y: 0.3400), new xyChromaticity(x: 0.2950, y: 0.6050), new xyChromaticity(x: 0.1500, y: 0.0750)));

        /// <summary>
        /// Don RGB 4.
        /// </summary>
        public static readonly RGBWorkingSpace DonRGB4 = new RGBWorkingSpace(in Illuminants.D50, new GammaCompanding(gamma: 2.2), new RGBPrimaries(new xyChromaticity(x: 0.6960, y: 0.3000), new xyChromaticity(x: 0.2150, y: 0.7650), new xyChromaticity(x: 0.1300, y: 0.0350)));

        /// <summary>
        /// Ekta Space PS5.
        /// </summary>
        public static readonly RGBWorkingSpace EktaSpacePS5 = new RGBWorkingSpace(in Illuminants.D50, new GammaCompanding(gamma: 2.2), new RGBPrimaries(new xyChromaticity(x: 0.6950, y: 0.3050), new xyChromaticity(x: 0.2600, y: 0.7000), new xyChromaticity(x: 0.1100, y: 0.0050)));

        /// <summary>
        /// NTSC RGB.
        /// </summary>
        public static readonly RGBWorkingSpace NTSCRGB = new RGBWorkingSpace(in Illuminants.C, new GammaCompanding(gamma: 2.2), new RGBPrimaries(new xyChromaticity(x: 0.6700, y: 0.3300), new xyChromaticity(x: 0.2100, y: 0.7100), new xyChromaticity(x: 0.1400, y: 0.0800)));

        /// <summary>
        /// PAL/SECAM RGB.
        /// </summary>
        public static readonly RGBWorkingSpace PALSECAMRGB = new RGBWorkingSpace(in Illuminants.D65, new GammaCompanding(gamma: 2.2), new RGBPrimaries(new xyChromaticity(x: 0.6400, y: 0.3300), new xyChromaticity(x: 0.2900, y: 0.6000), new xyChromaticity(x: 0.1500, y: 0.0600)));

        /// <summary>
        /// ProPhoto RGB.
        /// </summary>
        public static readonly RGBWorkingSpace ProPhotoRGB = new RGBWorkingSpace(in Illuminants.D50, new GammaCompanding(gamma: 1.8), new RGBPrimaries(new xyChromaticity(x: 0.7347, y: 0.2653), new xyChromaticity(x: 0.1596, y: 0.8404), new xyChromaticity(x: 0.0366, y: 0.0001)));

        /// <summary>
        /// SMPTE-C RGB.
        /// </summary>
        public static readonly RGBWorkingSpace SMPTECRGB = new RGBWorkingSpace(in Illuminants.D65, new GammaCompanding(gamma: 2.2), new RGBPrimaries(new xyChromaticity(x: 0.6300, y: 0.3400), new xyChromaticity(x: 0.3100, y: 0.5950), new xyChromaticity(x: 0.1550, y: 0.0700)));

        /// <summary>
        /// Wide Gamut RGB.
        /// </summary>
        public static readonly RGBWorkingSpace WideGamutRGB = new RGBWorkingSpace(in Illuminants.D50, new GammaCompanding(gamma: 2.2), new RGBPrimaries(new xyChromaticity(x: 0.7350, y: 0.2650), new xyChromaticity(x: 0.1150, y: 0.8260), new xyChromaticity(x: 0.1570, y: 0.0180)));
    }
}
