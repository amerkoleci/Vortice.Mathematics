// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Vortice.Mathematics;

/// <summary>
/// Describes a 5-by-4 floating point matrix.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public readonly struct Matrix5x4 : IEquatable<Matrix5x4>, IFormattable
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
    /// Value at row 4 column 4.
    /// </summary>
    public readonly float M44;

    /// <summary>
    /// Value at row 5 column 1.
    /// </summary>
    public readonly float M51;

    /// <summary>
    /// Value at row 5 column 2.
    /// </summary>
    public readonly float M52;

    /// <summary>
    /// Value at row 5 column 3.
    /// </summary>
    public readonly float M53;

    /// <summary>
    /// Value at row 5 column 4.
    /// </summary>
    public readonly float M54;

    /// <summary>
    /// Compares two <see cref="Matrix5x4"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Matrix5x4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Matrix5x4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    public static bool operator ==(Matrix5x4 left, Matrix5x4 right)
    {
        return (left.M11 == right.M11) && (left.M12 == right.M12) && (left.M13 == right.M13) && (left.M14 == right.M14)
            && (left.M21 == right.M21) && (left.M22 == right.M22) && (left.M23 == right.M23) && (left.M24 == right.M24)
            && (left.M31 == right.M31) && (left.M32 == right.M32) && (left.M33 == right.M33) && (left.M34 == right.M34)
            && (left.M41 == right.M41) && (left.M42 == right.M42) && (left.M43 == right.M43) && (left.M44 == right.M44)
            && (left.M51 == right.M51) && (left.M52 == right.M52) && (left.M53 == right.M53) && (left.M54 == right.M54);
    }

    /// <summary>
    /// Compares two <see cref="Matrix5x4"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Matrix5x4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Matrix5x4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    public static bool operator !=(Matrix5x4 left, Matrix5x4 right)
    {
        return (left.M11 != right.M11) || (left.M12 != right.M12) || (left.M13 != right.M13) || (left.M14 != right.M14)
            || (left.M21 != right.M21) || (left.M22 != right.M22) || (left.M23 != right.M23) || (left.M24 != right.M24)
            || (left.M31 != right.M31) || (left.M32 != right.M32) || (left.M33 != right.M33) || (left.M34 != right.M34)
            || (left.M41 != right.M41) || (left.M42 != right.M42) || (left.M43 != right.M43) || (left.M44 != right.M44)
            || (left.M51 != right.M51) || (left.M52 != right.M52) || (left.M53 != right.M53) || (left.M54 != right.M54);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => (obj is Matrix5x4 other) && Equals(other);

    /// <inheritdoc/>
    public bool Equals(Matrix5x4 other) => this == other;

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        {
            hashCode.Add(M11);
            hashCode.Add(M12);
            hashCode.Add(M13);
            hashCode.Add(M14);
            hashCode.Add(M21);
            hashCode.Add(M22);
            hashCode.Add(M23);
            hashCode.Add(M24);
            hashCode.Add(M31);
            hashCode.Add(M32);
            hashCode.Add(M33);
            hashCode.Add(M34);
            hashCode.Add(M41);
            hashCode.Add(M42);
            hashCode.Add(M43);
            hashCode.Add(M44);
            hashCode.Add(M51);
            hashCode.Add(M52);
            hashCode.Add(M53);
            hashCode.Add(M54);
        }
        return hashCode.ToHashCode();
    }

    /// <inheritdoc/>
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        var separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

        return new StringBuilder()
            .Append('<')
            .Append(M11.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M12.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M13.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M14.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M21.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M22.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M23.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M24.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M31.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M32.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M33.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M34.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M41.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M42.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M43.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M44.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M51.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M52.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M53.ToString(format, formatProvider)).Append(separator).Append(' ')
            .Append(M54.ToString(format, formatProvider))
            .Append('>')
            .ToString();
    }
}
