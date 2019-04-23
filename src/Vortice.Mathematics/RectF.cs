// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines a four floating-point numbers that represent the upper-left corner and lower-right corner of a rectangle.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct RectF : IEquatable<RectF>
    {
        /// <summary>
        /// The size of the <see cref="RectF"/> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<RectF>();

        /// <summary>
        /// Returns a <see cref="RectF"/> with all of its values set to zero.
        /// </summary>
        public static readonly RectF Empty;

        /// <summary>
        /// The x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public float X;

        /// <summary>
        /// The y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public float Y;

        /// <summary>
        /// The width of the rectangle, in pixels.
        /// </summary>
        public float Width;

        /// <summary>
        /// The height of the rectangle, in pixels.
        /// </summary>
        public float Height;

        /// <summary>
        /// The x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public float Left => X;

        /// <summary>
        /// The y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public float Top => Y;

        /// <summary>
        /// Gets the x-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public float Right
        {
            get => X + Width;
        }

        /// <summary>
        /// Gets the y-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public float Bottom
        {
            get => Y + Height;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RectF"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Gets the aspect ratio of the rectangle.
		/// </summary>
		public float AspectRatio => Width / Height;

        /// <summary>
        /// Gets or sets the upper-left value of the <see cref="RectF"/>.
        /// </summary>
        public PointF Location
        {
            get => new PointF(Left, Top);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
		/// Gets or sets the size of this <see cref="RectF"/>.
		/// </summary>
		public SizeF Size
        {
            get => new SizeF(Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public float CenterX => X + (Width / 2.0f);

        public float CenterY => Y + (Height / 2.0f);

        public PointF Center => new PointF(CenterX, CenterY);

        /// <summary>
		/// Initializes a new instance of the <see cref="RectF"/> struct.
		/// </summary>
		/// <param name="x">The x-coordinate of the rectangle.</param>
		/// <param name="y">The y-coordinate of the rectangle.</param>
		/// <param name="width">The width of the rectangle.</param>
		/// <param name="height">The height of the rectangle.</param>
		public RectF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="RectF"/> struct.
		/// </summary>
		/// <param name="width">The width of the rectangle.</param>
		/// <param name="height">The height of the rectangle.</param>
		public RectF(float width, float height)
        {
            X = 0.0f;
            Y = 0.0f;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectF"/> struct.
        /// </summary>
        /// <param name="location">The x and y location.</param>
        /// <param name="size">The rectangle size.</param>
        public RectF(PointF location, SizeF size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.Width;
            Height = size.Height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectF"/> struct.
        /// </summary>
        /// <param name="size">The rectangle size.</param>
        public RectF(SizeF size)
        {
            X = 0.0f;
            Y = 0.0f;
            Width = size.Width;
            Height = size.Height;
        }

        public void Deconstruct(out float x, out float y, out float width, out float height)
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
        public static RectF FromLTRB(float left, float top, float right, float bottom) =>
           new RectF(left, top, right - left, bottom - top);

        /// <summary>
        /// Inflates this <see cref="RectF"/> by the specified amount.
        /// </summary>
        /// <param name="x">X inflate amount.</param>
        /// <param name="y">Y inflate amount.</param>
        public void Inflate(float x, float y)
        {
            X -= x;
            Y -= y;
            Width += 2 * x;
            Height += 2 * y;
        }

        /// <summary>
        /// Inflates this <see cref="RectF"/> by the specified amount.
        /// </summary>
        /// <param name="size">The size amount.</param>
        public void Inflate(SizeF size) => Inflate(size.Width, size.Height);

        /// <summary>
        /// Creates a <see cref="RectF"/> that is inflated by the specified amount.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static RectF Inflate(RectF rect, float x, float y)
        {
            RectF r = rect;
            r.Inflate(x, y);
            return r;
        }

        /// <summary>
        /// Creates a Rectangle that represents the intersection between this <see cref="RectF"/> and rect.
        /// </summary>
        /// <param name="rect">The rect to intersects with.</param>
        public void Intersect(RectF rect)
        {
            RectF result = Intersect(rect, this);

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
        public static RectF Intersect(RectF value1, RectF value2)
        {
            float x1 = Math.Max(value1.X, value2.X);
            float x2 = Math.Min(value1.X + value1.Width, value2.X + value2.Width);
            float y1 = Math.Max(value1.Y, value2.Y);
            float y2 = Math.Min(value1.Y + value1.Height, value2.Y + value2.Height);

            if (x2 >= x1 && y2 >= y1)
            {
                return new RectF(x1, y1, x2 - x1, y2 - y1);
            }

            return Empty;
        }

        /// <summary>
        /// Determines if this rectangle intersects with rect.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool IntersectsWith(RectF rect) =>
            (rect.X < X + Width) && (X < rect.X + rect.Width) && (rect.Y < Y + Height) && (Y < rect.Y + rect.Height);

        /// <summary>
        /// Creates a rectangle that represents the union between two rectangles.
        /// </summary>
        /// <param name="value1">The first rectangle.</param>
        /// <param name="value2">The second rectangle.</param>
        /// <returns>The union rectangle.</returns>
        public static RectF Union(RectF value1, RectF value2)
        {
            float x1 = Math.Min(value1.X, value2.X);
            float x2 = Math.Max(value1.X + value1.Width, value2.X + value2.Width);
            float y1 = Math.Min(value1.Y, value2.Y);
            float y2 = Math.Max(value1.Y + value1.Height, value2.Y + value2.Height);

            return new RectF(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// Translates this rectangle by a specified offset.
        /// </summary>
        /// <param name="offset">The offset value.</param>
        public void Offset(PointF offset)
        {
            X += offset.X;
            Y += offset.Y;
        }

        /// <summary>
        /// Translates this rectangle by a specified offset.
        /// </summary>
        /// <param name="x">The offset in the x-direction.</param>
        /// <param name="y">The offset in the y-direction.</param>
        public void Offset(float x, float y)
        {
            X += x;
            Y += y;
        }

        public bool Contains(float x, float y) => X <= x && x < X + Width && Y <= y && y < Y + Height;

        public bool Contains(PointF point) => Contains(point.X, point.Y);

        public bool Contains(RectF rect) =>
            (X <= rect.X) && (rect.X + rect.Width <= X + Width) && (Y <= rect.Y) && (rect.Y + rect.Height <= Y + Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rect"/> to <see cref="RectF"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectF(Rect value) =>
            new RectF(value.X, value.Y, value.Width, value.Height);

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is RectF value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="RectF"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="RectF"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(RectF other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="RectF"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="RectF"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref RectF other)
        {
            return MathHelper.NearEqual(X, other.X)
                && MathHelper.NearEqual(Y, other.Y)
                && MathHelper.NearEqual(Width, other.Width)
                && MathHelper.NearEqual(Height, other.Height);
        }

        /// <summary>
        /// Compares two <see cref="RectF"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="RectF"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="RectF"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(RectF left, RectF right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="RectF"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="RectF"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="RectF"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(RectF left, RectF right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Width.GetHashCode();
                hashCode = (hashCode * 397) ^ Height.GetHashCode();
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
