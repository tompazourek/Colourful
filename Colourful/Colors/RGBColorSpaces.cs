using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using Colourful.RGBWorkingSpaces;

namespace Colourful.Colors
{
    public static class RGBColorSpaces
    {
        /// <summary>
        /// sRGB
        /// </summary>
        public static readonly IRGBWorkingSpace sRGB = new sRGBWorkingSpace();

        /// <summary>
        /// Simplified sRGB (uses <see cref="GammaCompanding">gamma companding</see> instead of <see cref="sRGBCompanding"/>).
        /// See also <see cref="sRGB"/>.
        /// </summary>
        public static readonly IRGBWorkingSpace sRGBSimplified = new sRGBSimplifiedWorkingSpace();

        /// <summary>
        /// ECI RGB v2
        /// </summary>
        public static readonly IRGBWorkingSpace ECIRGBv2 = new ECIRGBv2();

        /// <summary>
        /// Adobe RGB (1998)
        /// </summary>
        public static readonly IRGBWorkingSpace AdobeRGB1998 = new AdobeRGB1998();

        /// <summary>
        /// Apple sRGB
        /// </summary>
        public static readonly IRGBWorkingSpace ApplesRGB = new ApplesRGB();

        /// <summary>
        /// Best RGB
        /// </summary>
        public static readonly IRGBWorkingSpace BestRGB = new BestRGB();

        /// <summary>
        /// Beta RGB
        /// </summary>
        public static readonly IRGBWorkingSpace BetaRGB = new BetaRGB();

        /// <summary>
        /// Bruce RGB
        /// </summary>
        public static readonly IRGBWorkingSpace BruceRGB = new BruceRGB();

        /// <summary>
        /// CIE RGB
        /// </summary>
        public static readonly IRGBWorkingSpace CIERGB = new CIERGB();

        /// <summary>
        /// ColorMatch RGB
        /// </summary>
        public static readonly IRGBWorkingSpace ColorMatchRGB = new ColorMatchRGB();

        /// <summary>
        /// Don RGB 4
        /// </summary>
        public static readonly IRGBWorkingSpace DonRGB4 = new DonRGB4();

        /// <summary>
        /// Ekta Space PS5
        /// </summary>
        public static readonly IRGBWorkingSpace EktaSpacePS5 = new EktaSpacePS5();

        /// <summary>
        /// NTSC RGB
        /// </summary>
        public static readonly IRGBWorkingSpace NTSCRGB = new NTSCRGB();

        /// <summary>
        /// PAL/SECAM RGB
        /// </summary>
        public static readonly IRGBWorkingSpace PALSECAMRGB = new PALSECAMRGB();

        /// <summary>
        /// ProPhoto RGB
        /// </summary>
        public static readonly IRGBWorkingSpace ProPhotoRGB = new ProPhotoRGB();

        /// <summary>
        /// SMPTE-C RGB
        /// </summary>
        public static readonly IRGBWorkingSpace SMPTECRGB = new SMPTECRGB();

        /// <summary>
        /// Wide Gamut RGB
        /// </summary>
        public static readonly IRGBWorkingSpace WideGamutsRGB = new WideGamutsRGB();
    }
}