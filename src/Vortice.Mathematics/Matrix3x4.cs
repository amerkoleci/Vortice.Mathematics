// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Describes a 3-by-4 floating point matrix.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public readonly struct Matrix3x4
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
    /// Value at row 1 column 4.
    /// </summary>
    public readonly float M14;

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
    /// Value at row 2 column 4.
    /// </summary>
    public readonly float M24;

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
    /// Value at row 3 column 4.
    /// </summary>
    public readonly float M34;

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix3x4"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public Matrix3x4(float value)
    {
        M11 = M12 = M13 = M14 =
        M21 = M22 = M23 = M24 =
        M31 = M32 = M33 = M34 = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix3x4"/> struct.
    /// </summary>
    /// <param name="m11">The value to assign at row 1 column 1 of the matrix.</param>
    /// <param name="m12">The value to assign at row 1 column 2 of the matrix.</param>
    /// <param name="m13">The value to assign at row 1 column 3 of the matrix.</param>
    /// <param name="m14">The value to assign at row 1 column 4 of the matrix.</param>
    /// <param name="m21">The value to assign at row 2 column 1 of the matrix.</param>
    /// <param name="m22">The value to assign at row 2 column 2 of the matrix.</param>
    /// <param name="m23">The value to assign at row 2 column 3 of the matrix.</param>
    /// <param name="m24">The value to assign at row 2 column 4 of the matrix.</param>
    /// <param name="m31">The value to assign at row 3 column 1 of the matrix.</param>
    /// <param name="m32">The value to assign at row 3 column 2 of the matrix.</param>
    /// <param name="m33">The value to assign at row 3 column 3 of the matrix.</param>
    /// <param name="m34">The value to assign at row 3 column 4 of the matrix.</param>
    public Matrix3x4(float m11, float m12, float m13, float m14,
        float m21, float m22, float m23, float m24,
        float m31, float m32, float m33, float m34)
    {
        M11 = m11;
        M12 = m12;
        M13 = m13;
        M14 = m14;
        M21 = m21;
        M22 = m22;
        M23 = m23;
        M24 = m24;
        M31 = m31;
        M32 = m32;
        M33 = m33;
        M34 = m34;
    }
}
