#region License

// Copyright (C) Tomáš Pažourek, 2014
// All rights reserved.
// 
// Distributed under MIT license as a part of project Colourful.
// https://github.com/tompazourek/Colourful

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.ChromaticAdaptation;

namespace Colourful.Conversion
{
    /// <summary>
    /// Converts between color spaces and makes sure that the color is adapted using chromatic adaptation.
    /// </summary>
    public partial class ColorConverter
    {
        #region Attributes

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly IChromaticAdaptation DefaultChromaticAdaptationMethod = new BradfordChromaticAdaptation();

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly XYZColor DefaultWhitePoint = Illuminants.D50;

        /// <summary>
        /// Chromatic adaptation method used. When null, no adaptation will be performed.
        /// </summary>
        public IChromaticAdaptation ChromaticAdaptation { get; set; }

        /// <summary>
        /// White point used for chromatic adaptation in conversions from/to XYZ color space.
        /// When null, no adaptation will be performed.
        /// <seealso cref="TargetLabWhitePoint"/>
        /// </summary>
        public XYZColor WhitePoint { get; set; }

        /// <summary>
        /// White point used *when creating* Lab/LChab colors. (Lab/LChab colors on the input already contain the white point information)
        /// Defaults to: <see cref="LabColor.DefaultWhitePoint"/>.
        /// </summary>
        public XYZColor TargetLabWhitePoint { get; set; }

        /// <summary>
        /// White point used *when creating* Luv/LChuv colors. (Luv/LChuv colors on the input already contain the white point information)
        /// Defaults to: <see cref="LuvColor.DefaultWhitePoint"/>.
        /// </summary>
        public XYZColor TargetLuvWhitePoint { get; set; }

        /// <summary>
        /// White point used *when creating* HunterLab colors. (HunterLab colors on the input already contain the white point information)
        /// Defaults to: <see cref="HunterLabColor.DefaultWhitePoint"/>.
        /// </summary>
        public XYZColor TargetHunterLabWhitePoint { get; set; }

        /// <summary>
        /// Working space used *when creating* RGB colors. (RGB colors on the input already contain the working space information)
        /// Defaults to: <see cref="RGBColor.DefaultWorkingSpace"/>.
        /// </summary>
        public IRGBWorkingSpace TargetRGBWorkingSpace { get; set; }

        #endregion

        public ColorConverter()
        {
            WhitePoint = DefaultWhitePoint;
            ChromaticAdaptation = DefaultChromaticAdaptationMethod;
            TargetLabWhitePoint = LabColor.DefaultWhitePoint;
            TargetHunterLabWhitePoint = HunterLabColor.DefaultWhitePoint;
            TargetLuvWhitePoint = LuvColor.DefaultWhitePoint;
            TargetRGBWorkingSpace = RGBColor.DefaultWorkingSpace;
        }

        private bool IsChromaticAdaptationPerformed
        {
            get
            {
                bool result = WhitePoint != null && ChromaticAdaptation != null;
                return result;
            }
        }
    }
}