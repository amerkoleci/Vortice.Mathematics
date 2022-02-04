// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

#if NETSTANDARD2_0
using System.Runtime.CompilerServices;
namespace System;

internal static class MathF
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Abs(float value) => Math.Abs(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Acos(float value) => (float)Math.Acos(value);

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
}
#endif
