using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColorConverter
    {
        public LChabColor ToLChab(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion to XYZ (incl. adaptation)
            XYZColor xyzColor = ToXYZ(color);

            // conversion to LChab (incl. adaptation to lab white point (LabWhitePoint))
            LChabColor result = ToLChab(xyzColor);
            return result;
        }

        public LChabColor ToLChab(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion to Lab (incl. adaptation to lab white point (LabWhitePoint))
            LabColor labColor = ToLab(color);

            // conversion to LChab (perserving white point)
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
    }
}