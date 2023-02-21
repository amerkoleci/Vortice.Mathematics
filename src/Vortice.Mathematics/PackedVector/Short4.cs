// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

// This file includes code based on code from https://github.com/microsoft/DirectXMath
// The original code is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static Vortice.Mathematics.VectorUtilities;
#else
using static Vortice.Mathematics.Vector4Utilities;
#endif

namespace Vortice.Mathematics.PackedVector;

/// <summary>
/// Packed vector type containing four 16-bit signed integer values.
/// </summary>
/// <remarks>Equivalent of XMSHORT4.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly struct Short4 : IPackedVector<ulong>, IEquatable<Short4>
{
    [FieldOffset(0)]
    private readonly ulong _packedValue;

    /// <summary>
    /// The X component of the vector.
    /// </summary>
    [FieldOffset(0)]
    public readonly short X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    [FieldOffset(2)]
    public readonly short Y;

    /// <summary>
    /// The Z component of the vector.
    /// </summary>
    [FieldOffset(4)]
    public readonly short Z;

    /// <summary>
    /// The W component of the vector.
    /// </summary>
    [FieldOffset(6)]
    public readonly short W;

    /// <summary>
    /// Initializes a new instance of the <see cref="Short4"/> struct.
    /// </summary>
    /// <param name="packedValue">The packed value to assign.</param>
    public Short4(ulong packedValue)
    {
        Unsafe.SkipInit(out this);

        _packedValue = packedValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShortN4"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    /// <param name="z">The z value.</param>
    /// <param name="w">The w value.</param>
    public Short4(short x, short y, short z, short w)
    {
        Unsafe.SkipInit(out this);

        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Short4"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    /// <param name="z">The z value.</param>
    /// <param name="w">The w value.</param>
    public Short4(float x, float y, float z, float w)
    {
        Unsafe.SkipInit(out this);

#if NET6_0_OR_GREATER
        Vector128<float> vector = Vector128.Create(x, y, z, w);
        if (Sse41.IsSupported)
        {
            Vector128<float> result = Clamp(vector, ShortMin, ShortMax);
            Vector128<int> vInt = Sse2.ConvertToVector128Int32(result);
            Vector128<short> vShort = Sse2.PackSignedSaturate(vInt, vInt);

            X = vShort.GetElement(0);
            Y = vShort.GetElement(1);
            Z = vShort.GetElement(2);
            W = vShort.GetElement(3);
        }
        else
        {
            Vector128<float> result = Clamp(vector, ShortMin, ShortMax);
            result = Round(result);

            X = (short)vector.GetX();
            Y = (short)vector.GetY();
            Z = (short)vector.GetZ();
            W = (short)vector.GetW();
        }
#else
        Vector4 vector = Vector4.Clamp(new Vector4(x, y, z, w), ShortMin, ShortMax);
        vector = Round(vector);

        X = (short)vector.X;
        Y = (short)vector.Y;
        Z = (short)vector.Z;
        W = (short)vector.W;
#endif
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Short4"/> struct.
    /// </summary>
    /// <param name="vector">The <see cref="Vector4"/> containing X, Y, Z and W value.</param>
    public Short4(in Vector4 vector)
         : this(vector.X, vector.Y, vector.Z, vector.W)
    {
    }

    /// <summary>
    /// Gets the packed value.
    /// </summary>
    public ulong PackedValue => _packedValue;

    /// <summary>
    /// Expands the packed representation to a <see cref="Vector4"/>.
    /// </summary>
    public Vector4 ToVector4() => new(X, Y, Z, W);
    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Short4 other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(Short4 other) => PackedValue.Equals(other.PackedValue);

    /// <summary>
    /// Compares two <see cref="Short4"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Short4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Short4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Short4 left, Short4 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="ShortN4"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Short4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Short4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Short4 left, Short4 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => PackedValue.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => PackedValue.ToString("X16", CultureInfo.InvariantCulture);
}
