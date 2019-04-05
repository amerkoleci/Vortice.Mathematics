// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines an ordered pair of integers describing the width and height of a rectangle.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct SizeI : IEquatable<SizeI>
    {
        /// <summary>
        /// Represents a <see cref="SizeI"/> that has Width and Height values set to zero.
        /// </summary>
        public static readonly SizeI Empty;

        /// <summary>
        /// Gets or sets the width of this <see cref="SizeI"/> structure.
        /// </summary>
        public int Width;

        /// <summary>
        /// Gets or sets the height of this <see cref="SizeI"/> structure.
        /// </summary>
        public int Height;

        /// <summary>
        /// Gets a value indicating whether this <see cref="SizeI"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Gets the aspect ratio of the size.
		/// </summary>
		public float AspectRatio => (float)Width / Height;

        /// <summary>
		/// Initializes a new instance of the <see cref="SizeI"/> struct.
		/// </summary>
		/// <param name="width">Width value.</param>
		/// <param name="height">Height value.</param>
		public SizeI(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="SizeI"/> struct.
		/// </summary>
		/// <param name="point">The <see cref="PointI"/> value.</param>
		public SizeI(PointI point)
        {
            Width = point.X;
            Height = point.Y;
        }

        public void Deconstruct(out int width, out int height)
        {
            width = Width;
            height = Height;
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is SizeI value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="SizeI"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="SizeI"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(SizeI other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="SizeI"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="SizeI"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref SizeI other)
        {
            return Width == other.Width
                && Height == other.Height;
        }

        /// <summary>
        /// Compares two <see cref="SizeI"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="SizeI"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="SizeI"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(SizeI left, SizeI right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="SizeI"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="SizeI"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="SizeI"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(SizeI left, SizeI right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Width.GetHashCode();
                hashCode = (hashCode * 397) ^ Height.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }
}
