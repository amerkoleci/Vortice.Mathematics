// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents a two dimensional mathematical int vector.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Int2 : IEquatable<Int2>, IFormattable
    {
        /// <summary>
        /// The size of the <see cref="Int2"/> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<Int2>();

        /// <summary>
        /// A <see cref="Int2"/> with all of its components set to zero.
        /// </summary>
        public static readonly Int2 Zero = new Int2();

        /// <summary>
        /// The X unit <see cref="Int2"/> (1, 0).
        /// </summary>
        public static readonly Int2 UnitX = new Int2(1, 0);

        /// <summary>
        /// The Y unit <see cref="Int2"/> (0, 1).
        /// </summary>
        public static readonly Int2 UnitY = new Int2(0, 1);

        /// <summary>
        /// A <see cref="Int2"/> with all of its components set to one.
        /// </summary>
        public static readonly Int2 One = new Int2(1, 1);

        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public int X;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public int Y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Int2"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Int2(int value)
        {
            X = Y = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Int2" /> struct.
        /// </summary>
        /// <param name="x">Initial value for the X component of the vector.</param>
        /// <param name="y">Initial value for the Y component of the vector.</param>
        public Int2(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Int2"/> struct.
        /// </summary>
        /// <param name="value">A vector containing the values with which to initialize the X and Y components.</param>
        public Int2(Vector2 value)
        {
            X = (int)value.X;
            Y = (int)value.Y;
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        /// <summary>
        /// Creates an array containing the elements of the vector.
        /// </summary>
        /// <returns>A two-element array containing the components of the vector.</returns>
        public int[] ToArray() => new int[] { X, Y };

        /// <summary>
        /// Performs an explicit conversion from <see cre ="Int2"/> to <see cref="Vector2" />.
        /// </summary>
        /// <param name = "value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector2(Int2 value) => new Vector2(value.X, value.Y);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Int2"/> to <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector4(Int2 value) => new Vector4(value.X, value.Y, 0, 0);

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Int2 value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="Int2"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Int2"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Int2 other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Int2"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Int2"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Int2 other)
        {
            return X == other.X
                && Y == other.Y;
        }

        /// <summary>
        /// Compares two <see cref="Int2"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Int2"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Int2"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Int2 left, Int2 right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Int2"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Int2"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Int2"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Int2 left, Int2 right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(Int3)}({X}, {Y})";
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"{nameof(Int2)}({X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)})";
        }
    }
}
