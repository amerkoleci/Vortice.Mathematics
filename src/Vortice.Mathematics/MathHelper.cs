// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.
// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.
// Based on: https://github.com/terrafx/terrafx/blob/main/sources/Core/Utilities/MathUtilities.cs

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
#if NET5_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
#endif

namespace Vortice.Mathematics;

/// <summary>
/// Defines a math helper class.
/// </summary>
public static class MathHelper
{
    /// <summary>
    /// The value for which all absolute numbers smaller than are considered equal to zero.
    /// </summary>
    public const float ZeroTolerance = 1e-6f; // Value a 8x higher than 1.19209290E-07F

    /// <summary>
    /// Represents the mathematical constant e.
    /// </summary>
    public const float E = 2.71828175f;

    /// <summary>
    /// Represents the log base two of e.
    /// </summary>
    public const float Log2E = 1.442695f;

    /// <summary>
    /// Represents the log base ten of e.
    /// </summary>
    public const float Log10E = 0.4342945f;

    /// <summary>
    /// Represents the value of pi.
    /// </summary>
    public const float Pi = (float)Math.PI;

    /// <summary>
    /// Represents the value of pi times two.
    /// </summary>
    public const float TwoPi = (float)(2 * Math.PI);

    /// <summary>
    /// Represents the value of pi divided by two.
    /// </summary>
    public const float PiOver2 = (float)(Math.PI / 2);

    /// <summary>
    /// Represents the value of pi divided by four.
    /// </summary>
    public const float PiOver4 = (float)(Math.PI / 4);

    /// <summary>
    /// Computes the absolute value of a given 32-bit float.
    /// </summary>
    /// <param name="value">The float for which to compute its absolute.</param>
    /// <returns>The absolute value of <paramref name="value" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Abs(float value) => MathF.Abs(value);

    /// <summary>
    /// Computes the arc-cosine for a given 64-bit float.
    /// </summary>
    /// <param name="value">The float, in radians, for which to compute the arc-cosine.</param>
    /// <returns>The arc-cosine of <paramref name="value" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Acos(double value) => Math.Acos(value);

    /// <summary>
    /// Computes the arc-cosine for a given 32-bit float.
    /// </summary>
    /// <param name="value">The float, in radians, for which to compute the arc-cosine.</param>
    /// <returns>The arc-cosine of <paramref name="value" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Acos(float value) => MathF.Acos(value);

    /// <summary>Computes the maximum of two 32-bit signed integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The maximum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Max(byte left, byte right) => (left > right) ? left : right;

    /// <summary>Computes the maximum of two 64-bit floats.</summary>
    /// <param name="left">The float to compare with <paramref name="right" />.</param>
    /// <param name="right">The float to compare with <paramref name="left" />.</param>
    /// <returns>The maximum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Max(double left, double right)
    {
#if NET5_0_OR_GREATER
        if (Sse41.IsSupported)
        {
            // TODO: This isn't correctly taking +0.0 vs -0.0 into account

            var vLeft = Vector128.CreateScalarUnsafe(left);
            var vRight = Vector128.CreateScalarUnsafe(right);

            var tmp = Sse2.Max(vLeft, vRight);
            var msk = Sse2.CompareUnordered(vLeft, vLeft);

            return Sse41.BlendVariable(tmp, vLeft, msk).ToScalar();
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            return AdvSimd.Arm64.MaxScalar(
                Vector64.CreateScalar(left),
                Vector64.CreateScalar(right)
            ).ToScalar();
        }
        else
#endif
        {
            return Math.Max(left, right);
        }
    }

    /// <summary>Computes the maximum of two 16-bit signed integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The maximum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Max(short left, short right) => (left > right) ? left : right;

    /// <summary>Computes the maximum of two 32-bit signed integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The maximum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Max(int left, int right) => (left > right) ? left : right;

    /// <summary>Computes the maximum of two 64-bit signed integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The maximum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Max(long left, long right) => (left > right) ? left : right;

    /// <summary>Computes the maximum of two signed native integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The maximum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nint Max(nint left, nint right) => (left > right) ? left : right;

    /// <summary>Computes the maximum of two 8-bit signed integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The maximum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte Max(sbyte left, sbyte right) => (left > right) ? left : right;

