// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics.PackedVector
{
    /// <summary>
    /// Packed vector type containing two 16-bit signed integer values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Short2 : IPackedVector<uint>
    {
        private uint _packedValue;

        /// <summary>Gets or Sets the packed value.</summary>
        public uint PackedValue
        {
            get => _packedValue;
            set => _packedValue = value;
        }

        /// <summary>
        /// Expands the packed representation to a <see cref="Vector2"/>.
        /// </summary>
        public Vector2 ToVector2()
        {
            return new Vector2(
                (short)(_packedValue & 0xFFFF),
                (short)(_packedValue >> 0x10));
        }

        Vector4 IPackedVector.ToVector4()
        {
            var vector = ToVector2();
            return new Vector4(vector.X, vector.Y, 0.0f, 1.0f);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            _packedValue = Pack(vector.X, vector.Y);
        }

        private static uint Pack(float x, float y)
        {
            const float maxPos = 0x7FFF; // Largest two byte positive number 0xFFFF >> 1;
            const float minNeg = ~(int)maxPos; // two's complement

            // clamp the value between min and max values
            var word2 = ((uint)Math.Round(MathHelper.Clamp(x, minNeg, maxPos)) & 0xFFFF);
            var word1 = (((uint)Math.Round(MathHelper.Clamp(y, minNeg, maxPos)) & 0xFFFF) << 0x10);

            return (word2 | word1);
        }
    }
}
