using System.Collections.Generic;

namespace Colourful.Strategy
{
    /// <summary>
    /// Conversion metadata used to guide the conversion strategy builder.
    /// </summary>
    public interface IConversionMetadata
    {
        /// <param name="key">Item key</param>
        /// <returns>Item value (or default)</returns>
        TValue GetItemOrDefault<TValue>(in string key);

        /// <summary>
        /// Clones the context and modifies its items
        /// </summary>
        /// <param name="setItems">Item values to set</param>
        IConversionMetadata CloneWith(params KeyValuePair<string, object>[] setItems);
    }
}