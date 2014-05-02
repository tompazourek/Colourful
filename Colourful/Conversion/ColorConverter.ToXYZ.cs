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
        public XYZColor ToXYZ(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion
            var converter = new RGBToXYZConverter(color.WorkingSpace);
            XYZColor unadapted = converter.Convert(color);

            // adaptation
            XYZColor adapted = color.WorkingSpace.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WorkingSpace.WhitePoint);

            return adapted;
        }

        public XYZColor ToXYZ(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion
            var converter = new LabToXYZConverter();
            XYZColor unadapted = converter.Convert(color);

            // adaptation
            XYZColor adapted = color.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WhitePoint);

            return adapted;
        }

        public XYZColor ToXYZ(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion to Lab
            var labConverter = new LChabToLabConverter();
            LabColor labColor = labConverter.Convert(color);

            // conversion to XYZ (incl. adaptation)
            XYZColor result = ToXYZ(labColor);
            return result;
        }

        public XYZColor ToXYZ(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion
            var converter = new HunterLabToXYZConverter();
            XYZColor unadapted = converter.Convert(color);

            // adaptation
            XYZColor adapted = color.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WhitePoint);

            return adapted;
        }
    }
}