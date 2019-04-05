// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines a four floating-point numbers that represent the upper-left corner and lower-right corner of a rectangle.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect : IEquatable<Rect>
    {
        /// <summary>
        /// Returns a <see cref="Rect"/> with all of its values set to zero.
        /// </summary>
        public static readonly Rect Empty;

        /// <summary>
        /// Gets or sets the x-coordinate of the left edge of this <see cref="Rect" /> structure.
        /// </summary>
        public float Left;

        /// <summary>
        /// Gets or sets the y-coordinate of the top edge of this <see cref="Rect" /> structure.
        /// </summary>
        public float Top;

        /// <summary>
        /// Gets or sets the x-coordinate of the right edge of this <see cref="Rect" /> structure.
        /// </summary>
        public float Right;

        /// <summary>
        /// Gets or sets the y-coordinate of the bottom edge of this <see cref="Rect" /> structure.
        /// </summary>
        public float Bottom;

        /// <summary>
        /// Gets the width of the <see cref="Rect" />.
        /// </summary>
        public float Width
        {
            get => Right - Left;
            set => Right = Left + value;
        }

        /// <summary>
        /// Gets the height of the <see cref="Rect" />.
        /// </summary>
        public float Height
        {
            get => Bottom - Top;
            set => Bottom = Top + value;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Rect"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Gets the aspect ratio of the rectangle.
		/// </summary>
		public float AspectRatio => Width / Height;

        /// <summary>
        /// Gets or sets the upper-left value of the <see cref="Rect"/>.
        /// </summary>
        public Point Location
        {
            get => new Point(Left, Top);
            set
            {
                this = Create(value, Size);
            }
        }

        /// <summary>
		/// Gets or sets the size of this <see cref="Rect"/>.
		/// </summary>
		public Size Size
        {
            get => new Size(Width, Height);
            set
            {
                Right = Left + value.Width;
                Bottom = Top + value.Height;
            }
        }

        public float CenterX => Left + (Width / 2.0f);

        public float CenterY => Top + (Height / 2.0f);

        public Point Center => new Point(CenterX, CenterY);

        /// <summary>
		/// Initializes a new instance of the <see cref="Rect"/> struct.
		/// </summary>
		/// <param name="left">The x-coordinate of the rectangle.</param>
		/// <param name="top">The y-coordinate of the rectangle.</param>
		/// <param name="right">The right-coordinate of the rectangle.</param>
		/// <param name="bottom">The bottom-coordinate of the rectangle.</param>
		public Rect(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public void Deconstruct(out float left, out float top, out float right, out float bottom)
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
        public static Rect Create(float width, float height)
        {
            return new Rect(0.0f, 0.0f, width, height);
        }

        /// <summary>
        /// Creates a new rectangle with the specified location and size.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="width">The rectangle width.</param>
        /// <param name="height">The rectangle height.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static Rect Create(float x, float y, float width, float height)
        {
            return new Rect(x, y, x + width, y + height);
        }

        /// <summary>
        /// Creates a new rectangle with the specified size.
        /// </summary>
        /// <param name="size">The rectangle size.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static Rect Create(Size size)
        {
            return Create(Point.Empty, size);
        }

        /// <summary>
        /// Create a new rectangle with the specified location and size.
        /// </summary>
        /// <param name="location">The rectangle location.</param>
        /// <param name="size">The rectangle size.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static Rect Create(Point location, Size size)
        {
            return Create(location.X, location.Y, size.Width, size.Height);
        }

        /// <summary>
        /// Translates this rectangle by a specified offset.
        /// </summary>
        /// <param name="offset">The offset value.</param>
        public void Offset(Point offset)
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
        public void Offset(float x, float y)
        {
            Left += x;
            Top += y;
            Right += x;
            Bottom += y;
        }

        public bool Contains(float x, float y) =>
            (x >= Left) && (x < Right) && (y >= Top) && (y < Bottom);

        public bool Contains(Point point) => Contains(point.X, point.Y);

        public bool Contains(Rect rect) =>
            (Left <= rect.Left) && (Right >= rect.Right)
            && (Top <= rect.Top) && (Bottom >= rect.Bottom);

        /// <summary>
        /// Performs an implicit conversion from <see cref="RectI"/> to <see cref="Rect"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rect(RectI value) =>
            new Rect(value.Left, value.Top, value.Right, value.Bottom);

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Rect rect && Equals(ref rect);

        /// <summary>
        /// Determines whether the specified <see cref="Rect"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Rect"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Rect other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Rect"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Rect"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Rect other)
        {
            return MathHelper.NearEqual(Left, other.Left)
                && MathHelper.NearEqual(Top, other.Top)
                && MathHelper.NearEqual(Right, other.Right)
                && MathHelper.NearEqual(Bottom, other.Bottom);
        }

        /// <summary>
        /// Compares two <see cref="Rect"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Rect"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Rect"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Rect left, Rect right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Rect"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Rect"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Rect"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Rect left, Rect right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Left.GetHashCode();
                hashCode = (hashCode * 397) ^ Top.GetHashCode();
                hashCode = (hashCode * 397) ^ Right.GetHashCode();
                hashCode = (hashCode * 397) ^ Bottom.GetHashCode();
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