    /// <summary>Computes the maximum of two 32-bit floats.</summary>
    /// <param name="left">The float to compare with <paramref name="right" />.</param>
    /// <param name="right">The float to compare with <paramref name="left" />.</param>
    /// <returns>The maximum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Max(float left, float right)
    {
#if NET5_0_OR_GREATER
        if (Sse41.IsSupported)
        {
            // TODO: This isn't correctly taking +0.0 vs -0.0 into account

            var vLeft = Vector128.CreateScalarUnsafe(left);
            var vRight = Vector128.CreateScalarUnsafe(right);

            var tmp = Sse.Max(vLeft, vRight);
            var msk = Sse.CompareUnordered(vLeft, vLeft);

            return Sse41.BlendVariable(tmp, vLeft, msk).ToScalar();
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            return AdvSimd.Arm64.MaxScalar(
                Vector64.CreateScalar(left),
                Vector64.CreateScalar(right)
            ).ToScalar();
        }
        else
#endif
        {
            return MathF.Max(left, right);
        }
    }

    /// <summary>Computes the maximum of two 16-bit unsigned integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The maximum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Max(ushort left, ushort right) => (left > right) ? left : right;

    /// <summary>Computes the maximum of two 32-bit unsigned integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The maximum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Max(uint left, uint right) => (left > right) ? left : right;

    /// <summary>Computes the maximum of two 64-bit unsigned integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The maximum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Max(ulong left, ulong right) => (left > right) ? left : right;

    /// <summary>Computes the maximum of two unsigned native integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The maximum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nuint Max(nuint left, nuint right) => (left > right) ? left : right;

    /// <summary>Computes the minimum of two 8-bit unsigned integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The minimum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Min(byte left, byte right) => (left < right) ? left : right;

    /// <summary>Computes the minimum of two 64-bit floats.</summary>
    /// <param name="left">The float to compare with <paramref name="right" />.</param>
    /// <param name="right">The float to compare with <paramref name="left" />.</param>
    /// <returns>The minimum of <paramref name="left" /> and <paramref name="right" />.</returns>
    /// <remarks>This method does not account for <c>negative zero</c> and returns the other parameter if one is <see cref="double.NaN" />.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Min(double left, double right)
    {
#if NET5_0_OR_GREATER
        if (Sse41.IsSupported)
        {
            // TODO: This isn't correctly taking +0.0 vs -0.0 into account

            var vLeft = Vector128.CreateScalarUnsafe(left);
            var vRight = Vector128.CreateScalarUnsafe(right);

            var tmp = Sse2.Min(vLeft, vRight);
            var msk = Sse2.CompareUnordered(vLeft, vLeft);

            return Sse41.BlendVariable(tmp, vLeft, msk).ToScalar();
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            return AdvSimd.Arm64.MinScalar(
                Vector64.CreateScalar(left),
                Vector64.CreateScalar(right)
            ).ToScalar();
        }
        else
#endif
        {
            return Math.Min(left, right);
        }
    }

    /// <summary>Computes the minimum of two 16-bit signed integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The minimum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Min(short left, short right) => (left < right) ? left : right;

    /// <summary>Computes the minimum of two 32-bit signed integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The minimum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Min(int left, int right) => (left < right) ? left : right;

    /// <summary>Computes the minimum of two 64-bit signed integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The minimum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Min(long left, long right) => (left < right) ? left : right;

    /// <summary>Computes the minimum of two signed native integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The minimum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nint Min(nint left, nint right) => (left < right) ? left : right;

    /// <summary>Computes the minimum of two 8-bit signed integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The minimum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte Min(sbyte left, sbyte right) => (left < right) ? left : right;

    /// <summary>Computes the minimum of two 32-bit floats.</summary>
    /// <param name="left">The float to compare with <paramref name="right" />.</param>
    /// <param name="right">The float to compare with <paramref name="left" />.</param>
    /// <returns>The minimum of <paramref name="left" /> and <paramref name="right" />.</returns>
    /// <remarks>This method does not account for <c>negative zero</c> and returns the other parameter if one is <see cref="double.NaN" />.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Min(float left, float right)
    {
#if NET5_0_OR_GREATER
        if (Sse41.IsSupported)
        {
            // TODO: This isn't correctly taking +0.0 vs -0.0 into account

            var vLeft = Vector128.CreateScalarUnsafe(left);
            var vRight = Vector128.CreateScalarUnsafe(right);

            var tmp = Sse.Min(vLeft, vRight);
            var msk = Sse.CompareUnordered(vLeft, vLeft);

            return Sse41.BlendVariable(tmp, vLeft, msk).ToScalar();
        }
        else if (AdvSimd.Arm64.IsSupported)
        {
            return AdvSimd.Arm64.MinScalar(
                Vector64.CreateScalar(left),
                Vector64.CreateScalar(right)
            ).ToScalar();
        }
        else
#endif
        {
            return MathF.Min(left, right);
        }
    }

