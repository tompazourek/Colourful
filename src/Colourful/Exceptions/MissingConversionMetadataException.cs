using System;
using System.Runtime.Serialization;
using Colourful.Internals;

namespace Colourful
{
    /// <summary>
    /// Exception when trying to create a converter for an impossible conversion.
    /// </summary>
    [Serializable]
    public class MissingConversionMetadataException : Exception
    {
        /// <summary>
        /// Conversion metadata key, see <see cref="IConversionMetadata.GetItemOrDefault{TValue}"/>.
        /// </summary>
        public string Key { get; }

        /// <inheritdoc />
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
        public MissingConversionMetadataException(string message)
            : base(message) { }
    
        /// <inheritdoc />
        public MissingConversionMetadataException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        /// <inheritdoc />
        public MissingConversionMetadataException(string message, Exception innerException)
            : base(message, innerException) { }
    
        /// <inheritdoc />
        public MissingConversionMetadataException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    
        /// <inheritdoc />
        protected MissingConversionMetadataException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}