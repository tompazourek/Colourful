using System;
using System.Collections.Generic;
using Colourful.Companding;
using Colourful.Conversion;

namespace Colourful.Strategy
{
    /// <summary>
    /// Utilities for working with <see cref="IConversionMetadata"/>
    /// </summary>
    public static class ConversionMetadataUtils
    {
        #region White point

        /// <summary>
        /// Helper extension method to get <see cref="ConversionMetadataKeys.WhitePoint" />
        /// </summary>
        public static XYZColor? GetWhitePoint(this IConversionMetadata node)
            => node.GetItemOrDefault<XYZColor?>(ConversionMetadataKeys.WhitePoint);

        /// <summary>
        /// Helper extension method to get <see cref="ConversionMetadataKeys.WhitePoint" />
        /// </summary>
        public static XYZColor GetWhitePointRequired(this IConversionMetadata node)
            => node.GetWhitePoint() ?? throw new InvalidOperationException("White point is not specified, but is required for the conversion.");

        /// <summary>
        /// Returns true if the nodes have the same white point.
        /// </summary>
        public static bool EqualWhitePoints(in IConversionMetadata sourceNode, in IConversionMetadata targetNode)
            => Equals(sourceNode.GetWhitePoint(), targetNode.GetWhitePoint());

        /// <summary>
        /// Creates a new item for <see cref="ConversionMetadataKeys.WhitePoint" />.
        /// </summary>
        public static KeyValuePair<string, object> CreateWhitePoint(XYZColor? whitePoint)
            => new KeyValuePair<string, object>(ConversionMetadataKeys.WhitePoint, whitePoint);

        #endregion
        
        #region RGB Primaries

        /// <summary>
        /// Helper extension method to get <see cref="ConversionMetadataKeys.RGBPrimaries" />
        /// </summary>
        public static RGBPrimaries? GetRGBPrimaries(this IConversionMetadata node)
            => node.GetItemOrDefault<RGBPrimaries?>(ConversionMetadataKeys.RGBPrimaries);

        /// <summary>
        /// Helper extension method to get <see cref="ConversionMetadataKeys.RGBPrimaries" />
        /// </summary>
        public static RGBPrimaries GetRGBPrimariesRequired(this IConversionMetadata node)
            => node.GetRGBPrimaries() ?? throw new InvalidOperationException("RGB primaries of the working space are not specified, but are required for the conversion.");

        /// <summary>
        /// Returns true if the nodes have the same RGB primaries.
        /// </summary>
        public static bool EqualRGBPrimaries(in IConversionMetadata sourceNode, in IConversionMetadata targetNode)
            => Equals(sourceNode.GetRGBPrimaries(), targetNode.GetRGBPrimaries());

        /// <summary>
        /// Creates a new item for <see cref="ConversionMetadataKeys.RGBPrimaries" />.
        /// </summary>
        public static KeyValuePair<string, object> CreateRGBPrimaries(RGBPrimaries? primaries)
            => new KeyValuePair<string, object>(ConversionMetadataKeys.RGBPrimaries, primaries);

        #endregion
        
        #region Companding

        /// <summary>
        /// Helper extension method to get <see cref="ConversionMetadataKeys.Companding" />
        /// </summary>
        public static ICompanding GetCompanding(this IConversionMetadata node)
            => node.GetItemOrDefault<ICompanding>(ConversionMetadataKeys.Companding);

        /// <summary>
        /// Helper extension method to get <see cref="ConversionMetadataKeys.Companding" />
        /// </summary>
        public static ICompanding GetCompandingRequired(this IConversionMetadata node)
            => node.GetCompanding() ?? throw new InvalidOperationException("Companding of the RGB working space is not specified, but is required for the conversion.");

        /// <summary>
        /// Returns true if the nodes have the same companding.
        /// </summary>
        public static bool EqualCompanding(in IConversionMetadata sourceNode, in IConversionMetadata targetNode)
            => Equals(sourceNode.GetCompanding(), targetNode.GetCompanding());

        /// <summary>
        /// Creates a new item for <see cref="ConversionMetadataKeys.Companding" />.
        /// </summary>
        public static KeyValuePair<string, object> CreateCompanding(ICompanding companding)
            => new KeyValuePair<string, object>(ConversionMetadataKeys.Companding, companding);

        #endregion
    }
}