using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Conversion;

namespace Colourful.Colors
{
    /// <summary>
    /// CIE 1931 XYZ with reference white
    /// </summary>
    public class XYZColor : XYZColorBase
    {
        #region Other

        /// <summary>
        /// D50, used when reference white is not specified explicitly
        /// </summary>
        public static readonly XYZColorBase DefaultReferenceWhite = Illuminants.D50;

        #endregion

        #region Constructor

        public XYZColor(double x, double y, double z) : this(x, y, z, DefaultReferenceWhite)
        {
        }

        public XYZColor(double x, double y, double z, XYZColorBase referenceWhite) : base(x, y, z)
        {
            x.CheckRange(0, referenceWhite.X);
            y.CheckRange(0, referenceWhite.Y);
            z.CheckRange(0, referenceWhite.Z);
            ReferenceWhite = referenceWhite;
        }

        #endregion

        #region Attributes

        /// <see cref="Illuminants"/>
        public XYZColorBase ReferenceWhite { get; private set; }

        #endregion

        #region Equality

        protected bool Equals(XYZColor other)
        {
            return base.Equals(other) && ReferenceWhite.Equals(other.ReferenceWhite);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((XYZColor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ ReferenceWhite.GetHashCode();
            }
        }

        public static bool operator ==(XYZColor left, XYZColor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(XYZColor left, XYZColor right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Conversions

        #region From

        /// <remarks>
        /// Target reference white is <see cref="DefaultReferenceWhite"/>.
        /// </remarks>
        public static XYZColor FromLab(LabColor input)
        {
            var converter = new XYZAndLabConverter();
            var result = converter.Convert(input);
            return result;
        }

        /// <remarks>
        /// Target XYZ color has given reference white.
        /// </remarks>
        public static XYZColor FromLab(LabColor input, XYZColorBase referenceWhite)
        {
            var converter = new XYZAndLabConverter();
            var result = converter.Convert(input, referenceWhite);
            return result;
        }

        #endregion

        #region To

        public LabColor ToLab()
        {
            var converter = new XYZAndLabConverter();
            LabColor result = converter.Convert(this);
            return result;
        }

        #endregion

        #endregion
    }
}