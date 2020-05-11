using System.Collections.Generic;
using System.Linq;

namespace Colourful.Strategy
{
    internal class ConversionMetadata : IConversionMetadata
    {
        private readonly Dictionary<string, object> _items = new Dictionary<string, object>();
        
        public TValue GetItemOrDefault<TValue>(in string key)
        {
            if (_items.TryGetValue(key, out var untypedValue) && untypedValue is TValue typedValue)
                return typedValue;
            
            return default;
        }

        public IConversionMetadata CloneWith(params KeyValuePair<string, object>[] setItems)
        {
            var clone = new ConversionMetadata();
            foreach (var item in _items.Concat(setItems))
            {
                clone._items[item.Key] = item.Value;
            }
            return clone;
        }
    }
}
