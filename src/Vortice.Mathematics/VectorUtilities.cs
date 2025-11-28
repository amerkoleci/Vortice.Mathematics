// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// This file includes code based on code from https://github.com/microsoft/DirectXMath
// The original code is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using static Vortice.Mathematics.MathHelper;

namespace Vortice.Mathematics;

/// <summary>
/// Provides a set of methods to supplement or replace <see cref="Vector128" /> and <see cref="Vector128{T}" />.
/// </summary>
public static unsafe class VectorUtilities
{
    /// <summary>Gets a vector where the x-component is one and all other components are zero.</summary>
    public static Vector128<float> UnitX
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(1.0f, 0.0f, 0.0f, 0.0f);
        }
    }

    /// <summary>Gets a vector where the y-component is one and all other components are zero.</summary>
    public static Vector128<float> UnitY
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(0.0f, 1.0f, 0.0f, 0.0f);
        }
    }

    /// <summary>Gets a vector where the z-component is one and all other components are zero.</summary>
    public static Vector128<float> UnitZ
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(0.0f, 0.0f, 1.0f, 0.0f);
        }
    }

    /// <summary>Gets a vector where the w-component is one and all other components are zero.</summary>
    public static Vector128<float> UnitW
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(0.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    /// <summary>Gets a vector where all components are one.</summary>
    public static Vector128<float> One
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128<float>.One;
        }
    }

    public static Vector128<float> OneHalf
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }

    public static Vector128<float> ByteMin
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(-127.0f, -127.0f, -127.0f, -127.0f);
        }
    }

    public static Vector128<float> ByteMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(127.0f, 127.0f, 127.0f, 127.0f);
        }
    }

    public static Vector128<float> UByteMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(255.0f, 255.0f, 255.0f, 255.0f);
        }
    }

    public static Vector128<float> ScaleUByteN4
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(255.0f, 255.0f * 256.0f * 0.5f, 255.0f * 256.0f * 256.0f, 255.0f * 256.0f * 256.0f * 256.0f * 0.5f);
        }
    }

    public static Vector128<int> MaskUByteN4
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(0xFF, 0xFF << (8 - 1), 0xFF << 16, 0xFF << (24 - 1));
        }
    }

    public static Vector128<float> ShortMin
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(-32767.0f, -32767.0f, -32767.0f, -32767.0f);
        }
    }

    public static Vector128<float> ShortMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(32767.0f, 32767.0f, 32767.0f, 32767.0f);
        }
    }


    public static Vector128<float> UShortMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(65535.0f, 65535.0f, 65535.0f, 65535.0f);
        }
    }

    public static Vector128<int> AbsMask
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(0x7FFFFFFF, 0x7FFFFFFF, 0x7FFFFFFF, 0x7FFFFFFF);
        }
    }

    public static Vector128<uint> NegativeZero
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(0x80000000, 0x80000000, 0x80000000, 0x80000000);
        }
    }

    public static Vector128<float> NegativeOne
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(-1.0f, -1.0f, -1.0f, -1.0f);
        }
    }

    public static Vector128<float> NoFraction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Vector128.Create(8388608.0f, 8388608.0f, 8388608.0f, 8388608.0f);
        }
    }

    public static byte Shuffle(byte fp3, byte fp2, byte fp1, byte fp0)
    {
        return (byte)(((fp3) << 6) | ((fp2) << 4) | ((fp1) << 2) | (fp0));
    }

    /// <summary>Gets the x-component of the vector.</summary>
    /// <param name="self">The vector.</param>
    /// <returns>The x-component of <paramref name="self" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float GetX(this Vector128<float> self) => self.ToScalar();

    /// <summary>Gets the y-component of the vector.</summary>
    /// <param name="self">The vector.</param>
    /// <returns>The y-component of <paramref name="self" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float GetY(this Vector128<float> self) => self.GetElement(1);

    /// <summary>Gets the z-component of the vector.</summary>
    /// <param name="self">The vector.</param>
    /// <returns>The z-component of <paramref name="self" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float GetZ(this Vector128<float> self) => self.GetElement(2);

    /// <summary>Gets the w-component of the vector.</summary>
    /// <param name="self">The vector.</param>
    /// <returns>The w-component of <paramref name="self" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float GetW(this Vector128<float> self) => self.GetElement(3);

    /// <summary>Computes the product of two vectors and then adds a third.</summary>
    /// <param name="addend">The vector which is added to the product of <paramref name="left" /> and <paramref name="right" />.</param>
    /// <param name="left">The vector to multiply by <paramref name="right" />.</param>
    /// <param name="right">The vector which is used to multiply <paramref name="left" />.</param>
    /// <returns>The sum of <paramref name="addend" /> and the product of <paramref name="left" /> multipled by <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> MultiplyAdd(Vector128<float> addend, Vector128<float> left, Vector128<float> right)
    {
        if (Sse41.IsSupported)
        {
            Vector128<float> result = Sse.Multiply(left, right);
            return Sse.Add(addend, result);
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            Vector128<float> result = AdvSimd.Multiply(left, right);
            return AdvSimd.Add(addend, result);
        }
        else
        {
            return SoftwareFallback(addend, left, right);
        }

        static Vector128<float> SoftwareFallback(Vector128<float> addend, Vector128<float> left, Vector128<float> right)
        {
            return Vector128.Create(
                addend.GetX() + (left.GetX() * right.GetX()),
                addend.GetY() + (left.GetY() * right.GetY()),
                addend.GetZ() + (left.GetZ() * right.GetZ()),
                addend.GetW() + (left.GetW() * right.GetW())
            );
        }
    }

    /// <summary>Checks a vector to determine if all elements represent <c>true</c>.</summary>
    /// <param name="value">The vector to check.</param>
    /// <returns><c>true</c> if all elements in <paramref name="value" /> represent <c>true</c>; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CompareTrueAll(Vector128<float> value)
    {
        if (Sse41.IsSupported)
        {
            return Sse.MoveMask(value) == 0x0F;
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            return AdvSimd.Arm64.MinAcross(value).ToScalar() != 0;
        }
        else
        {
            return SoftwareFallback(value.AsUInt32());
        }

        static bool SoftwareFallback(Vector128<uint> value)
        {
            return (value.GetElement(0) != 0)
                && (value.GetElement(1) != 0)
                && (value.GetElement(2) != 0)
                && (value.GetElement(3) != 0);
        }
    }

    /// <summary>Compares two vectors to determine which elements are lesser.</summary>
    /// <param name="left">The vector to compare with <paramref name="right" />.</param>
    /// <param name="right">The vector to compare with <paramref name="left" />.</param>
    /// <returns>A vector that contains the element-wise comparison of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> CompareLessThan(Vector128<float> left, Vector128<float> right)
    {
        if (Sse41.IsSupported)
        {
            return Sse.CompareLessThan(left, right);
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            return AdvSimd.CompareLessThan(left, right);
        }
        else
        {
            return SoftwareFallback(left, right);
        }

        static Vector128<float> SoftwareFallback(Vector128<float> left, Vector128<float> right)
        {
            return Vector128.Create(
                (left.GetX() < right.GetX()) ? AllBitsSet : 0.0f,
                (left.GetY() < right.GetY()) ? AllBitsSet : 0.0f,
                (left.GetZ() < right.GetZ()) ? AllBitsSet : 0.0f,
                (left.GetW() < right.GetW()) ? AllBitsSet : 0.0f
            );
        }
    }

    /// <summary>Compares two vectors to determine which elements are lesser or equivalent.</summary>
    /// <param name="left">The vector to compare with <paramref name="right" />.</param>
    /// <param name="right">The vector to compare with <paramref name="left" />.</param>
    /// <returns>A vector that contains the element-wise comparison of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> CompareLessThanOrEqual(Vector128<float> left, Vector128<float> right)
    {
        if (Sse41.IsSupported)
        {
            return Sse.CompareLessThanOrEqual(left, right);
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            return AdvSimd.CompareLessThanOrEqual(left, right);
        }
        else
        {
            return SoftwareFallback(left, right);
        }

        static Vector128<float> SoftwareFallback(Vector128<float> left, Vector128<float> right)
        {
            return Vector128.Create(
                (left.GetX() <= right.GetX()) ? AllBitsSet : 0.0f,
                (left.GetY() <= right.GetY()) ? AllBitsSet : 0.0f,
                (left.GetZ() <= right.GetZ()) ? AllBitsSet : 0.0f,
                (left.GetW() <= right.GetW()) ? AllBitsSet : 0.0f
            );
        }
    }

    /// <summary>Compares two vectors to determine which elements are lesser or equivalent.</summary>
    /// <param name="left">The vector to compare with <paramref name="right" />.</param>
    /// <param name="right">The vector to compare with <paramref name="left" />.</param>
    /// <returns>A vector that contains the element-wise comparison of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool LessThanOrEqual(Vector128<float> left, Vector128<float> right)
    {
        return CompareTrueAll(CompareLessThanOrEqual(left, right));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Round(Vector128<float> vector)
    {
        if (Sse41.IsSupported)
        {
            return Sse41.RoundToNearestInteger(vector);
        }
        else if (Sse.IsSupported)
        {
            Vector128<float> sign = Sse.And(vector, NegativeZero.AsSingle());
            Vector128<float> sMagic = Sse.Or(NoFraction, sign);
            Vector128<float> R1 = Sse.Add(vector, sMagic);
            R1 = Sse.Subtract(R1, sMagic);
            Vector128<float> R2 = Sse.And(vector, AbsMask.AsSingle());
            Vector128<float> mask = Sse.CompareLessThanOrEqual(R2, NoFraction);
            R2 = Sse.AndNot(mask, vector);
            R1 = Sse.And(R1, mask);
            Vector128<float> result = Sse.Xor(R1, R2);
            return result;
        }
        else if (AdvSimd.IsSupported)
        {
            return AdvSimd.RoundToNearest(vector);
        }
        else
        {
            return SoftwareFallback(vector);
        }

        static Vector128<float> SoftwareFallback(Vector128<float> vector)
        {
            return Vector128.Create(
                MathF.Round(vector.GetX()),
                MathF.Round(vector.GetY()),
                MathF.Round(vector.GetZ()),
                MathF.Round(vector.GetW())
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Truncate(Vector128<float> vector)
    {
        if (Sse.IsSupported)
        {
            return Sse41.RoundToZero(vector);
        }
        else if (AdvSimd.IsSupported)
        {
            return AdvSimd.RoundToZero(vector);
        }
        else
        {
            return SoftwareFallback(vector);
        }

        static Vector128<float> SoftwareFallback(Vector128<float> vector)
            => Vector128.Create(
                MathF.Truncate(vector.GetX()),
                MathF.Truncate(vector.GetY()),
                MathF.Truncate(vector.GetZ()),
                MathF.Truncate(vector.GetW())
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Clamp(Vector128<float> vector, Vector128<float> min, Vector128<float> max)
    {
        Debug.Assert(LessThanOrEqual(min, max));

        Vector128<float> result = Vector128.Max(min, vector);
        result = Vector128.Min(max, result);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Saturate(Vector128<float> vector)
    {
        return Clamp(vector, Vector128<float>.Zero, One);
    }
}
