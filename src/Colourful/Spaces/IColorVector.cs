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
        double[] Vector { get; }
    }
}