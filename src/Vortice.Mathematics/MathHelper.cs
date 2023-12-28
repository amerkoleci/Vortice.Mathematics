// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.
// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.
// Based on: https://github.com/terrafx/terrafx/blob/main/sources/Core/Utilities/MathUtilities.cs

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace Vortice.Mathematics;

/// <summary>
/// Defines a math helper class.
/// </summary>
public static class MathHelper
{
    public static bool Is64BitProcess { get; } = Unsafe.SizeOf<nuint>() == 8;

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
    public const float NearZeroEpsilon = 4.7683716E-07f; // 2^-21: 0x35000000

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
        value = ((value > max) ? max : value);
        value = ((value < min) ? min : value);
        return value;
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
        value = ((value > max) ? max : value);
        value = ((value < min) ? min : value);
        return value;
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
        value = ((value > max) ? max : value);
        value = ((value < min) ? min : value);
        return value;
    }

    /// <summary>
    /// Performs smooth (cubic Hermite) interpolation between 0 and 1.
    /// </summary>
    /// <remarks>
    /// See https://en.wikipedia.org/wiki/Smoothstep
    /// </remarks>
    /// <param name="amount">Value between 0 and 1 indicating interpolation amount.</param>
    public static float SmoothStep(float amount)
    {
        return (amount <= 0) ? 0 : (amount >= 1) ? 1 : amount * amount * (3 - (2 * amount));
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
    public static float Distance(float value1, float value2) => MathF.Abs(value1 - value2);

    /// <summary>
    /// Determines whether a given value is a power of two.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if <paramref name="value" /> is a power of two; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPow2(uint value)
    {
        return BitOperations.IsPow2(value);
    }

    /// <summary>Determines whether a given value is a power of two.</summary>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if <paramref name="value" /> is a power of two; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPow2(ulong value)
    {
        return BitOperations.IsPow2(value);
    }

    /// <summary>Determines whether a given value is a power of two.</summary>
    /// <param name="value">The value to check.</param>
    /// <returns><c>true</c> if <paramref name="value" /> is a power of two; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPow2(nuint value)
    {
        return Is64BitProcess ? BitOperations.IsPow2(value) : BitOperations.IsPow2((uint)value);
    }

    // <summary>Rounds a given address down to the nearest alignment.</summary>
    /// <param name="address">The address to be aligned.</param>
    /// <param name="alignment">The target alignment, which should be a power of two.</param>
    /// <returns><paramref name="address" /> rounded down to the specified <paramref name="alignment" />.</returns>
    /// <remarks>This method does not account for an <paramref name="alignment" /> which is not a <c>power of two</c>.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint AlignDown(uint address, uint alignment)
    {
        Debug.Assert(IsPow2(alignment));
        return address & ~(alignment - 1);
    }

    /// <summary>Rounds a given address down to the nearest alignment.</summary>
    /// <param name="address">The address to be aligned.</param>
    /// <param name="alignment">The target alignment, which should be a power of two.</param>
    /// <returns><paramref name="address" /> rounded down to the specified <paramref name="alignment" />.</returns>
    /// <remarks>This method does not account for an <paramref name="alignment" /> which is not a <c>power of two</c>.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong AlignDown(ulong address, ulong alignment)
    {
        Debug.Assert(IsPow2(alignment));
        return address & ~(alignment - 1);
    }

    /// <summary>Rounds a given address down to the nearest alignment.</summary>
    /// <param name="address">The address to be aligned.</param>
    /// <param name="alignment">The target alignment, which should be a power of two.</param>
    /// <returns><paramref name="address" /> rounded down to the specified <paramref name="alignment" />.</returns>
    /// <remarks>This method does not account for an <paramref name="alignment" /> which is not a <c>power of two</c>.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nuint AlignDown(nuint address, nuint alignment)
    {
        Debug.Assert(IsPow2(alignment));
        return address & ~(alignment - 1);
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint DivideByMultiple(uint value, uint alignment)
    {
        return ((value + alignment - 1) / alignment);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong DivideByMultiple(ulong value, ulong alignment)
    {
        return ((value + alignment - 1) / alignment);
    }

    /// <summary>
    /// Converts a float value from sRGB to linear.
    /// </summary>
    /// <param name="sRgbValue">The sRGB value.</param>
    /// <returns>A linear value.</returns>
    public static float SRgbToLinear(float sRgbValue)
    {
        if (sRgbValue <= 0.04045f)
            return sRgbValue / 12.92f;
        return MathF.Pow((sRgbValue + 0.055f) / 1.055f, 2.4f);
    }

    /// <summary>
    /// Converts a float value from linear to sRGB.
    /// </summary>
    /// <param name="linearValue">The linear value.</param>
    /// <returns>The encoded sRGB value.</returns>
    public static float LinearToSRgb(float linearValue)
    {
        if (linearValue <= 0.0031308f)
            return 12.92f * linearValue;

        return 1.055f * MathF.Pow(linearValue, 0.4166667f) - 0.055f;
    }
}
