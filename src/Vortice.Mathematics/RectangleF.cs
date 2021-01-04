// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

// Implementation is based on, see below
// ImageSharp: https://github.com/SixLabors/ImageSharp/blob/master/LICENSE
// SkiaSharp: https://github.com/mono/SkiaSharp/blob/master/LICENSE.md

using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines a floating-point rectangle structure defining X, Y, Width, Height.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RectangleF : IEquatable<RectangleF>
    {
        /// <summary>
        /// Returns a <see cref="RectangleF"/> with all of its values set to zero.
        /// </summary>
        public static readonly RectangleF Empty = default;

        /// <summary>
		/// Initializes a new instance of the <see cref="RectangleF"/> struct.
		/// </summary>
		/// <param name="x">The horizontal position of the rectangle.</param>
        /// <param name="y">The vertical position of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
		public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleF"/> struct.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public RectangleF(float width, float height)
        {
            X = 0.0f;
            Y = 0.0f;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleF"/> struct.
        /// </summary>
        /// <param name="size">The size of the rectangle.</param>
        public RectangleF(in SizeF size)
        {
            X = 0;
            Y = 0;
            Width = size.Width;
            Height = size.Height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleF"/> struct.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> which specifies the rectangles point in a two-dimensional plane.</param>
        /// <param name="size">The <see cref="SizeF"/> which specifies the rectangles height and width.</param>
        public RectangleF(in PointF point, in SizeF size)
        {
            X = point.X;
            Y = point.Y;
            Width = size.Width;
            Height = size.Height;
        }

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="RectangleF"/>.
        /// </summary>
        public float X { readonly get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="RectangleF"/>.
        /// </summary>
        public float Y { readonly get; set; }

        /// <summary>
        /// Gets or sets the width of this <see cref="RectangleF"/>.
        /// </summary>
        public float Width { readonly get; set; }

        /// <summary>
        /// Gets or sets the height of this <see cref="RectangleF"/>.
        /// </summary>
        public float Height { readonly get; set; }

        /// <summary>
        /// Gets or sets the upper-left value of the <see cref="RectangleF"/>.
        /// </summary>
        public PointF Location
        {
            readonly get => new PointF(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
		/// Gets or sets the size of this <see cref="RectangleF"/>.
		/// </summary>
		public SizeF Size
        {
            readonly get => new SizeF(Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the left edge of this <see cref="RectangleF"/>.
        /// </summary>
        public float Left
        {
            readonly get => X;
            set { X = value; }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the left edge of this <see cref="RectangleF"/>.
        /// </summary>
        public float Top
        {
            readonly get => Y;
            set { Y = value; }
        }

        /// <summary>
        /// Gets the x-coordinate of the right edge of this <see cref="RectangleF"/>.
        /// </summary>
        public readonly float Right => X + Width;

        /// <summary>
        /// Gets the y-coordinate of the bottom edge of this <see cref="RectangleF"/>.
        /// </summary>
        public readonly float Bottom => Y + Height;

        /// <summary>
        /// Gets the <see cref="PointF"/> that specifies the center of this <see cref="RectangleF"/>.
        /// </summary>
        public readonly PointF Center => new PointF(X + (Width / 2), Y + (Height / 2));

        /// <summary>
        /// Gets the x-coordinate of the center of this <see cref="RectangleF"/>.
        /// </summary>
        public readonly float CenterX => X + (Width / 2);

        /// <summary>
        /// Gets the y-coordinate of the center of this <see cref="RectangleF"/>.
        /// </summary>
        public readonly float CenterY => Y + (Height / 2);

        /// <summary>
        /// Gets the aspect ratio of this <see cref="RectangleF"/>.
        /// </summary>
        public readonly float AspectRatio => Width / Height;

        /// <summary>
        /// Gets the position of the top-left corner of this <see cref="RectangleF"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly PointF TopLeft => new PointF(X, Y);

        /// <summary>
        /// Gets the position of the top-right corner of this <see cref="RectangleF"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly PointF TopRight => new PointF(Right, Y);

        /// <summary>
        /// Gets the position of the bottom-left corner of <see cref="RectangleF"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly PointF BottomLeft => new PointF(X, Bottom);

        /// <summary>
        /// Gets the position of the bottom-right corner of <see cref="RectangleF"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly PointF BottomRight => new PointF(Right, Bottom);

        /// <summary>
        /// Gets a value indicating whether this <see cref="RectangleF"/> is empty.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly bool IsEmpty => (Width == 0) && (Height == 0) && (X == 0) && (Y == 0);

        /// <summary>
        /// Deconstructs this rectangle into four floats.
        /// </summary>
        /// <param name="x">The out value for X.</param>
        /// <param name="y">The out value for Y.</param>
        /// <param name="width">The out value for the width.</param>
        /// <param name="height">The out value for the height.</param>
        public void Deconstruct(out float x, out float y, out float width, out float height)
        {
            x = X;
            y = Y;
            width = Width;
            height = Height;
        }

        /// <summary>
        /// Inflates this <see cref="RectangleF"/> by the specified amount.
        /// </summary>
        /// <param name="horizontalAmount">X inflate amount.</param>
        /// <param name="verticalAmount">Y inflate amount.</param>
        public void Inflate(float horizontalAmount, float verticalAmount)
        {
            X -= horizontalAmount;
            Y -= verticalAmount;
            Width += 2 * horizontalAmount;
            Height += 2 * verticalAmount;
        }

        /// <summary>
        /// Inflates this <see cref="RectangleF"/> by the specified amount.
        /// </summary>
        /// <param name="size">The size amount.</param>
        public void Inflate(in SizeF size) => Inflate(size.Width, size.Height);

        /// <summary>
        /// Creates a <see cref="RectangleF"/> that is inflated by the specified amount.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static RectangleF Inflate(in RectangleF rect, float x, float y)
        {
            RectangleF result = new RectangleF(rect.Left, rect.Top, rect.Right, rect.Bottom);
            result.Inflate(x, y);
            return result;
        }

        /// <summary>
        /// Creates a rectangle that represents the intersection between <paramref name="a"/> and
        /// <paramref name="b"/>. If there is no intersection, an empty rectangle is returned.
        /// </summary>
        /// <param name="a">The first rectangle.</param>
        /// <param name="b">The second rectangle.</param>
        /// <returns>The <see cref="RectangleF"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleF Intersect(in RectangleF a, in RectangleF b)
        {
            float x1 = MathF.Max(a.X, b.X);
            float x2 = MathF.Min(a.Right, b.Right);
            float y1 = MathF.Max(a.Y, b.Y);
            float y2 = MathF.Min(a.Bottom, b.Bottom);

            if (x2 >= x1 && y2 >= y1)
            {
                return new RectangleF(x1, y1, x2 - x1, y2 - y1);
            }

            return Empty;
        }

        /// <summary>
        /// Determines if the specfied <see cref="RectangleF"/> intersects the rectangular region defined by
        /// this <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="rectangle">The other Rectange. </param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IntersectsWith(in RectangleF rectangle) =>
            (rectangle.X < Right) && (X < rectangle.Right) &&
            (rectangle.Y < Bottom) && (Y < rectangle.Bottom);

        /// <summary>
        /// Creates a rectangle that represents the union between two rectangles.
        /// </summary>
        /// <param name="value1">The first rectangle.</param>
        /// <param name="value2">The second rectangle.</param>
        /// <returns>The union rectangle.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleF Union(in RectangleF value1, in RectangleF value2)
        {
            float x1 = MathF.Min(value1.X, value2.X);
            float x2 = MathF.Max(value1.Right, value2.Right);
            float y1 = MathF.Min(value1.Y, value2.Y);
            float y2 = MathF.Max(value1.Bottom, value2.Bottom);

            return new RectangleF(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// Translates this rectangle by a specified offset.
        /// </summary>
        /// <param name="offset">The offset value.</param>
        public void Offset(in PointF offset)
        {
            X += offset.X;
            Y += offset.Y;
        }

        /// <summary>
        /// Translates this rectangle by a specified offset.
        /// </summary>
        /// <param name="offsetX">The offset in the x-direction.</param>
        /// <param name="offsetY">The offset in the y-direction.</param>
        public void Offset(float offsetX, float offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        /// <summary>
        /// Determines whether the specified coordinates are inside this rectangle.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>Returns true if the coordinates are inside this rectangle, otherwise false.</returns>
        public bool Contains(float x, float y) => X <= x && x < Right && Y <= y && y < Bottom;

        /// <summary>
        /// Determines whether the specified coordinates are inside this rectangle.
        /// </summary>
        /// <param name="point">The point to test.</param>
        /// <returns>Returns true if the coordinates are inside this rectangle, otherwise false.</returns>
        public bool Contains(in PointF point) => Contains(point.X, point.Y);

        /// <summary>
        /// Determines whether the specified rectangle is inside this rectangle.
        /// </summary>
        /// <param name="rect">The rectangle to test.</param>
        /// <returns>Returns true if the rectangle is inside this rectangle, otherwise false.</returns>
        public bool Contains(RectangleF rect)
        {
            return (X <= rect.X) && (Right >= rect.Right)
                && (Y <= rect.Y) && (Bottom >= rect.Bottom);
        }

        /// <summary>
        /// Transforms a rectangle by the given matrix.
        /// </summary>
        /// <param name="rectangle">The source rectangle.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>A transformed <see cref="RectangleF"/>.</returns>
        public static RectangleF Transform(RectangleF rectangle, Matrix3x2 matrix)
        {
            PointF bottomRight = PointF.Transform(new PointF(rectangle.Right, rectangle.Bottom), matrix);
            PointF topLeft = PointF.Transform(rectangle.Location, matrix);
            return new RectangleF(topLeft, new SizeF(bottomRight - topLeft));
        }

        /// <summary>
        /// Performs an explicit  conversion from <see cref="RectangleF"/> to <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rectangle(RectangleF value) => Rectangle.Truncate(value);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rectangle"/> to <see cref="System.Drawing.RectangleF" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.RectangleF(RectangleF value) => new System.Drawing.RectangleF(value.X, value.Y, value.Width, value.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.RectangleF"/> to <see cref="RectangleF" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectangleF(System.Drawing.RectangleF value) => new RectangleF(value.X, value.Y, value.Width, value.Height);

        /// <summary>
        /// Creates a new <see cref='Rectangle'/> with the specified left, top, right and bottom coordinate.
        /// </summary>
        public static RectangleF FromLTRB(float left, float top, float right, float bottom) =>
            new RectangleF(left, top, right - left, bottom - top);

        /// <summary>
        /// Compares two <see cref="RectangleF"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="RectangleF"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="RectangleF"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(RectangleF left, RectangleF right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="RectangleF"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="RectangleF"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="RectangleF"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(RectangleF left, RectangleF right) => !left.Equals(right);

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is RectangleF other && Equals(other);

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(RectangleF other) =>
            X.Equals(other.X) &&
            Y.Equals(other.Y) &&
            Width.Equals(other.Width) &&
            Height.Equals(other.Height);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();
            {
                hashCode.Add(X);
                hashCode.Add(Y);
                hashCode.Add(Width);
                hashCode.Add(Height);
            }

            return hashCode.ToHashCode();
        }

        /// <inheritdoc/>
        public override readonly string ToString() => $"{{X={X},Y={Y},Width={Width},Height={Height}}}";
    }
}
