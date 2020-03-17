// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Numerics;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents an ordered pair of floating-point x and y-coordinates that defines a point in a two-dimensional plane.
    /// </summary>
    public struct Point : IEquatable<Point>
    {
        /// <summary>
        /// Represents a <see cref="Point"/> that has X and Y values set to zero.
        /// </summary>
        public static readonly Point Empty = default;

        public static readonly Point UnitX = new Point(1.0f, 0.0f);
        public static readonly Point UnitY = new Point(0.0f, 1.0f);
        public static readonly Point One = new Point(1.0f, 1.0f);
        public static readonly Point Right = new Point(1.0f, 0.0f);
        public static readonly Point Left = new Point(-1.0f, 0.0f);
        public static readonly Point Up = new Point(0.0f, -1.0f);
        public static readonly Point Down = new Point(0.0f, 1.0f);

        /// <summary>
        /// Initializes a new instance of the <see cref="Point" /> struct.
        /// </summary>
        /// <param name="x">Initial value for the X component of the point.</param>
        /// <param name="y">Initial value for the Y component of the point.</param>
        public Point(float x, float y)
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
        public float X { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="Point"/>.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point"/> is empty.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly bool IsEmpty => this == Empty;

        public readonly float Length => MathF.Sqrt(X * X + Y * Y);

        public readonly float LengthSquared => X * X + Y * Y;

        /// <summary>
        /// Deconstructs this point into two floats.
        /// </summary>
        /// <param name="x">The out value for X.</param>
        /// <param name="y">The out value for Y.</param>
        public void Deconstruct(out float x, out float y)
        {
            x = X;
            y = Y;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Vector2"/> to <see cref="Point" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Point(Vector2 value) => new Point(value.X, value.Y);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Point"/> to <see cref="Vector2" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Vector2(Point value) => new Vector2(value.X, value.Y);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Point"/> to <see cref="System.Drawing.PointF" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.PointF(Point value) => new System.Drawing.PointF(value.X, value.Y);

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

        /// <inheritdoc/>
		public override int GetHashCode() => HashCode.Combine(X, Y);

        /// <inheritdoc/>
        public override string ToString() => $"{{X={X}, Y={Y}}}";

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Point other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(Point other) => X.Equals(other.X) && Y.Equals(other.Y);
    }
}
