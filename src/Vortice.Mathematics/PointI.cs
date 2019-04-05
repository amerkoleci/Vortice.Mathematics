// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines an ordered pair of integer x- and y-coordinates that defines a point in a two-dimensional plane.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct PointI : IEquatable<PointI>
    {
        /// <summary>
        /// Represents a <see cref="PointI"/> that has X and Y values set to zero.
        /// </summary>
        public static readonly PointI Empty;

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="PointI"/>.
        /// </summary>
        public int X;

        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="PointI"/>.
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
		public PointI(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointI"/> struct.
        /// </summary>
        /// <param name="size">The <see cref="SizeI"/> to initialize from.</param>
        public PointI(SizeI size)
        {
            X = size.Width;
            Y = size.Height;
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        /// <summary>
        /// Translates a given point by a specified offset.
        /// </summary>
        /// <param name="offset">The offset value.</param>
        public void Offset(PointI offset)
        {
            X += offset.X;
            Y += offset.Y;
        }

        /// <summary>
        /// Translates a given point by a specified offset.
        /// </summary>
        /// <param name="dx">The offset in the x-direction.</param>
        /// <param name="dy">The offset in the y-direction.</param>
        public void Offset(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        public static PointI Add(PointI left, SizeI right) => left + right;
        public static PointI Add(PointI left, PointI right) => left + right;

        public static PointI Subtract(PointI left, SizeI right) => left - right;
        public static PointI Subtract(PointI left, PointI right) => left - right;

        public static PointI operator +(PointI left, SizeI right) =>
            new PointI(left.X + right.Width, left.Y + right.Height);
        public static PointI operator +(PointI left, PointI right) =>
            new PointI(left.X + right.X, left.Y + right.Y);

        public static PointI operator -(PointI left, SizeI right) =>
            new PointI(left.X - right.Width, left.Y - right.Height);
        public static PointI operator -(PointI left, PointI right) =>
            new PointI(left.X - right.X, left.Y - right.Y);

        /// <summary>
        /// Performs an explicit conversion from <see cref="PointI"/> to <see cref="SizeI"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator SizeI(PointI point)
        {
            return new SizeI(point.X, point.Y);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="PointI"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="point">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Point(PointI point)
        {
            return new Point(point.X, point.Y);
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is PointI point && Equals(ref point);

        /// <summary>
        /// Determines whether the specified <see cref="PointI"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="PointI"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(PointI other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Point"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Point"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref PointI other)
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
        public static bool operator ==(PointI left, PointI right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Point"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Point"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Point"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(PointI left, PointI right) => !left.Equals(ref right);

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
