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
        public LabColor ToLab(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LabColor result = ToLab(xyzColor);
            return result;
        }

        public LabColor ToLab(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // adaptation
            XYZColor adapted = !WhitePoint.Equals(TargetLabWhitePoint) && IsChromaticAdaptationPerformed
                ? ChromaticAdaptation.Transform(color, WhitePoint, TargetLabWhitePoint)
                : color;

            // conversion
            var converter = new XYZToLabConverter(TargetLabWhitePoint);
            LabColor result = converter.Convert(adapted);
            return result;
        }

        public LabColor ToLab(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion (perserving white point)
            var converter = new LChabToLabConverter();
            LabColor unadapted = converter.Convert(color);

            if (!IsChromaticAdaptationPerformed)
                return unadapted;

            // adaptation to target lab white point (LabWhitePoint)
            LabColor adapted = Adapt(unadapted);
            return adapted;
        }

        public LabColor ToLab(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LabColor result = ToLab(xyzColor);
            return result;
        }

        public LabColor ToLuv(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LabColor result = ToLab(xyzColor);
            return result;
        }
    }
}