namespace Colourful.Difference
{
    /// <summary>
    /// Computes distance between two vectors in color space
    /// </summary>
    /// <typeparam name="TColor"></typeparam>
    public interface IColorDifference<TColor>
        where TColor : struct
    {
        /// <summary>
        /// Computes distance between color x and y.
        /// </summary>
        double ComputeDifference(in TColor x, in TColor y);
    }
}