using Colourful.Strategy;
using Xunit;

namespace Colourful.Tests
{
    public class RuleEngineTest
    {
        [Fact]
        public void Sample()
        {
            var inputNode = new ChainNode(typeof(LChabColor), Illuminants.D50);
            var outputNode = new ChainNode(typeof(HunterLabColor), Illuminants.D65);

            var chain = new Chain(inputNode, outputNode);
            var chainString = chain.ToString();

            var engine = new RuleEngine();
            AddRules(engine);

            var generatedOps = engine.ProcessChain(chain);
        }

        private static void AddRules(RuleEngine engine)
        {
            AddHunterLabRules(engine);
            AddLChabRules(engine);
            AddLabRules(engine);
            AddXYZRules(engine);
            AddLMSRules(engine);
        }

        private static void AddHunterLabRules(RuleEngine engine)
        {
            // if the same, resolve
            engine.AddRule(priority: 100, (a, b) =>
                a.ColorType == typeof(HunterLabColor)
                && b.ColorType == typeof(HunterLabColor)
                && Equals(a.WhitePoint, b.WhitePoint)
                    ? new RuleResult(a)
                    : RuleResult.NotApplies);

            // actual conversion
            engine.AddRule(priority: 200, (a, b) =>
                a.ColorType == typeof(HunterLabColor)
                && b.ColorType == typeof(XYZColor)
                && Equals(a.WhitePoint, b.WhitePoint) && a.WhitePoint != null
                    ? new RuleResult($"op. {a.GetColorTypeAsString()} -> {b.GetColorTypeAsString()} using {a.GetWhitePointAsString()}", b)
                    : RuleResult.NotApplies);

            engine.AddRule(priority: 200, (a, b) =>
                a.ColorType == typeof(XYZColor)
                && b.ColorType == typeof(HunterLabColor)
                && Equals(a.WhitePoint, b.WhitePoint) && a.WhitePoint != null
                    ? new RuleResult($"op. {a.GetColorTypeAsString()} -> {b.GetColorTypeAsString()} using {a.GetWhitePointAsString()}", b)
                    : RuleResult.NotApplies);

            // always go through XYZ
            engine.AddRule(priority: 300, (a, b) =>
                a.ColorType == typeof(HunterLabColor)
                    ? new RuleResult(a, new ChainNode(typeof(XYZColor), a.WhitePoint), b)
                    : RuleResult.NotApplies);

            engine.AddRule(priority: 400, (a, b) =>
                b.ColorType == typeof(HunterLabColor)
                    ? new RuleResult(a, new ChainNode(typeof(XYZColor), b.WhitePoint), b)
                    : RuleResult.NotApplies);
        }
        
        private static void AddLChabRules(RuleEngine engine)
        {
            // if the same, resolve
            engine.AddRule(priority: 100, (a, b) =>
                a.ColorType == typeof(LChabColor)
                && b.ColorType == typeof(LChabColor)
                && Equals(a.WhitePoint, b.WhitePoint)
                    ? new RuleResult(a)
                    : RuleResult.NotApplies);

            // actual conversion
            engine.AddRule(priority: 200, (a, b) =>
                a.ColorType == typeof(LChabColor)
                && b.ColorType == typeof(LabColor)
                && Equals(a.WhitePoint, b.WhitePoint)
                    ? new RuleResult($"op. {a.GetColorTypeAsString()} -> {b.GetColorTypeAsString()}", b)
                    : RuleResult.NotApplies);

            engine.AddRule(priority: 200, (a, b) =>
                a.ColorType == typeof(LabColor)
                && b.ColorType == typeof(LChabColor)
                && Equals(a.WhitePoint, b.WhitePoint)
                    ? new RuleResult($"op. {a.GetColorTypeAsString()} -> {b.GetColorTypeAsString()}", b)
                    : RuleResult.NotApplies);

            // always go through Lab
            engine.AddRule(priority: 300, (a, b) =>
                a.ColorType == typeof(LChabColor)
                    ? new RuleResult(a, new ChainNode(typeof(LabColor), a.WhitePoint), b)
                    : RuleResult.NotApplies);

            engine.AddRule(priority: 400, (a, b) =>
                b.ColorType == typeof(LChabColor)
                    ? new RuleResult(a, new ChainNode(typeof(LabColor), b.WhitePoint), b)
                    : RuleResult.NotApplies);
        }

