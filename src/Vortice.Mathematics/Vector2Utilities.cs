// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
#endif

using static Vortice.Mathematics.MathHelper;

namespace Vortice.Mathematics;

/// <summary>
/// Provides a set of methods to supplement or replace <see cref="Vector2" />.
/// </summary>
public static class Vector2Utilities
{
    public static Vector2 NegativeOne
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector2(-1.0f, -1.0f);
        }
    }

    public static Vector2 OneHalf
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector2(0.5f, 0.5f);
        }
    }

    public static Vector2 ShortMin
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector2(-32767.0f, -32767.0f);
        }
    }

    public static Vector2 ShortMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector2(32767.0f, 32767.0f);
        }
    }

    public static Vector2 UShortMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector2(65535.0f, 65535.0f);
        }
    }

    public static Vector2 UByteMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector2(255.0f, 255.0f);
        }
    }

    public static Vector2 ByteMin
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector2(-127.0f, -127.0f);
        }
    }

    public static Vector2 ByteMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new Vector2(127.0f, 127.0f);
        }
    }

    /// <summary>
    /// Saturates each component of a vector to the range 0.0f to 1.0f.
    /// </summary>
    /// <param name="value">Vector to saturate.</param>
    /// <returns>Returns a vector, each of whose components are saturated.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Saturate(in Vector2 value)
    {
        return Vector2.Clamp(value, Vector2.Zero, Vector2.One);
    }

    /// <summary>
    /// Rounds each component of a vector to the nearest even integer.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Truncate(in Vector2 value)
    {
        return new Vector2(
            MathF.Truncate(value.X),
            MathF.Truncate(value.Y)
            );
    }

    /// <summary>
    /// Rounds each component of a vector to the nearest even integer.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Round(in Vector2 value)
    {
        return new Vector2(
            MathF.Round(value.X),
            MathF.Round(value.Y)
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
    public static Vector2 MultiplyAdd(in Vector2 multiplier, in Vector2 multiplicand, in Vector2 addend)
    {
        return new Vector2(
            multiplier.X * multiplicand.X + addend.X,
            multiplier.Y * multiplicand.Y + addend.Y
            );
    }

    /// <summary>Compares two vectors to determine element-wise equality.</summary>
    /// <param name="left">The vector to compare with <paramref name="right" />.</param>
    /// <param name="right">The vector to compare with <paramref name="left" />.</param>
    /// <returns>A vector that contains the element-wise comparison of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 CompareEqual(in Vector2 left, in Vector2 right)
    {
#if NET6_0_OR_GREATER
        Vector128<float> result = VectorUtilities.CompareEqual(left.AsVector128(), right.AsVector128());
        return result.AsVector2();
#else
        return new Vector2(
                (left.X == right.X) ? AllBitsSet : 0.0f,
                (left.Y == right.Y) ? AllBitsSet : 0.0f
            );
#endif
    }

    /// <summary>Compares two vectors to determine equality.</summary>
    /// <param name="left">The vector to compare with <paramref name="right" />.</param>
    /// <param name="right">The vector to compare with <paramref name="left" />.</param>
    /// <returns><c>true</c> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CompareEqualAll(in Vector2 left, in Vector2 right)
    {
#if NET6_0_OR_GREATER
        return VectorUtilities.CompareEqualAll(left.AsVector128(), right.AsVector128());
#else
        return left == right;
#endif
    }

    /// <returns><c>true</c> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CompareEqualAll(in Vector2 left, in Vector2 right, in Vector2 epsilon)
    {
#if NET6_0_OR_GREATER
        return VectorUtilities.CompareEqualAll(left.AsVector128(), right.AsVector128(), epsilon.AsVector128());
#else
        return (MathF.Abs(left.X - right.X) <= epsilon.X)
                && (MathF.Abs(left.Y - right.Y) <= epsilon.Y);
#endif
    }

    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearEqual(in Vector2 vector1, in Vector2 vector2, float epsilon)
    {
        float dx = MathF.Abs(vector1.X - vector2.X);
        float dy = MathF.Abs(vector1.Y - vector2.Y);
        return (dx <= epsilon) && (dy <= epsilon);
    }
}
