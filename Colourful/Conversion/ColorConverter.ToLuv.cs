using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColorConverter
    {
        public LuvColor ToLuv(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion to XYZ (incl. adaptation to current white point (WhitePoint))
            XYZColor xyzColor = ToXYZ(color);

            // conversion to Luv (incl. adaptation to lab white point (LuvWhitePoint))
            LuvColor result = ToLuv(xyzColor);
            return result;
        }

        public LuvColor ToLuv(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // adaptation
            XYZColor adapted = !WhitePoint.Equals(TargetLuvWhitePoint) && IsChromaticAdaptationPerformed
                ? ChromaticAdaptation.Transform(color, WhitePoint, TargetLuvWhitePoint)
                : color;

            // conversion
            var converter = new XYZToLuvConverter(TargetLuvWhitePoint);
            LuvColor result = converter.Convert(adapted);
            return result;
        }

        public LuvColor ToLuv(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion to XYZ (incl. adaptation to current white point (WhitePoint))
            XYZColor xyzColor = ToXYZ(color);

            // conversion to Luv (incl. adaptation to lab white point (LuvWhitePoint))
            LuvColor result = ToLuv(xyzColor);
            return result;
        }

        public LuvColor ToLuv(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion to XYZ (incl. adaptation to current white point (WhitePoint))
            XYZColor xyzColor = ToXYZ(color);

            // conversion to Luv (incl. adaptation to lab white point (LuvWhitePoint))
            LuvColor result = ToLuv(xyzColor);
            return result;
        }

        public LuvColor ToLuv(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion to XYZ (incl. adaptation to current white point (WhitePoint))
            XYZColor xyzColor = ToXYZ(color);

            // conversion to Luv (incl. adaptation to lab white point (LuvWhitePoint))
            LuvColor result = ToLuv(xyzColor);
            return result;
        }
    }
}