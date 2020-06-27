using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Colourful.Internals;

namespace Colourful
{
    /// <summary>
    /// Exception when trying to create a converter for an impossible conversion.
    /// </summary>
    [Serializable]
    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Using the default exception formatting.")]
    public class MissingConversionMetadataException : Exception
    {
        /// <summary>
        /// Conversion metadata key, see <see cref="IConversionMetadata.GetItemOrDefault{TValue}"/>.
        /// </summary>
        public string Key { get; }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public MissingConversionMetadataException()
        {
        }

        /// <param name="message">Exception message</param>
        /// <param name="key">Key that was missing</param>
        public MissingConversionMetadataException(string message, string key)
            : base(message)
        {
            Key = key;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public MissingConversionMetadataException(string message)
            : base(message) { }
    
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public MissingConversionMetadataException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public MissingConversionMetadataException(string message, Exception innerException)
            : base(message, innerException) { }
    
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public MissingConversionMetadataException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        protected MissingConversionMetadataException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}