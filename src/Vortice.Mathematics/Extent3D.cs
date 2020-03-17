// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents a three-dimensional extent.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Extent3D : IEquatable<Extent3D>, IFormattable
    {
        /// <summary>
        /// A <see cref="Extent3D"/> with all of its components set to zero.
        /// </summary>
        public static readonly Extent3D Zero = new Extent3D();

        /// <summary>
        /// The width component of the extent.
        /// </summary>
        public int Width;

        /// <summary>
        /// The height component of the extent.
        /// </summary>
        public int Height;

        /// <summary>
        /// The depth component of the extent.
        /// </summary>
        public int Depth;

        /// <summary>
        /// Initializes a new instance of <see cref="Extent3D"/> structure.
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

        /// <summary>
        /// Initializes a new instance of <see cref="Extent3D"/> structure.
        /// </summary>
        /// <param name="size">The width and height component of the extent.</param>
        /// <param name="depth">The depth component of the extent.</param>
        public Extent3D(Size size, int depth)
        {
            Width = size.Width;
            Height = size.Height;
            Depth = depth;
        }

        public void Deconstruct(out int width, out int height, out int depth)
        {
            width = Width;
            height = Height;
            depth = Depth;
        }

        /// <summary>
        /// Creates an array containing the elements of the extent.
        /// </summary>
        /// <returns>A three-element array containing the components of the extent.</returns>
        public int[] ToArray() => new int[] { Width, Height, Depth };

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
                var hashCode = Width.GetHashCode();
                hashCode = (hashCode * 397) ^ Height.GetHashCode();
                hashCode = (hashCode * 397) ^ Depth.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(Extent3D)}({Width}, {Height}, {Depth})";
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"{nameof(Extent3D)}({Width.ToString(format, formatProvider)}, {Height.ToString(format, formatProvider)}, {Depth.ToString(format, formatProvider)})";
        }
    }
}
