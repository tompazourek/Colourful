using System;
using System.Collections.Generic;

namespace Colourful.Strategy
{
    internal class ConversionChain
    {
        private readonly LinkedList<IConversionMetadata> _linkedNodes;

        public ConversionChain(IConversionMetadata sourceNode, IConversionMetadata targetNode)
        {
            if (sourceNode == null) throw new ArgumentNullException(nameof(sourceNode));
            if (targetNode == null) throw new ArgumentNullException(nameof(targetNode));

            _linkedNodes = new LinkedList<IConversionMetadata>(new[] { sourceNode, targetNode });
        }

        public IEnumerable<ConversionChainNodePair> GetPairs()
        {
            for (var node = _linkedNodes.First; node?.Next != null; node = node.Next)
            {
                yield return new ConversionChainNodePair(node);
            }
        }

        public int Count => _linkedNodes.Count;
    }
}