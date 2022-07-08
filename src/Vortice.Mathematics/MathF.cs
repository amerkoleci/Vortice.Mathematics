// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

#if !NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
namespace System;

internal static class MathF
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Abs(float value) => Math.Abs(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Asin(float value) => (float)Math.Asin(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Acos(float value) => (float)Math.Acos(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Atan(float value) => (float)Math.Atan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Atan2(float y, float x) => (float)Math.Atan2(y, x);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Sin(float value) => (float)Math.Sin(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Cos(float value) => (float)Math.Cos(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Tan(float f) => (float)Math.Tan(f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Sqrt(float f) => (float)Math.Sqrt(f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Ceiling(float f) => (float)Math.Ceiling(f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Round(float f) => (float)Math.Round(f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Truncate(float f) => (float)Math.Truncate(f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Max(float val1, float val2) => Math.Max(val1, val2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Min(float val1, float val2) => Math.Min(val1, val2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Pow(float x, float y) => (float)Math.Pow(x, y);
}
#endif
