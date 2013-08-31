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
            return input.ToXYZ();
        }

        /// <remarks>
        /// Target XYZ color has given reference white.
        /// </remarks>
        public static XYZColor FromLab(LabColor input, XYZColorBase referenceWhite)
        {
            return input.ToXYZ(referenceWhite);
        }

        /// <remarks>
        /// Reference white of output is taken from RGB working space.
        /// </remarks>
        public static XYZColor FromRGB(RGBColor input)
        {
            return input.ToXYZ();
        }

        /// <remarks>
        /// Output color is adjusted to the given reference white (using <see cref="RGBAndXYZConverter.DefaultChromaticAdaptation"/>).
        /// </remarks>
        public static XYZColor FromRGB(RGBColor input, XYZColorBase referenceWhite)
        {
            return input.ToXYZ(referenceWhite);
        }

        #endregion

        #region To

        public LabColor ToLab()
        {
            var converter = new XYZAndLabConverter();
            LabColor result = converter.Convert(this);
            return result;
        }

        /// <remarks>
        /// Target RGB working space is <see cref="RGBColor.DefaultWorkingSpace"/>.
        /// Input color is adjusted to target RGB working space reference white (using <see cref="RGBAndXYZConverter.DefaultChromaticAdaptation"/>).
        /// </remarks>
        public RGBColor ToRGB()
        {
            var converter = new RGBAndXYZConverter();
            RGBColor result = converter.Convert(this);
            return result;
        }

        /// <remarks>
        /// Input color is adjusted to target RGB working space reference white (using <see cref="RGBAndXYZConverter.DefaultChromaticAdaptation"/>).
        /// </remarks>
        public RGBColor ToRGB(IRGBWorkingSpace workingSpace)
        {
            var converter = new RGBAndXYZConverter();
            RGBColor result = converter.Convert(this, workingSpace);
            return result;
        }

        #endregion

        #endregion
    }
}