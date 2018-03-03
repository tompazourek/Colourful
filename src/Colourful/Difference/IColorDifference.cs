using System.Diagnostics.CodeAnalysis;

namespace Colourful.Difference
{
    /// <summary>
    /// Computes distance between two vectors in color space
    /// </summary>
    /// <typeparam name="TColor"></typeparam>
    public interface IColorDifference<in TColor>
    {
        /// <summary>
        /// Computes distance between color x and y.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        double ComputeDifference(TColor x, TColor y);
    }
}