// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

// This file includes code based on code from https://github.com/microsoft/DirectXMath
// The original code is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics.PackedVector;

/// <summary>
/// Packed vector type containing four 16-bit floating-point values.
/// </summary>
/// <remarks>Equivalent of XMHALF4.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly struct Half4 : IPackedVector<ulong>, IEquatable<Half4>, IFormattable
{
    [FieldOffset(0)]
    private readonly ulong _packedValue;

    /// <summary>
    /// Gets or sets the X component of the vector.
    /// </summary>
    [FieldOffset(0)]
    public readonly Half X;

    /// <summary>
    /// Gets or sets the Y component of the vector.
    /// </summary>
    [FieldOffset(2)]
    public readonly Half Y;

    /// <summary>
    /// Gets or sets the Z component of the vector.
    /// </summary>
    [FieldOffset(2)]
    public readonly Half Z;

    /// <summary>
    /// Gets or sets the W component of the vector.
    /// </summary>
    [FieldOffset(2)]
    public readonly Half W;

    /// <summary>
    /// Initializes a new instance of the <see cref="Half4"/> struct.
    /// </summary>
    /// <param name="packedValue">The packed value to assign.</param>
    public Half4(ulong packedValue)
    {
        Unsafe.SkipInit(out this);

        _packedValue = packedValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half4"/> structure.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    /// <param name="w">The W component.</param>
    public Half4(Half x, Half y, Half z, Half w)
    {
        Unsafe.SkipInit(out this);

        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half4"/> structure.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    /// <param name="w">The W component.</param>
    public Half4(float x, float y, float z, float w)
    {
        Unsafe.SkipInit(out this);

        X = (Half)x;
        Y = (Half)y;
        Z = (Half)z;
        W = (Half)w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half4" /> structure.
    /// </summary>
    /// <param name="value">The value to set for both the X, Y, Z and W components.</param>
    public Half4(Half value)
    {
        Unsafe.SkipInit(out this);

        X = value;
        Y = value;
        Z = value;
        W = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half4" /> structure.
    /// </summary>
    /// <param name="value">The value to set for both the X, Y, Z and W components.</param>
    public Half4(float value)
    {
        Unsafe.SkipInit(out this);

        X = (Half)value;
        Y = (Half)value;
        Z = (Half)value;
        W = (Half)value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half4"/> structure.
    /// </summary>
    /// <param name="vector">The <see cref="Vector4"/> to pack from.</param>
    public Half4(Vector4 vector)
    {
        Unsafe.SkipInit(out this);

        X = (Half)vector.X;
        Y = (Half)vector.Y;
        Z = (Half)vector.Z;
        W = (Half)vector.W;
    }

    /// <summary>
    /// Gets the packed value.
    /// </summary>
    public ulong PackedValue => _packedValue;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Vector2"/> to <see cref="Half4"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Half4(Vector4 value) => new(value.X, value.Y, value.Z, value.W);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Half4"/> to <see cref="Vector4"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Vector4(Half4 value) => value.ToVector4();

    /// <summary>
    /// Expands this <see cref="Half4"/> structure to a <see cref="Vector4"/>.
    /// </summary>
    public Vector4 ToVector4()
    {
        return new((float)X, (float)Y, (float)Z, (float)W);
    }

    #region IPackedVector Implementation
    ulong IPackedVector<ulong>.PackedValue => PackedValue;

    Vector4 IPackedVector.ToVector4()
    {
        return ToVector4();
    }
    #endregion IPackedVector Implementation

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Half4 value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="Half4"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Half4"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Half4 other) =>
        X == other.X
        && Y == other.Y
        && Z == other.Z
        && W == other.W;

    /// <summary>
    /// Compares two <see cref="Half4"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Half4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Half4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Half4 left, Half4 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Half4"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Half4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Half4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Half4 left, Half4 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        Vector4 vector = ToVector4();
        return vector.ToString(format, formatProvider);
    }
}
