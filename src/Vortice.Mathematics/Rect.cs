// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Stores a set of four integers that represent the upper-left corner and lower-right corner of a rectangle.
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
        /// The x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int Left;

        /// <summary>
        /// The y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int Top;

        /// <summary>
        /// The x-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public int Right;

        /// <summary>
        /// The y-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public int Bottom;

        /// <summary>
        /// Gets the width of the <see cref="Rect"/>.
        /// </summary>
        /// <remarks />
        public int Width => unchecked(Right - Left);

        /// <summary>
        /// Gets the height of the <see cref="RectF"/>.
        /// </summary>
        /// <remarks />
        public int Height => unchecked(Bottom - Top);

        /// <summary>
        /// Gets a value indicating whether this <see cref="Rect"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Gets the aspect ratio of the rectangle.
		/// </summary>
		public float AspectRatio => (float)Width / Height;

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

        /// <summary>
        /// Gets the x-coordinate of the center of this rectangle.
        /// </summary>
        public int CenterX => unchecked(Left + (Width / 2));

        /// <summary>
        /// Gets the y-coordinate of the center of this rectangle.
        /// </summary>
        public int CenterY => unchecked(Top + (Height / 2));

        /// <summary>
		/// Initializes a new instance of the <see cref="Rect"/> struct.
		/// </summary>
		/// <param name="left">The x-coordinate of the rectangle.</param>
		/// <param name="top">The y-coordinate of the rectangle.</param>
		/// <param name="right">The x-coordinate of the lower-right corner of the rectangle.</param>
		/// <param name="bottom">The y-coordinate of the lower-right corner of the rectangle.</param>
		public Rect(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Creates a new rectangle with the specified location and size.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="width">The rectangle width.</param>
        /// <param name="height">The rectangle height.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static Rect Create(int x, int y, int width, int height)
        {
            return new Rect(x, y, x + width, y + height);
        }

        /// <summary>
        /// Creates a new rectangle with the specified size.
        /// </summary>
        /// <param name="width">The rectangle width.</param>
        /// <param name="height">The rectangle height.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static Rect Create(int width, int height)
        {
            return new Rect(0, 0, width, height);
        }

        /// <summary>
        /// Creates a new rectangle with the specified location and size.
        /// </summary>
        /// <param name="location">The rectangle location.</param>
        /// <param name="size">The rectangle size.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static Rect Create(Point location, Size size)
        {
            return new Rect(location.X, location.Y, location.X + size.Width, location.Y + size.Height);
        }

        /// <summary>
        /// Creates a new rectangle with the specified size.
        /// </summary>
        /// <param name="size">The rectangle size.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static Rect Create(Size size)
        {
            return new Rect(0, 0, size.Width, size.Height);
        }

        /// <summary>
        /// Inflates this <see cref="Rect"/> by the specified amount.
        /// </summary>
        /// <param name="x">X inflate amount.</param>
        /// <param name="y">Y inflate amount.</param>
        public void Inflate(int x, int y)
        {
            Left -= x;
            Top -= y;
            Right += x;
            Bottom += y;
        }

        /// <summary>
        /// Inflates this <see cref="Rect"/> by the specified amount.
        /// </summary>
        /// <param name="size">The size amount.</param>
        public void Inflate(Size size) => Inflate(size.Width, size.Height);

        /// <summary>Creates and returns an enlarged copy of the specified <see cref="Rect" /> structure. 
        /// The copy is enlarged by the specified amount and the original rectangle remains unmodified.
        /// </summary>
        /// <param name="rect">The <see cref="Rect" /> to be copied. This rectangle is not modified.</param>
        /// <param name="x">The amount to enlarge the copy of the rectangle horizontally.</param>
        /// <param name="y">The amount to enlarge the copy of the rectangle vertically.</param>
        /// <returns>The enlarged <see cref="Rect" />.</returns>
        public static Rect Inflate(Rect rect, int x, int y)
        {
            var result = new Rect(rect.Left, rect.Top, rect.Right, rect.Bottom);
            result.Inflate(x, y);
            return result;
        }

        /// <summary>
        /// Replaces this <see cref="Rect" /> structure with the intersection of itself and the specified <see cref="Rect" /> structure.
        /// </summary>
        /// <param name="rect">The rect to intersects with.</param>
        public void Intersect(Rect rect)
        {
            this = Intersect(rect, this);
        }

        /// <summary>
        /// Creates a rectangle that represents the intersection between two rectangles. 
        /// If there is no intersection, <see cref="Empty"/> is returned.
        /// </summary>
        /// <param name="value1">The first rectangle.</param>
        /// <param name="value2">The second rectangle.</param>
        /// <returns>The intersection rectangle or empty.</returns>
        public static Rect Intersect(Rect value1, Rect value2)
        {
            if (!value1.IntersectsWithInclusive(value2))
            {
                return Empty;
            }

            return new Rect(
                Math.Max(value1.Left, value2.Left), 
                Math.Max(value1.Top, value2.Top), 
                Math.Min(value1.Right, value2.Right), 
                Math.Min(value1.Bottom, value2.Bottom));
        }

        /// <summary>
        /// Determines if this rectangle intersects with rect.
        /// </summary>
        /// <param name="rect">The <see cref="Rect"/> to test.</param>
        /// <returns>This method returns true if there is any intersection.</returns>
        public bool IntersectsWith(Rect rect)
        {
            return (Left < rect.Right) && (Right > rect.Left) && (Top < rect.Bottom) && (Bottom > rect.Top);
        }

        /// <summary>
        /// Determines if this rectangle intersects with another rectangle.
        /// </summary>
        /// <param name="rect">The rectangle to test.</param>
        /// <returns>This method returns true if there is any intersection.</returns>
        public bool IntersectsWithInclusive(Rect rect)
        {
            return (Left <= rect.Right) && (Right >= rect.Left) 
                && (Top <= rect.Bottom) && (Bottom >= rect.Top);
        }

        /// <summary>
        /// Creates a rectangle that represents the union between two rectangles.
        /// </summary>
        /// <param name="value1">The first rectangle.</param>
        /// <param name="value2">The second rectangle.</param>
        /// <returns>The union rectangle.</returns>
        public static Rect Union(Rect value1, Rect value2)
        {
            return new Rect(
                Math.Min(value1.Left, value2.Left), 
                Math.Min(value1.Top, value2.Top), 
                Math.Max(value1.Right, value2.Right), 
                Math.Max(value1.Bottom, value2.Bottom));
        }

        /// <summary>
        /// Translates this rectangle by a specified offset.
        /// </summary>
        /// <param name="offset">The offset value.</param>
        public void Offset(Point offset)
        {
            unchecked
            {
                Left += offset.X;
                Top += offset.Y;
                Right += offset.X;
                Bottom += offset.Y;
            }
        }

        /// <summary>
        /// Translates this rectangle by a specified offset.
        /// </summary>
        /// <param name="x">The offset in the x-direction.</param>
        /// <param name="y">The offset in the y-direction.</param>
        public void Offset(int x, int y)
        {
            unchecked
            {
                Left += x;
                Top += y;
                Right += x;
                Bottom += y;
            }
        }

        /// <summary>
        /// Determines whether the specified coordinates are inside this rectangle.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>Returns true if the coordinates are inside this rectangle, otherwise false.</returns>
        public bool Contains(int x, int y)
        {
            return (x >= Left) && (x < Right) && (y >= Top) && (y < Bottom);
        }

        /// <summary>
        /// Determines whether the specified coordinates are inside this rectangle.
        /// </summary>
        /// <param name="point">The point to test.</param>
        /// <returns>Returns true if the coordinates are inside this rectangle, otherwise false.</returns>
        public bool Contains(Point point) => Contains(point.X, point.Y);

        /// <summary>
        /// Determines whether the specified rectangle is inside this rectangle.
        /// </summary>
        /// <param name="rect">The rectangle to test.</param>
        /// <returns>Returns true if the rectangle is inside this rectangle, otherwise false.</returns>
        public bool Contains(Rect rect)
        {
            return (Left <= rect.Left) && (Right >= rect.Right) 
                && (Top <= rect.Top) && (Bottom >= rect.Bottom);
        }

        /// <summary>
        /// Converts the specified <see cref="RectF" /> structure to a <see cref="Rect" /> structure by rounding the <see cref="Rect" /> values to the nearest integer values.
        /// </summary>
        /// <param name="value">The <see cref="RectF" /> structure to be converted.</param>
        /// <returns>Returns a <see cref="Rect" />.</returns>
        public static Rect Round(RectF value)
        {
            int x, y, r, b;
            checked
            {
                x = (int)Math.Round(value.Left);
                y = (int)Math.Round(value.Top);
                r = (int)Math.Round(value.Right);
                b = (int)Math.Round(value.Bottom);
            }

            return new Rect(x, y, r, b);
        }

        /// <summary>
        /// Converts the specified <see cref="RectF" /> structure to a <see cref="Rect" /> structure by truncating the <see cref="RectF" /> values.
        /// </summary>
        /// <param name="value">The <see cref="RectF" /> to be converted.</param>
        /// <returns>The truncated value of the <see cref="Rect" />.</returns>
        public static Rect Truncate(RectF value)
        {
            int x, y, r, b;
            checked
            {
                x = (int)value.Left;
                y = (int)value.Top;
                r = (int)value.Right;
                b = (int)value.Bottom;
            }

            return new Rect(x, y, r, b);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rectangle"/> to <see cref="Rect"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rect(Rectangle value)
        {
            return new Rect(value.Left, value.Top, value.Right, value.Bottom);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rect"/> to <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rectangle(Rect value)
        {
            return Rectangle.FromLTRB(value.Left, value.Top, value.Right, value.Bottom);
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Rect value && Equals(ref value);

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
            return Left == other.Left
                && Top == other.Top
                && Right == other.Right
                && Bottom == other.Bottom;
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
