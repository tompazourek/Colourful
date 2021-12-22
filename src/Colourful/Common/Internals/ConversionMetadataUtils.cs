using System.Collections.Generic;

namespace Colourful.Internals;

/// <summary>
/// Utilities for working with <see cref="IConversionMetadata" />
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
    /// Helper extension method to get <see cref="ConversionMetadataKeys.WhitePoint" /> as Key-Value pair.
    /// </summary>
    public static KeyValuePair<string, object> GetWhitePointItem(this IConversionMetadata node)
        => new KeyValuePair<string, object>(ConversionMetadataKeys.WhitePoint, node.GetWhitePoint());

    /// <summary>
    /// Helper extension method to get <see cref="ConversionMetadataKeys.WhitePoint" />
    /// </summary>
    public static XYZColor GetWhitePointRequired(this IConversionMetadata node)
        => node.GetWhitePoint() ?? throw new MissingConversionMetadataException("White point is not specified, but is required for the conversion.", ConversionMetadataKeys.WhitePoint);

    /// <summary>
    /// Returns true if the nodes have the same white point.
    /// </summary>
    public static bool EqualWhitePoints(in IConversionMetadata sourceNode, in IConversionMetadata targetNode)
        => Equals(sourceNode.GetWhitePoint(), targetNode.GetWhitePoint());

    /// <summary>
    /// Creates a new item for <see cref="ConversionMetadataKeys.WhitePoint" />.
    /// </summary>
    public static KeyValuePair<string, object> CreateWhitePoint(in XYZColor? whitePoint)
        => new KeyValuePair<string, object>(ConversionMetadataKeys.WhitePoint, whitePoint);

    #endregion

    #region RGB Primaries

    /// <summary>
    /// Helper extension method to get <see cref="ConversionMetadataKeys.RGBPrimaries" />
    /// </summary>
    public static RGBPrimaries? GetRGBPrimaries(this IConversionMetadata node)
        => node.GetItemOrDefault<RGBPrimaries?>(ConversionMetadataKeys.RGBPrimaries);

    /// <summary>
    /// Helper extension method to get <see cref="ConversionMetadataKeys.RGBPrimaries" /> as Key-Value pair.
    /// </summary>
    public static KeyValuePair<string, object> GetRGBPrimariesItem(this IConversionMetadata node)
        => new KeyValuePair<string, object>(ConversionMetadataKeys.RGBPrimaries, node.GetRGBPrimaries());

    /// <summary>
    /// Helper extension method to get <see cref="ConversionMetadataKeys.RGBPrimaries" />
    /// </summary>
    public static RGBPrimaries GetRGBPrimariesRequired(this IConversionMetadata node)
        => node.GetRGBPrimaries() ?? throw new MissingConversionMetadataException("RGB primaries of the working space are not specified, but are required for the conversion.", ConversionMetadataKeys.RGBPrimaries);

    /// <summary>
    /// Returns true if the nodes have the same RGB primaries.
    /// </summary>
    public static bool EqualRGBPrimaries(in IConversionMetadata sourceNode, in IConversionMetadata targetNode)
        => Equals(sourceNode.GetRGBPrimaries(), targetNode.GetRGBPrimaries());

    /// <summary>
    /// Creates a new item for <see cref="ConversionMetadataKeys.RGBPrimaries" />.
    /// </summary>
    public static KeyValuePair<string, object> CreateRGBPrimaries(in RGBPrimaries? primaries)
        => new KeyValuePair<string, object>(ConversionMetadataKeys.RGBPrimaries, primaries);

    #endregion

    #region Companding

    /// <summary>
    /// Helper extension method to get <see cref="ConversionMetadataKeys.Companding" />
    /// </summary>
    public static ICompanding GetCompanding(this IConversionMetadata node)
        => node.GetItemOrDefault<ICompanding>(ConversionMetadataKeys.Companding);

    /// <summary>
    /// Helper extension method to get <see cref="ConversionMetadataKeys.Companding" /> as Key-Value pair.
    /// </summary>
    public static KeyValuePair<string, object> GetCompandingItem(this IConversionMetadata node)
        // note: this might not be needed at the moment
        => new KeyValuePair<string, object>(ConversionMetadataKeys.Companding, node.GetCompanding());

    /// <summary>
    /// Helper extension method to get <see cref="ConversionMetadataKeys.Companding" />
    /// </summary>
    public static ICompanding GetCompandingRequired(this IConversionMetadata node)
        => node.GetCompanding() ?? throw new MissingConversionMetadataException("Companding of the RGB working space is not specified, but is required for the conversion.", ConversionMetadataKeys.Companding);

    /// <summary>
    /// Returns true if the nodes have the same companding.
    /// </summary>
    public static bool EqualCompanding(in IConversionMetadata sourceNode, in IConversionMetadata targetNode)
        => Equals(sourceNode.GetCompanding(), targetNode.GetCompanding());

    /// <summary>
    /// Creates a new item for <see cref="ConversionMetadataKeys.Companding" />.
    /// </summary>
    public static KeyValuePair<string, object> CreateCompanding(in ICompanding companding)
        => new KeyValuePair<string, object>(ConversionMetadataKeys.Companding, companding);

    #endregion
}
