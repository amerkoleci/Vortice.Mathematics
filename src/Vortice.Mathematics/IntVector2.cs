// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents a two dimensional mathematical int vector.
    /// </summary>
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct IntVector2 : IEquatable<IntVector2>
    {
        /// <summary>
        /// The size in bytes of the <see cref="IntVector2"/> type.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<IntVector2>();

        /// <summary>
        /// A <see cref="IntVector2"/> with all of its components set to zero.
        /// </summary>
        public static readonly IntVector2 Zero = new IntVector2();

        /// <summary>
        /// The X unit <see cref="IntVector2"/> (1, 0).
        /// </summary>
        public static readonly IntVector2 UnitX = new IntVector2(1, 0);

        /// <summary>
        /// The Y unit <see cref="IntVector2"/> (0, 1).
        /// </summary>
        public static readonly IntVector2 UnitY = new IntVector2(0, 1);

        /// <summary>
        /// A <see cref="IntVector2"/> with all of its components set to one.
        /// </summary>
        public static readonly IntVector2 One = new IntVector2(1, 1);

        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public int X;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public int Y;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntVector2"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public IntVector2(int value)
        {
            X = Y = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntVector2" /> struct.
        /// </summary>
        /// <param name="x">Initial value for the X component of the vector.</param>
        /// <param name="y">Initial value for the Y component of the vector.</param>
        public IntVector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cre ="IntVector2"/> to <see cref="Vector2" />.
        /// </summary>
        /// <param name = "value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector2(IntVector2 value) => new Vector2(value.X, value.Y);

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is IntVector2 value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="IntVector2"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="IntVector2"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(IntVector2 other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="IntVector2"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="IntVector2"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref IntVector2 other)
        {
            return X == other.X
                && Y == other.Y;
        }

        /// <summary>
        /// Compares two <see cref="IntVector2"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="IntVector2"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="IntVector2"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(IntVector2 left, IntVector2 right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="IntVector2"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="IntVector2"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="IntVector2"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(IntVector2 left, IntVector2 right) => !left.Equals(ref right);

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
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }
    }
}
