// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines a two-dimensional integer rectangle.
    /// </summary>
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect2D : IEquatable<Rect2D>
    {
        /// <summary>
        /// The size in bytes of the <see cref="Rect2D"/> type.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<Rect2D>();

        /// <summary>
        /// Returns a <see cref="Rect2D"/> with all of its values set to zero.
        /// </summary>
        public static readonly Rect2D Empty = new Rect2D();

        /// <summary>
        /// The offset component of the rectangle.
        /// </summary>
        public Offset2D Offset;

        /// <summary>
        /// The extent component of the rectangle.
        /// </summary>
        public Extent2D Extent;

        /// <summary>
		/// Gets or sets the x-coordinate of the left side of the rectangle.
		/// </summary>
		public int X
        {
            get => Offset.X;
            set => Offset.X = value;
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the top of the rectangle.
        /// </summary>
        public int Y
        {
            get => Offset.Y;
            set => Offset.Y = value;
        }

        /// <summary>
        /// Gets or sets the width of the rectangle.
        /// </summary>
        public int Width
        {
            get => Extent.Width;
            set => Extent.Width = value;
        }

        /// <summary>
        /// Gets or sets the height of the rectangle.
        /// </summary>
        public int Height
        {
            get => Extent.Height;
            set => Extent.Height = value;
        }

        /// <summary>
		/// Gets or set the x-coordinate of the left side of the rectangle.
		/// </summary>
		public int Left
        {
            get => Offset.X;
            set => Offset.X = value;
        }

        /// <summary>
        /// Gets or set the y-coordinate of the top of the rectangle.
        /// </summary>
        public int Top
        {
            get => Offset.Y;
            set => Offset.Y = value;
        }

        /// <summary>
        /// Gets the x-coordinate of the right side of the rectangle.
        /// </summary>
        public int Right
        {
            get => X + Extent.Width;
            set => Extent.Width = value - X;
        }

        /// <summary>
        /// Gets the y-coordinate of the bottom of the rectangle.
        /// </summary>
        public int Bottom
        {
            get => Y + Extent.Height;
            set => Extent.Height = value - Y;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Rect2D"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Gets the aspect ratio of the rectangle.
		/// </summary>
		public float AspectRatio => (float)Extent.Width / Extent.Height;

        /// <summary>
		/// Initializes a new instance of the <see cref="Rect2D"/> struct.
		/// </summary>
		/// <param name="x">The x-coordinate of the rectangle.</param>
		/// <param name="y">The y-coordinate of the rectangle.</param>
		/// <param name="width">Width of the rectangle.</param>
		/// <param name="height">Height of the rectangle.</param>
		public Rect2D(int x, int y, int width, int height)
        {
            Offset = new Offset2D(x, y);
            Extent = new Extent2D(width, height);
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="Rect2D"/> struct.
		/// </summary>
		/// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
		public Rect2D(int width, int height)
        {
            Offset = new Offset2D();
            Extent = new Extent2D(width, height);
        }

        public void Deconstruct(out int x, out int y, out int width, out int height)
        {
            x = X;
            y = Y;
            width = Extent.Width;
            height = Extent.Height;
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Rect2D value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="Rect2D"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Rect2D"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Rect2D other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Rect2D"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Rect2D"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Rect2D other)
        {
            return Offset.Equals(ref other.Offset)
                && Extent.Equals(ref other.Extent);
        }

        /// <summary>
        /// Compares two <see cref="Rect2D"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Rect2D"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Rect2D"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Rect2D left, Rect2D right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Rect2D"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Rect2D"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Rect2D"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Rect2D left, Rect2D right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Offset.GetHashCode();
                hashCode = (hashCode * 397) ^ Extent.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Extent.Width)}: {Extent.Width}, {nameof(Extent.Height)}: {Extent.Height}";
        }
    }
}
