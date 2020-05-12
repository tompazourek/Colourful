using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Colourful.Strategy
{
    internal class ConversionChainNodePair
    {
        private readonly LinkedListNode<IConversionMetadata> _node;

        public ConversionChainNodePair(LinkedListNode<IConversionMetadata> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.Next == null) throw new ArgumentException("Argument cannot be the last node.", nameof(node));

            _node = node;
        }

        public IConversionMetadata SourceNode => _node.Value;

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public IConversionMetadata TargetNode => _node.Next.Value;

        public void ReplaceInChain(IConversionMetadata[] replacementNodes)
        {
            // add nodes
            foreach (var newNode in replacementNodes)
            {
                _node.List.AddBefore(_node, newNode);
            }

            // remove nodes
            var leftNode = _node;
            var rightNode = _node.Next ?? throw new InvalidOperationException();
            _node.List.Remove(leftNode);
            _node.List.Remove(rightNode);
        }
    }
}