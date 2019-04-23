// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines a 2D integer rectangle.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect : IEquatable<Rect>
    {
        /// <summary>
        /// The size of the <see cref="Rect"/> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<Rect>();

        /// <summary>
        /// Returns a <see cref="Rect"/> with all of its values set to zero.
        /// </summary>
        public static readonly Rect Empty;

        /// <summary>
        /// The x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int X;

        /// <summary>
        /// The y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int Y;

        /// <summary>
        /// The width of the rectangle, in pixels.
        /// </summary>
        public int Width;

        /// <summary>
        /// The height of the rectangle, in pixels.
        /// </summary>
        public int Height;

        /// <summary>
        /// The x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int Left => X;

        /// <summary>
        /// The y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int Top => Y;

        /// <summary>
        /// Gets the x-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public int Right
        {
            get => unchecked(X + Width);
        }

        /// <summary>
        /// Gets the y-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public int Bottom
        {
            get => unchecked(Y + Height);
        }

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
                X = value.X;
                Y = value.Y;
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
                Width = value.Width;
                Height = value.Height;
            }
        }

        public int CenterX => Left + (Width / 2);

        public int CenterY => Top + (Height / 2);

        public Point Center => new Point(CenterX, CenterY);

        /// <summary>
		/// Initializes a new instance of the <see cref="Rect"/> struct.
		/// </summary>
		/// <param name="x">The x-coordinate of the rectangle.</param>
		/// <param name="y">The y-coordinate of the rectangle.</param>
		/// <param name="width">The width of the rectangle.</param>
		/// <param name="height">The height of the rectangle.</param>
		public Rect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="Rect"/> struct.
		/// </summary>
		/// <param name="width">The width of the rectangle.</param>
		/// <param name="height">The height of the rectangle.</param>
		public Rect(int width, int height)
        {
            X = 0;
            Y = 0;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rect"/> struct.
        /// </summary>
        /// <param name="location">The x and y location.</param>
        /// <param name="size">The rectangle size.</param>
        public Rect(Point location, Size size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.Width;
            Height = size.Height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rect"/> struct.
        /// </summary>
        /// <param name="size">The rectangle size.</param>
        public Rect(Size size)
        {
            X = 0;
            Y = 0;
            Width = size.Width;
            Height = size.Height;
        }

        public void Deconstruct(out int x, out int y, out int width, out int height)
        {
            x = X;
            y = Y;
            width = Width;
            height = Height;
        }

        /// <summary>
        /// Create new instance of <see cref="RectF"/> from left, top, right, bottom coordinate.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public static Rect FromLTRB(int left, int top, int right, int bottom) =>
           new Rect(left, top, unchecked(right - left), unchecked(bottom - top));

        /// <summary>
        /// Inflates this <see cref="Rect"/> by the specified amount.
        /// </summary>
        /// <param name="x">X inflate amount.</param>
        /// <param name="y">Y inflate amount.</param>
        public void Inflate(int x, int y)
        {
            unchecked
            {
                X -= x;
                Y -= y;
                Width += 2 * x;
                Height += 2 * y;
            }
        }

        /// <summary>
        /// Inflates this <see cref="Rect"/> by the specified amount.
        /// </summary>
        /// <param name="size">The size amount.</param>
        public void Inflate(Size size) => Inflate(size.Width, size.Height);

        /// <summary>
        /// Creates a <see cref="Rect"/> that is inflated by the specified amount.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Rect Inflate(Rect rect, int x, int y)
        {
            Rect r = rect;
            r.Inflate(x, y);
            return r;
        }

        /// <summary>
        /// Creates a Rectangle that represents the intersection between this <see cref="Rect"/> and rect.
        /// </summary>
        /// <param name="rect">The rect to intersects with.</param>
        public void Intersect(Rect rect)
        {
            Rect result = Intersect(rect, this);

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
            int x1 = Math.Max(value1.X, value2.X);
            int x2 = Math.Min(value1.X + value1.Width, value2.X + value2.Width);
            int y1 = Math.Max(value1.Y, value2.Y);
            int y2 = Math.Min(value1.Y + value1.Height, value2.Y + value2.Height);

            if (x2 >= x1 && y2 >= y1)
            {
                return new Rect(x1, y1, x2 - x1, y2 - y1);
            }

            return Empty;
        }

        /// <summary>
        /// Determines if this rectangle intersects with rect.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool IntersectsWith(Rect rect) =>
            (rect.X < X + Width) && (X < rect.X + rect.Width) && (rect.Y < Y + Height) && (Y < rect.Y + rect.Height);

        /// <summary>
        /// Creates a rectangle that represents the union between two rectangles.
        /// </summary>
        /// <param name="value1">The first rectangle.</param>
        /// <param name="value2">The second rectangle.</param>
        /// <returns>The union rectangle.</returns>
        public static Rect Union(Rect value1, Rect value2)
        {
            int x1 = Math.Min(value1.X, value2.X);
            int x2 = Math.Max(value1.X + value1.Width, value2.X + value2.Width);
            int y1 = Math.Min(value1.Y, value2.Y);
            int y2 = Math.Max(value1.Y + value1.Height, value2.Y + value2.Height);

            return new Rect(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// Translates this rectangle by a specified offset.
        /// </summary>
        /// <param name="offset">The offset value.</param>
        public void Offset(Point offset)
        {
            unchecked
            {
                X += offset.X;
                Y += offset.Y;
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
                X += x;
                Y += y;
            }
        }

        public bool Contains(int x, int y) => X <= x && x < X + Width && Y <= y && y < Y + Height;

        public bool Contains(Point point) => Contains(point.X, point.Y);

        public bool Contains(Rect rect) =>
           (X <= rect.X) && (rect.X + rect.Width <= X + Width) &&
            (Y <= rect.Y) && (rect.Y + rect.Height <= Y + Height);

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
            return X == other.X
                && Y == other.Y
                && Width == other.Width
                && Height == other.Height;
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
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Width;
                hashCode = (hashCode * 397) ^ Height;
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }
}
