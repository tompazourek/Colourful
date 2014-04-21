using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Implementation.RGB;

namespace Colourful.Colors
{
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public static class RGBWorkingSpaces
    {
        /// <summary>
        /// sRGB
        /// </summary>
        /// <remarks>
        /// Uses proper companding function, according to:
        /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
        /// </remarks>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "s")]
        public static readonly IRGBWorkingSpace sRGB = new RGBWorkingSpace(Illuminants.D65, new sRGBCompanding(), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6400, 0.3300), new ChromaticityCoordinates(0.3000, 0.6000), new ChromaticityCoordinates(0.1500, 0.0600)));

        /// <summary>
        /// Simplified sRGB (uses <see cref="GammaCompanding">gamma companding</see> instead of <see cref="sRGBCompanding"/>).
        /// See also <see cref="sRGB"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "s")]
        public static readonly IRGBWorkingSpace sRGBSimplified = new RGBWorkingSpace(Illuminants.D65, new GammaCompanding(2.2), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6400, 0.3300), new ChromaticityCoordinates(0.3000, 0.6000), new ChromaticityCoordinates(0.1500, 0.0600)));

        /// <summary>
        /// ECI RGB v2
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Bv")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Bv")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ECIRG")]
        public static readonly IRGBWorkingSpace ECIRGBv2 = new RGBWorkingSpace(Illuminants.D50, new LCompanding(), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6700, 0.3300), new ChromaticityCoordinates(0.2100, 0.7100), new ChromaticityCoordinates(0.1400, 0.0800)));

        /// <summary>
        /// Adobe RGB (1998)
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly IRGBWorkingSpace AdobeRGB1998 = new RGBWorkingSpace(Illuminants.D65, new GammaCompanding(2.2), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6400, 0.3300), new ChromaticityCoordinates(0.2100, 0.7100), new ChromaticityCoordinates(0.1500, 0.0600)));

        /// <summary>
        /// Apple sRGB
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly IRGBWorkingSpace ApplesRGB = new RGBWorkingSpace(Illuminants.D65, new GammaCompanding(1.8), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6250, 0.3400), new ChromaticityCoordinates(0.2800, 0.5950), new ChromaticityCoordinates(0.1550, 0.0700)));

        /// <summary>
        /// Best RGB
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly IRGBWorkingSpace BestRGB = new RGBWorkingSpace(Illuminants.D50, new GammaCompanding(2.2), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.7347, 0.2653), new ChromaticityCoordinates(0.2150, 0.7750), new ChromaticityCoordinates(0.1300, 0.0350)));

        /// <summary>
        /// Beta RGB
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly IRGBWorkingSpace BetaRGB = new RGBWorkingSpace(Illuminants.D50, new GammaCompanding(2.2), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6888, 0.3112), new ChromaticityCoordinates(0.1986, 0.7551), new ChromaticityCoordinates(0.1265, 0.0352)));

        /// <summary>
        /// Bruce RGB
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly IRGBWorkingSpace BruceRGB = new RGBWorkingSpace(Illuminants.D65, new GammaCompanding(2.2), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6400, 0.3300), new ChromaticityCoordinates(0.2800, 0.6500), new ChromaticityCoordinates(0.1500, 0.0600)));

        /// <summary>
        /// CIE RGB
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CIERGB")]
        public static readonly IRGBWorkingSpace CIERGB = new RGBWorkingSpace(Illuminants.E, new GammaCompanding(2.2), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.7350, 0.2650), new ChromaticityCoordinates(0.2740, 0.7170), new ChromaticityCoordinates(0.1670, 0.0090)));

        /// <summary>
        /// ColorMatch RGB
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly IRGBWorkingSpace ColorMatchRGB = new RGBWorkingSpace(Illuminants.D50, new GammaCompanding(1.8), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6300, 0.3400), new ChromaticityCoordinates(0.2950, 0.6050), new ChromaticityCoordinates(0.1500, 0.0750)));

        /// <summary>
        /// Don RGB 4
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly IRGBWorkingSpace DonRGB4 = new RGBWorkingSpace(Illuminants.D50, new GammaCompanding(2.2), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6960, 0.3000), new ChromaticityCoordinates(0.2150, 0.7650), new ChromaticityCoordinates(0.1300, 0.0350)));

        /// <summary>
        /// Ekta Space PS5
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ekta")]
        public static readonly IRGBWorkingSpace EktaSpacePS5 = new RGBWorkingSpace(Illuminants.D50, new GammaCompanding(2.2), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6950, 0.3050), new ChromaticityCoordinates(0.2600, 0.7000), new ChromaticityCoordinates(0.1100, 0.0050)));

        /// <summary>
        /// NTSC RGB
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NTSCRGB")]
        public static readonly IRGBWorkingSpace NTSCRGB = new RGBWorkingSpace(Illuminants.C, new GammaCompanding(2.2), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6700, 0.3300), new ChromaticityCoordinates(0.2100, 0.7100), new ChromaticityCoordinates(0.1400, 0.0800)));

        /// <summary>
        /// PAL/SECAM RGB
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "PALSECAMRGB")]
        public static readonly IRGBWorkingSpace PALSECAMRGB = new RGBWorkingSpace(Illuminants.D65, new GammaCompanding(2.2), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6400, 0.3300), new ChromaticityCoordinates(0.2900, 0.6000), new ChromaticityCoordinates(0.1500, 0.0600)));

        /// <summary>
        /// ProPhoto RGB
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly IRGBWorkingSpace ProPhotoRGB = new RGBWorkingSpace(Illuminants.D50, new GammaCompanding(1.8), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.7347, 0.2653), new ChromaticityCoordinates(0.1596, 0.8404), new ChromaticityCoordinates(0.0366, 0.0001)));

        /// <summary>
        /// SMPTE-C RGB
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SMPTECRGB")]
        public static readonly IRGBWorkingSpace SMPTECRGB = new RGBWorkingSpace(Illuminants.D65, new GammaCompanding(2.2), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6300, 0.3400), new ChromaticityCoordinates(0.3100, 0.5950), new ChromaticityCoordinates(0.1550, 0.0700)));

        /// <summary>
        /// Wide Gamut RGB
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gamuts")]
        public static readonly IRGBWorkingSpace WideGamutRGB = new RGBWorkingSpace(Illuminants.D50, new GammaCompanding(2.2), new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.7350, 0.2650), new ChromaticityCoordinates(0.1150, 0.8260), new ChromaticityCoordinates(0.1570, 0.0180)));
    }
}