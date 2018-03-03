using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
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
    /// Converts from <see cref="XYZColor"/> to <see cref="LMSColor"/> and back.
    /// </summary>
    public class XYZAndLMSConverter : IColorConversion<XYZColor, LMSColor>, IColorConversion<LMSColor, XYZColor>
    {
        /// <summary>
        /// Default transformation matrix used, when no other is set. (Bradford)
        /// <see cref="LMSTransformationMatrix"/>
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static readonly Matrix DefaultTransformationMatrix = LMSTransformationMatrix.Bradford;

        /// <summary>
        /// Transformation matrix used for the conversion (definition of the cone response domain).
        /// <see cref="LMSTransformationMatrix"/>
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public Matrix TransformationMatrix
        {
            get { return _transformationMatrix; }
            internal set
            {
                _transformationMatrix = value;
                _transformationMatrixInverse = TransformationMatrix.Inverse();
            }
        }

        private Matrix _transformationMatrixInverse;
        private Matrix _transformationMatrix;

        /// <summary>
        /// Constructs with <see cref="DefaultTransformationMatrix"/>
        /// </summary>
        public XYZAndLMSConverter() : this(DefaultTransformationMatrix)
        {
        }

        /// <param name="transformationMatrix">Definition of the cone response domain (see <see cref="LMSTransformationMatrix"/>), if not set <see cref="DefaultTransformationMatrix"/> will be used.</param>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public XYZAndLMSConverter(Matrix transformationMatrix)
        {
            TransformationMatrix = transformationMatrix;
        }

        /// <summary>
        /// Converts from <see cref="XYZColor"/> to <see cref="LMSColor"/>.
        /// </summary>
        public LMSColor Convert(XYZColor input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var outputVector = TransformationMatrix.MultiplyBy(input.Vector);
            var output = new LMSColor(outputVector);
            return output;
        }

        /// <summary>
        /// Converts from <see cref="LMSColor"/> to <see cref="XYZColor"/>.
        /// </summary>
        public XYZColor Convert(LMSColor input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var outputVector = _transformationMatrixInverse.MultiplyBy(input.Vector);
            var output = new XYZColor(outputVector);
            return output;
        }
    }
}