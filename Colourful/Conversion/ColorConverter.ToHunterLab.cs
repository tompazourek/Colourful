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
        public HunterLabColor ToHunterLab(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            HunterLabColor result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // adaptation
            XYZColor adapted = !WhitePoint.Equals(TargetHunterLabWhitePoint) && IsChromaticAdaptationPerformed
                ? ChromaticAdaptation.Transform(color, WhitePoint, TargetHunterLabWhitePoint)
                : color;

            // conversion
            var converter = new XYZToHunterLabConverter(TargetHunterLabWhitePoint);
            HunterLabColor result = converter.Convert(adapted);
            return result;
        }

        public HunterLabColor ToHunterLab(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            HunterLabColor result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            HunterLabColor result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            HunterLabColor result = ToHunterLab(xyzColor);
            return result;
        }
    }
}