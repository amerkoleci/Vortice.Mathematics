// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Describes a 3-by-3 floating point matrix.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public readonly struct Matrix3x3
{
    /// <summary>
    /// A <see cref="Matrix3x3"/> with all of its components set to zero.
    /// </summary>
    public static readonly Matrix3x3 Zero = new();

    /// <summary>
    /// The identity <see cref="Matrix3x3"/>.
    /// </summary>
    public static readonly Matrix3x3 Identity = new(1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f);

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
    /// Initializes a new instance of the <see cref="Matrix3x3"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public Matrix3x3(float value)
    {
        M11 = value;
        M12 = value;
        M13 = value;
        M21 = value;
        M22 = value;
        M23 = value;
        M31 = value;
        M32 = value;
        M33 = value;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Matrix3x3"/> struct.
    /// </summary>
    /// <param name="m11">The value to assign at row 1 column 1 of the Matrix3x3.</param>
    /// <param name="m12">The value to assign at row 1 column 2 of the Matrix3x3.</param>
    /// <param name="m13">The value to assign at row 1 column 3 of the Matrix3x3.</param>
    /// <param name="m21">The value to assign at row 2 column 1 of the Matrix3x3.</param>
    /// <param name="m22">The value to assign at row 2 column 2 of the Matrix3x3.</param>
    /// <param name="m23">The value to assign at row 2 column 3 of the Matrix3x3.</param>
    /// <param name="m31">The value to assign at row 3 column 1 of the Matrix3x3.</param>
    /// <param name="m32">The value to assign at row 3 column 2 of the Matrix3x3.</param>
    /// <param name="m33">The value to assign at row 3 column 3 of the Matrix3x3.</param>
    public Matrix3x3(float m11, float m12, float m13,
        float m21, float m22, float m23,
        float m31, float m32, float m33)
    {
        M11 = m11; M12 = m12; M13 = m13;
        M21 = m21; M22 = m22; M23 = m23;
        M31 = m31; M32 = m32; M33 = m33;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix3x3"/> struct.
    /// </summary>
    /// <param name="values">The values to assign to the components of the Matrix3x3. This must be an array with sixteen elements.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="values"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="values"/> contains more or less than sixteen elements.</exception>
    public Matrix3x3(float[] values)
    {
        if (values == null)
            throw new ArgumentNullException(nameof(values));
        if (values.Length != 9)
            throw new ArgumentOutOfRangeException(nameof(values), "There must be only nine input values for Matrix3x3.");

        M11 = values[0];
        M12 = values[1];
        M13 = values[2];

        M21 = values[3];
        M22 = values[4];
        M23 = values[5];

        M31 = values[6];
        M32 = values[7];
        M33 = values[8];
    }
}
