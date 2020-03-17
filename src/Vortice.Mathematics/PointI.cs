// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Numerics;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents an ordered pair of integer x and y-coordinates that defines a point in a two-dimensional plane.
    /// </summary>
    public struct PointI : IEquatable<PointI>
    {
        /// <summary>
        /// Represents a <see cref="PointI"/> that has X and Y values set to zero.
        /// </summary>
        public static readonly PointI Empty = default;

        public static readonly PointI UnitX = new PointI(1, 0);
        public static readonly PointI UnitY = new PointI(0, 1);
        public static readonly PointI One = new PointI(1, 1);
        public static readonly PointI Right = new PointI(1, 0);
        public static readonly PointI Left = new PointI(-1, 0);
        public static readonly PointI Up = new PointI(0, -1);
        public static readonly PointI Down = new PointI(0, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="PointI"/> struct.
        /// </summary>
        /// <param name="value">The horizontal and vertical position of the point.</param>
        public PointI(int value)
        {
            X = LowInt16(value);
            Y = HighInt16(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointI" /> struct.
        /// </summary>
        /// <param name="x">Initial value for the X component of the point.</param>
        /// <param name="y">Initial value for the Y component of the point.</param>
        public PointI(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointI" /> struct.
        /// </summary>
        /// <param name="size">The <see cref="SizeI"/> to initialize from.</param>
        public PointI(SizeI size)
        {
            X = size.Width;
            Y = size.Height;
        }

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="PointI"/>.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="PointI"/>.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="PointI"/> is empty.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly bool IsEmpty => this == Empty;

        /// <summary>
        /// Performs an implicit conversion from <see cref="PointI"/> to <see cref="Point" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Point(PointI value) => new Point(value.X, value.Y);

        /// <summary>
        /// Performs an implicit conversion from <see cref="PointI"/> to <see cref="Vector2" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Vector2(PointI value) => new Vector2(value.X, value.Y);

        /// <summary>
        /// Performs an explicit conversion from <see cref="PointI"/> to <see cref="SizeI" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator SizeI(PointI value) => new SizeI(value.X, value.Y);

        /// <summary>Translates a <see cref="PointI"/> /> by a given <see cref="SizeI"/>.</summary>
        /// <param name="point">The <see cref="PointI"/> to translate.</param>
        /// <param name="size">A <see cref="SizeI"/> that specifies the pair of numbers to add to the coordinates of <paramref name="point" />.</param>
        /// <returns>Returns the translated <see cref="PointI"/>.</returns>
        public static PointI operator +(PointI point, SizeI size) => new PointI(point.X + size.Width, point.Y + size.Height);

        /// <param name="left">The <see cref="PointI"/> to translate.</param>
        /// <param name="right">A point that specifies the pair of numbers to add to the coordinates of <paramref name="left" />.</param>
        /// <summary>Translates a <see cref="PointI"/> by a given offset.</summary>
        /// <returns>Returns the translated <see cref="PointI"/>.</returns>
        public static PointI operator +(PointI left, PointI right) => new PointI(left.X + right.X, left.Y + right.Y);

        /// <summary>
        /// Translates a <see cref="PointI"/> by the negative of a given <see cref="SizeI" />.
        /// </summary>
        /// <param name="point">The <see cref="PointI"/> to translate.</param>
        /// <param name="size">The <see cref="SizeI" /> that specifies the numbers to subtract from the coordinates of <paramref name="point" />.</param>
        /// <returns>The translated <see cref="PointI"/>.</returns>
        /// <remarks />
        public static PointI operator -(PointI point, SizeI size) => new PointI(point.X - size.Width, point.Y - size.Height);

        /// <summary>
        /// Translates a <see cref="PointI"/> by the negative of a given point.
        /// </summary>
        /// <param name="left">The <see cref="PointI"/> to translate.</param>
        /// <param name="right">The point that specifies the numbers to subtract from the coordinates of <paramref name="left" />.</param>
        /// <returns>The translated <see cref="PointI"/>.</returns>
        /// <remarks />
        public static PointI operator -(PointI left, PointI right) => new PointI(left.X - right.X, left.Y - right.Y);

        /// <summary>
        /// Multiplies <see cref="PointI"/> by a <see cref="int"/> producing <see cref="PointI"/>.
        /// </summary>
        /// <param name="left">Multiplier of type <see cref="int"/>.</param>
        /// <param name="right">Multiplicand of type <see cref="PointI"/>.</param>
        /// <returns>Product of type <see cref="PointI"/>.</returns>
        public static PointI operator *(int left, PointI right) => Multiply(right, left);

        /// <summary>
        /// Multiplies <see cref="PointI"/> by a <see cref="int"/> producing <see cref="PointI"/>.
        /// </summary>
        /// <param name="left">Multiplicand of type <see cref="PointI"/>.</param>
        /// <param name="right">Multiplier of type <see cref="int"/>.</param>
        /// <returns>Product of type <see cref="PointI"/>.</returns>
        public static PointI operator *(PointI left, int right) => Multiply(left, right);

        /// <summary>
        /// Divides <see cref="PointI"/> by a <see cref="int"/> producing <see cref="PointI"/>.
        /// </summary>
        /// <param name="left">Dividend of type <see cref="PointI"/>.</param>
        /// <param name="right">Divisor of type <see cref="int"/>.</param>
        /// <returns>Result of type <see cref="PointI"/>.</returns>
        public static PointI operator /(PointI left, int right) => new PointI(left.X / right, left.Y / right);

        /// <summary>
        /// Compares two <see cref="PointI"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="PointI"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="PointI"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator ==(PointI left, PointI right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="PointI"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="PointI"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="PointI"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator !=(PointI left, PointI right) => !left.Equals(right);

        /// <summary>
        /// Translates a given <see cref="PointI"/> by the specified <see cref="SizeI"/>.
        /// </summary>
        /// <param name="point">The point to translate.</param>
        /// <param name="size">The size that specifies the number to add to the coordinates of <paramref name="point" />.</param>
        /// <returns>The translated point.</returns>
        public static PointI Add(PointI point, SizeI size) => point + size;

        /// <summary>
        /// Translates a given <see cref="PointI"/> by the specified point.
        /// </summary>
        /// <param name="left">The point to translate.</param>
        /// <param name="right">The point that specifies the number to add to the coordinates of <paramref name="left" />.</param>
        /// <returns>The translated point.</returns>
        public static PointI Add(PointI left, PointI right) => left + right;


        /// <summary>
        /// Translates a <see cref="PointI"/> by the negative of a given value.
        /// </summary>
        /// <param name="point">The point on the left hand of the operand.</param>
        /// <param name="value">The value on the right hand of the operand.</param>
        /// <returns>The <see cref="PointI"/>.</returns>
        public static PointI Multiply(PointI point, int value) => new PointI(unchecked(point.X * value), unchecked(point.Y * value));

        /// <summary>
        /// Returns the result of subtracting specified <see cref="SizeI" /> from the specified <see cref="PointI" />.
        /// </summary>
        /// <param name="point">The <see cref="PointI" /> to be subtracted from.</param>
        /// <param name="size">The <see cref="SizeI" /> to subtract from the <see cref="PointI" />.</param>
        /// <returns>The <see cref="PointI" /> that is the result of the subtraction operation.</returns>
        /// <remarks />
        public static PointI Subtract(PointI point, SizeI size) => point - size;

        /// <summary>
        /// Returns the result of subtracting specified point from the specified <see cref="PointI" />.
        /// </summary>
        /// <param name="left">The <see cref="PointI" /> to be subtracted from.</param>
        /// <param name="right">The point to subtract from the <see cref="PointI" />.</param>
        /// <returns>The <see cref="PointI" /> that is the result of the subtraction operation.</returns>
        /// <remarks />
        public static PointI Subtract(PointI left, PointI right) => left - right;

        /// <summary>
        /// Converts a <see cref="Point"/> to a <see cref="PointI"/> by performing a ceiling operation on all the coordinates.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point"/>.</returns>
        public static PointI Ceiling(Point point) => new PointI(unchecked((int)MathF.Ceiling(point.X)), unchecked((int)MathF.Ceiling(point.Y)));

        /// <summary>
        /// Converts a <see cref="Point"/> to a <see cref="PointI"/> by performing a round operation on all the coordinates.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="PointI"/>.</returns>
        public static PointI Round(Point point) => new PointI(unchecked((int)MathF.Round(point.X)), unchecked((int)MathF.Round(point.Y)));

        /// <summary>
        /// Converts a <see cref="Vector2"/> to a <see cref="PointI"/> by performing a round operation on all the coordinates.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The <see cref="PointI"/>.</returns>
        public static PointI Round(Vector2 vector) => new PointI(unchecked((int)MathF.Round(vector.X)), unchecked((int)MathF.Round(vector.Y)));

        /// <summary>
        /// Converts a <see cref="Point"/> to a <see cref="PointI"/> by performing a truncate operation on all the coordinates.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point"/>.</returns>
        public static PointI Truncate(Point point) => new PointI(unchecked((int)point.X), unchecked((int)point.Y));

        /// <summary>
        /// Returns a point with the same direction as the specified point, but with a length of one.
        /// </summary>
        /// <param name="point">The point to normalize.</param>
        /// <returns>Returns a point with a length of one.</returns>
        public static PointI Normalize(PointI point)
        {
            var ls = point.X * point.X + point.Y * point.Y;
            var invNorm = 1.0f / MathF.Sqrt(ls);
            return new PointI((int)(point.X * invNorm), (int)(point.Y * invNorm));
        }

        /// <summary>
        /// Calculate the Euclidean distance between two points.
        /// </summary>
        /// <param name="first">The first point.</param>
        /// <param name="second">The second point.</param>
        /// <returns>Returns the Euclidean distance between two points.</returns>
        public static float Distance(PointI first, PointI second)
        {
            var dx = first.X - second.X;
            var dy = first.Y - second.Y;
            var ls = dx * dx + dy * dy;
            return MathF.Sqrt(ls);
        }

        /// <summary>
        /// Calculate the Euclidean distance squared between two points.
        /// </summary>
        /// <param name="first">The first point.</param>
        /// <param name="second">The second point.</param>
        /// <returns>Returns the Euclidean distance squared between two points</returns>
        public static float DistanceSquared(PointI first, PointI second)
        {
            var dx = first.X - second.X;
            var dy = first.Y - second.Y;
            return dx * dx + dy * dy;
        }

        /// <summary>
        /// Returns the reflection of a point off a surface that has the specified normal.
        /// </summary>
        /// <param name="point">The point to reflect.</param>
        /// <param name="normal">The normal</param>
        /// <returns>Returns the reflection of a point.</returns>
        public static PointI Reflect(PointI point, PointI normal)
        {
            int dot = point.X * point.X + point.Y * point.Y;
            return new PointI(
                (int)(point.X - 2.0f * dot * normal.X),
                (int)(point.Y - 2.0f * dot * normal.Y));
        }

        /// <summary>
        /// Transforms a point by a specified 3x2 matrix.
        /// </summary>
        /// <param name="point">The point to transform.</param>
        /// <param name="matrix">The transformation matrix used.</param>
        /// <returns>The transformed <see cref="PointI"/>.</returns>
        public static PointI Transform(PointI point, Matrix3x2 matrix) => Round(Vector2.Transform(new Vector2(point.X, point.Y), matrix));

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
        public void Offset(int dx, int dy)
        {
            unchecked
            {
                X += dx;
                Y += dy;
            }
        }

        /// <summary>
        /// Translates this <see cref="Point"/> by the specified amount.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> used offset this <see cref="Point"/>.</param>
        public void Offset(PointI point) => Offset(point.X, point.Y);

        /// <inheritdoc/>
		public override int GetHashCode() => HashCode.Combine(X, Y);

        /// <inheritdoc/>
        public override string ToString() => $"{{X={X}, Y={Y}}}";

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is PointI other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(PointI other) => X == other.X && Y == other.Y;

        private static short HighInt16(int n) => unchecked((short)((n >> 16) & 0xffff));

        private static short LowInt16(int n) => unchecked((short)(n & 0xffff));
    }
}
