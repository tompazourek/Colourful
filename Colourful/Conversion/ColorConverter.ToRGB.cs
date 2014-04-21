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
        public RGBColor ToRGB(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // adaptation
            XYZColor adapted = TargetRGBWorkingSpace.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? color
                : ChromaticAdaptation.Transform(color, WhitePoint, TargetRGBWorkingSpace.WhitePoint);

            // conversion
            var converter = new XYZToRGBConverter(TargetRGBWorkingSpace);
            RGBColor result = converter.Convert(adapted);
            return result;
        }

        public RGBColor ToRGB(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion to XYZ
            XYZColor xyzColor = ToXYZ(color);

            // conversion to RGB
            RGBColor result = ToRGB(xyzColor);
            return result;
        }

        public RGBColor ToRGB(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion to XYZ
            XYZColor xyzColor = ToXYZ(color);

            // conversion to RGB
            RGBColor result = ToRGB(xyzColor);
            return result;
        }
    }
}