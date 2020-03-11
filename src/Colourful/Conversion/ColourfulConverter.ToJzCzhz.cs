using System;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to JzCzhz color
        /// </summary>
        public JzCzhzColor ToJzCzhz(in RGBColor color)
        {
            var jzazbzColor = ToJzazbz(in color);
            var result = ToJzCzhz(in jzazbzColor);
            return result;
        }

        /// <summary>
        /// Convert to JzCzhz color
        /// </summary>
        public JzCzhzColor ToJzCzhz(in LinearRGBColor color)
        {
            var jzazbzColor = ToJzazbz(in color);
            var result = ToJzCzhz(in jzazbzColor);
            return result;
        }

        /// <summary>
        /// Convert to JzCzhz color
        /// </summary>
        public JzCzhzColor ToJzCzhz(in XYZColor color)
        {
            var jzazbzColor = ToJzazbz(in color);
            var result = ToJzCzhz(in jzazbzColor);
            return result;
        }

        /// <summary>
        /// Convert to JzCzhz color
        /// </summary>
        public JzCzhzColor ToJzCzhz(in xyYColor color)
        {
            var jzazbzColor = ToJzazbz(in color);
            var result = ToJzCzhz(in jzazbzColor);
            return result;
        }

        /// <summary>
        /// Convert to JzCzhz color
        /// </summary>
        public JzCzhzColor ToJzCzhz(in LabColor color)
        {
            var jzazbzColor = ToJzazbz(in color);
            var result = ToJzCzhz(in jzazbzColor);
            return result;
        }

        /// <summary>
        /// Convert to JzCzhz color
        /// </summary>
        public JzCzhzColor ToJzCzhz(in LChabColor color)
        {
            var jzazbzColor = ToJzazbz(in color);
            var result = ToJzCzhz(in jzazbzColor);
            return result;
        }

        /// <summary>
        /// Convert to JzCzhz color
        /// </summary>
        public JzCzhzColor ToJzCzhz(in HunterLabColor color)
        {
            var jzazbzColor = ToJzazbz(in color);
            var result = ToJzCzhz(in jzazbzColor);
            return result;
        }

        /// <summary>
        /// Convert to JzCzhz color
        /// </summary>
        public JzCzhzColor ToJzCzhz(in LuvColor color)
        {
            var jzazbzColor = ToJzazbz(in color);
            var result = ToJzCzhz(in jzazbzColor);
            return result;
        }

        /// <summary>
        /// Convert to JzCzhz color
        /// </summary>
        public JzCzhzColor ToJzCzhz(in LChuvColor color)
        {
            var jzazbzColor = ToJzazbz(in color);
            var result = ToJzCzhz(in jzazbzColor);
            return result;
        }

        /// <summary>
        /// Convert to JzCzhz color
        /// </summary>
        public JzCzhzColor ToJzCzhz(in LMSColor color)
        {
            var jzazbzColor = ToJzazbz(in color);
            var result = ToJzCzhz(in jzazbzColor);
            return result;
        }

        /// <summary>
        /// Convert to JzCzhz color
        /// </summary>
        public JzCzhzColor ToJzCzhz(in JzazbzColor color)
        {
            var converter = JzazbzToJzCzhzConverter.Default;
            var result = converter.Convert(in color);
            return result;
        }

        /// <summary>
        /// Convert to JzCzhz color
        /// </summary>
        public JzCzhzColor ToJzCzhz<T>(T color) where T : IColorVector
        {
            switch (color)
            {
                case RGBColor typedColor:
                    return ToJzCzhz(in typedColor);
                case LinearRGBColor typedColor:
                    return ToJzCzhz(in typedColor);
                case XYZColor typedColor:
                    return ToJzCzhz(in typedColor);
                case xyYColor typedColor:
                    return ToJzCzhz(in typedColor);
                case HunterLabColor typedColor:
                    return ToJzCzhz(in typedColor);
                case LabColor typedColor:
                    return ToJzCzhz(in typedColor);
                case LChabColor typedColor:
                    return ToJzCzhz(in typedColor);
                case LuvColor typedColor:
                    return ToJzCzhz(in typedColor);
                case LChuvColor typedColor:
                    return ToJzCzhz(in typedColor);
                case LMSColor typedColor:
                    return ToJzCzhz(in typedColor);
                case JzazbzColor typedColor:
                    return ToJzCzhz(in typedColor);
                case JzCzhzColor typedColor:
                    return typedColor;
                default:
                    throw new ArgumentException($"Cannot accept type '{typeof(T)}'.", nameof(color));
            }
        }
    }
}