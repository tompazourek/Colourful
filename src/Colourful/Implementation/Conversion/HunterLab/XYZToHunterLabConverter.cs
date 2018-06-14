using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="HunterLabColor" /> to <see cref="XYZColor" />.
    /// </summary>
    public sealed class XYZToHunterLabConverter : XYZAndHunterLabConverterBase, IColorConversion<XYZColor, HunterLabColor>
    {
        /// <summary>
        /// Construct with <see cref="HunterLabColor.DefaultWhitePoint" />
        /// </summary>
        public XYZToHunterLabConverter()
            : this(HunterLabColor.DefaultWhitePoint)
        {
        }

        /// <summary>
        /// Construct with arbitrary white point
        /// </summary>
        public XYZToHunterLabConverter(XYZColor labWhitePoint)
        {
            HunterLabWhitePoint = labWhitePoint;
        }

        /// <summary>
        /// Target reference white. When not set, <see cref="LabColor.DefaultWhitePoint" /> is used.
        /// </summary>
        public XYZColor HunterLabWhitePoint { get; }

        /// <summary>
        /// Converts from <see cref="HunterLabColor" /> to <see cref="XYZColor" />.
        /// </summary>
        public HunterLabColor Convert(in XYZColor input)
        {
            // conversion algorithm described here: http://en.wikipedia.org/wiki/Lab_color_space#Hunter_Lab
            double X = input.X, Y = input.Y, Z = input.Z;
            double Xn = HunterLabWhitePoint.X, Yn = HunterLabWhitePoint.Y, Zn = HunterLabWhitePoint.Z;

            var Ka = ComputeKa(HunterLabWhitePoint);
            var Kb = ComputeKb(HunterLabWhitePoint);

            var L = 100 * Math.Sqrt(Y / Yn);
            var a = Ka * ((X / Xn - Y / Yn) / Math.Sqrt(Y / Yn));
            var b = Kb * ((Y / Yn - Z / Zn) / Math.Sqrt(Y / Yn));

            if (double.IsNaN(a))
                a = 0;

            if (double.IsNaN(b))
                b = 0;

            var output = new HunterLabColor(L, a, b, HunterLabWhitePoint);
            return output;
        }
    }
}