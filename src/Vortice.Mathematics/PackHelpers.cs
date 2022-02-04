// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using static Vortice.Mathematics.VectorUtilities;
#endif

namespace Vortice.Mathematics;

public static unsafe class PackHelpers
{
    private static float ClampAndRound(float value, float min, float max)
    {
        if (float.IsNaN(value))
        {
            return 0.0f;
        }

        if (float.IsInfinity(value))
        {
            return float.IsNegativeInfinity(value) ? min : max;
        }

        if (value < min)
        {
            return min;
        }

        if (value > max)
        {
            return max;
        }

        return MathF.Round(value);
    }

    public static uint PackUNorm(float bitmask, float value)
    {
        value *= bitmask;
        return (uint)ClampAndRound(value, 0.0f, bitmask);
    }

    public static float UnpackUNorm(uint bitmask, uint value)
    {
        value &= bitmask;
        return (float)value / bitmask;
    }

#if NET6_0_OR_GREATER && TODO
    public static uint PackRGBA(float x, float y, float z, float w)
    {
        Vector128<float> vector = Vector128.Create(x, y, z, w);
        if (Sse41.IsSupported)
        {
            // Clamp to bounds
            Vector128<float> result = Sse.Max(vector, Vector128<float>.Zero);
            result = Sse.Min(result, One);
            // Scale by multiplication
            result = Sse.Multiply(result, ScaleUByteN4);
            // Convert to int
            Vector128<int> vResulti = Sse2.ConvertToVector128Int32WithTruncation(result);
            // Mask off any fraction
            vResulti = Sse2.Add(vResulti, MaskUByteN4);
            // Do a horizontal or of 4 entries
            Vector128<int> vResulti2 = Sse2.Shuffle(vResulti, Shuffle(3, 2, 3, 2));
            // x = x|z, y = y|w
            vResulti = Sse2.Or(vResulti, vResulti2);
            // Move Z to the x position
            vResulti2 = Sse2.Shuffle(vResulti, Shuffle(1, 1, 1, 1));
            // Perform a single bit left shift to fix y|w
            vResulti2 = Sse2.Add(vResulti2, vResulti2);
            // i = x|y|z|w
            vResulti = Sse2.Or(vResulti, vResulti2);

            uint resultScalar;
            Sse.StoreScalar((float*)&resultScalar, Sse2.ConvertToVector128Single(vResulti));
            return resultScalar;
        }
        //else if (AdvSimd.Arm64.IsSupported)
        //{
        //    return AdvSimd.Add(left, right);
        //}
        else
        {
            return SoftwareFallback(vector);
        }

        static unsafe uint SoftwareFallback(Vector128<float> vector)
        {
            var N = Saturate(vector);
            N = Multiply(N, UByteMax);
            N = Truncate(N);

            float data;
            Sse.StoreAligned(&data, N);

            return 0;
        }
    }
#else
    public static uint PackRGBA(float x, float y, float z, float w)
    {
        uint red = PackUNorm(255.0f, x);
        uint green = PackUNorm(255.0f, y) << 8;
        uint blue = PackUNorm(255.0f, z) << 16;
        uint alpha = PackUNorm(255.0f, w) << 24;
        return red | green | blue | alpha;
    }
#endif

    public static uint PackSigned(uint bitmask, float value)
    {
        float max = bitmask >> 1;
        float min = 0f - max - 1.0f;
        return (uint)(int)ClampAndRound(value, min, max) & bitmask;
    }

    public static void UnpackRGBA(uint packedValue, out float x, out float y, out float z, out float w)
    {
        x = UnpackUNorm(255, packedValue);
        y = UnpackUNorm(255, packedValue >> 8);
        z = UnpackUNorm(255, packedValue >> 16);
        w = UnpackUNorm(255, packedValue >> 24);
    }

    public static byte ToByte(int value)
    {
        return (byte)(value < 0 ? 0 : value > 255 ? 255 : value);
    }

    public static byte ToByte(float component)
    {
        int value = (int)(component * 255.0f);
        return (byte)(value < 0 ? 0 : value > 255 ? 255 : value);
    }
}
