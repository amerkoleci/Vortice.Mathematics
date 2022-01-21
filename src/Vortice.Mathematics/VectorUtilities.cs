// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// This file includes code based on code from https://github.com/microsoft/DirectXMath
// The original code is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

#if NET5_0_OR_GREATER
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace Vortice.Mathematics;

/// <summary>Provides a set of methods to supplement or replace <see cref="Vector128" /> and <see cref="Vector128{T}" />.</summary>
public static class VectorUtilities
{
    /// <summary>Gets a value used to represent all bits set.</summary>
    public static float AllBitsSet
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return BitConverter.UInt32BitsToSingle(0xFFFFFFFF);
        }
    }

    /// <summary>Gets a value used to determine if a value is near zero.</summary>
    public static float NearZeroEpsilon
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return 4.7683716E-07f; // 2^-21: 0x35000000
        }
    }

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
            return Vector128.Create(1.0f, 1.0f, 1.0f, 1.0f);
        }
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

    /// <summary>Compares two vectors to determine equality.</summary>
    /// <param name="left">The vector to compare with <paramref name="right" />.</param>
    /// <param name="right">The vector to compare with <paramref name="left" />.</param>
    /// <returns><c>true</c> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CompareEqualAll(Vector128<float> left, Vector128<float> right)
    {
        if (Sse41.IsSupported)
        {
            Vector128<float> result = Sse.CompareNotEqual(left, right);
            return Sse.MoveMask(result) == 0x00;
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            Vector128<float> result = AdvSimd.CompareEqual(left, right);
            return AdvSimd.Arm64.MinAcross(result).ToScalar() != 0;
        }
        else
        {
            return SoftwareFallback(left, right);
        }

        static bool SoftwareFallback(Vector128<float> left, Vector128<float> right)
        {
            return (left.GetX() == right.GetX())
                && (left.GetY() == right.GetY())
                && (left.GetZ() == right.GetZ())
                && (left.GetW() == right.GetW());
        }
    }

    /// <summary>Compares two vectors to determine equality.</summary>
    /// <param name="left">The vector to compare with <paramref name="right" />.</param>
    /// <param name="right">The vector to compare with <paramref name="left" />.</param>
    /// <returns><c>true</c> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CompareNotEqualAny(Vector128<float> left, Vector128<float> right)
    {
        if (Sse41.IsSupported)
        {
            Vector128<float> result = Sse.CompareNotEqual(left, right);
            return Sse.MoveMask(result) != 0x00;
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            Vector128<float> result = AdvSimd.CompareEqual(left, right);
            return AdvSimd.Arm64.MaxAcross(result).ToScalar() == 0;
        }
        else
        {
            return SoftwareFallback(left, right);
        }

        static bool SoftwareFallback(Vector128<float> left, Vector128<float> right)
        {
            return (left.GetX() != right.GetX())
                || (left.GetY() != right.GetY())
                || (left.GetZ() != right.GetZ())
                || (left.GetW() != right.GetW());
        }
    }

    /// <summary>Computes the sum of two vectors.</summary>
    /// <param name="left">The vector to which to add <paramref name="right" />.</param>
    /// <param name="right">The vector which is added to <paramref name="left" />.</param>
    /// <returns>The sum of <paramref name="right" /> added to <paramref name="left" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Add(Vector128<float> left, Vector128<float> right)
    {
        if (Sse41.IsSupported)
        {
            return Sse.Add(left, right);
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            return AdvSimd.Add(left, right);
        }
        else
        {
            return SoftwareFallback(left, right);
        }

        static Vector128<float> SoftwareFallback(Vector128<float> left, Vector128<float> right)
        {
            return Vector128.Create(
                left.GetX() + right.GetX(),
                left.GetY() + right.GetY(),
                left.GetZ() + right.GetZ(),
                left.GetW() + right.GetW()
            );
        }
    }

    /// <summary>Computes the difference of two vectors.</summary>
    /// <param name="left">The vector from which to subtract <paramref name="right" />.</param>
    /// <param name="right">The vector which is subtracted from <paramref name="left" />.</param>
    /// <returns>The difference of <paramref name="right" /> subtracted from <paramref name="left" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Subtract(Vector128<float> left, Vector128<float> right)
    {
        if (Sse41.IsSupported)
        {
            return Sse.Subtract(left, right);
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            return AdvSimd.Subtract(left, right);
        }
        else
        {
            return SoftwareFallback(left, right);
        }

        static Vector128<float> SoftwareFallback(Vector128<float> left, Vector128<float> right)
        {
            return Vector128.Create(
                left.GetX() - right.GetX(),
                left.GetY() - right.GetY(),
                left.GetZ() - right.GetZ(),
                left.GetW() - right.GetW()
            );
        }
    }

    /// <summary>Computes the product of a vector and a float.</summary>
    /// <param name="left">The vector to multiply by <paramref name="right" />.</param>
    /// <param name="right">The float which is used to multiply <paramref name="left" />.</param>
    /// <returns>The product of <paramref name="left" /> multipled by <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Multiply(Vector128<float> left, float right)
    {
        if (Sse41.IsSupported)
        {
            var scalar = Vector128.Create(right);
            return Sse.Multiply(left, scalar);
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            var scalar = Vector64.CreateScalar(right);
            return AdvSimd.MultiplyBySelectedScalar(left, scalar, 0);
        }
        else
        {
            return SoftwareFallback(left, right);
        }

        static Vector128<float> SoftwareFallback(Vector128<float> left, float right)
        {
            return Vector128.Create(
                left.GetX() * right,
                left.GetY() * right,
                left.GetZ() * right,
                left.GetW() * right
            );
        }
    }

    /// <summary>Computes the product of two vectors.</summary>
    /// <param name="left">The vector to multiply by <paramref name="right" />.</param>
    /// <param name="right">The vector which is used to multiply <paramref name="left" />.</param>
    /// <returns>The product of <paramref name="left" /> multipled by <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Multiply(Vector128<float> left, Vector128<float> right)
    {
        if (Sse41.IsSupported)
        {
            return Sse.Multiply(left, right);
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            return AdvSimd.Multiply(left, right);
        }
        else
        {
            return SoftwareFallback(left, right);
        }

        static Vector128<float> SoftwareFallback(Vector128<float> left, Vector128<float> right)
        {
            return Vector128.Create(
                left.GetX() * right.GetX(),
                left.GetY() * right.GetY(),
                left.GetZ() * right.GetZ(),
                left.GetW() * right.GetW()
            );
        }
    }

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
            var result = Sse.Multiply(left, right);
            return Sse.Add(addend, result);
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            var result = AdvSimd.Multiply(left, right);
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

    /// <summary>Compares two vectors to determine the element-wise maximum.</summary>
    /// <param name="left">The vector to compare with <paramref name="right" />.</param>
    /// <param name="right">The vector to compare with <paramref name="left" />.</param>
    /// <returns>The element-wise maximum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Max(Vector128<float> left, Vector128<float> right)
    {
        if (Sse41.IsSupported)
        {
            return Sse.Max(left, right);
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            return AdvSimd.Max(left, right);
        }
        else
        {
            return SoftwareFallback(left, right);
        }

        static Vector128<float> SoftwareFallback(Vector128<float> left, Vector128<float> right)
        {
            return Vector128.Create(
                MathHelper.Max(left.GetX(), right.GetX()),
                MathHelper.Max(left.GetY(), right.GetY()),
                MathHelper.Max(left.GetZ(), right.GetZ()),
                MathHelper.Max(left.GetW(), right.GetW())
            );
        }
    }

    /// <summary>Compares two vectors to determine the element-wise minimum.</summary>
    /// <param name="left">The vector to compare with <paramref name="right" />.</param>
    /// <param name="right">The vector to compare with <paramref name="left" />.</param>
    /// <returns>The element-wise minimum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Min(Vector128<float> left, Vector128<float> right)
    {
        if (Sse41.IsSupported)
        {
            return Sse.Min(left, right);
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            return AdvSimd.Min(left, right);
        }
        else
        {
            return SoftwareFallback(left, right);
        }

        static Vector128<float> SoftwareFallback(Vector128<float> left, Vector128<float> right)
        {
            return Vector128.Create(
                MathHelper.Min(left.GetX(), right.GetX()),
                MathHelper.Min(left.GetY(), right.GetY()),
                MathHelper.Min(left.GetZ(), right.GetZ()),
                MathHelper.Min(left.GetW(), right.GetW())
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
    public static Vector128<float> Clamp(Vector128<float> vector, Vector128<float> min, Vector128<float> max)
    {
        Debug.Assert(LessThanOrEqual(min, max));

        if (Sse41.IsSupported)
        {
            Vector128<float> result = Sse.Max(min, vector);
            result = Sse.Min(max, result);
            return result;
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            Vector128<float> result = AdvSimd.Max(min, vector);
            result = AdvSimd.Min(max, result);
            return result;
        }
        else
        {
            Vector128<float> result = Max(min, vector);
            result = Min(max, result);
            return result;

        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Saturate(Vector128<float> vector)
    {
        if (Sse41.IsSupported)
        {
            Vector128<float> result = Sse.Max(vector, Vector128<float>.Zero);
            result = Sse.Min(result, One);
            return result;
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            Vector128<float> result = AdvSimd.Max(vector, Vector128<float>.Zero);
            result = Sse.Min(result, One);
            return result;
        }
        else
        {
            return Clamp(vector, Vector128<float>.Zero, One);
        }
    }
}
#endif
