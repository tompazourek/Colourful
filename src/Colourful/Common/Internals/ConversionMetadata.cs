using System.Collections.Generic;
using System.Linq;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class ConversionMetadata : IConversionMetadata
    {
        private readonly Dictionary<string, object> _items = new Dictionary<string, object>();

        /// <summary>
        /// No conversion metadata, empty instance.
        /// </summary>
        public static readonly ConversionMetadata Empty = new ConversionMetadata();

        /// <summary>
        /// Creates new instance given an array of items.
        /// </summary>
        /// <param name="setItems">Items to set as the metadata.</param>
        public ConversionMetadata(params KeyValuePair<string, object>[] setItems)
        {
            foreach (var item in setItems)
            {
                _items[item.Key] = item.Value;
            }
        }

        /// <inheritdoc />
        public TValue GetItemOrDefault<TValue>(in string key)
        {
            if (_items.TryGetValue(key, out var untypedValue) && untypedValue is TValue typedValue)
                return typedValue;

            return default;
        }

        /// <inheritdoc />
        public IConversionMetadata CloneWith(params KeyValuePair<string, object>[] setItems)
            // note: this might not be used currently
            => new ConversionMetadata(_items.Concat(setItems).ToArray());
    }
}
