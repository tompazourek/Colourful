using System.Collections.Generic;
using System.Linq;

namespace Colourful.Internals
{
    public class ConversionMetadata : IConversionMetadata
    {
        private readonly Dictionary<string, object> _items = new Dictionary<string, object>();

        public ConversionMetadata(params KeyValuePair<string, object>[] setItems)
        {
            foreach (var item in setItems)
            {
                _items[item.Key] = item.Value;
            }
        }
        
        public TValue GetItemOrDefault<TValue>(in string key)
        {
            if (_items.TryGetValue(key, out var untypedValue) && untypedValue is TValue typedValue)
                return typedValue;
            
            return default;
        }

        public IConversionMetadata CloneWith(params KeyValuePair<string, object>[] setItems) 
            => new ConversionMetadata(_items.Concat(setItems).ToArray());
    }
}
