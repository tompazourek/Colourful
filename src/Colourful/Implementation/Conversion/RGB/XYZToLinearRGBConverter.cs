using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
#if (!READONLYCOLLECTIONS)
using Vector = System.Collections.Generic.IList<double>;
using Matrix = System.Collections.Generic.IList<System.Collections.Generic.IList<double>>;

#else
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;
#endif

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="XYZColor"/> to <see cref="LinearRGBColor"/>.
    /// </summary>
    /// <remarks>
    /// The target RGB working space is <see cref="RGBColor.DefaultWorkingSpace"/> when not set.
    /// </remarks>
    public class XYZToLinearRGBConverter : LinearRGBAndXYZConverterBase, IColorConversion<XYZColor, LinearRGBColor>
    {
        private readonly Matrix _conversionMatrix;

        /// <summary>
        /// Constructs with <see cref="RGBColor.DefaultWorkingSpace"/>.
        /// </summary>
        public XYZToLinearRGBConverter() : this(null)
        {
        }

        /// <summary>
        /// Constructs with arbitrary working space.
        /// </summary>
        public XYZToLinearRGBConverter(IRGBWorkingSpace targetRGBWorkingSpace)
        {
            TargetRGBWorkingSpace = targetRGBWorkingSpace ?? RGBColor.DefaultWorkingSpace;
            _conversionMatrix = GetXYZToRGBMatrix(TargetRGBWorkingSpace);
        }

        /// <summary>
        /// Target RGB working space. When not set, target RGB working space is <see cref="RGBColor.DefaultWorkingSpace"/>.
        /// </summary>
        public IRGBWorkingSpace TargetRGBWorkingSpace { get; }

        /// <summary>
        /// Converts from <see cref="XYZColor"/> to <see cref="LinearRGBColor"/>.
        /// </summary>
        public LinearRGBColor Convert(XYZColor input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var inputVector = input.Vector;
            Vector uncompandedVector = _conversionMatrix.MultiplyBy(inputVector).CropRange(0, 1);
            var result = new LinearRGBColor(uncompandedVector, TargetRGBWorkingSpace);
            return result;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        protected bool Equals(XYZToLinearRGBConverter other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return Equals(TargetRGBWorkingSpace, other.TargetRGBWorkingSpace);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((XYZToLinearRGBConverter)obj);
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            return (TargetRGBWorkingSpace != null ? TargetRGBWorkingSpace.GetHashCode() : 0);
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(XYZToLinearRGBConverter left, XYZToLinearRGBConverter right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(XYZToLinearRGBConverter left, XYZToLinearRGBConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}