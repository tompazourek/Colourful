using System.Collections.Generic;

namespace Colourful.Internals
{
    /// <summary>
    /// Conversion metadata used by the conversion strategies to decide how to compose converters.
    /// It can be thought of as an extensible collection of key-value pairs.
    /// See <see cref="ConversionMetadataKeys" /> for keys of the built-in items.
    /// </summary>
    public interface IConversionMetadata
    {
        /// <param name="key">Item key.</param>
        /// <returns>Item value (or default value if not found).</returns>
        TValue GetItemOrDefault<TValue>(in string key);

        /// <summary>
        /// Clones the metadata and modifies its items.
        /// </summary>
        /// <param name="setItems">Item values to set.</param>
        IConversionMetadata CloneWith(params KeyValuePair<string, object>[] setItems);
    }
}
