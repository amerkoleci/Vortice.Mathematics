// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Vortice.Mathematics.PackedVector;

namespace Vortice.Mathematics
{
    /// <summary>
    /// A half precision (16 bit) floating point value.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Half : IPackedVector<ushort>, IEquatable<Half>, IFormattable
    {
        /// <summary>
        /// Number of decimal digits of precision.
        /// </summary>
        public const int PrecisionDigits = 3;

        /// <summary>
        /// Number of bits in the mantissa.
        /// </summary>
        public const int MantissaBits = 11;

        /// <summary>
        /// Maximum decimal exponent.
        /// </summary>
        public const int MaximumDecimalExponent = 4;

        /// <summary>
        /// Maximum binary exponent.
        /// </summary>
        public const int MaximumBinaryExponent = 15;

        /// <summary>
        /// Minimum decimal exponent.
        /// </summary>
        public const int MinimumDecimalExponent = -4;

        /// <summary>
        /// Minimum binary exponent.
        /// </summary>
        public const int MinimumBinaryExponent = -14;

        /// <summary>
        /// Exponent radix.
        /// </summary>
        public const int ExponentRadix = 2;

        /// <summary>
        /// Additional rounding.
        /// </summary>
        public const int AdditionRounding = 1;

        /// <summary>
        /// Smallest such that 1.0 + epsilon != 1.0
        /// </summary>
        public static readonly float Epsilon = 0.0004887581f;

        /// <summary>
        /// Maximum value of the number.
        /// </summary>
        public static readonly float MaxValue = 65504f;

        /// <summary>
        /// Minimum value of the number.
        /// </summary>
        public static readonly float MinValue = 6.103516E-05f;
#pragma warning disable IDE0032 // DO NOT REMOVE UNLESS https://github.com/Microsoft/dotnet/issues/807 IS FIXED
        private ushort _packedValue;
#pragma warning disable IDE0032
        /// <summary>
        /// Gets or sets the packed value of this <see cref="Half"/> structure. 
        /// </summary>
        public ushort PackedValue { get => _packedValue; set => _packedValue = value; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Half"/> structure.
        /// </summary>
        /// <param name="value">The floating point value that should be stored in 16 bit format.</param>
        public Half(float value)
        {
            _packedValue = HalfUtils.ConvertFloatToHalf(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Half"/> structure.
        /// </summary>
        /// <param name="value">The pack value.</param>
        public Half(ushort value)
        {
            _packedValue = value;
        }

        /// <summary>
        /// Expands the packed value to a <see cref="float"/>.
        /// </summary>
        public float ToSingle() => HalfUtils.ConvertHalfToFloat(PackedValue);

        #region IPackedVector Implementation
        Vector4 IPackedVector.ToVector4()
        {
            float x = ToSingle();
            return new Vector4(x, 0.0f, 0.0f, 1.0f);
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            PackedValue = HalfUtils.ConvertFloatToHalf(vector.X);
        }
        #endregion IPackedVector Implementation

        /// <summary>
        /// Performs an explicit conversion from <see cref="float"/> to <see cref="Half"/>.
        /// </summary>
        /// <param name = "value">The value to be converted.</param>
        /// <returns>The converted value.</returns>
        public static implicit operator Half(float value) => new Half(value);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Half"/> to <see cref="float"/>.
        /// </summary>
        /// <param name = "value">The value to be converted.</param>
        /// <returns>The converted value.</returns>
        public static implicit operator float(Half value) => HalfUtils.ConvertHalfToFloat(value.PackedValue);

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Half value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="Half"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Half"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Half other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Half"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Half"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Half other)
        {
            return PackedValue == other.PackedValue;
        }

        /// <summary>
        /// Compares two <see cref="Half"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Half"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Half"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Half left, Half right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Half"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Half"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Half"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Half left, Half right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode() => PackedValue.GetHashCode();

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(Half)}({ToSingle()})";
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"{nameof(Half)}({ToSingle().ToString(format, formatProvider)})";
        }
    }
}
