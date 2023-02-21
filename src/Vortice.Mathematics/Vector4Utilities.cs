// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics;

/// <summary>
/// Provides a set of methods to supplement or replace <see cref="Vector4" />.
/// </summary>
public static class Vector4Utilities
{
    public static Vector4 NegativeOne
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector4(-1.0f, -1.0f, -1.0f, -1.0f);
        }
    }

    public static Vector4 ByteMin
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector4(-127.0f, -127.0f, -127.0f, -127.0f);
        }
    }

    public static Vector4 ByteMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector4(127.0f, 127.0f, 127.0f, 127.0f);
        }
    }

    public static Vector4 UByteMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector4(255.0f, 255.0f, 255.0f, 255.0f);
        }
    }

    public static Vector4 ShortMin
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector4(-32767.0f, -32767.0f, -32767.0f, -32767.0f);
        }
    }

    public static Vector4 ShortMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector4(32767.0f, 32767.0f, 32767.0f, 32767.0f);
        }
    }

    public static Vector4 UShortMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector4(65535.0f, 65535.0f, 65535.0f, 65535.0f);
        }
    }

    public static Vector4 OneHalf
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector4(0.5f);
        }
    }

    /// <summary>
    /// Saturates each component of a vector to the range 0.0f to 1.0f.
    /// </summary>
    /// <param name="value">Vector to saturate.</param>
    /// <returns>Returns a vector, each of whose components are saturated.</returns>
    public static Vector4 Saturate(in Vector4 value)
    {
        return Vector4.Clamp(value, Vector4.Zero, Vector4.One);
    }

    /// <summary>
    /// Rounds each component of a vector to the nearest even integer.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Truncate(in Vector4 value)
    {
        return new Vector4(
            MathF.Truncate(value.X),
            MathF.Truncate(value.Y),
            MathF.Truncate(value.Z),
            MathF.Truncate(value.W)
            );
    }

    /// <summary>
    /// Rounds each component of a vector to the nearest even integer.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Vector4 Round(in Vector4 value)
    {
        return new(
            MathF.Round(value.X),
            MathF.Round(value.Y),
            MathF.Round(value.Z),
            MathF.Round(value.W)
            );
    }

    /// <summary>
    /// Computes the product of the first two vectors added to the third vector.
    /// </summary>
    /// <param name="multiplier">Vector multiplier.</param>
    /// <param name="multiplicand">Vector multiplicand.</param>
    /// <param name="addend">Vector addend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 MultiplyAdd(in Vector4 multiplier, in Vector4 multiplicand, in Vector4 addend)
    {
        return new Vector4(
            multiplier.X * multiplicand.X + addend.X,
            multiplier.Y * multiplicand.Y + addend.Y,
            multiplier.Z * multiplicand.Z + addend.Z,
            multiplier.W * multiplicand.W + addend.W
            );
    }
}
