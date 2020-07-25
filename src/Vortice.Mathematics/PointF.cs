// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

// Implementation is based on, see below
// ImageSharp: https://github.com/SixLabors/ImageSharp/blob/master/LICENSE
// SkiaSharp: https://github.com/mono/SkiaSharp/blob/master/LICENSE.md

using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents an ordered pair of floating-point x and y-coordinates that defines a point in a two-dimensional plane.
    /// </summary>
    public struct PointF : IEquatable<PointF>
    {
        /// <summary>
        /// Represents a <see cref="PointF"/> that has X and Y values set to zero.
        /// </summary>
        public static readonly PointF Empty = default;

        public static readonly PointF UnitX = new PointF(1.0f, 0.0f);
        public static readonly PointF UnitY = new PointF(0.0f, 1.0f);
        public static readonly PointF One = new PointF(1.0f, 1.0f);
        public static readonly PointF Right = new PointF(1.0f, 0.0f);
        public static readonly PointF Left = new PointF(-1.0f, 0.0f);
        public static readonly PointF Up = new PointF(0.0f, -1.0f);
        public static readonly PointF Down = new PointF(0.0f, 1.0f);

        /// <summary>
        /// Initializes a new instance of the <see cref="PointF" /> struct.
        /// </summary>
        /// <param name="x">Initial value for the X component of the point.</param>
        /// <param name="y">Initial value for the Y component of the point.</param>
        public PointF(float x, float y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointF" /> struct.
        /// </summary>
        /// <param name="size">The <see cref="SizeF"/> to initialize from.</param>
        public PointF(SizeF size)
        {
            _x = size.Width;
            _y = size.Height;
        }
#pragma warning disable IDE0032 // DO NOT REMOVE UNLESS https://github.com/Microsoft/dotnet/issues/807 IS FIXED
        private float _x;
        private float _y;
#pragma warning restore IDE0032
        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="PointF"/>.
        /// </summary>
        public float X
        {
            get => _x;
            set => _x = value;
        }
        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="PointF"/>.
        /// </summary>
        public float Y
        {
            get => _y;
            set => _y = value;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="PointF"/> is empty.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly bool IsEmpty => this == Empty;

        public readonly float Length => MathF.Sqrt(_x * _x + _y * _y);

        public readonly float LengthSquared => _x * _x + _y * _y;

        public void Offset(PointF p)
        {
            X += p.X;
            Y += p.Y;
        }

        public void Offset(float dx, float dy)
        {
            X += dx;
            Y += dy;
        }

        public static PointF Add(PointF point, Size size) => point + size;
        public static PointF Add(PointF point, SizeF size) => point + size;
        public static PointF Add(PointF left, Point right) => left + right;
        public static PointF Add(PointF left, PointF right) => left + right;

        public static PointF Subtract(PointF point, Size size) => point - size;
        public static PointF Subtract(PointF point, SizeF size) => point - size;
        public static PointF Subtract(PointF left, Point right) => left - right;
        public static PointF Subtract(PointF left, PointF right) => left - right;

        public static PointF Normalize(PointF point)
        {
            var ls = point.X * point.X + point.Y * point.Y;
            var invNorm = 1.0f / MathF.Sqrt(ls);
            return new PointF(point.X * invNorm, point.Y * invNorm);
        }

        public static float Distance(PointF point, PointF other)
        {
            var dx = point.X - other.X;
            var dy = point.Y - other.Y;
            var ls = dx * dx + dy * dy;
            return MathF.Sqrt(ls);
        }

        public static float DistanceSquared(PointF point, PointF other)
        {
            var dx = point.X - other.Y;
            var dy = point.Y - other.Y;
            return dx * dx + dy * dy;
        }

        public static PointF Reflect(PointF point, PointF normal)
        {
            var dot = point.X * point.X + point.Y * point.Y;
            return new PointF(
                point.X - 2.0f * dot * normal.X,
                point.Y - 2.0f * dot * normal.Y);
        }

        /// <summary>
        /// Transforms a point by a specified 3x2 matrix.
        /// </summary>
        /// <param name="point">The point to transform.</param>
        /// <param name="matrix">The transformation matrix used.</param>
        /// <returns>The transformed <see cref="PointF"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Transform(PointF point, Matrix3x2 matrix) => Vector2.Transform(point, matrix);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Vector2"/> to <see cref="PointF" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator PointF(Vector2 value) => new PointF(value.X, value.Y);

        /// <summary>
        /// Performs an implicit conversion from <see cref="PointF"/> to <see cref="Vector2" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Vector2(PointF value) => new Vector2(value.X, value.Y);

        /// <summary>
        /// Performs an implicit conversion from <see cref="PointF"/> to <see cref="System.Drawing.PointF" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.PointF(PointF value) => new System.Drawing.PointF(value.X, value.Y);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.PointF" /> to <see cref="PointF"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator PointF(System.Drawing.PointF value) => new PointF(value.X, value.Y);

        /// <summary>
        /// Performs an explicit conversion from <see cref="PointF"/> to <see cref="Point" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Point(PointF value) => Point.Truncate(value);

        public static PointF operator +(PointF point, Size size) =>
            new PointF(point.X + size.Width, point.Y + size.Height);
        public static PointF operator +(PointF point, SizeF size) =>
            new PointF(point.X + size.Width, point.Y + size.Height);
        public static PointF operator +(PointF left, Point right) =>
            new PointF(left.X + right.X, left.Y + right.Y);
        public static PointF operator +(PointF left, PointF right) =>
            new PointF(left.X + right.X, left.Y + right.Y);

        public static PointF operator -(PointF point, Size size) =>
            new PointF(point.X - size.Width, point.Y - size.Height);
        public static PointF operator -(PointF point, SizeF size) =>
            new PointF(point.X - size.Width, point.Y - size.Height);
        public static PointF operator -(PointF left, Point right) =>
            new PointF(left.X - right.X, left.Y - right.Y);
        public static PointF operator -(PointF left, PointF right) =>
            new PointF(left.X - right.X, left.Y - right.Y);

        /// <summary>
        /// Compares two <see cref="PointF"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="PointF"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="PointF"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator ==(PointF left, PointF right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="PointF"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="PointF"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="PointF"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator !=(PointF left, PointF right) => !left.Equals(right);

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

        /// <inheritdoc/>
		public override int GetHashCode() => HashCode.Combine(X, Y);

        /// <inheritdoc/>
        public override string ToString() => $"{{X={X}, Y={Y}}}";

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is PointF other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(PointF other) => X.Equals(other.X) && Y.Equals(other.Y);
    }
}
