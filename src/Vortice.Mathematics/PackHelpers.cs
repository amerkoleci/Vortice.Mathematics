// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Mathematics
{
    internal static class PackHelpers
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

            return (float)Math.Round(value);
        }

        public static uint PackUNorm(float bitmask, float value)
        {
            value *= bitmask;
            return (uint)ClampAndRound(value, 0.0f, bitmask);
        }

        public static float UnpackUNorm(uint bitmask, uint value)
        {
            value &= bitmask;
            return (float)value / (float)bitmask;
        }

        public static uint PackRGBA(float x, float y, float z, float w)
        {
            uint red = PackUNorm(255.0f, x);
            uint green = PackUNorm(255.0f, y) << 8;
            uint blue = PackUNorm(255.0f, z) << 16;
            uint alpha = PackUNorm(255.0f, w) << 24;
            return red | green | blue | alpha;
        }

        public static void UnpackRGBA(uint packedValue, out float x, out float y, out float z, out float w)
        {
            x = UnpackUNorm(255, packedValue);
            y = UnpackUNorm(255, packedValue >> 8);
            z = UnpackUNorm(255, packedValue >> 16);
            w = UnpackUNorm(255, packedValue >> 24);
        }
    }
}
