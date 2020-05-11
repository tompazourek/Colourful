using System;
using System.Collections.Generic;
using static Colourful.Strategy.ConversionMetadataKeys;

namespace Colourful.Strategy.Rules
{
    /// <summary>
    /// Utilities for building <see cref="IConversionRule" />.
    /// </summary>
    public static class ConversionRuleUtils
    {
        /// <summary>
        /// Helper extension method to get <see cref="ConversionMetadataKeys.ColorType" />
        /// </summary>
        public static Type GetColorType(this IConversionMetadata node)
            => node.GetItemOrDefault<Type>(ColorType);

        /// <summary>
        /// Returns true if the node has the color type.
        /// </summary>
        public static bool HasColorType<TColor>(this IConversionMetadata node)
            => node.GetColorType() == typeof(TColor);

        /// <summary>
        /// Returns true if the nodes have the color types.
        /// </summary>
        public static bool HaveColorTypes<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode)
            => sourceNode.HasColorType<TSource>() && targetNode.HasColorType<TTarget>();

        /// <summary>
        /// Clones the node and sets the color type.
        /// </summary>
        public static IConversionMetadata CloneWithColorType<TColor>(this IConversionMetadata node)
            => node.CloneWith(new KeyValuePair<string, object>(ColorType, typeof(TColor)));

        /// <summary>
        /// Helper extension method to get <see cref="ConversionMetadataKeys.WhitePoint" />
        /// </summary>
        public static XYZColor? GetWhitePoint(this IConversionMetadata node)
            => node.GetItemOrDefault<XYZColor?>(WhitePoint);

        /// <summary>
        /// Helper extension method to get <see cref="ConversionMetadataKeys.WhitePoint" />
        /// </summary>
        public static XYZColor GetWhitePointRequired(this IConversionMetadata node)
            => node.GetWhitePoint() ?? throw new InvalidOperationException("White point is not specified, but it's required for the conversion.");

        /// <summary>
        /// Returns true if the nodes have the same white point.
        /// </summary>
        public static bool EqualWhitePoints(in IConversionMetadata sourceNode, in IConversionMetadata targetNode)
            => Equals(sourceNode.GetWhitePoint(), targetNode.GetWhitePoint());
    }
}