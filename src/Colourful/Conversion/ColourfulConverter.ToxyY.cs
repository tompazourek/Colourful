using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Toxy")]
        public xyYColor ToxyY(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Toxy")]
        public xyYColor ToxyY(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Toxy"), SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public xyYColor ToxyY(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion
            var converter = new xyYAndXYZConverter();
            var result = converter.Convert(color);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Toxy")]
        public xyYColor ToxyY(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Toxy")]
        public xyYColor ToxyY(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Toxy")]
        public xyYColor ToxyY(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Toxy")]
        public xyYColor ToxyY(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Toxy")]
        public xyYColor ToxyY(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Toxy")]
        public xyYColor ToxyY(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

#if (DYNAMIC)
        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Toxy")]
        public xyYColor ToxyY<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as xyYColor;

            if (converted != null)
            {
                return converted;
            }
            else
            {
                dynamic source = color;

                return ToxyY(source);
            }
        }
#endif
    }
}