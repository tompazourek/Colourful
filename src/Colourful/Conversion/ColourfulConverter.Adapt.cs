using System;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Performs chromatic adaptation of given XYZ color.
        /// Target white point is <see cref="WhitePoint" />.
        /// </summary>
        public XYZColor Adapt(in XYZColor color, in XYZColor sourceWhitePoint)
        {
            if (!IsChromaticAdaptationPerformed)
                throw new InvalidOperationException("Cannot perform chromatic adaptation, provide chromatic adaptation method and white point.");

            var result = ChromaticAdaptation.Transform(color, sourceWhitePoint, WhitePoint);
            return result;
        }

        /// <summary>
        /// Adapts linear RGB color from the source working space to working space set in <see cref="TargetRGBWorkingSpace" />.
        /// </summary>
        public LinearRGBColor Adapt(in LinearRGBColor color)
        {
            if (!IsChromaticAdaptationPerformed)
                throw new InvalidOperationException("Cannot perform chromatic adaptation, provide chromatic adaptation method and white point.");

            if (color.WorkingSpace.Equals(TargetRGBWorkingSpace))
                return color;

            // conversion to XYZ
            var converterToXYZ = GetLinearRGBToXYZConverter(color.WorkingSpace);
            var unadapted = converterToXYZ.Convert(color);

            // adaptation
            var adapted = ChromaticAdaptation.Transform(unadapted, color.WorkingSpace.WhitePoint, TargetRGBWorkingSpace.WhitePoint);

            // conversion back to RGB
            var converterToRGB = GetXYZToLinearRGBConverter(TargetRGBWorkingSpace);
            var result = converterToRGB.Convert(adapted);

            return result;
        }

        /// <summary>
        /// Adapts RGB color from the source working space to working space set in <see cref="TargetRGBWorkingSpace" />.
        /// </summary>
        public RGBColor Adapt(in RGBColor color)
        {
            var linearInput = ToLinearRGB(color);
            var linearOutput = Adapt(linearInput);
            var compandedOutput = ToRGB(linearOutput);

            return compandedOutput;
        }

        /// <summary>
        /// Adapts Lab color from the source white point to white point set in <see cref="TargetLabWhitePoint" />.
        /// </summary>
        public LabColor Adapt(in LabColor color)
        {
            if (!IsChromaticAdaptationPerformed)
                throw new InvalidOperationException("Cannot perform chromatic adaptation, provide chromatic adaptation method and white point.");

            if (color.WhitePoint.Equals(TargetLabWhitePoint))
                return color;

            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Adapts LChab color from the source white point to white point set in <see cref="TargetLabWhitePoint" />.
        /// </summary>
        public LChabColor Adapt(in LChabColor color)
        {
            if (!IsChromaticAdaptationPerformed)
                throw new InvalidOperationException("Cannot perform chromatic adaptation, provide chromatic adaptation method and white point.");

            if (color.WhitePoint.Equals(TargetLabWhitePoint))
                return color;

            var labColor = ToLab(color);
            var result = ToLChab(labColor);
            return result;
        }

        /// <summary>
        /// Adapts Lab color from the source white point to white point set in <see cref="TargetHunterLabWhitePoint" />.
        /// </summary>
        public HunterLabColor Adapt(in HunterLabColor color)
        {
            if (!IsChromaticAdaptationPerformed)
                throw new InvalidOperationException("Cannot perform chromatic adaptation, provide chromatic adaptation method and white point.");

            if (color.WhitePoint.Equals(TargetHunterLabWhitePoint))
                return color;

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Adapts Luv color from the source white point to white point set in <see cref="TargetLuvWhitePoint" />.
        /// </summary>
        public LuvColor Adapt(in LuvColor color)
        {
            if (!IsChromaticAdaptationPerformed)
                throw new InvalidOperationException("Cannot perform chromatic adaptation, provide chromatic adaptation method and white point.");

            if (color.WhitePoint.Equals(TargetLuvWhitePoint))
                return color;

            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }
    }
}