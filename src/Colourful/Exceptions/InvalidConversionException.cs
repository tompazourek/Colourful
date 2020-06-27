using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Colourful
{
    /// <summary>
    /// Exception when trying to create a converter for an impossible conversion.
    /// </summary>
    [Serializable]
    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Using the default exception formatting.")]
    public class InvalidConversionException : Exception
    {
        /// <summary>
        /// Source color type
        /// </summary>
        public Type SourceType { get; }

        /// <summary>
        /// Target color type
        /// </summary>
        public Type TargetType { get; }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public InvalidConversionException()
        {
        }

        /// <param name="sourceType">Source color type</param>
        /// <param name="targetType">Target color type</param>
        public InvalidConversionException(Type sourceType, Type targetType)
            : base($"Conversion between {sourceType} and {targetType} is not possible according to registered strategies.")
        {
            SourceType = sourceType;
            TargetType = targetType;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public InvalidConversionException(string message)
            : base(message) { }
    
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public InvalidConversionException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public InvalidConversionException(string message, Exception innerException)
            : base(message, innerException) { }
    
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public InvalidConversionException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        protected InvalidConversionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}