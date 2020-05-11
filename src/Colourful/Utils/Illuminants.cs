namespace Colourful
{
    /// <summary>
    /// Standard illuminants
    /// </summary>
    /// <remarks>
    /// Coefficients taken from:
    /// http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
    /// <br />
    /// Descriptions taken from:
    /// http://en.wikipedia.org/wiki/Standard_illuminant
    /// </remarks>
    public static class Illuminants
    {
        /// <summary>
        /// Incandescent / Tungsten
        /// </summary>
        public static readonly XYZColor A = new XYZColor(1.09850, 1, 0.35585);

        /// <summary>
        /// Direct sunlight at noon (obsolete)
        /// </summary>
        public static readonly XYZColor B = new XYZColor(0.99072, 1, 0.85223);

        /// <summary>
        /// Average / North sky Daylight (obsolete)
        /// </summary>
        public static readonly XYZColor C = new XYZColor(0.98074, 1, 1.18232);

        /// <summary>
        /// Horizon Light. ICC profile PCS
        /// </summary>
        public static readonly XYZColor D50 = new XYZColor(0.96422, 1, 0.82521);

        /// <summary>
        /// Mid-morning / Mid-afternoon Daylight
        /// </summary>
        public static readonly XYZColor D55 = new XYZColor(0.95682, 1, 0.92149);

        /// <summary>
        /// Noon Daylight: Television, sRGB color space
        /// </summary>
        public static readonly XYZColor D65 = new XYZColor(0.95047, 1, 1.08883);

        /// <summary>
        /// North sky Daylight
        /// </summary>
        public static readonly XYZColor D75 = new XYZColor(0.94972, 1, 1.22638);

        /// <summary>
        /// Equal energy
        /// </summary>
        public static readonly XYZColor E = new XYZColor(1, 1, 1);

        /// <summary>
        /// Cool White Fluorescent
        /// </summary>
        public static readonly XYZColor F2 = new XYZColor(0.99186, 1, 0.67393);

        /// <summary>
        /// D65 simulator, Daylight simulator
        /// </summary>
        public static readonly XYZColor F7 = new XYZColor(0.95041, 1, 1.08747);

        /// <summary>
        /// Philips TL84, Ultralume 40
        /// </summary>
        public static readonly XYZColor F11 = new XYZColor(1.00962, 1, 0.64350);
    }
}