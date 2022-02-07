// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

// This file includes code based on code from https://github.com/microsoft/DirectXMath
// The original code is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics.PackedVector;

/// <summary>
/// Packed vector type containing four 16-bit signed integer values.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct Short4 : IPackedVector<ulong>, IEquatable<Short4>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Short4"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    /// <param name="z">The z value.</param>
    /// <param name="w">The w value.</param>
    public Short4(float x, float y, float z, float w)
    {
        PackedValue = Pack(x, y, z, w);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Short4"/> struct.
    /// </summary>
    /// <param name="vector">The <see cref="Vector4"/> containing X, Y, Z and W value.</param>
    public Short4(Vector4 vector)
    {
        PackedValue = Pack(vector.X, vector.Y, vector.Z, vector.W);
    }

    /// <summary>
    /// Gets the packed value.
    /// </summary>
    public ulong PackedValue { get; }

    /// <summary>
    /// Expands the packed representation to a <see cref="Vector4"/>.
    /// </summary>
    public Vector4 ToVector4() => new Vector4(
        (short)PackedValue,
        (short)(PackedValue >> 16),
        (short)(PackedValue >> 32),
        (short)(PackedValue >> 48)
        );

    private static ulong Pack(float x, float y, float z, float w)
    {
        ulong word1 = PackHelpers.PackSigned(ushort.MaxValue, x);
        ulong word2 = (ulong)PackHelpers.PackSigned(ushort.MaxValue, y) << 16;
        ulong word3 = (ulong)PackHelpers.PackSigned(ushort.MaxValue, z) << 32;
        ulong word4 = (ulong)PackHelpers.PackSigned(ushort.MaxValue, w) << 48;
        return word1 | word2 | word3 | word4;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Short4 other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(Short4 other) => PackedValue.Equals(other.PackedValue);

    /// <inheritdoc/>
    public override int GetHashCode() => PackedValue.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => PackedValue.ToString("X16", CultureInfo.InvariantCulture);
}
