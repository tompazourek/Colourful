using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        public LChuvColor ToLChuv(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        public LChuvColor ToLChuv(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        public LChuvColor ToLChuv(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var luvColor = ToLuv(color);
            var result = ToLChuv(luvColor);
            return result;
        }

        public LChuvColor ToLChuv(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        public LChuvColor ToLChuv(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        public LChuvColor ToLChuv(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        public LChuvColor ToLChuv(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        public LChuvColor ToLChuv(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // adaptation to target luv white point (LuvWhitePoint)
            var adapted = IsChromaticAdaptationPerformed ? Adapt(color) : color;

            // conversion (preserving white point)
            var converter = new LuvToLChuvConverter();
            var result = converter.Convert(adapted);
            return result;
        }

        public LChuvColor ToLChuv(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

#if (DYNAMIC)
        public LChuvColor ToLChuv<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as LChuvColor;

            if (converted != null)
            {
                return converted;
            }
            else
            {
                dynamic source = color;

                return ToLChuv(source);
            }
        }
#endif
    }
}