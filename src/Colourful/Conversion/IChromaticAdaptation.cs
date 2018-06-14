namespace Colourful.Conversion
{
    /// <summary>
    /// Chromatic adaptation.
    /// A linear transformation of a source color (XS, YS, ZS) into a destination color (XD, YD, ZD) by a linear transformation [M]
    /// which is dependent on the source reference white (XWS, YWS, ZWS) and the destination reference white (XWD, YWD, ZWD).
    /// </summary>
    public interface IChromaticAdaptation
    {
        /// <remarks>Doesn't crop the resulting color space coordinates (e. g. allows negative values for XYZ coordinates).</remarks>
        XYZColor Transform(in XYZColor sourceColor, in XYZColor sourceWhitePoint, in XYZColor targetWhitePoint);
    }
}