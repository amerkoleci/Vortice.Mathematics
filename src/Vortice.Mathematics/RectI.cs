// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines a 2D integer rectangle.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct RectI : IEquatable<RectI>
    {
        /// <summary>
        /// Returns a <see cref="RectI"/> with all of its values set to zero.
        /// </summary>
        public static readonly RectI Empty;

        /// <summary>
        /// Gets or sets the x-coordinate of the left edge of this <see cref="RectI" /> structure.
        /// </summary>
        public int Left;

        /// <summary>
        /// Gets or sets the y-coordinate of the top edge of this <see cref="RectI" /> structure.
        /// </summary>
        public int Top;

        /// <summary>
        /// Gets or sets the x-coordinate of the right edge of this <see cref="RectI" /> structure.
        /// </summary>
        public int Right;

        /// <summary>
        /// Gets or sets the y-coordinate of the bottom edge of this <see cref="RectI" /> structure.
        /// </summary>
        public int Bottom;

        /// <summary>
        /// Gets the width of the <see cref="RectI" />.
        /// </summary>
        public int Width
        {
            get => Right - Left;
            set => Right = Left + value;
        }

        /// <summary>
        /// Gets the height of the <see cref="RectI" />.
        /// </summary>
        public int Height
        {
            get => Bottom - Top;
            set => Bottom = Top + value;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RectI"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Gets the aspect ratio of the rectangle.
		/// </summary>
		public float AspectRatio => (float)Width / Height;

        /// <summary>
        /// Gets or sets the upper-left value of the <see cref="RectI"/>.
        /// </summary>
        public PointI Location
        {
            get => new PointI(Left, Top);
            set
            {
                this = Create(value, Size);
            }
        }

        /// <summary>
		/// Gets or sets the size of this <see cref="RectI"/>.
		/// </summary>
		public SizeI Size
        {
            get => new SizeI(Width, Height);
            set
            {
                Right = Left + value.Width;
                Bottom = Top + value.Height;
            }
        }

        public int CenterX => Left + (Width / 2);

        public int CenterY => Top + (Height / 2);

        public PointI Center => new PointI(CenterX, CenterY);

        /// <summary>
		/// Initializes a new instance of the <see cref="RectI"/> struct.
		/// </summary>
		/// <param name="left">The x-coordinate of the rectangle.</param>
		/// <param name="top">The y-coordinate of the rectangle.</param>
		/// <param name="right">The right-coordinate of the rectangle.</param>
		/// <param name="bottom">The bottom-coordinate of the rectangle.</param>
		public RectI(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public void Deconstruct(out int left, out int top, out int right, out int bottom)
        {
            left = Left;
            top = Top;
            right = Right;
            bottom = Bottom;
        }

        /// <summary>
        /// Creates a new rectangle with the specified size.
        /// </summary>
        /// <param name="width">The rectangle width.</param>
        /// <param name="height">The rectangle height.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static RectI Create(int width, int height)
        {
            return new RectI(0, 0, width, height);
        }

        /// <summary>
        /// Creates a new rectangle with the specified location and size.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="width">The rectangle width.</param>
        /// <param name="height">The rectangle height.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static RectI Create(int x, int y, int width, int height)
        {
            return new RectI(x, y, x + width, y + height);
        }

        /// <summary>
        /// Creates a new rectangle with the specified size.
        /// </summary>
        /// <param name="size">The rectangle size.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static RectI Create(SizeI size)
        {
            return Create(PointI.Empty, size);
        }

        /// <summary>
        /// Create a new rectangle with the specified location and size.
        /// </summary>
        /// <param name="location">The rectangle location.</param>
        /// <param name="size">The rectangle size.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static RectI Create(PointI location, SizeI size)
        {
            return Create(location.X, location.Y, size.Width, size.Height);
        }

        /// <summary>
        /// Translates this rectangle by a specified offset.
        /// </summary>
        /// <param name="offset">The offset value.</param>
        public void Offset(PointI offset)
        {
            Left += offset.X;
            Top += offset.Y;
            Right += offset.X;
            Bottom += offset.Y;
        }

        /// <summary>
        /// Translates this rectangle by a specified offset.
        /// </summary>
        /// <param name="x">The offset in the x-direction.</param>
        /// <param name="y">The offset in the y-direction.</param>
        public void Offset(int x, int y)
        {
            Left += x;
            Top += y;
            Right += x;
            Bottom += y;
        }

        public bool Contains(int x, int y) =>
            (x >= Left) && (x < Right) && (y >= Top) && (y < Bottom);

        public bool Contains(PointI point) => Contains(point.X, point.Y);

        public bool Contains(RectI rect) =>
            (Left <= rect.Left) && (Right >= rect.Right)
            && (Top <= rect.Top) && (Bottom >= rect.Bottom);

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is RectI value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="RectI"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="RectI"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(RectI other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="RectI"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="RectI"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref RectI other)
        {
            return Left == other.Left
                && Top == other.Top
                && Right == other.Right
                && Bottom == other.Bottom;
        }

        /// <summary>
        /// Compares two <see cref="RectI"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="RectI"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="RectI"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(RectI left, RectI right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="RectI"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="RectI"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="RectI"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(RectI left, RectI right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Left;
                hashCode = (hashCode * 397) ^ Top;
                hashCode = (hashCode * 397) ^ Right;
                hashCode = (hashCode * 397) ^ Bottom;
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(Left)}: {Left}, {nameof(Top)}: {Top}, {nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }
}
