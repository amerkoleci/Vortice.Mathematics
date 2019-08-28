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
    /// Defines a four floating-point numbers that represent the upper-left corner and lower-right corner of a rectangle.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct RectF : IEquatable<RectF>
    {
        /// <summary>
        /// Returns a <see cref="RectF"/> with all of its values set to zero.
        /// </summary>
        public static readonly RectF Empty;

        /// <summary>
        /// The x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public float Left;

        /// <summary>
        /// The y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public float Top;

        /// <summary>
        /// The x-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public float Right;

        /// <summary>
        /// The y-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public float Bottom;

        /// <summary>
        /// Gets the width of the <see cref="RectF"/>.
        /// </summary>
        /// <remarks />
        public float Width => Right - Left;

        /// <summary>
        /// Gets the height of the <see cref="RectF"/>.
        /// </summary>
        /// <remarks />
        public float Height => Bottom - Top;

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
                this = Create(value, Size);
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
                Right = Left + value.Width;
                Bottom = Top + value.Height;
            }
        }

        /// <summary>
        /// Gets the x-coordinate of the center of this rectangle.
        /// </summary>
        public float CenterX => Left + (Width / 2.0f);

        /// <summary>
        /// Gets the y-coordinate of the center of this rectangle.
        /// </summary>
        public float CenterY => Top + (Height / 2.0f);

        /// <summary>
		/// Initializes a new instance of the <see cref="RectF"/> struct.
		/// </summary>
		/// <param name="left">The x-coordinate of the rectangle.</param>
		/// <param name="top">The y-coordinate of the rectangle.</param>
		/// <param name="right">The x-coordinate of the lower-right corner of the rectangle.</param>
		/// <param name="bottom">The y-coordinate of the lower-right corner of the rectangle.</param>
		public RectF(float left, float top, float right, float bottom)
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
        public static RectF Create(float x, float y, float width, float height)
        {
            return new RectF(x, y, x + width, y + height);
        }

        /// <summary>
        /// Creates a new rectangle with the specified size.
        /// </summary>
        /// <param name="width">The rectangle width.</param>
        /// <param name="height">The rectangle height.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static RectF Create(float width, float height)
        {
            return new RectF(0.0f, 0.0f, width, height);
        }

        /// <summary>
        /// Creates a new rectangle with the specified location and size.
        /// </summary>
        /// <param name="location">The rectangle location.</param>
        /// <param name="size">The rectangle size.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static RectF Create(PointF location, SizeF size)
        {
            return new RectF(location.X, location.Y, location.X + size.Width, location.Y + size.Height);
        }

        /// <summary>
        /// Creates a new rectangle with the specified size.
        /// </summary>
        /// <param name="size">The rectangle size.</param>
        /// <returns>Returns the new rectangle.</returns>
        public static RectF Create(SizeF size)
        {
            return new RectF(0.0f, 0.0f, size.Width, size.Height);
        }

        /// <summary>
        /// Inflates this <see cref="RectF"/> by the specified amount.
        /// </summary>
        /// <param name="x">X inflate amount.</param>
        /// <param name="y">Y inflate amount.</param>
        public void Inflate(float x, float y)
        {
            Left -= x;
            Top -= y;
            Right += x;
            Bottom += y;
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
            RectF result = new RectF(rect.Left, rect.Top, rect.Right, rect.Bottom);
            result.Inflate(x, y);
            return result;
        }

        /// <summary>
        /// Creates a Rectangle that represents the intersection between this <see cref="RectF"/> and rect.
        /// </summary>
        /// <param name="rect">The rect to intersects with.</param>
        public void Intersect(RectF rect)
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
        public static RectF Intersect(RectF value1, RectF value2)
        {
            if (!value1.IntersectsWithInclusive(value2))
            {
                return Empty;
            }

            return new RectF(
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
        public bool IntersectsWith(RectF rect)
        {
            return (Left < rect.Right) && (Right > rect.Left) && (Top < rect.Bottom) && (Bottom > rect.Top);
        }

        /// <summary>
        /// Determines if this rectangle intersects with another rectangle.
        /// </summary>
        /// <param name="rect">The rectangle to test.</param>
        /// <returns>This method returns true if there is any intersection.</returns>
        public bool IntersectsWithInclusive(RectF rect)
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
        public static RectF Union(RectF value1, RectF value2)
        {
            return new RectF(
                Math.Min(value1.Left, value2.Left),
                Math.Min(value1.Top, value2.Top),
                Math.Max(value1.Right, value2.Right),
                Math.Max(value1.Bottom, value2.Bottom));
        }

        /// <summary>
        /// Translates this rectangle by a specified offset.
        /// </summary>
        /// <param name="offset">The offset value.</param>
        public void Offset(PointF offset)
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

        /// <summary>
        /// Determines whether the specified coordinates are inside this rectangle.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>Returns true if the coordinates are inside this rectangle, otherwise false.</returns>
        public bool Contains(float x, float y)
        {
            return (x >= Left) && (x < Right) && (y >= Top) && (y < Bottom);
        }

        /// <summary>
        /// Determines whether the specified coordinates are inside this rectangle.
        /// </summary>
        /// <param name="point">The point to test.</param>
        /// <returns>Returns true if the coordinates are inside this rectangle, otherwise false.</returns>
        public bool Contains(PointF point) => Contains(point.X, point.Y);

        /// <summary>
        /// Determines whether the specified rectangle is inside this rectangle.
        /// </summary>
        /// <param name="rect">The rectangle to test.</param>
        /// <returns>Returns true if the rectangle is inside this rectangle, otherwise false.</returns>
        public bool Contains(RectF rect)
        {
            return (Left <= rect.Left) && (Right >= rect.Right)
                && (Top <= rect.Top) && (Bottom >= rect.Bottom);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rect"/> to <see cref="RectF"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectF(Rect value)
        {
            return new RectF(value.Left, value.Top, value.Right, value.Bottom);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="RectangleF"/> to <see cref="RectF"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectF(RectangleF value)
        {
            return new RectF(value.Left, value.Top, value.Right, value.Bottom);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="RectF"/> to <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectangleF(RectF value)
        {
            return RectangleF.FromLTRB(value.Left, value.Top, value.Right, value.Bottom);
        }

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
            return MathHelper.NearEqual(Left, other.Left)
                && MathHelper.NearEqual(Top, other.Top)
                && MathHelper.NearEqual(Right, other.Right)
                && MathHelper.NearEqual(Bottom, other.Bottom);
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
