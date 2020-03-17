#if TODO
// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// An integer rectangle structure defining X, Y, Width, Height.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Rectangle : IEquatable<Rectangle>
    {
        /// <summary>
        /// Returns a <see cref="Rectangle"/> with all of its values set to zero.
        /// </summary>
        public static readonly Rectangle Empty = default;

        /// <summary>
		/// Initializes a new instance of the <see cref="Rectangle"/> struct.
		/// </summary>
		/// <param name="x">The horizontal position of the rectangle.</param>
        /// <param name="y">The vertical position of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
		public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="Rectangle"/> struct.
		/// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
		public Rectangle(int width, int height)
        {
            X = 0;
            Y = 0;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> struct.
        /// </summary>
        /// <param name="point">
        /// The <see cref="Point"/> which specifies the rectangles point in a two-dimensional plane.
        /// </param>
        /// <param name="size">
        /// The <see cref="Size"/> which specifies the rectangles height and width.
        /// </param>
        public Rectangle(Point point, Size size)
        {
            X = point.X;
            Y = point.Y;
            Width = size.Width;
            Height = size.Height;
        }

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="Rectangle"/>.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="Rectangle"/>.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the width of this <see cref="Rectangle"/>.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of this <see cref="Rectangle"/>.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the upper-left value of the <see cref="Rectangle"/>.
        /// </summary>
        public Point Location
        {
            get => new Point(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
		/// Gets or sets the size of this <see cref="Rectangle"/>.
		/// </summary>
		public Size Size
        {
            get => new Size(Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the left edge of this <see cref="Rectangle"/>.
        /// </summary>
        public int Left
        {
            get => X;
            set => X = value;
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the left edge of this <see cref="Rectangle"/>.
        /// </summary>
        public int Top
        {
            get => Y;
            set => Y = value;
        }

        /// <summary>
        /// Gets the x-coordinate of the right edge of this <see cref="Rectangle"/>.
        /// </summary>
        public int Right
        {
            get => unchecked(X + Width);
        }

        /// <summary>
        /// Gets the y-coordinate of the bottom edge of this <see cref="Rectangle"/>.
        /// </summary>
        public int Bottom
        {
            get => unchecked(Y + Height);
        }

        /// <summary>
        /// Gets the <see cref="Point"/> that specifies the center of this <see cref="Rectangle"/>.
        /// </summary>
        public Point Center => new Point(X + (Width / 2), Y + (Height / 2));

        /// <summary>
        /// Gets the x-coordinate of the center of this <see cref="Rectangle"/>.
        /// </summary>
        public int CenterX => unchecked(X + (Width / 2));

        /// <summary>
        /// Gets the y-coordinate of the center of this <see cref="Rectangle"/>.
        /// </summary>
        public int CenterY => unchecked(Y + (Height / 2));

        /// <summary>
        /// Gets the aspect ratio of this <see cref="Rectangle"/>.
        /// </summary>
        public float AspectRatio => (float)Width / Height;

        /// <summary>
        /// Gets the position of the top-left corner of this <see cref="Rectangle"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Point TopLeft => new Point(X, Y);

        /// <summary>
        /// Gets the position of the top-right corner of this <see cref="Rectangle"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Point TopRight => new Point(Right, Y);

        /// <summary>
        /// Gets the position of the bottom-left corner of <see cref="Rectangle"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Point BottomLeft => new Point(X, Bottom);

        /// <summary>
        /// Gets the position of the bottom-right corner of <see cref="Rectangle"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Point BottomRight => new Point(Right, Bottom);

        /// <summary>
        /// Gets a value indicating whether this <see cref="Rectangle"/> is empty.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsEmpty => (Width == 0) && (Height == 0) && (X == 0) && (Y == 0);

        /// <summary>
        /// Inflates this <see cref="Rectangle"/> by the specified amount.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void Inflate(int width, int height)
        {
            unchecked
            {
                X -= width;
                Y -= height;

                Width += 2 * width;
                Height += 2 * height;
            }
        }

        /// <summary>
        /// Inflates this <see cref="Rectangle"/> by the specified amount.
        /// </summary>
        /// <param name="size">The size amount.</param>
        public void Inflate(Size size) => Inflate(size.Width, size.Height);

        /// <summary>
        /// Creates a <see cref="Rectangle"/> that is inflated by the specified amount.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="x">The amount to inflate the width by.</param>
        /// <param name="y">The amount to inflate the height by.</param>
        /// <returns>A new <see cref="Rectangle"/>.</returns>
        public static Rectangle Inflate(Rectangle rectangle, int x, int y)
        {
            Rectangle r = rectangle;
            r.Inflate(x, y);
            return r;
        }

        /// <summary>
        /// Replaces this <see cref="Rectangle" /> structure with the intersection of itself and the specified <see cref="Rect" /> structure.
        /// </summary>
        /// <param name="rectangle">The rect to intersects with.</param>
        public void Intersect(Rectangle rectangle)
        {
            Rectangle result = Intersect(rectangle, this);

            X = result.X;
            Y = result.Y;
            Width = result.Width;
            Height = result.Height;
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
        public bool Contains(int x, int y) => X <= x && x < Right && Y <= y && y < Bottom;

        /// <summary>
        /// Determines whether the specified coordinates are inside this rectangle.
        /// </summary>
        /// <param name="point">The point to test.</param>
        /// <returns>Returns true if the coordinates are inside this rectangle, otherwise false.</returns>
        public bool Contains(Point point) => Contains(point.X, point.Y);

        /// <summary>
        /// Determines whether the specified rectangle is inside this rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to test.</param>
        /// <returns>Returns true if the rectangle is inside this rectangle, otherwise false.</returns>
        public bool Contains(Rectangle rectangle)
        {
            return
                (X <= rectangle.X) && (rectangle.Right <= Right)
                && (Y <= rectangle.Y) && (rectangle.Bottom <= Bottom);
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
        /// <param name="left">The <see cref="Rectangle"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Rectangle"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Rectangle left, Rectangle right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Rectangle"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Rectangle"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Rectangle"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Rectangle left, Rectangle right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Rectangle [ X={X}, Y={Y}, Width={Width}, Height={Height} ]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Rectangle other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(Rectangle other) => X.Equals(other.X) && Y.Equals(other.Y) && Width.Equals(other.Width) && Height.Equals(other.Height);
    }
}

#endif
