namespace Colourful.Internals
{
    /// <summary>
    /// Pair of companding functions for <see cref="IRGBWorkingSpace" />.
    /// Used for conversion to XYZ and backwards.
    /// See also: <seealso cref="IRGBWorkingSpace.Companding" />
    /// </summary>
    public interface ICompanding
    {
        /// <summary>
        /// Inverse companding. The input companded channel is made linear with respect to the energy.
        /// </summary>
        /// <remarks>
        /// For more info see:
        /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
        /// </remarks>
        double ConvertToLinear(in double nonLinearChannel);

        /// <summary>
        /// Companding. The input uncompanded channel (linear) is made nonlinear (depends on the RGB color system).
        /// </summary>
        /// <remarks>
        /// For more info see:
        /// http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_RGB.html
        /// </remarks>
        double ConvertToNonLinear(in double linearChannel);
    }
}