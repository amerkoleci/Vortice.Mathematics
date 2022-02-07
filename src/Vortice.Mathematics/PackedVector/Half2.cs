// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

// This file includes code based on code from https://github.com/microsoft/DirectXMath
// The original code is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics.PackedVector;

/// <summary>
/// Packed vector type containing two 16-bit floating-point values.
/// </summary>
/// <remarks>Equivalent of XMHALF2.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly struct Half2 : IPackedVector<uint>, IEquatable<Half2>, IFormattable
{
    [FieldOffset(0)]
    private readonly uint _packedValue;

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
    /// Initializes a new instance of the <see cref="Half2"/> struct.
    /// </summary>
    /// <param name="packedValue">The packed value to assign.</param>
    public Half2(uint packedValue)
    {
        Unsafe.SkipInit(out this);

        _packedValue = packedValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half2"/> structure.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    public Half2(Half x, Half y)
    {
        Unsafe.SkipInit(out this);

        X = x;
        Y = y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half2"/> structure.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    public Half2(float x, float y)
    {
        Unsafe.SkipInit(out this);

        X = (Half)x;
        Y = (Half)y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half2" /> structure.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    public Half2(ushort x, ushort y)
    {
        Unsafe.SkipInit(out this);

        X = (Half)x;
        Y = (Half)y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half2" /> structure.
    /// </summary>
    /// <param name="value">The value to set for both the X and Y components.</param>
    public Half2(Half value)
    {
        Unsafe.SkipInit(out this);

        X = value;
        Y = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half2" /> structure.
    /// </summary>
    /// <param name="value">Value to initialize X and Y components with.</param>
    public Half2(float value)
    {
        Unsafe.SkipInit(out this);

        X = (Half)value;
        Y = (Half)value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Half2"/> structure.
    /// </summary>
    /// <param name="vector">The <see cref="Vector4"/> to pack from.</param>
    public Half2(Vector4 vector)
    {
        Unsafe.SkipInit(out this);

        X = (Half)vector.X;
        Y = (Half)vector.Y;
    }

    /// <summary>
    /// Gets the packed value.
    /// </summary>
    public uint PackedValue => _packedValue;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Vector2"/> to <see cref="Half2"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Half2(Vector2 value) => new(value.X, value.Y);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Half2"/> to <see cref="Vector2"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Vector2(Half2 value) => value.ToVector2();

    /// <summary>
    /// Expands this <see cref="Half2"/> structure to a <see cref="Vector2"/>.
    /// </summary>
    public Vector2 ToVector2()
    {
        return new((float)X, (float)Y);
    }

    #region IPackedVector Implementation
    uint IPackedVector<uint>.PackedValue => PackedValue;

    Vector4 IPackedVector.ToVector4()
    {
        Vector2 vector = ToVector2();
        return new Vector4(vector.X, vector.Y, 0.0f, 1.0f);
    }
    #endregion IPackedVector Implementation

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Half2 value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="Half2"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Half2"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Half2 other) => X == other.X && Y == other.Y;

    /// <summary>
    /// Compares two <see cref="Half2"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Half2"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Half2"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Half2 left, Half2 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Half2"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Half2"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Half2"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Half2 left, Half2 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        Vector2 vector = ToVector2();
        return vector.ToString(format, formatProvider);
    }
}
