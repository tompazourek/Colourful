using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Colourful.Companding;
using Colourful.Conversion;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Colourful.Strategy
{
    public class RuleEngine
    {
        private List<Rule> _rules = new List<Rule>();

        public void AddRule(int priority, Func<ChainNode, ChainNode, RuleResult> tryApply)
        {
            _rules.Add(new Rule(priority, tryApply));
        }

        public List<string> ProcessChain(Chain chain)
        {
            var generatedOperations = new List<string>();
            var rulesSorted = _rules.OrderBy(x => x.Priority).ToList();

            restart:
            chain.ResetPosition();
            
            while (chain.PositionHasTwoNodes()) 
            {
                chain.GetCurrentTwoNodes(out var leftNode, out var rightNode);
                foreach (var rule in rulesSorted)
                {
                    var ruleResult = rule.TryApplyFunc(leftNode, rightNode);
                    if (ruleResult.Success)
                    {
                        if (ruleResult.GeneratedOperation != null)
                        {
                            generatedOperations.Add(ruleResult.GeneratedOperation);
                        }

                        chain.ReplaceCurrentTwoNodes(ruleResult.ResolvedNodes);
                        goto restart;
                    }
                }

                chain.MovePositionNext();
            }

            return generatedOperations;
        }

        private class Rule
        {
            public Rule(int priority, Func<ChainNode, ChainNode, RuleResult> tryApplyFunc)
            {
                Priority = priority;
                TryApplyFunc = tryApplyFunc;
            }

            public int Priority { get; }
            public Func<ChainNode, ChainNode, RuleResult> TryApplyFunc { get; }
        }
    }

    public class RuleResult
    {
        public RuleResult(string generatedOperation, params ChainNode[] resolvedNodes)
        {
            Success = true;
            ResolvedNodes = resolvedNodes;
            GeneratedOperation = generatedOperation;
        }

        public RuleResult(params ChainNode[] resolvedNodes)
        {
            Success = true;
            ResolvedNodes = resolvedNodes;
        }

        private RuleResult()
        {
        }
        
        public static RuleResult NotApplies = new RuleResult();

        public bool Success { get; }
        public ChainNode[] ResolvedNodes { get; }
        public string GeneratedOperation { get; }
    }

    public class Chain
    {
        private LinkedList<ChainNode> _nodes = new LinkedList<ChainNode>();

        private LinkedListNode<ChainNode> _position;

        public Chain(params ChainNode[] nodes)
        {
            foreach (var node in nodes)
            {
                _nodes.AddLast(node);
            }

            ResetPosition();
        }

        public void ResetPosition()
        {
            _position = _nodes.First;
        }

        public void MovePositionNext()
        {
            _position = _position.Next;
        }

        public bool PositionHasTwoNodes() => _position?.Next != null;

        public void GetCurrentTwoNodes(out ChainNode leftNode, out ChainNode rightNode)
        {
            leftNode = _position.Value;
            rightNode = (_position.Next ?? throw new InvalidOperationException()).Value;
        }

        public override string ToString()
        {
            var result = string.Join(" -> ", _nodes.Select(x => x.ToString()).ToArray());
            return result;
        }

        public void ReplaceCurrentTwoNodes(ChainNode[] newNodes)
        {
            // add nodes
            foreach (var newNode in newNodes)
            {
                _nodes.AddBefore(_position, newNode);
            }
            
            // remove nodes
            var leftNode = _position;
            var rightNode = _position.Next ?? throw new InvalidOperationException();
            _nodes.Remove(leftNode);
            _nodes.Remove(rightNode);
        }
    }

    public class ChainNode
    {
        public ChainNode(Type colorType, XYZColor? whitePoint = null, RGBPrimaries? rgbPrimaries = null, ICompanding companding = null)
        {
            ColorType = colorType;
            WhitePoint = whitePoint;
            RGBPrimaries = rgbPrimaries;
            Companding = companding;
        }

        public Type ColorType { get; }

        public XYZColor? WhitePoint { get; }
        public RGBPrimaries? RGBPrimaries { get; }
        public ICompanding Companding { get; }

        public override string ToString()
        {
            var colorTypeName = GetColorTypeAsString();
            var wpName = GetWhitePointAsString();
            var pName = GetRGBPrimariesAsString();
            var cName = GetCompandingAsString();

            var parts = new[] { colorTypeName, wpName, cName, pName }.Where(x => x != null).ToArray();
            var result = "(" + string.Join(", ", parts) + ")";
            return result;
        }

        public string GetColorTypeAsString()
        {
            var colorTypeName = ColorType.Name;
            var removedSuffixes = new[] { "Color", "ChromaticityCoordinates" };
            foreach (var removedSuffix in removedSuffixes)
            {
                if (colorTypeName.EndsWith(removedSuffix))
                {
                    colorTypeName = colorTypeName.Substring(startIndex: 0, colorTypeName.Length - removedSuffix.Length);
                }
            }

            return colorTypeName;
        }

        public string GetCompandingAsString()
        {
            unchecked
            {
                return Companding == null ? null : "C_" + Crockbase32.Encode(BitConverter.GetBytes((Companding.GetType().GetHashCode() * 397) ^ Companding.GetHashCode()));
            }
        }

        public string GetRGBPrimariesAsString() => RGBPrimaries.HasValue ? "P_" + Crockbase32.Encode(BitConverter.GetBytes(RGBPrimaries.Value.GetHashCode())) : null;

        public string GetWhitePointAsString() => WhitePoint.HasValue ? "WP_" + Crockbase32.Encode(BitConverter.GetBytes(WhitePoint.Value.GetHashCode())) : null;
    }
}