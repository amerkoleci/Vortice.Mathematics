// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines a three-dimensional extent.
    /// </summary>
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Extent3D : IEquatable<Extent3D>
    {
        /// <summary>
        /// The size in bytes of the <see cref="Extent3D"/> type.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<Extent3D>();

        /// <summary>
        /// Returns a <see cref="Rect2D"/> with all of its values set to zero.
        /// </summary>
        public static readonly Extent3D Empty = new Extent3D(0, 0, 0);

        /// <summary>
        /// A special valued <see cref="Extent3D"/>.
        /// </summary>
        public static readonly Extent3D WholeSize = new Extent3D(~0, ~0, ~0);

        /// <summary>
        /// The width component of the <see cref="Extent3D"/>.
        /// </summary>
        public int Width;

        /// <summary>
        /// The height component of the <see cref="Extent3D"/>.
        /// </summary>
        public int Height;

        /// <summary>
        /// The depth component of the <see cref="Extent3D"/>.
        /// </summary>
        public int Depth;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Extent3D"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Gets the aspect ratio of the size.
		/// </summary>
		public float AspectRatio => (float)Width / Height;

        /// <summary>
		/// Initializes a new instance of the <see cref="Extent3D"/> struct.
		/// </summary>
		/// <param name="width">The width component of the extent.</param>
        /// <param name="height">The height component of the extent.</param>
        /// <param name="depth">The depth component of the extent.</param>
		public Extent3D(int width, int height, int depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
        }

        public void Deconstruct(out int width, out int height, out int depth)
        {
            width = Width;
            height = Height;
            depth = Depth;
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Extent3D value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="Extent3D"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Extent3D"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Extent3D other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Extent3D"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Extent3D"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Extent3D other)
        {
            return Width == other.Width
                && Height == other.Height
                && Depth == other.Depth;
        }

        /// <summary>
        /// Compares two <see cref="Extent3D"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Extent3D"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Extent3D"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Extent3D left, Extent3D right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Extent3D"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Extent3D"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Extent3D"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Extent3D left, Extent3D right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Width.GetHashCode();
                hashCode = (hashCode * 397) ^ Height.GetHashCode();
                hashCode = (hashCode * 397) ^ Depth.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}, {nameof(Depth)}: {Depth}";
        }
    }
}
