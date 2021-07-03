using System;

namespace Colourful.Internals
{
    /// <summary>
    /// Keys for values in <see cref="IConversionMetadata" /> that are used by the built-in color spaces.
    /// </summary>
    public static class ConversionMetadataKeys
    {
        /// <summary>
        /// White point of the color converted. The value is a <see cref="Nullable{T}" />.
        /// </summary>
        public const string WhitePoint = nameof(WhitePoint);

        /// <summary>
        /// RGB primaries of the color converted. The value is a <see cref="Nullable{RGBPrimaries}" />.
        /// </summary>
        public const string RGBPrimaries = nameof(RGBPrimaries);

        /// <summary>
        /// Companding function of the color converted. The value is a <see cref="ICompanding" />.
        /// </summary>
        public const string Companding = nameof(Companding);
    }
}