    /// <summary>Computes the minimum of two 16-bit unsigned integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The minimum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Min(ushort left, ushort right) => (left < right) ? left : right;

    /// <summary>Computes the minimum of two 32-bit unsigned integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The minimum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Min(uint left, uint right) => (left < right) ? left : right;

    /// <summary>Computes the minimum of two 64-bit unsigned integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The minimum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Min(ulong left, ulong right) => (left < right) ? left : right;

    /// <summary>Computes the minimum of two unsigned native integers.</summary>
    /// <param name="left">The integer to compare with <paramref name="right" />.</param>
    /// <param name="right">The integer to compare with <paramref name="left" />.</param>
    /// <returns>The minimum of <paramref name="left" /> and <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nuint Min(nuint left, nuint right) => (left < right) ? left : right;

    /// <summary>
    /// Checks if a - b are almost equals within a float epsilon.
    /// </summary>
    /// <param name="a">The left value to compare.</param>
    /// <param name="b">The right value to compare.</param>
    /// <param name="epsilon">Epsilon value</param>
    /// <returns><c>true</c> if a almost equal to b within a float epsilon, <c>false</c> otherwise</returns>
    public static bool WithinEpsilon(float a, float b, float epsilon)
    {
        float diff = a - b;
        return (-epsilon <= diff) && (diff <= epsilon);
    }

    /// <summary>
    /// Checks if a and b are almost equals, taking into account the magnitude of floating point numbers (unlike <see cref="WithinEpsilon"/> method). See Remarks.
    /// See remarks.
    /// </summary>
    /// <param name="a">The left value to compare.</param>
    /// <param name="b">The right value to compare.</param>
    /// <returns><c>true</c> if a almost equal to b, <c>false</c> otherwise</returns>
    /// <remarks>
    /// The code is using the technique described by Bruce Dawson in 
    /// <a href="http://randomascii.wordpress.com/2012/02/25/comparing-floating-point-numbers-2012-edition/">Comparing Floating point numbers 2012 edition</a>. 
    /// </remarks>
    public static unsafe bool NearEqual(float a, float b)
    {
        // Check if the numbers are really close -- needed
        // when comparing numbers near zero.
        if (IsZero(a - b))
            return true;

        // Original from Bruce Dawson: http://randomascii.wordpress.com/2012/02/25/comparing-floating-point-numbers-2012-edition/
        int aInt = *(int*)&a;
        int bInt = *(int*)&b;

        // Different signs means they do not match.
        if ((aInt < 0) != (bInt < 0))
            return false;

        // Find the difference in ULPs.
        int ulp = Math.Abs(aInt - bInt);

        // Choose of maxUlp = 4
        // according to http://code.google.com/p/googletest/source/browse/trunk/include/gtest/internal/gtest-internal.h
        const int maxUlp = 4;
        return ulp <= maxUlp;
    }

    /// <summary>
    /// Determines whether the specified value is close to zero (0.0f).
    /// </summary>
    /// <param name="a">The floating value.</param>
    /// <returns><c>true</c> if the specified value is close to zero (0.0f); otherwise, <c>false</c>.</returns>
    public static bool IsZero(float a) => Math.Abs(a) < ZeroTolerance;

    /// <summary>
    /// Determines whether the specified value is close to one (1.0f).
    /// </summary>
    /// <param name="a">The floating value.</param>
    /// <returns><c>true</c> if the specified value is close to one (1.0f); otherwise, <c>false</c>.</returns>
    public static bool IsOne(float a) => IsZero(a - 1.0f);

    /// <summary>
    /// Converts radians to degrees.
    /// </summary>
    /// <param name="radians">The angle in radians.</param>
    /// <returns>The converted value.</returns>
    public static float ToDegrees(float radians) => radians * (180.0f / Pi);