        private static void AddLabRules(RuleEngine engine)
        {
            // if the same, resolve
            engine.AddRule(priority: 100, (a, b) =>
                a.ColorType == typeof(LabColor)
                && b.ColorType == typeof(LabColor)
                && Equals(a.WhitePoint, b.WhitePoint)
                    ? new RuleResult(a)
                    : RuleResult.NotApplies);

            // actual conversion
            engine.AddRule(priority: 200, (a, b) =>
                a.ColorType == typeof(LabColor)
                && b.ColorType == typeof(XYZColor)
                && Equals(a.WhitePoint, b.WhitePoint) && a.WhitePoint != null
                    ? new RuleResult($"op. {a.GetColorTypeAsString()} -> {b.GetColorTypeAsString()} using {a.GetWhitePointAsString()}", b)
                    : RuleResult.NotApplies);

            engine.AddRule(priority: 200, (a, b) =>
                a.ColorType == typeof(XYZColor)
                && b.ColorType == typeof(LabColor)
                && Equals(a.WhitePoint, b.WhitePoint) && a.WhitePoint != null
                    ? new RuleResult($"op. {a.GetColorTypeAsString()} -> {b.GetColorTypeAsString()} using {a.GetWhitePointAsString()}", b)
                    : RuleResult.NotApplies);

            // always go through XYZ
            engine.AddRule(priority: 300, (a, b) =>
                a.ColorType == typeof(LabColor)
                    ? new RuleResult(a, new ChainNode(typeof(XYZColor), a.WhitePoint), b)
                    : RuleResult.NotApplies);

            engine.AddRule(priority: 400, (a, b) =>
                b.ColorType == typeof(LabColor)
                    ? new RuleResult(a, new ChainNode(typeof(XYZColor), b.WhitePoint), b)
                    : RuleResult.NotApplies);
        }
        
        private static void AddXYZRules(RuleEngine engine)
        {
            // if the same, resolve
            engine.AddRule(priority: 100, (a, b) =>
                a.ColorType == typeof(XYZColor)
                && b.ColorType == typeof(XYZColor)
                && Equals(a.WhitePoint, b.WhitePoint)
                    ? new RuleResult(a)
                    : RuleResult.NotApplies);

            // if different WP, go through LMS
            engine.AddRule(priority: 100, (a, b) =>
                a.ColorType == typeof(XYZColor)
                && b.ColorType == typeof(XYZColor)
                && !Equals(a.WhitePoint, b.WhitePoint) && a.WhitePoint != null && b.WhitePoint != null
                    ? new RuleResult(a, new ChainNode(typeof(LMSColor), a.WhitePoint), b)
                    : RuleResult.NotApplies);
        }
        
        private static void AddLMSRules(RuleEngine engine)
        {
            // if the same, resolve
            engine.AddRule(priority: 100, (a, b) =>
                a.ColorType == typeof(LMSColor)
                && b.ColorType == typeof(LMSColor)
                && Equals(a.WhitePoint, b.WhitePoint)
                    ? new RuleResult(a)
                    : RuleResult.NotApplies);

            // actual adaptation
            engine.AddRule(priority: 100, (a, b) =>
                a.ColorType == typeof(LMSColor)
                && b.ColorType == typeof(LMSColor)
                && !Equals(a.WhitePoint, b.WhitePoint) && a.WhitePoint != null && b.WhitePoint != null
                    ? new RuleResult($"op. {a.GetColorTypeAsString()} adapt {a.GetWhitePointAsString()} -> {b.GetWhitePointAsString()}", b)
                    : RuleResult.NotApplies);

            // actual conversion
            engine.AddRule(priority: 200, (a, b) =>
                a.ColorType == typeof(LMSColor)
                && b.ColorType == typeof(XYZColor)
                && Equals(a.WhitePoint, b.WhitePoint)
                    ? new RuleResult($"op. {a.GetColorTypeAsString()} -> {b.GetColorTypeAsString()} using a matrix", b)
                    : RuleResult.NotApplies);

            engine.AddRule(priority: 200, (a, b) =>
                a.ColorType == typeof(XYZColor)
                && b.ColorType == typeof(LMSColor)
                && Equals(a.WhitePoint, b.WhitePoint)
                    ? new RuleResult($"op. {a.GetColorTypeAsString()} -> {b.GetColorTypeAsString()} using a matrix", b)
                    : RuleResult.NotApplies);
            
            // always go through XYZ on same whitepoint
            engine.AddRule(priority: 300, (a, b) =>
                a.ColorType == typeof(LMSColor) && Equals(a.WhitePoint, b.WhitePoint) && a.WhitePoint != null && b.WhitePoint != null
                    ? new RuleResult(a, new ChainNode(typeof(XYZColor), a.WhitePoint), b)
                    : RuleResult.NotApplies);

            engine.AddRule(priority: 400, (a, b) =>
                b.ColorType == typeof(LMSColor) && Equals(a.WhitePoint, b.WhitePoint) && a.WhitePoint != null && b.WhitePoint != null
                    ? new RuleResult(a, new ChainNode(typeof(XYZColor), b.WhitePoint), b)
                    : RuleResult.NotApplies);
            
            // always go through LMS on different whitepoint
            engine.AddRule(priority: 300, (a, b) =>
                a.ColorType == typeof(LMSColor) && !Equals(a.WhitePoint, b.WhitePoint) && a.WhitePoint != null && b.WhitePoint != null
                    ? new RuleResult(a, new ChainNode(typeof(LMSColor), b.WhitePoint), b)
                    : RuleResult.NotApplies);

            engine.AddRule(priority: 400, (a, b) =>
                b.ColorType == typeof(LMSColor) && !Equals(a.WhitePoint, b.WhitePoint) && a.WhitePoint != null && b.WhitePoint != null
                    ? new RuleResult(a, new ChainNode(typeof(LMSColor), a.WhitePoint), b)
                    : RuleResult.NotApplies);
        }
    }
}