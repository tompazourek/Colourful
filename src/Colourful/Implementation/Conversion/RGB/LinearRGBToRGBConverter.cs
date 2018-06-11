using System;
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
    /// Converts from <see cref="LinearRGBColor" /> to <see cref="RGBColor" />.
    /// </summary>
    public class LinearRGBToRGBConverter : IColorConversion<LinearRGBColor, RGBColor>
    {
        /// <summary>
        /// Converts from <see cref="LinearRGBColor" /> to <see cref="RGBColor" />.
        /// </summary>
        public RGBColor Convert(LinearRGBColor input)
        {
            var result = CompandVector(input.Vector, input.WorkingSpace);
            return result;
        }

        /// <summary>
        /// Applying the working space companding function (<see cref="IRGBWorkingSpace.Companding" />) to uncompanded vector.
        /// </summary>
        private static RGBColor CompandVector(Vector uncompandedVector, IRGBWorkingSpace workingSpace)
        {
            var companding = workingSpace.Companding;
            Vector compandedVector = new[]
            {
                companding.Companding(uncompandedVector[0]).CropRange(0, 1),
                companding.Companding(uncompandedVector[1]).CropRange(0, 1),
                companding.Companding(uncompandedVector[2]).CropRange(0, 1)
            };
            var result = new RGBColor(compandedVector, workingSpace);
            return result;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        protected bool Equals(LinearRGBToRGBConverter other)
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
            return Equals((LinearRGBToRGBConverter)obj);
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            return 1;
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(LinearRGBToRGBConverter left, LinearRGBToRGBConverter right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(LinearRGBToRGBConverter left, LinearRGBToRGBConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}