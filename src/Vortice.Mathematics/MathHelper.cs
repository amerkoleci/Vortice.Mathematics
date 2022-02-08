// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.
// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.
// Based on: https://github.com/terrafx/terrafx/blob/main/sources/Core/Utilities/MathUtilities.cs

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
#if NET6_0_OR_GREATER
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

    /// <summary>Gets a value used to represent all bits set.</summary>
    public static float AllBitsSet
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
#if NET6_0_OR_GREATER
            return BitConverter.UInt32BitsToSingle(0xFFFFFFFF);
#else
            return UInt32BitsToSingle(0xFFFFFFFF);
#endif
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
#if NET6_0_OR_GREATER
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
#if NET6_0_OR_GREATER
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
#if NET6_0_OR_GREATER
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
#if NET6_0_OR_GREATER
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

    /// <summary>Compares two 32-bit floats to determine approximate equality.</summary>
    /// <param name="left">The float to compare with <paramref name="right" />.</param>
    /// <param name="right">The float to compare with <paramref name="left" />.</param>
    /// <returns><c>true</c> if <paramref name="left" /> and <paramref name="right" /> differ by no more than <see cref="NearZeroEpsilon"/>; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CompareEqual(float left, float right) => MathF.Abs(left - right) <= NearZeroEpsilon;

    /// <summary>Compares two 64-bit floats to determine approximate equality.</summary>
    /// <param name="left">The float to compare with <paramref name="right" />.</param>
    /// <param name="right">The float to compare with <paramref name="left" />.</param>
    /// <param name="epsilon">The maximum (inclusive) difference between <paramref name="left" /> and <paramref name="right" /> for which they should be considered equivalent.</param>
    /// <returns><c>true</c> if <paramref name="left" /> and <paramref name="right" /> differ by no more than <paramref name="epsilon" />; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CompareEqual(double left, double right, double epsilon) => Math.Abs(left - right) <= epsilon;

    /// <summary>Compares two 32-bit floats to determine approximate equality.</summary>
    /// <param name="left">The float to compare with <paramref name="right" />.</param>
    /// <param name="right">The float to compare with <paramref name="left" />.</param>
    /// <param name="epsilon">The maximum (inclusive) difference between <paramref name="left" /> and <paramref name="right" /> for which they should be considered equivalent.</param>
    /// <returns><c>true</c> if <paramref name="left" /> and <paramref name="right" /> differ by no more than <paramref name="epsilon" />; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CompareEqual(float left, float right, float epsilon) => MathF.Abs(left - right) <= epsilon;

    /// <summary>
    /// Determines whether the specified value is close to zero (0.0f).
    /// </summary>
    /// <param name="a">The floating value.</param>
    /// <returns><c>true</c> if the specified value is close to zero (0.0f); otherwise, <c>false</c>.</returns>
    public static bool IsZero(float a) => MathF.Abs(a) < NearZeroEpsilon;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ToDegrees(float radians) => radians * (180.0f / Pi);

    /// <summary>
    /// Converts degrees to radians.
    /// </summary>
    /// <param name="degree">Converts degrees to radians.</param>
    /// <returns>The converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ToRadians(float degree) => degree * (Pi / 180.0f);

    /// <summary>Clamps a 32-bit float to be between a minimum and maximum value.</summary>
    /// <param name="value">The value to restrict.</param>
    /// <param name="min">The minimum value (inclusive).</param>
    /// <param name="max">The maximum value (inclusive).</param>
    /// <returns><paramref name="value" /> clamped to be between <paramref name="min" /> and <paramref name="max" />.</returns>
    /// <remarks>This method does not account for <paramref name="min" /> being greater than <paramref name="max" />.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp(float value, float min, float max)
    {
        Debug.Assert(min <= max);

        // The compare order here is important.
        // It ensures we match HLSL behavior for the scenario where min is larger than max.

        float result = value;
        result = Max(result, min);
        result = Min(result, max);
        return result;
    }

    /// <summary>Clamps a 64-bit float to be between a minimum and maximum value.</summary>
    /// <param name="value">The value to restrict.</param>
    /// <param name="min">The minimum value (inclusive).</param>
    /// <param name="max">The maximum value (inclusive).</param>
    /// <returns><paramref name="value" /> clamped to be between <paramref name="min" /> and <paramref name="max" />.</returns>
    /// <remarks>This method does not account for <paramref name="min" /> being greater than <paramref name="max" />.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Clamp(double value, double min, double max)
    {
        Debug.Assert(min <= max);
        // The compare order here is important.
        // It ensures we match HLSL behavior for the scenario where min is larger than max.

        double result = value;

        result = Max(result, min);
        result = Min(result, max);

        return result;
    }

    /// <summary>Clamps a 32-bit signed integer to be between a minimum and maximum value.</summary>
    /// <param name="value">The value to restrict.</param>
    /// <param name="min">The minimum value (inclusive).</param>
    /// <param name="max">The maximum value (inclusive).</param>
    /// <returns><paramref name="value" /> clamped to be between <paramref name="min" /> and <paramref name="max" />.</returns>
    /// <remarks>This method does not account for <paramref name="min" /> being greater than <paramref name="max" />.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Clamp(int value, int min, int max)
    {
        Debug.Assert(min <= max);

        // The compare order here is important.
        // It ensures we match HLSL behavior for the scenario where min is larger than max.

        var result = value;

        result = Max(result, min);
        result = Min(result, max);

        return result;
    }

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
        return (value & (value - 1)) == 0 && value != 0;
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
        return (value & (value - 1)) == 0 && value != 0;
#endif
    }

    /// <summary>Determines whether a given value is a power of two.</summary>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if <paramref name="value" /> is a power of two; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPow2(nuint value)
    {
        return (value & (value - 1)) == 0 && value != 0;
    }

    // <summary>Rounds a given address up to the nearest alignment.</summary>
    /// <param name="address">The address to be aligned.</param>
    /// <param name="alignment">The target alignment, which should be a power of two.</param>
    /// <returns><paramref name="address" /> rounded up to the specified <paramref name="alignment" />.</returns>
    /// <remarks>This method does not account for an <paramref name="alignment" /> which is not a <c>power of two</c>.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint AlignUp(uint address, uint alignment)
    {
        Debug.Assert(IsPow2(alignment));

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
        Debug.Assert(IsPow2(alignment));

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

#if !NET6_0_OR_GREATER
    /// <summary>
    /// Converts the specified 32-bit signed integer to a single-precision floating point number.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    /// <returns>A single-precision floating point number whose bits are identical to <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe float Int32BitsToSingle(int value)
    {
        return *((float*)&value);
    }

    public static unsafe float UInt32BitsToSingle(uint value) => Int32BitsToSingle((int)value);
#endif
}
