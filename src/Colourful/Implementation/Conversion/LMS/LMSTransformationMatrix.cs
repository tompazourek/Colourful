using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;
using Vector = System.Collections.Generic.IReadOnlyList<double>;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Matrix used for transformation from XYZ to LMS, defining the cone response domain.
    /// Used in <see cref="Colourful.Conversion.IChromaticAdaptation" />
    /// </summary>
    /// <remarks>
    /// Matrix data obtained from:
    /// Two New von Kries Based Chromatic Adaptation Transforms Found by Numerical Optimization
    /// S. Bianco, R. Schettini
    /// DISCo, Department of Informatics, Systems and Communication, University of Milan-Bicocca, viale Sarca 336, 20126 Milan, Italy
    /// http://www.ivl.disco.unimib.it/papers2003/CRA-CAT.pdf
    /// </remarks>
    public static class LMSTransformationMatrix
    {
        /// <summary>
        /// Von Kries chromatic adaptation transform matrix (Hunt-Pointer-Estevez adjusted for D65)
        /// </summary>
        public static readonly Matrix VonKriesHPEAdjusted = new Vector[]
        {
            new[] { 0.40024, 0.7076, -0.08081 },
            new[] { -0.2263, 1.16532, 0.0457 },
            new[] { 0, 0, 0.91822 },
        };

        /// <summary>
        /// Von Kries chromatic adaptation transform matrix (Hunt-Pointer-Estevez for equal energy)
        /// </summary>
        public static readonly Matrix VonKriesHPE = new Vector[]
        {
            new[] { 0.3897, 0.6890, -0.0787 },
            new[] { -0.2298, 1.1834, 0.0464 },
            new[] { 0.0, 0.0, 1.0 },
        };

        /// <summary>
        /// XYZ scaling chromatic adaptation transform matrix
        /// </summary>
        public static readonly Matrix XYZScaling = MatrixFactory.CreateIdentity(3);

        /// <summary>
        /// Bradford chromatic adaptation transform matrix (used in CMCCAT97)
        /// </summary>
        public static readonly Matrix Bradford = new Vector[]
        {
            new[] { 0.8951, 0.2664, -0.1614 },
            new[] { -0.7502, 1.7135, 0.0367 },
            new[] { 0.0389, -0.0685, 1.0296 },
        };

        /// <summary>
        /// Spectral sharpening and the Bradford transform
        /// </summary>
        public static readonly Matrix BradfordSharp = new Vector[]
        {
            new[] { 1.2694, -0.0988, -0.1706 },
            new[] { -0.8364, 1.8006, 0.0357 },
            new[] { 0.0297, -0.0315, 1.0018 },
        };

        /// <summary>
        /// CMCCAT2000 (fitted from all available color data sets)
        /// </summary>
        public static readonly Matrix CMCCAT2000 = new Vector[]
        {
            new[] { 0.7982, 0.3389, -0.1371 },
            new[] { -0.5918, 1.5512, 0.0406 },
            new[] { 0.0008, 0.239, 0.9753 },
        };

        /// <summary>
        /// CAT02 (optimized for minimizing CIELAB differences)
        /// </summary>
        public static readonly Matrix CAT02 = new Vector[]
        {
            new[] { 0.7328, 0.4296, -0.1624 },
            new[] { -0.7036, 1.6975, 0.0061 },
            new[] { 0.0030, 0.0136, 0.9834 },
        };
    }
}