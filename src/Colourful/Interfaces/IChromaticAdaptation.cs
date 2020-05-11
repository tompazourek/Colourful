namespace Colourful
{
    /// <summary>
    /// Chromatic adaptation.
    /// A linear transformation of a source color (S1, S2, S3) into a destination color (D1, D2, D3) by a linear transformation [M]
    /// which is dependent on the source reference white (WS1, WS2, WS3) and the destination reference white (WD1, WD2, WD3).
    /// </summary>
    public interface IChromaticAdaptation<TColor>
        where TColor : struct
    {
        /// <summary>
        /// Adapts the input color from the source white point to the destination white point.
        /// </summary>
        TColor Transform(in TColor sourceColor);
    }
}