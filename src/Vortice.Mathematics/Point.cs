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
    /// Defines a 2D integer point.
    /// </summary>
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Point : IEquatable<Point>
    {
        /// <summary>
		/// The total size, in bytes, of an <see cref="Point"/> value.
		/// </summary>
		public static readonly int SizeInBytes = 8;

        /// <summary>
        /// Represents a <see cref="Point"/> that has X and Y values set to zero.
        /// </summary>
        public static readonly Point Empty = new Point();

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="Point"/>.
        /// </summary>
        public int X;

        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="Point"/>.
        /// </summary>
        public int Y;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Initializes a new instance of the <see cref="Point"/> struct.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Point point && Equals(ref point);

        /// <summary>
        /// Determines whether the specified <see cref="Point"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Point"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Point other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Point"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Point"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Point other)
        {
            return X == other.X
                && Y == other.Y;
        }

        /// <summary>
        /// Compares two <see cref="Point"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Point"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Point"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Point left, Point right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Point"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Point"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Point"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Point left, Point right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode() => HashHelpers.Combine(X.GetHashCode(), Y.GetHashCode());

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Point [ X={X}, Y={Y} ]";
        }
    }
}
