using System;

namespace Colourful.Strategy
{
    /// <summary>
    /// Keys for values in <see cref="IConversionMetadata"/>.
    /// </summary>
    public static class ConversionMetadataKeys
    {
        /// <summary>
        /// Type of the color converted. The value is a <see cref="System.Type"/>.
        /// </summary>
        public const string ColorType = nameof(ColorType);

        /// <summary>
        /// White point of the color converted. The value is a <see cref="Nullable{XYZColor}"/>.
        /// </summary>
        public const string WhitePoint = nameof(WhitePoint);
    }
}