// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Describes a 4-by-3 floating point matrix.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct Matrix4x3
    {
        /// <summary>
        /// Value at row 1 column 1.
        /// </summary>
        public readonly float M11;

        /// <summary>
        /// Value at row 1 column 2.
        /// </summary>
        public readonly float M12;

        /// <summary>
        /// Value at row 1 column 3.
        /// </summary>
        public readonly float M13;

        /// <summary>
        /// Value at row 2 column 1.
        /// </summary>
        public readonly float M21;

        /// <summary>
        /// Value at row 2 column 2.
        /// </summary>
        public readonly float M22;

        /// <summary>
        /// Value at row 2 column 3.
        /// </summary>
        public readonly float M23;

        /// <summary>
        /// Value at row 3 column 1.
        /// </summary>
        public readonly float M31;

        /// <summary>
        /// Value at row 3 column 2.
        /// </summary>
        public readonly float M32;

        /// <summary>
        /// Value at row 3 column 3.
        /// </summary>
        public readonly float M33;

        /// <summary>
        /// Value at row 4 column 1.
        /// </summary>
        public readonly float M41;

        /// <summary>
        /// Value at row 4 column 2.
        /// </summary>
        public readonly float M42;

        /// <summary>
        /// Value at row 4 column 3.
        /// </summary>
        public readonly float M43;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix3x3"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Matrix4x3(float value)
        {
            M11 = M12 = M13 =
            M21 = M22 = M23 =
            M31 = M32 = M33 =
            M41 = M42 = M43 = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4x3"/> struct.
        /// </summary>
        /// <param name="m11">The value to assign at row 1 column 1 of the Matrix4x3.</param>
        /// <param name="m12">The value to assign at row 1 column 2 of the Matrix4x3.</param>
        /// <param name="m13">The value to assign at row 1 column 3 of the Matrix4x3.</param>
        /// <param name="m21">The value to assign at row 2 column 1 of the Matrix4x3.</param>
        /// <param name="m22">The value to assign at row 2 column 2 of the Matrix4x3.</param>
        /// <param name="m23">The value to assign at row 2 column 3 of the Matrix4x3.</param>
        /// <param name="m31">The value to assign at row 3 column 1 of the Matrix4x3.</param>
        /// <param name="m32">The value to assign at row 3 column 2 of the Matrix4x3.</param>
        /// <param name="m33">The value to assign at row 3 column 3 of the Matrix4x3.</param>
        /// <param name="m41">The value to assign at row 4 column 1 of the Matrix4x3.</param>
        /// <param name="m42">The value to assign at row 4 column 2 of the Matrix4x3.</param>
        /// <param name="m43">The value to assign at row 4 column 3 of the Matrix4x3.</param>
        public Matrix4x3(
            float m11, float m12, float m13,
            float m21, float m22, float m23,
            float m31, float m32, float m33,
            float m41, float m42, float m43)
        {
            M11 = m11; M12 = m12; M13 = m13;
            M21 = m21; M22 = m22; M23 = m23;
            M31 = m31; M32 = m32; M33 = m33;
            M41 = m41; M42 = m42; M43 = m43;
        }
    }
}
