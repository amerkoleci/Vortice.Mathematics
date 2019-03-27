// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines a two-dimensional extent.
    /// </summary>
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Extent2D : IEquatable<Extent2D>
    {
        /// <summary>
        /// The size in bytes of the <see cref="Extent2D"/> type.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<Extent2D>();

        /// <summary>
        /// Returns a <see cref="Rect2D"/> with all of its values set to zero.
        /// </summary>
        public static readonly Extent2D Empty = new Extent2D();

        /// <summary>
        /// A special valued <see cref="Extent2D"/>.
        /// </summary>
        public static readonly Extent2D WholeSize = new Extent2D(~0, ~0);

        /// <summary>
        /// Gets or sets the width of this <see cref="Extent2D"/>.
        /// </summary>
        public int Width;

        /// <summary>
        /// Gets or sets the height of this <see cref="Extent2D"/>.
        /// </summary>
        public int Height;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Extent2D"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Gets the aspect ratio of the size.
		/// </summary>
		public float AspectRatio => (float)Width / Height;

        /// <summary>
		/// Initializes a new instance of the <see cref="Extent2D"/> struct.
		/// </summary>
		/// <param name="width">Width value.</param>
		/// <param name="height">Height value.</param>
		public Extent2D(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void Deconstruct(out int width, out int height)
        {
            width = Width;
            height = Height;
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Extent2D value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="Extent2D"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Extent2D"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Extent2D other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Extent2D"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Extent2D"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Extent2D other)
        {
            return Width == other.Width
                && Height == other.Height;
        }

        /// <summary>
        /// Compares two <see cref="Extent2D"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Extent2D"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Extent2D"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Extent2D left, Extent2D right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Extent2D"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Extent2D"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Extent2D"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Extent2D left, Extent2D right) => !left.Equals(ref right);

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
