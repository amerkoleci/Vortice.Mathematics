// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics.PackedVector;

/// <summary>
/// Packed vector type containing two 16-bit signed integer values.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct Short2 : IPackedVector<uint>, IEquatable<Short2>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Short2"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    public Short2(float x, float y)
    {
        PackedValue = Pack(x, y);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Short2"/> struct.
    /// </summary>
    /// <param name="vector">The <see cref="Vector2"/> containing X and Y value.</param>
    public Short2(Vector2 vector)
    {
        PackedValue = Pack(vector.X, vector.Y);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Short2"/> struct.
    /// </summary>
    /// <param name="vector">The <see cref="Vector2"/> containing X and Y value.</param>
    public Short2(Vector4 vector)
    {
        PackedValue = Pack(vector.X, vector.Y);
    }

    /// <summary>
    /// Gets the packed value.
    /// </summary>
    public uint PackedValue { get; }

    /// <summary>
    /// Expands the packed representation to a <see cref="Vector2"/>.
    /// </summary>
    public Vector2 ToVector2() => new Vector2((short)PackedValue, (short)(PackedValue >> 16));

    Vector4 IPackedVector.ToVector4()
    {
        Vector2 vector = ToVector2();
        return new Vector4(vector.X, vector.Y, 0.0f, 1.0f);
    }

    private static uint Pack(float x, float y)
    {
        uint word1 = PackHelpers.PackSigned(ushort.MaxValue, x);
        uint word2 = PackHelpers.PackSigned(ushort.MaxValue, y) << 16;
        return word1 | word2;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Short2 other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(Short2 other) => PackedValue.Equals(other.PackedValue);

    /// <inheritdoc/>
    public override int GetHashCode() => PackedValue.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => PackedValue.ToString("X8", CultureInfo.InvariantCulture);
}
