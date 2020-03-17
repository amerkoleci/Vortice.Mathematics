// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

// Implementation is based on, see below
// ImageSharp: https://github.com/SixLabors/ImageSharp/blob/master/LICENSE
// SkiaSharp: https://github.com/mono/SkiaSharp/blob/master/LICENSE.md

using System;
using System.ComponentModel;
using System.Numerics;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Stores an ordered pair of floating-point numbers describing the width and height of a rectangle.
    /// </summary>
    public struct SizeF : IEquatable<SizeF>
    {
        /// <summary>
        /// Represents a <see cref="SizeF"/> that has Width and Height values set to zero.
        /// </summary>
        public static readonly SizeF Empty = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="SizeF" /> struct.
        /// </summary>
        /// <param name="width">The width of the size.</param>
        /// <param name="height">The height of the size.</param>
        public SizeF(float width, float height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SizeF"/> struct from the given <see cref="PointF"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        public SizeF(PointF point)
        {
            Width = point.X;
            Height = point.Y;
        }

        /// <summary>
        /// Gets or sets the width of this <see cref="SizeF"/>.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Gets or sets the height of this <see cref="SizeF"/>.
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SizeF"/> is empty.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly bool IsEmpty => this == Empty;

        /// <summary>
        /// Deconstructs this size into two floats.
        /// </summary>
        /// <param name="width">The out value for the width.</param>
        /// <param name="height">The out value for the height.</param>
        public void Deconstruct(out float width, out float height)
        {
            width = Width;
            height = Height;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cre ="Size"/> to <see cref="Vector2" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Vector2(SizeF value) => new Vector2(value.Width, value.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cre ="Size"/> to <see cref="System.Drawing.SizeF" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.SizeF(SizeF value) => new System.Drawing.SizeF(value.Width, value.Height);

        /// <summary>
        /// Performs an explicit conversion from <see cref="SizeF"/> to <see cref="PointF"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator PointF(SizeF value) => new PointF(value.Width, value.Height);

        /// <summary>
        /// Compares two <see cref="SizeF"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="SizeF"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="SizeF"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator ==(SizeF left, SizeF right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="SizeF"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="SizeF"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="SizeF"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator !=(SizeF left, SizeF right) => !left.Equals(right);

        /// <inheritdoc/>
		public override int GetHashCode() => HashCode.Combine(Width, Height);

        /// <inheritdoc/>
        public override string ToString() => $"{{Width={Width}, Height={Height}}}";

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is SizeF other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(SizeF other) => Width.Equals(other.Width) && Height.Equals(other.Height);
    }
}
