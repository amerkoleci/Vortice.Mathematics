// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

// Implementation is based on, see below
// ImageSharp: https://github.com/SixLabors/ImageSharp/blob/master/LICENSE
// SkiaSharp: https://github.com/mono/SkiaSharp/blob/master/LICENSE.md

using System;
using System.ComponentModel;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents an ordered pair of integer x and y-coordinates that defines a point in a two-dimensional plane.
    /// </summary>
    public readonly struct Point : IEquatable<Point>, IFormattable
    {
        /// <summary>
        /// Represents a <see cref="Point"/> that has X and Y values set to zero.
        /// </summary>
        public static readonly Point Empty = default;

        public static readonly Point UnitX = new Point(1, 0);
        public static readonly Point UnitY = new Point(0, 1);
        public static readonly Point One = new Point(1, 1);
        public static readonly Point Right = new Point(1, 0);
        public static readonly Point Left = new Point(-1, 0);
        public static readonly Point Up = new Point(0, -1);
        public static readonly Point Down = new Point(0, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> struct.
        /// </summary>
        /// <param name="value">The horizontal and vertical position of the point.</param>
        public Point(int value)
        {
            X = LowInt16(value);
            Y = HighInt16(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point" /> struct.
        /// </summary>
        /// <param name="x">Initial value for the X component of the point.</param>
        /// <param name="y">Initial value for the Y component of the point.</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point" /> struct.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to initialize from.</param>
        public Point(Size size)
        {
            X = size.Width;
            Y = size.Height;
        }

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="Point"/>.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="Point"/>.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point"/> is empty.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly bool IsEmpty => this == Empty;

        /// <summary>
        /// Performs an implicit conversion from <see cref="Point"/> to <see cref="PointF" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator PointF(Point value) => new PointF(value.X, value.Y);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Point"/> to <see cref="Vector2" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Vector2(Point value) => new Vector2(value.X, value.Y);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Point"/> to <see cref="System.Drawing.Point" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.Point(Point value) => new System.Drawing.Point(value.X, value.Y);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Point" /> to <see cref="Point"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Point(System.Drawing.Point value) => new Point(value.X, value.Y);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Point"/> to <see cref="Size" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Size(Point value) => new Size(value.X, value.Y);

        /// <summary>Translates a <see cref="Point"/> /> by a given <see cref="Size"/>.</summary>
        /// <param name="point">The <see cref="Point"/> to translate.</param>
        /// <param name="size">A <see cref="Size"/> that specifies the pair of numbers to add to the coordinates of <paramref name="point" />.</param>
        /// <returns>Returns the translated <see cref="Point"/>.</returns>
        public static Point operator +(Point point, Size size) => new Point(point.X + size.Width, point.Y + size.Height);

        /// <param name="left">The <see cref="Point"/> to translate.</param>
        /// <param name="right">A point that specifies the pair of numbers to add to the coordinates of <paramref name="left" />.</param>
        /// <summary>Translates a <see cref="Point"/> by a given offset.</summary>
        /// <returns>Returns the translated <see cref="Point"/>.</returns>
        public static Point operator +(Point left, Point right) => new Point(left.X + right.X, left.Y + right.Y);

        /// <summary>
        /// Translates a <see cref="Point"/> by the negative of a given <see cref="Size" />.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to translate.</param>
        /// <param name="size">The <see cref="Size" /> that specifies the numbers to subtract from the coordinates of <paramref name="point" />.</param>
        /// <returns>The translated <see cref="Point"/>.</returns>
        /// <remarks />
        public static Point operator -(Point point, Size size) => new Point(point.X - size.Width, point.Y - size.Height);

        /// <summary>
        /// Translates a <see cref="Point"/> by the negative of a given point.
        /// </summary>
        /// <param name="left">The <see cref="Point"/> to translate.</param>
        /// <param name="right">The point that specifies the numbers to subtract from the coordinates of <paramref name="left" />.</param>
        /// <returns>The translated <see cref="Point"/>.</returns>
        /// <remarks />
        public static Point operator -(Point left, Point right) => new Point(left.X - right.X, left.Y - right.Y);

        /// <summary>
        /// Multiplies <see cref="Point"/> by a <see cref="int"/> producing <see cref="Point"/>.
        /// </summary>
        /// <param name="left">Multiplier of type <see cref="int"/>.</param>
        /// <param name="right">Multiplicand of type <see cref="Point"/>.</param>
        /// <returns>Product of type <see cref="Point"/>.</returns>
        public static Point operator *(int left, Point right) => Multiply(right, left);

        /// <summary>
        /// Multiplies <see cref="Point"/> by a <see cref="int"/> producing <see cref="Point"/>.
        /// </summary>
        /// <param name="left">Multiplicand of type <see cref="Point"/>.</param>
        /// <param name="right">Multiplier of type <see cref="int"/>.</param>
        /// <returns>Product of type <see cref="Point"/>.</returns>
        public static Point operator *(Point left, int right) => Multiply(left, right);

        /// <summary>
        /// Divides <see cref="Point"/> by a <see cref="int"/> producing <see cref="Point"/>.
        /// </summary>
        /// <param name="left">Dividend of type <see cref="Point"/>.</param>
        /// <param name="right">Divisor of type <see cref="int"/>.</param>
        /// <returns>Result of type <see cref="Point"/>.</returns>
        public static Point operator /(Point left, int right) => new Point(left.X / right, left.Y / right);

        /// <summary>
        /// Compares two <see cref="Point"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Point"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Point"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator ==(Point left, Point right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="Point"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Point"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Point"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator !=(Point left, Point right) => !left.Equals(right);

        /// <summary>
        /// Translates a given <see cref="Point"/> by the specified <see cref="Size"/>.
        /// </summary>
        /// <param name="point">The point to translate.</param>
        /// <param name="size">The size that specifies the number to add to the coordinates of <paramref name="point" />.</param>
        /// <returns>The translated point.</returns>
        public static Point Add(Point point, Size size) => point + size;

        /// <summary>
        /// Translates a given <see cref="Point"/> by the specified point.
        /// </summary>
        /// <param name="left">The point to translate.</param>
        /// <param name="right">The point that specifies the number to add to the coordinates of <paramref name="left" />.</param>
        /// <returns>The translated point.</returns>
        public static Point Add(Point left, Point right) => left + right;


        /// <summary>
        /// Translates a <see cref="Point"/> by the negative of a given value.
        /// </summary>
        /// <param name="point">The point on the left hand of the operand.</param>
        /// <param name="value">The value on the right hand of the operand.</param>
        /// <returns>The <see cref="Point"/>.</returns>
        public static Point Multiply(Point point, int value) => new Point(unchecked(point.X * value), unchecked(point.Y * value));

        /// <summary>
        /// Returns the result of subtracting specified <see cref="Size" /> from the specified <see cref="Point" />.
        /// </summary>
        /// <param name="point">The <see cref="Point" /> to be subtracted from.</param>
        /// <param name="size">The <see cref="Size" /> to subtract from the <see cref="Point" />.</param>
        /// <returns>The <see cref="Point" /> that is the result of the subtraction operation.</returns>
        /// <remarks />
        public static Point Subtract(Point point, Size size) => point - size;

        /// <summary>
        /// Returns the result of subtracting specified point from the specified <see cref="Point" />.
        /// </summary>
        /// <param name="left">The <see cref="Point" /> to be subtracted from.</param>
        /// <param name="right">The point to subtract from the <see cref="Point" />.</param>
        /// <returns>The <see cref="Point" /> that is the result of the subtraction operation.</returns>
        /// <remarks />
        public static Point Subtract(Point left, Point right) => left - right;

        /// <summary>
        /// Converts a <see cref="PointF"/> to a <see cref="Point"/> by performing a ceiling operation on all the coordinates.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point"/>.</returns>
        public static Point Ceiling(PointF point) => new Point(unchecked((int)MathF.Ceiling(point.X)), unchecked((int)MathF.Ceiling(point.Y)));

        /// <summary>
        /// Converts a <see cref="PointF"/> to a <see cref="Point"/> by performing a round operation on all the coordinates.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point"/>.</returns>
        public static Point Round(PointF point) => new Point(unchecked((int)MathF.Round(point.X)), unchecked((int)MathF.Round(point.Y)));

        /// <summary>
        /// Converts a <see cref="Vector2"/> to a <see cref="Point"/> by performing a round operation on all the coordinates.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The <see cref="Point"/>.</returns>
        public static Point Round(Vector2 vector) => new Point(unchecked((int)MathF.Round(vector.X)), unchecked((int)MathF.Round(vector.Y)));

        /// <summary>
        /// Converts a <see cref="PointF"/> to a <see cref="Point"/> by performing a truncate operation on all the coordinates.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point"/>.</returns>
        public static Point Truncate(PointF point) => new Point(unchecked((int)point.X), unchecked((int)point.Y));

        /// <summary>
        /// Returns a point with the same direction as the specified point, but with a length of one.
        /// </summary>
        /// <param name="point">The point to normalize.</param>
        /// <returns>Returns a point with a length of one.</returns>
        public static Point Normalize(Point point)
        {
            int ls = point.X * point.X + point.Y * point.Y;
            float invNorm = 1.0f / MathF.Sqrt(ls);
            return new Point((int)(point.X * invNorm), (int)(point.Y * invNorm));
        }

        /// <summary>
        /// Calculate the Euclidean distance between two points.
        /// </summary>
        /// <param name="first">The first point.</param>
        /// <param name="second">The second point.</param>
        /// <returns>Returns the Euclidean distance between two points.</returns>
        public static float Distance(Point first, Point second)
        {
            int dx = first.X - second.X;
            int dy = first.Y - second.Y;
            int ls = dx * dx + dy * dy;
            return MathF.Sqrt(ls);
        }

        /// <summary>
        /// Calculate the Euclidean distance squared between two points.
        /// </summary>
        /// <param name="first">The first point.</param>
        /// <param name="second">The second point.</param>
        /// <returns>Returns the Euclidean distance squared between two points</returns>
        public static float DistanceSquared(Point first, Point second)
        {
            int dx = first.X - second.X;
            int dy = first.Y - second.Y;
            return dx * dx + dy * dy;
        }

        /// <summary>
        /// Returns the reflection of a point off a surface that has the specified normal.
        /// </summary>
        /// <param name="point">The point to reflect.</param>
        /// <param name="normal">The normal</param>
        /// <returns>Returns the reflection of a point.</returns>
        public static Point Reflect(Point point, Point normal)
        {
            int dot = point.X * point.X + point.Y * point.Y;
            return new Point(
                (int)(point.X - 2.0f * dot * normal.X),
                (int)(point.Y - 2.0f * dot * normal.Y));
        }

        /// <summary>
        /// Transforms a point by a specified 3x2 matrix.
        /// </summary>
        /// <param name="point">The point to transform.</param>
        /// <param name="matrix">The transformation matrix used.</param>
        /// <returns>The transformed <see cref="Point"/>.</returns>
        public static Point Transform(Point point, Matrix3x2 matrix) => Round(Vector2.Transform(new Vector2(point.X, point.Y), matrix));

        /// <summary>
        /// Deconstructs this point into two integers.
        /// </summary>
        /// <param name="x">The out value for X.</param>
        /// <param name="y">The out value for Y.</param>
        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        /// <summary>
        /// Translates this <see cref="Point"/> by the specified amount.
        /// </summary>
        /// <param name="dx">The amount to offset the x-coordinate.</param>
        /// <param name="dy">The amount to offset the y-coordinate.</param>
        public Point Offset(int dx, int dy)
        {
            unchecked
            {
                return new Point(X + dx, Y + dy);
            }
        }

        /// <summary>
        /// Translates this <see cref="Point"/> by the specified amount.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> used offset this <see cref="Point"/>.</param>
        public Point Offset(Point point) => Offset(point.X, point.Y);

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Point other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(Point other) => X == other.X && Y == other.Y;

        private static short HighInt16(int n) => unchecked((short)((n >> 16) & 0xffff));

        private static short LowInt16(int n) => unchecked((short)(n & 0xffff));

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(X);
                hashCode.Add(Y);
            }
            return hashCode.ToHashCode();
        }

        /// <inheritdoc/>
        public override string ToString() => ToString(format: null, formatProvider: null);

        /// <inheritdoc />
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            string? separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

            return new StringBuilder()
                .Append('<')
                .Append(X.ToString(format, formatProvider))
                .Append(separator)
                .Append(' ')
                .Append(Y.ToString(format, formatProvider))
                .Append('>')
                .ToString();
        }
    }
}
