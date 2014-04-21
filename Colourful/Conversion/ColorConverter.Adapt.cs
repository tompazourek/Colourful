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
        /// <summary>
        /// Performs chromatic adaptation of given XYZ color.
        /// Target white point is <see cref="WhitePoint"/>.
        /// </summary>
        public XYZColor Adapt(XYZColor color, XYZColor sourceWhitePoint)
        {
            if (color == null) throw new ArgumentNullException("color");
            if (sourceWhitePoint == null) throw new ArgumentNullException("sourceWhitePoint");

            if (!IsChromaticAdaptationPerformed)
                throw new InvalidOperationException("Cannot perform chromatic adaptation, provide chromatic adaptation method and white point.");

            XYZColor result = ChromaticAdaptation.Transform(color, sourceWhitePoint, WhitePoint);
            return result;
        }

        /// <summary>
        /// Adapts RGB color from the source working space to working space set in <see cref="TargetRGBWorkingSpace"/>.
        /// </summary>
        public RGBColor Adapt(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            if (!IsChromaticAdaptationPerformed)
                throw new InvalidOperationException("Cannot perform chromatic adaptation, provide chromatic adaptation method and white point.");

            if (color.WorkingSpace.Equals(TargetRGBWorkingSpace))
                return color;

            // conversion to XYZ
            var converterToXYZ = new RGBToXYZConverter(color.WorkingSpace);
            XYZColor unadapted = converterToXYZ.Convert(color);

            // adaptation
            XYZColor adapted = ChromaticAdaptation.Transform(unadapted, color.WorkingSpace.WhitePoint, TargetRGBWorkingSpace.WhitePoint);

            // conversion back to RGB
            var converterToRGB = new XYZToRGBConverter(TargetRGBWorkingSpace);
            RGBColor result = converterToRGB.Convert(adapted);

            return result;
        }

        /// <summary>
        /// Adapts Lab color from the source white point to white point set in <see cref="TargetLabWhitePoint"/>.
        /// </summary>
        public LabColor Adapt(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            if (!IsChromaticAdaptationPerformed)
                throw new InvalidOperationException("Cannot perform chromatic adaptation, provide chromatic adaptation method and white point.");

            if (color.WhitePoint.Equals(TargetLabWhitePoint))
                return color;

            XYZColor xyzColor = ToXYZ(color);
            LabColor result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Adapts LChab color from the source white point to white point set in <see cref="TargetLabWhitePoint"/>.
        /// </summary>
        public LChabColor Adapt(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            if (!IsChromaticAdaptationPerformed)
                throw new InvalidOperationException("Cannot perform chromatic adaptation, provide chromatic adaptation method and white point.");

            if (color.WhitePoint.Equals(TargetLabWhitePoint))
                return color;

            LabColor labColor = ToLab(color);
            LChabColor result = ToLChab(labColor);
            return result;
        }
    }
}