    /// <summary>
    /// Converts degrees to radians.
    /// </summary>
    /// <param name="degree">Converts degrees to radians.</param>
    /// <returns>The converted value.</returns>
    public static float ToRadians(float degree) => degree * (Pi / 180.0f);

    /// <summary>
    /// Clamps the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="min">The min.</param>
    /// <param name="max">The max.</param>
    /// <returns>The result of clamping a value between min and max</returns>
    public static float Clamp(float value, float min, float max) => value < min ? min : value > max ? max : value;

    /// <summary>
    /// Clamps the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="min">The min.</param>
    /// <param name="max">The max.</param>
    /// <returns>The result of clamping a value between min and max</returns>
    public static int Clamp(int value, int min, int max) => value < min ? min : value > max ? max : value;

    /// <summary>
    /// Linearly interpolates between two values.
    /// </summary>
    /// <param name="value1">Source value1.</param>
    /// <param name="value2">Source value2.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
    /// <returns>The result of linear interpolation of values based on the amount.</returns>
    public static float Lerp(float value1, float value2, float amount) => value1 + ((value2 - value1) * amount);

    /// <summary>
    /// Calculates the absolute value of the difference of two values.
    /// </summary>
    /// <param name="value1">Source value1.</param>
    /// <param name="value2">Source value2.</param>
    /// <returns>The distance value.</returns>
    public static float Distance(float value1, float value2) => Math.Abs(value1 - value2);

    /// <summary>
    /// Determines whether a given value is a power of two.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if <paramref name="value" /> is a power of two; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPow2(uint value)
    {
#if NET6_0_OR_GREATER
        return BitOperations.IsPow2(value);
#else
        return unchecked((value & (value - 1)) == 0) && (value != 0);
#endif
    }

    /// <summary>Determines whether a given value is a power of two.</summary>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if <paramref name="value" /> is a power of two; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPow2(ulong value)
    {
#if NET6_0_OR_GREATER
            return BitOperations.IsPow2(value);
#else
        return unchecked((value & (value - 1)) == 0) && (value != 0);
#endif
    }

    /// <summary>Determines whether a given value is a power of two.</summary>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if <paramref name="value" /> is a power of two; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPow2(nuint value)
    {
#if NET6_0_OR_GREATER
            if (Unsafe.SizeOf<nuint>() == 8)
            {
                return BitOperations.IsPow2(value);
            }
            else
            {
                return BitOperations.IsPow2((uint)value);
            }
#else
        if (Unsafe.SizeOf<nuint>() == 8)
        {
            return IsPow2((ulong)value);
        }
        else
        {
            return IsPow2((uint)value);
        }
#endif
    }

    // <summary>Rounds a given address up to the nearest alignment.</summary>
    /// <param name="address">The address to be aligned.</param>
    /// <param name="alignment">The target alignment, which should be a power of two.</param>
    /// <returns><paramref name="address" /> rounded up to the specified <paramref name="alignment" />.</returns>
    /// <remarks>This method does not account for an <paramref name="alignment" /> which is not a <c>power of two</c>.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint AlignUp(uint address, uint alignment)
    {
        Debug.Assert( IsPow2(alignment));
        return (address + (alignment - 1)) & ~(alignment - 1);
    }

    /// <summary>Rounds a given address up to the nearest alignment.</summary>
    /// <param name="address">The address to be aligned.</param>
    /// <param name="alignment">The target alignment, which should be a power of two.</param>
    /// <returns><paramref name="address" /> rounded up to the specified <paramref name="alignment" />.</returns>
    /// <remarks>This method does not account for an <paramref name="alignment" /> which is not a <c>power of two</c>.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong AlignUp(ulong address, ulong alignment)
    {
        Debug.Assert( IsPow2(alignment));
        return (address + (alignment - 1)) & ~(alignment - 1);
    }

    /// <summary>Rounds a given address up to the nearest alignment.</summary>
    /// <param name="address">The address to be aligned.</param>
    /// <param name="alignment">The target alignment, which should be a power of two.</param>
    /// <returns><paramref name="address" /> rounded up to the specified <paramref name="alignment" />.</returns>
    /// <remarks>This method does not account for an <paramref name="alignment" /> which is not a <c>power of two</c>.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nuint AlignUp(nuint address, nuint alignment)
    {
        Debug.Assert(IsPow2(alignment));
        return (address + (alignment - 1)) & ~(alignment - 1);
    }
}
