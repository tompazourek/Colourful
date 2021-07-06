using System;
using System.Diagnostics.CodeAnalysis;
using Colourful.Internals;

#if !NETSTANDARD1_1
using System.Runtime.Serialization;

#endif

namespace Colourful
{
    /// <summary>
    /// Exception when trying to create a converter for an impossible conversion.
    /// </summary>
#if !NETSTANDARD1_1
    [Serializable]
#endif
    [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Using the default exception formatting.")]
    public class MissingConversionMetadataException : Exception
    {
        /// <summary>
        /// Conversion metadata key, see <see cref="IConversionMetadata.GetItemOrDefault{TValue}" />.
        /// </summary>
        public string Key { get; }

        /// <inheritdoc />
#if !NETSTANDARD1_1
        [ExcludeFromCodeCoverage]
#endif
        public MissingConversionMetadataException()
        {
        }

        /// <param name="message">Exception message</param>
        /// <param name="key">Key that was missing</param>
        public MissingConversionMetadataException(string message, string key)
            : base(message)
            => Key = key;

        /// <inheritdoc />
#if !NETSTANDARD1_1
        [ExcludeFromCodeCoverage]
#endif
        public MissingConversionMetadataException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
#if !NETSTANDARD1_1
        [ExcludeFromCodeCoverage]
#endif
        public MissingConversionMetadataException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        /// <inheritdoc />
#if !NETSTANDARD1_1
        [ExcludeFromCodeCoverage]
#endif
        public MissingConversionMetadataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
#if !NETSTANDARD1_1
        [ExcludeFromCodeCoverage]
#endif
        public MissingConversionMetadataException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

#if !NETSTANDARD1_1
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        protected MissingConversionMetadataException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
