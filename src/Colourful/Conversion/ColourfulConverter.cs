using System.Diagnostics.CodeAnalysis;
using Colourful.Implementation.Conversion;
#if (!READONLYCOLLECTIONS)
using Vector = System.Collections.Generic.IList<double>;
using Matrix = System.Collections.Generic.IList<System.Collections.Generic.IList<double>>;

#else
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;
#endif

namespace Colourful.Conversion
{
    /// <summary>
    /// Converts between color spaces and makes sure that the color is adapted using chromatic adaptation.
    /// </summary>
    public partial class ColourfulConverter
    {
        #region Attributes

        /// <summary>
        /// Default white point
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly XYZColor DefaultWhitePoint = Illuminants.D65;

        private Matrix _transformationMatrix;

        /// <summary>
        /// Chromatic adaptation method used. When null, no adaptation will be performed.
        /// </summary>
        public IChromaticAdaptation ChromaticAdaptation { get; set; }

        /// <summary>
        /// Transformation matrix used in conversion to <see cref="LMSColor"/>, also used in the default Von Kries Chromatic Adaptation method.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public Matrix LMSTransformationMatrix
        {
            get { return _transformationMatrix; }
            set
            {
                _transformationMatrix = value;

                if (_cachedXYZAndLMSConverter == null)
                    _cachedXYZAndLMSConverter = new XYZAndLMSConverter(value);
                else
                    _cachedXYZAndLMSConverter.TransformationMatrix = value;
            }
        }

        private XYZAndLMSConverter _cachedXYZAndLMSConverter;

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

        /// <summary>
        /// Constructs the converter and sets the defaults
        /// </summary>
        public ColourfulConverter()
        {
            WhitePoint = DefaultWhitePoint;
            LMSTransformationMatrix = XYZAndLMSConverter.DefaultTransformationMatrix;
            ChromaticAdaptation = new VonKriesChromaticAdaptation(_cachedXYZAndLMSConverter, _cachedXYZAndLMSConverter);

            TargetLabWhitePoint = LabColor.DefaultWhitePoint;
            TargetHunterLabWhitePoint = HunterLabColor.DefaultWhitePoint;
            TargetLuvWhitePoint = LuvColor.DefaultWhitePoint;
            TargetRGBWorkingSpace = RGBColor.DefaultWorkingSpace;
        }

        private bool IsChromaticAdaptationPerformed
        {
            get
            {
                var result = WhitePoint != null && ChromaticAdaptation != null;
                return result;
            }
        }
    }
}