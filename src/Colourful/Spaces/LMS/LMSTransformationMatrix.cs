using System.Diagnostics.CodeAnalysis;
using Colourful.Internals;

namespace Colourful
{
    /// <summary>
    /// Matrix used for transformation from XYZ to LMS, defining the cone response domain.
    /// </summary>
    /// <remarks>
    /// Matrix data obtained from:
    /// Two New von Kries Based Chromatic Adaptation Transforms Found by Numerical Optimization
    /// S. Bianco, R. Schettini
    /// DISCo, Department of Informatics, Systems and Communication, University of Milan-Bicocca, viale Sarca 336, 20126 Milan, Italy
    /// http://www.ivl.disco.unimib.it/papers2003/CRA-CAT.pdf
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class LMSTransformationMatrix
    {
        /// <summary>
        /// Von Kries chromatic adaptation transform matrix (Hunt-Pointer-Estevez adjusted for D65).
        /// </summary>
        public static readonly double[,] VonKriesHPEAdjusted =
        {
            { 0.40024, 0.7076, -0.08081 },
            { -0.2263, 1.16532, 0.0457 },
            { 0, 0, 0.91822 },
        };

        /// <summary>
        /// Von Kries chromatic adaptation transform matrix (Hunt-Pointer-Estevez for equal energy).
        /// </summary>
        public static readonly double[,] VonKriesHPE =
        {
            { 0.3897, 0.6890, -0.0787 },
            { -0.2298, 1.1834, 0.0464 },
            { 0.0, 0.0, 1.0 },
        };

        /// <summary>
        /// XYZ scaling chromatic adaptation transform matrix.
        /// </summary>
        public static readonly double[,] XYZScaling = MatrixUtils.CreateIdentity(size: 3);

        /// <summary>
        /// Bradford chromatic adaptation transform matrix (used in CMCCAT97).
        /// </summary>
        public static readonly double[,] Bradford =
        {
            { 0.8951, 0.2664, -0.1614 },
            { -0.7502, 1.7135, 0.0367 },
            { 0.0389, -0.0685, 1.0296 },
        };

        /// <summary>
        /// Spectral sharpening and the Bradford transform.
        /// </summary>
        public static readonly double[,] BradfordSharp =
        {
            { 1.2694, -0.0988, -0.1706 },
            { -0.8364, 1.8006, 0.0357 },
            { 0.0297, -0.0315, 1.0018 },
        };

        /// <summary>
        /// CMCCAT2000 (fitted from all available color data sets).
        /// </summary>
        public static readonly double[,] CMCCAT2000 =
        {
            { 0.7982, 0.3389, -0.1371 },
            { -0.5918, 1.5512, 0.0406 },
            { 0.0008, 0.239, 0.9753 },
        };

        /// <summary>
        /// CAT02 (optimized for minimizing CIELAB differences).
        /// </summary>
        public static readonly double[,] CAT02 =
        {
            { 0.7328, 0.4296, -0.1624 },
            { -0.7036, 1.6975, 0.0061 },
            { 0.0030, 0.0136, 0.9834 },
        };
    }
}
