using Colourful.Strategy.Rules;

namespace Colourful.Strategy
{
    /// <summary>
    /// Priorities for the commonly used conversion rules.
    /// </summary>
    public static class ConversionRulePriorities
    {
        /// <summary>
        /// Priority for <see cref="Return_EqSpace_EqWhitePoint{TColor}"/> kinds of rules.
        /// </summary>
        public const int Return = 100;

        /// <summary>
        /// Priority for Convert kind of rules that generate operations.
        /// </summary>
        public const int Convert = 200;
        
        /// <summary>
        /// Priority for Intermediate kind of rules to arbitrary target spaces.
        /// </summary>
        public const int IntermediateToAny = 300;

        /// <summary>
        /// Priority for Intermediate kind of rules from arbitrary target spaces.
        /// </summary>
        public const int IntermediateFromAny = 400;
    }
}