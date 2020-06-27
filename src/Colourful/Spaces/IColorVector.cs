using System.Diagnostics.CodeAnalysis;

namespace Colourful
{
    /// <summary>
    /// Color that can be represented as a vector in its color space.
    /// </summary>
    public interface IColorVector
    {
        /// <summary>
        /// Vector.
        /// </summary>
        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Array for performance reasons.")]
        double[] Vector { get; }
    }
}