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
        public xyYColor ToxyY(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            xyYColor result = ToxyY(xyzColor);
            return result;
        }

        public xyYColor ToxyY(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion
            var converter = new xyYAndXYZConverter();
            var result = converter.Convert(color);
            return result;
        }

        public xyYColor ToxyY(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            xyYColor result = ToxyY(xyzColor);
            return result;
        }

        public xyYColor ToxyY(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            xyYColor result = ToxyY(xyzColor);
            return result;
        }

        public xyYColor ToxyY(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            xyYColor result = ToxyY(xyzColor);
            return result;
        }

        public xyYColor ToxyY(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            xyYColor result = ToxyY(xyzColor);
            return result;
        }
    }
}