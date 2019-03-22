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
    /// Defines a 2D integer size.
    /// </summary>
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Size : IEquatable<Size>
    {
        /// <summary>
		/// The total size, in bytes, of an <see cref="Size"/> value.
		/// </summary>
		public static readonly int SizeInBytes = 8;

        /// <summary>
        /// Represents a <see cref="Size"/> that has Width and Height values set to zero.
        /// </summary>
        public static readonly Size Empty = new Size();

        /// <summary>
        /// Gets or sets the width of this <see cref="Size"/>.
        /// </summary>
        public int Width;

        /// <summary>
        /// Gets or sets the height of this <see cref="Size"/>.
        /// </summary>
        public int Height;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Size"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Gets the aspect ratio of the size.
		/// </summary>
		public float AspectRatio => (float)Width / Height;

        /// <summary>
		/// Initializes a new instance of the <see cref="Size"/> struct.
		/// </summary>
		/// <param name="width">Width value.</param>
		/// <param name="height">Height value.</param>
		public Size(int width, int height)
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
		public override bool Equals(object obj) => obj is Size size && Equals(ref size);

        /// <summary>
        /// Determines whether the specified <see cref="Size"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Size"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Size other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Size"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Size"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Size other)
        {
            return Width == other.Width
                && Height == other.Height;
        }

        /// <summary>
        /// Compares two <see cref="Size"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Size"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Size"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Size left, Size right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Size"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Size"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Size"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Size left, Size right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode() => HashHelpers.Combine(Width.GetHashCode(), Height.GetHashCode());

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Size [ Width={Width}, Height={Height} ]";
        }
    }
}
