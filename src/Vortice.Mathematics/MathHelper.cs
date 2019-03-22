// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Numerics.Hashing;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines a math helper class.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>Represents the mathematical constant e.</summary>
        public const float E = 2.71828175f;

        /// <summary>Represents the log base two of e.</summary>
        public const float Log2E = 1.442695f;

        /// <summary>Represents the log base ten of e.</summary>
        public const float Log10E = 0.4342945f;

        /// <summary>Represents the value of pi.</summary>
        public const float Pi = 3.14159274f;

        /// <summary>Represents the value of pi times two.</summary>
        public const float TwoPi = 6.28318548f;

        /// <summary>Represents the value of pi divided by two.</summary>
        public const float PiOver2 = 1.57079637f;

        /// <summary>Represents the value of pi divided by four.</summary>
        public const float PiOver4 = 0.7853982f;

        /// <summary>Restricts a value to be within a specified range. Reference page contains links to related code samples.</summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If value is less than min, min will be returned.</param>
        /// <param name="max">The maximum value. If value is greater than max, max will be returned.</param>
        public static float Clamp(float value, float min, float max)
        {
            value = (value > max) ? max : value;
            value = (value < min) ? min : value;
            return value;
        }
    }
}
