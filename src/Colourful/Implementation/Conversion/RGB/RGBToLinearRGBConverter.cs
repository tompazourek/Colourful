using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
    /// Converts from <see cref="RGBColor"/> to <see cref="LinearRGBColor"/>.
    /// </summary>
    public class RGBToLinearRGBConverter : IColorConversion<RGBColor, LinearRGBColor>
    {
        /// <summary>
        /// Converts from <see cref="RGBColor"/> to <see cref="LinearRGBColor"/>.
        /// </summary>
        public LinearRGBColor Convert(RGBColor input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var uncompandedVector = UncompandVector(input);
            var converted = new LinearRGBColor(uncompandedVector, input.WorkingSpace);
            return converted;
        }

        /// <summary>
        /// Applying the working space inverse companding function (<see cref="IRGBWorkingSpace.Companding"/>) to RGB vector.
        /// </summary>
        private static Vector UncompandVector(RGBColor rgbColor)
        {
            var companding = rgbColor.WorkingSpace.Companding;
            var compandedVector = rgbColor.Vector;
            Vector uncompandedVector = new[]
            {
                companding.InverseCompanding(compandedVector[0]).CropRange(0, 1),
                companding.InverseCompanding(compandedVector[1]).CropRange(0, 1),
                companding.InverseCompanding(compandedVector[2]).CropRange(0, 1)
            };
            return uncompandedVector;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        protected bool Equals(RGBToLinearRGBConverter other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return true;
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RGBToLinearRGBConverter)obj);
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            return 1;
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(RGBToLinearRGBConverter left, RGBToLinearRGBConverter right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(RGBToLinearRGBConverter left, RGBToLinearRGBConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}