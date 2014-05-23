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
        public LChabColor ToLChab(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LChabColor result = ToLChab(xyzColor);
            return result;
        }

        public LChabColor ToLChab(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            LabColor labColor = ToLab(color);
            LChabColor result = ToLChab(labColor);
            return result;
        }

        public LChabColor ToLChab(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // adaptation to target lab white point (LabWhitePoint)
            LabColor adapted = IsChromaticAdaptationPerformed ? Adapt(color) : color;

            // conversion (perserving white point)
            var converter = new LabToLChabConverter();
            LChabColor result = converter.Convert(adapted);
            return result;
        }

        public LChabColor ToLChab(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LChabColor result = ToLChab(xyzColor);
            return result;
        }

        public LChabColor ToLChab(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LChabColor result = ToLChab(xyzColor);
            return result;
        }
    }
}