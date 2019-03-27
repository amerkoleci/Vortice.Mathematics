// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines a two-dimensional offset.
    /// </summary>
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Offset2D : IEquatable<Offset2D>
    {
        /// <summary>
        /// The size in bytes of the <see cref="Offset2D"/> type.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<Offset2D>();

        /// <summary>
        /// Returns a <see cref="Rect2D"/> with all of its values set to zero.
        /// </summary>
        public static readonly Offset2D Empty = new Offset2D();

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="Offset2D"/>.
        /// </summary>
        public int X;

        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="Offset2D"/>.
        /// </summary>
        public int Y;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Offset2D"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Initializes a new instance of the <see cref="Offset2D"/> struct.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public Offset2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Offset2D"/> to <see cref="System.Drawing.Point"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.Point(Offset2D value)
        {
            return new System.Drawing.Point(value.X, value.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Point"/> to <see cref="Offset2D"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Offset2D(System.Drawing.Point value)
        {
            return new Offset2D(value.X, value.Y);
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Offset2D value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="Offset2D"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Offset2D"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Offset2D other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Offset2D"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Offset2D"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Offset2D other)
        {
            return X == other.X
                && Y == other.Y;
        }

        /// <summary>
        /// Compares two <see cref="Offset2D"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Offset2D"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Offset2D"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Offset2D left, Offset2D right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Offset2D"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Offset2D"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Offset2D"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Offset2D left, Offset2D right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X.GetHashCode();
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
