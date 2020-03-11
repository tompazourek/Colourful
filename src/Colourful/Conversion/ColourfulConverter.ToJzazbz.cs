using System;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to Jzazbz color
        /// </summary>
        public JzazbzColor ToJzazbz(in RGBColor color)
        {
            var xyzColor = ToXYZ(in color);
            var result = ToJzazbz(in xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Jzazbz color
        /// </summary>
        public JzazbzColor ToJzazbz(in LinearRGBColor color)
        {
            var xyzColor = ToXYZ(in color);
            var result = ToJzazbz(in xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Jzazbz color
        /// </summary>
        public JzazbzColor ToJzazbz(in XYZColor color)
        {
            // adaptation
            var adapted = !WhitePoint.Equals(XYZToJzazbzConverter.XYZWhitePoint) && IsChromaticAdaptationPerformed
                ? ChromaticAdaptation.Transform(in color, WhitePoint, in XYZToJzazbzConverter.XYZWhitePoint)
                : color;

            // conversion
            var converter = new XYZToJzazbzConverter();

            var result = converter.Convert(in adapted);
            return result;
        }

        /// <summary>
        /// Convert to Jzazbz color
        /// </summary>
        public JzazbzColor ToJzazbz(in xyYColor color)
        {
            var xyzColor = ToXYZ(in color);
            var result = ToJzazbz(in xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Jzazbz color
        /// </summary>
        public JzazbzColor ToJzazbz(in LabColor color)
        {
            var xyzColor = ToXYZ(in color);
            var result = ToJzazbz(in xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Jzazbz color
        /// </summary>
        public JzazbzColor ToJzazbz(in LChabColor color)
        {
            var xyzColor = ToXYZ(in color);
            var result = ToJzazbz(in xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Jzazbz color
        /// </summary>
        public JzazbzColor ToJzazbz(in HunterLabColor color)
        {
            var xyzColor = ToXYZ(in color);
            var result = ToJzazbz(in xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Jzazbz color
        /// </summary>
        public JzazbzColor ToJzazbz(in LuvColor color)
        {
            var xyzColor = ToXYZ(in color);
            var result = ToJzazbz(in xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Jzazbz color
        /// </summary>
        public JzazbzColor ToJzazbz(in LChuvColor color)
        {
            var xyzColor = ToXYZ(in color);
            var result = ToJzazbz(in xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Jzazbz color
        /// </summary>
        public JzazbzColor ToJzazbz(in LMSColor color)
        {
            var xyzColor = ToXYZ(in color);
            var result = ToJzazbz(in xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Jzazbz color
        /// </summary>
        public JzazbzColor ToJzazbz(in JzCzhzColor color)
        {
            var converter = JzCzhzToJzazbzConverter.Default;
            var result = converter.Convert(in color);
            return result;
        }

        /// <summary>
        /// Convert to Jzazbz color
        /// </summary>
        public JzazbzColor ToJzazbz<T>(T color) where T : IColorVector
        {
            switch (color)
            {
                case RGBColor typedColor:
                    return ToJzazbz(in typedColor);
                case LinearRGBColor typedColor:
                    return ToJzazbz(in typedColor);
                case XYZColor typedColor:
                    return ToJzazbz(in typedColor);
                case xyYColor typedColor:
                    return ToJzazbz(in typedColor);
                case HunterLabColor typedColor:
                    return ToJzazbz(in typedColor);
                case LabColor typedColor:
                    return ToJzazbz(in typedColor);
                case LChabColor typedColor:
                    return ToJzazbz(in typedColor);
                case LuvColor typedColor:
                    return ToJzazbz(in typedColor);
                case LChuvColor typedColor:
                    return ToJzazbz(in typedColor);
                case LMSColor typedColor:
                    return ToJzazbz(in typedColor);
                case JzazbzColor typedColor:
                    return typedColor;
                case JzCzhzColor typedColor:
                    return ToJzazbz(in typedColor);
                default:
                    throw new ArgumentException($"Cannot accept type '{typeof(T)}'.", nameof(color));
            }
        }
    }
}